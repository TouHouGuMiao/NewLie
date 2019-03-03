using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : IView
{
    public LoginPanel()
    {
        m_Layer = Layer.bottom;
    }

    private UIButton loginButton;
    private UIButton developerBtn;
    private UIButton closeGameBtn;
    private UIButton HelpBtn;
    private UISprite m_Dimon;
    private GameObject m_go;
    private Transform gameChoice;

    //private GameObject go;
    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnStart()
    {


        gameChoice = this.GetChild("GameChoice");
        loginButton = this.GetChild("LoginButton").GetComponent<UIButton>();
        EventDelegate OnLoginClick = new global::EventDelegate(OnLoginBtnClick);
        loginButton.onClick.Add(OnLoginClick);

        OnLoginBtnHover();//鼠标悬浮选项放大的事件
                          // InitDimon();
        HelpBtn = this.GetChild("LoginButton (3)").GetComponent<UIButton>();
        HelpBtn.onClick.Add(new EventDelegate(OnHelpPanelClick));

        developerBtn = this.GetChild("LoginButton (4)").GetComponent<UIButton>();
        EventDelegate OnDeveloperBtn = new global::EventDelegate(OnDeveloperBtnClick);
        developerBtn.onClick.Add(OnDeveloperBtn);
       
        closeGameBtn = this.GetChild("LoginButton (5)").GetComponent<UIButton>();
        EventDelegate OnCloseBtn = new global::EventDelegate(OnCloseGameBtn);
        closeGameBtn.onClick.Add(OnCloseBtn);

        

    }


    void OnLoginBtnClick()
    {
        GameStateManager.LoadScene(2);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "PlayerPanel";
    }

    private List<Transform> m_PicList=new List<Transform> ();
    void OnLoginBtnHover(){//鼠标悬浮选项放大的事件
        GameObject go = GameObject.Find("ButtonGrid");
        foreach (Transform child in go.transform) {
            m_PicList.Add(child);
        } 
        BtnControl();
        
    }
    void BtnControl() {//悬停鼠标功能控制

        foreach (Transform child in m_PicList)
        {
            child.gameObject.AddComponent<OnGamStarBunHover>();
           
        }
    }

    void OnDeveloperBtnClick() {//开发人员界面显示
        GUIManager.ShowView("DeveloperPanel");
        GUIManager.HideView("LoginPanel");
        //LoadingPanel.LoadingName("")
    }
    void OnCloseGameBtn() {
        Application.Quit();

    }//游戏整体退出
    void OnHelpPanelClick() {
        GUIManager.ShowView("GameHelpPanel");
    }
    
    
    
    
    
    
    
    
    
    //void InitDimon() {
    //    foreach (Transform child in m_PicList) {
    //        GameObject go = GameObject.Find("Dimon");
    //        GameObject go_Clone = GameObject.Instantiate(go);
    //        go_Clone.transform.SetParent(gameChoice, false);
    //        go_Clone.transform.localPosition = child.transform.localPosition + new Vector3(-950, -100, 0);

    //        //GameObject.Instantiate(go, child.transform.localPosition+new Vector3(0,100,0), child.transform.localRotation);

    //    }

    //}
}
 