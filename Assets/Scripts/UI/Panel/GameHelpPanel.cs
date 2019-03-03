using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelpPanel : IView
{   
    private UIButton closeBtn;
    // Use this for initialization
    public GameHelpPanel()
    {
        m_Layer = Layer.UI;
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
        
        closeBtn = this.GetChild("CloseBtn").GetComponent<UIButton>();
        EventDelegate OnCloseClick = new global::EventDelegate(ClosePanel);
        closeBtn.onClick.Add(OnCloseClick);
    }
    
    void ClosePanel()
    {
        if (closeBtn)
        {
            GUIManager.HideView("GameHelpPanel");
        }
    }
}
