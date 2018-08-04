using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginState : GameState
{
    protected override void OnStart()
    {
       
    }

    protected override void OnStop()
    {
        
    }

    protected override void OnLoadComplete(params object[] args)
    {
        GUIManager.ShowView("LoginPanel");
    }

}
