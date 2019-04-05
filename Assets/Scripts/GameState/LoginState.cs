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
        GUIManager.ShowView("CoverPanel");
        //List<int> list = new List<int>();
        //list.Add(1);
        //list.Add(2);
        //list.Add(3);
        //list.Add(4);
        //list.Add(5);
        //list.Add(6);
        //list.Add(7);
        //list.Add(8);
        //list.Add(9);
        //DiceManager.Instance.ShowDicePanel(list, 0.01f);
        GUIManager.ShowView("LoginPanel");
       // GUIManager.ShowView("BGStoryPanel");
    }




}
