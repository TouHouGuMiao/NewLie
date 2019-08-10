using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager
{
    private static BattleManager _Instance = null;
    public static BattleManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new BattleManager();
            }
            return _Instance;
        }
    }


    public void ShowPlayerBattleSlider(float neeedValue, PropEventDelegate OnSliderFished = null)
    {
        GameObject panel = GUIManager.FindPanel("BattleUIPanel");
        if (!panel.activeSelf)
        {
            GUIManager.ShowView("BattleUIPanel");
        }

        BattleUIPanel.ShowChangePlayerBattlePressureSlider(neeedValue, OnSliderFished);
        GameObject player = GameObject.FindWithTag("Player");



        if (neeedValue == 0)
        {
            return;
        }
        GUIManager.ShowView("ExpressionEffectPanel");
        ExpressionEffectPanel.ShowPressEffect(player, (int)neeedValue);
    }

    public void ShowRoundStart()
    {
        GameObject panel = GUIManager.FindPanel("BattleUIPanel");
        if (!panel.activeSelf || panel == null)
        {
            GUIManager.ShowView("BattleUIPanel");
        }
        BattleUIPanel.RoundStart();
    }


    public void SetPlayerBattleSliderPos()
    {
        GameObject panel = GUIManager.FindPanel("BattleUIPanel");
        if (!panel.activeSelf || panel == null)
        {
            GUIManager.ShowView("BattleUIPanel");
        }
        BattleUIPanel.SetBattlePressSliderPos();
    }

    public void SetExpress_Press(NPCAnimatorManager.BGEnmu bgEnum,string characterName,int pressValue)
    {
        GameObject go = null;
        if (characterName == "player")
        {
            go = GameObject.FindWithTag("Player");
        }

        else
        {
            GameObject bg=null;
            if(bgEnum == NPCAnimatorManager.BGEnmu.ShenShe){
                bg = GameObject.FindWithTag("ShenShe");
            }
            else if(bgEnum == NPCAnimatorManager.BGEnmu.Village)
            {
                bg = GameObject.FindWithTag("Village");
            }
            go = bg.transform.FindRecursively(characterName).gameObject;
        }



        GUIManager.ShowView("ExpressionEffectPanel");

        ExpressionEffectPanel.ShowPressEffect(go, pressValue);

    }
}
