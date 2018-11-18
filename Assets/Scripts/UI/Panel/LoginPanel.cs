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
        loginButton = this.GetChild("LoginButton").GetComponent<UIButton>();
        EventDelegate OnLoginClick = new global::EventDelegate(OnLoginBtnClick);
        loginButton.onClick.Add(OnLoginClick);
        OnLoginBtnHover();//鼠标悬浮选项放大的事件
        developerBtn = this.GetChild("LoginButton (4)").GetComponent<UIButton>();
        EventDelegate OnDeveloperBtn = new global::EventDelegate(OnDeveloperBtnClick);
        developerBtn.onClick.Add(OnDeveloperBtn);

    }


    void OnLoginBtnClick()
    {
        GameStateManager.LoadScene(2);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "PlayerPanel";
    }

    private List<Transform> m_PicList=new List<Transform> ();
    void OnLoginBtnHover(){//鼠标悬浮选项放大的事件
        GameObject go = GameObject.Find("GameChoice");
        foreach (Transform child in go.transform) {
            m_PicList.Add(child);
        } 
        BtnControl();
    }
    void BtnControl() {

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
}
