using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager
{
    private static DiceManager _instance = null;
    public static  DiceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DiceManager();
            }
            return _instance;
        }
    }
   

    public void ShowDicePanel(List<int>DiceNumerList,float rate)
    {
        DicePanel.DiceNumberList = DiceNumerList;
        DicePanel.rate = rate;
        GUIManager.ShowView("DicePanel");
    }



}
