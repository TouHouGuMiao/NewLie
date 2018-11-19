using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperPanel :IView {
    private UILabel lb;
    private UIButton closeBtn;
    // Use this for initialization
    public DeveloperPanel() {
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
        lb = this.GetChild("SomeTest").GetComponent<UILabel>();
        ShowLabel();
        closeBtn = this.GetChild("CloseBtn").GetComponent<UIButton>();
        EventDelegate OnCloseClick = new global::EventDelegate(ClosePanel);
        closeBtn.onClick.Add(OnCloseClick);
    }
    void ShowLabel() {
        lb.text = "keep your siliver I'll get my gold 拿好你的银牌，冠军是我的";
    }
    void ClosePanel() {
        if (closeBtn)
        {
            GUIManager.HideView("DeveloperPanel");
            GUIManager.ShowView("LoginPanel");
        }
    }
}
