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
    }


    void OnLoginBtnClick()
    {
        GameStateManager.LoadScene(2);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "PlayerPanel";
    }

}
