﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPanel : IView
{
    private UIButton InventoryBtn;
    private UIButton BacktoMenuBtn;
    private UIButton SkillBtn;
    private Transform ParentGo;
    public static Transform ChooseBtnContainer;
    private Transform SpriteControlContainer;
    private static Transform systemPanel;
    public static bool Bg_IsActive = false;
    public static bool isOn = false;
    public static bool CardCollectionsIsActive = false;
    public static bool SystemPanelIsActive = false;
    private UITexture systemBg;

    private List<Transform> goList = new List<Transform>();
    private List<string> goActiveList = new List<string>();
    private List<GameObject> m_CardList = new List<GameObject>();//主要用来存储卡片物体       
    private GameObject rightMark;

    public SystemPanel()
    {
        m_Layer = Layer.System;
    }

    protected override void OnStart()
    {
        systemPanel = GUIManager.FindPanel("SystemPanel").transform;
        ParentGo = GameObject.Find("UI Root").transform;      
        InventoryBtn = this.GetChild("InventoryBtn").GetComponent<UIButton>();
        BacktoMenuBtn = this.GetChild("BacktoMenuBtn").GetComponent<UIButton>();
        SkillBtn = this.GetChild("SkillBtn").GetComponent<UIButton>();
        ChooseBtnContainer = this.GetChild("ChooseBtnContainer").transform;
        SpriteControlContainer = this.GetChild("BtnControlWidget").transform;
        rightMark = this.GetChild("RightShow").gameObject;
        systemBg = this.GetChild("SystemBG").GetComponent<UITexture>();
      
       // AddEventBtn();
        AddDelegate();
       
    }
    protected override void OnShow()
    {
        PlayerControl.isFirstEsc = true;
        StartShowChooseBtn();
        StartShowMark();
        GUIManager.HideView("BattleUIPanel");
        GUIManager.HideView("EventStoryPanel");
        GUIManager.HideView("CGPanel");
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        if (SpriteControlContainer == null)
        {
            SpriteControlContainer = this.GetChild("BtnControlWidget").transform;
            SpriteControlContainer.gameObject.SetActive(false);
        }
        else
        {
            SpriteControlContainer.gameObject.SetActive(false);
        }
        BackToNull();
        HideRightMark();
        isFinishedDown = false;
        PlayerControl.isFirstEsc = false;
        //SystemPanelIsActive = false;
    }    
    public override void Update()
    {

    }   
    void TrasverAllChild() {
        for (int i = 0; i < ChooseBtnContainer.childCount; i++) {
            GameObject go = ChooseBtnContainer.GetChild(i).gameObject;
            for (int j = 0; j < go.transform.childCount; j++) {
                if (IsHaveUIButtonComponent(go.transform.GetChild(j).gameObject) == true) {
                    m_CardList.Add(go.transform.GetChild(j).gameObject);
                }
            }
        }
    }
    bool IsHaveUIButtonComponent(GameObject go) {
        if (go.GetComponent<UIButton>() == null)
        {
            return false;
        }
        else {
            return true;
        }
       
    }
    void AddEventBtn()
    {
        EventDelegate InventoryBtnEvent = new global::EventDelegate(OnInventoryBtnClick);
        EventDelegate BacktoMenuEvent = new global::EventDelegate(OnBacktoMenuClick);


        InventoryBtn.onClick.Add(InventoryBtnEvent);
        BacktoMenuBtn.onClick.Add(BacktoMenuEvent);
        SkillBtn.onClick.Add(new EventDelegate(OnClickSkillBtn));
        
    }

    void OnInventoryBtnClick()
    {       
        GUIManager.ShowView("BagPanel");
        Bg_IsActive = true;
    }

    void OnBacktoMenuClick() {
        BackMenuHide();                
        //GUIManager.ShowView("BGStoryPanel");
    }
    void BackMenuHide() {
        foreach (Transform child in ParentGo.transform)
        {
            goList.Add(child);
        }
        for (int i = 0; i < goList.Count; i++)
        {
            if (goList[i].gameObject.activeInHierarchy == true)
            {
                goActiveList.Add(goList[i].name);
            }
        }
        for (int j = 0; j < goActiveList.Count; j++)
        {
            if (goActiveList[j] != "Camera")
            {
                GUIManager.HideView(goActiveList[j]);
            }
        }                   
    }
    void OnClickSkillBtn() {
        GUIManager.ShowView("SkillPanel");
        GUIManager.HideView("SystemPanel");
    }
    void AddDelegate() {
        for (int i = 0; i < SpriteControlContainer.childCount; i++) {
            GameObject go = SpriteControlContainer.GetChild(i).gameObject;
            go.GetComponent<UIButton>().onClick.Add(new EventDelegate(OnClickBtn));
        }
    }
    void OnClickBtn() {
        if (UIButton.current.name.Contains("0"))
        {
            BackMenuHide();
            GameMain.isFirstStartGame = false;
            GameStateManager.LoadScene(1);
            GUIManager.ShowView("CoverPanel");
            GUIManager.ShowView("LoginPanel");
        }
        else if (UIButton.current.name.Contains("1"))
        {
            GUIManager.ShowView("BagPanel");
            Bg_IsActive = true;
        }
        else if (UIButton.current.name.Contains("2")) {

        }
        else if (UIButton.current.name.Contains("3"))
        {
            BackMenuHide();
            GUIManager.ShowView("CoverPanel");
            //GUIManager.HideView("SystemPanel");
           // GameMain.isFirstStartGame = false;
            //GameStateManager.LoadScene(1);
            GUIManager.ShowView("CardCollectionsPanel");
            CardCollectionsIsActive = true;
        }
    }
    void SetCardsRotation() {
        for (int i = 0; i < ChooseBtnContainer.childCount; i++)
        {
            GameObject go = ChooseBtnContainer.GetChild(i).gameObject;
            go.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        }
    public static bool isFinishedDown;
    void StartShowMark() {       
        TweenPosition tp = rightMark.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.onFinished.Clear();
        tp.duration = 0.3f;
        tp.delay = 0.2f;
        tp.from = tp.transform.localPosition;
        tp.to = new Vector3(880, 0, 0);
        tp.ResetToBeginning();

        TweenAlpha ta = systemBg.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.onFinished.Clear();
        ta.duration = 0.3f;
        ta.delay = 0.1f;
        ta.from = 0;
        ta.to = 0.94f;
        ta.ResetToBeginning();
    }
    void HideRightMark() {        
        rightMark.transform.localPosition = new Vector3(1300, 0, 0);
        systemBg.alpha = 0;
    }

    void StartShowChooseBtn() {
        for (int i = 0; i < ChooseBtnContainer.childCount; i++) {
            GameObject go = ChooseBtnContainer.GetChild(i).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;

            tp.duration = 0.5f;
            tp.delay = 0.5f +0.3f* i;
            tp.onFinished.Clear();
            tp.from = go.transform.localPosition;
            tp.to = new Vector3(-8.0f + i * 3.0f, 0, 3.5f);
            tp.ResetToBeginning();

            TweenRotation tr = go.GetComponent<TweenRotation>();
            tr.enabled = true;
            tr.duration = 1.0f;
            tr.delay = i * 0.5f;
            tr.onFinished.Clear();           
            tr.from = go.transform.localRotation.eulerAngles;
            tr.to = new Vector3(0, 0, 0);
            tr.ResetToBeginning();

            if (i >= ChooseBtnContainer.childCount - 1)
            {                
                tr.onFinished.Add(new EventDelegate(AddComponentToCard));              
                
            }
        }
    }
    void OnFinshedMove() {
        Debug.LogError("you have it already");
            for (int i = 0; i < ChooseBtnContainer.childCount; i++) {
            GameObject go = ChooseBtnContainer.GetChild(i).gameObject;
            TweenRotation tr = go.GetComponent<TweenRotation>();
            tr.enabled = true;
            tr.duration = 0.3f;
            tr.delay = i * 0.3f;
            tr.onFinished.Clear();
            tr.from = go.transform.localRotation.eulerAngles;
            tr.to = new Vector3(0, 0, 0);
            tr.ResetToBeginning();

            if (i >= ChooseBtnContainer.childCount - 1)
            {               //Debug.LogError(ChooseBtnContainer.GetChild(ChooseBtnContainer.childCount - 1).transform.localRotation);                  
                    //isFinishedDown = true;
                    tr.onFinished.Add(new EventDelegate(AddComponentToCard));
                
            }
        }
        }
    void AddComponentToCard() {
        SpriteControlContainer.gameObject.SetActive(true);
        for (int i = 0; i < SpriteControlContainer.childCount; i++) {
            GameObject go = SpriteControlContainer.GetChild(i).gameObject;
            if (go.GetComponent<OnHoverRotate>() == null)
            {
                go.AddComponent<OnHoverRotate>();
            }
            else {
                continue;
            }
        }
    }
    void BackToNull()
    {
        for (int i = 0; i < ChooseBtnContainer.childCount; i++)
        {
            GameObject go = ChooseBtnContainer.GetChild(i).gameObject;
            go.transform.localPosition = new Vector3(-8.0f + i * 3.0f, 10, 3.5f);
            go.transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
    }
    
}

