using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCardManager
{
    private static TableCardManager _instance = null;
    public static TableCardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TableCardManager();
            }
            return _instance;
        }
    }


    public void ReplaceTable_ChildGroundCard(int count)
    {
        CardEffectPanel.isReplaceChildGround = true;
        CardEffectPanel.isReplaceEventGround = false;
        CardEffectPanel.isReplaceEventCard = false;
        CardEffectPanel.isSetHandCard = false;
        CardEffectPanel.eventChildGroundReplaceCount = 10;
        GUIManager.ShowView("CardEffectPanel");
    }

    public void ShowHandCardChouKa(List<string>cardNameList)
    {
        CardEffectPanel.isReplaceChildGround = false;
        CardEffectPanel.isReplaceEventGround = false;
        CardEffectPanel.isReplaceEventCard = false;
        CardEffectPanel.isSetHandCard = true;
        CardEffectPanel.addToHandCardNameList = cardNameList;
        GUIManager.ShowView("CardEffectPanel");
    }

	
}
