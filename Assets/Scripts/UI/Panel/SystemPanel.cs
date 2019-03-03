using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPanel : IView
{
    private UIButton InventoryBtn;
    private UIButton BacktoMenuBtn;
    private Transform ParentGo;
    public static bool Bg_IsActive = false;
    private List<Transform> goList = new List<Transform>();
    private List<string> goActiveList = new List<string>();

    public SystemPanel()
    {
        m_Layer = Layer.System;
    }

    protected override void OnStart()
    {
        ParentGo = GameObject.Find("UI Root").transform;
        //  InventoryBtn = this.GetChild("InventoryBtn").GetComponent<UIButton>();
        InventoryBtn = this.GetChild("InventoryBtn").GetComponent<UIButton>();
        BacktoMenuBtn = this.GetChild("BacktoMenuBtn").GetComponent<UIButton>();
        AddEventBtn();
    }  
    protected override void OnShow()
    {
     
    }

    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
    
    }

    void AddEventBtn()
    {
        EventDelegate InventoryBtnEvent = new global::EventDelegate(OnInventoryBtnClick);
        EventDelegate BacktoMenuEvent = new global::EventDelegate(OnBacktoMenuClick);

        InventoryBtn.onClick.Add(InventoryBtnEvent);
        BacktoMenuBtn.onClick.Add(BacktoMenuEvent);
    }

    void OnInventoryBtnClick()
    {
        // GUIManager.ShowView("InventoryPanel");
        GUIManager.ShowView("BagPanel");
        Bg_IsActive = true;
    }

    void OnBacktoMenuClick() {

        BackMenuHide();
        GUIManager.ShowView("LoginPanel");
       
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
                Debug.Log(goList[i].name);
            }
        }
        for (int j = 0; j < goActiveList.Count; j++)
        {
            if (goActiveList[j] != "Camera")
            {
                GUIManager.HideView(goActiveList[j]);
            }
        }
        GameStateManager.LoadScene(1);
    }
}
