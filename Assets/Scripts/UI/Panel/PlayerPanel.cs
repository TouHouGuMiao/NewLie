using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : IView
{

    private UIButton ChouKaBtn;
    private UIButton CardsGroundBtn;
    private UIButton BattleBtn;
    private UIButton ShopBtn;
    private UIButton InventoryBtn;
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
        BattleBtn = this.GetChild("BattleBtn").GetComponent<UIButton>();
        CardsGroundBtn = this.GetChild("CardBtn").GetComponent<UIButton>();
        ChouKaBtn = this.GetChild("chouKaBtn").GetComponent<UIButton>();
        ShopBtn = this.GetChild("ShopBtn").GetComponent<UIButton>();
        InventoryBtn = this.GetChild("BackBtn").GetComponent<UIButton>();
        AddEventDelete();

       
    }


    private void AddEventDelete()
    {
        EventDelegate ChouKaClick = new global::EventDelegate(OnChouKaBtnClick);
        EventDelegate BattleClick = new EventDelegate(OnBattleBtnClick);
        EventDelegate CardsClick = new EventDelegate(OnCardsGroundBtnClick);
        EventDelegate ShopClick = new global::EventDelegate(OnSpeakPanelClick);
        EventDelegate InventoryEvent = new global::EventDelegate(OnInventoryBtnClick);
        

        ShopBtn.onClick.Add(ShopClick); 
        ChouKaBtn.onClick.Add(ChouKaClick);
        BattleBtn.onClick.Add(BattleClick);
        CardsGroundBtn.onClick.Add(CardsClick);
        InventoryBtn.onClick.Add(InventoryEvent);
      
        
    }

    void OnInventoryBtnClick()
    {
        GUIManager.ShowView("InventoryPanel");
    }

    void OnChouKaBtnClick()
    {
        GUIManager.ShowView("ChouKaPanel");
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
