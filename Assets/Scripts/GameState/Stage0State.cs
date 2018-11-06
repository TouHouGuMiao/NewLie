using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0State : GameState
{
    protected override void OnLoadComplete(params object[] args)
    {
        GUIManager.ShowView("BattleUIPanel");
       
    }

    protected override void OnStart()
    {
        //StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0State0List());
    }

    protected override void OnStop()
    {
      
    }

}
