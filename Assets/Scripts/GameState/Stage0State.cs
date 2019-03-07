using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0State : GameState
{
    protected override void OnLoadComplete(params object[] args)
    {
        CGManager.instance.ShowCGPanel("CG1");
        GUIManager.ShowView("BattleUIPanel");
        GameZaXiangManager.Instance.ShowCover();
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0);
        GUIManager.HideView("GameHelpPanel");
        
    }

    protected override void OnStart()
    {
        //StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0State0List());
    }

    protected override void OnStop()
    {
      
    }

}
