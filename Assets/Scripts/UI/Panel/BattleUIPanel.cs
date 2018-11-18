using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIPanel :IView
{
    private GameObject m_Item;
    private UIGrid m_Grid;
    //private List<CardData> m_HandCardsList;
    private UIButton chouKaBtn;
    private AnimationCurve m_Curve;
    private Vector3 initVec = new Vector3(0, 1, 0);
    private CharacterBase playerProBase;
    private GameObject player;

    private UISprite bulletIcon;
    private UILabel bulletNum;

    public BattleUIPanel()
    {
        m_Layer = Layer.city;
    }
   /// <summary>
   /// 
   /// </summary>
    protected override void OnStart()
    {

        GameObject panel = GUIManager.FindPanel("BattleUIPanel");


        BattleCommoUIManager.Instance.InitUI(panel);
    }

    protected override void OnShow()
    {
        BattleCommoUIManager.Instance.UpdataSlider_Player();
    }

    protected override void OnHide()
    {
       


    }

    protected override void OnDestroy()
    {
        
    }

    public override void Update()
    {
        BattleCommoUIManager.Instance.UpdataSlider_Player();
       
    }
}
