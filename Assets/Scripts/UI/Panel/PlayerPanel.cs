using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : IView
{


    private UIButton ShopBtn;

    public PlayerPanel()
    {
        m_Layer = Layer.city;
    }

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

        ShopBtn = this.GetChild("ShopBtn").GetComponent<UIButton>();

        AddEventDelete();

       
    }


    private void AddEventDelete()
    {

        EventDelegate ShopClick = new global::EventDelegate(OnSpeakPanelClick);

        

        ShopBtn.onClick.Add(ShopClick); 

      
        
    }



    void OnBattleBtnClick()
    {

        GameStateManager.LoadScene(3);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "BattleUIPanel";
    }

    void OnCardsGroundBtnClick()
    {
        GUIManager.ShowView("CardsPanel");
    }

    void OnSpeakPanelClick()
    {
        GameStateManager.LoadScene(4);
        GUIManager.ShowView("LoadingPanel");
    }
    
    
}
