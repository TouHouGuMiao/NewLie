using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPanel : IView
{
    private UIButton InventoryBtn;
    protected override void OnStart()
    {
        //  InventoryBtn = this.GetChild("InventoryBtn").GetComponent<UIButton>();
        InventoryBtn = this.GetChild("InventoryBtn").GetComponent<UIButton>();
        AddEventBtn();
    }  
    protected override void OnShow()
    {
     
    }

    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
    
    }

    void AddEventBtn()
    {
        EventDelegate InventoryBtnEvent = new global::EventDelegate(OnInventoryBtnClick);
        InventoryBtn.onClick.Add(InventoryBtnEvent);
    }

    void OnInventoryBtnClick()
    {
        // GUIManager.ShowView("InventoryPanel");
        GUIManager.ShowView("BagPanel");
    }


}
