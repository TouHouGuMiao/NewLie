using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityState : GameState
{
    protected override void OnStart()
    {
        XMLDataManager.Instance.LoadXmlData();
       // GUIManager.ShowView("GameHelpPanel");
    }

    protected override void OnStop()
    {
        
    }

    protected override void OnLoadComplete(params object[] args)
    {
 
    }
}
