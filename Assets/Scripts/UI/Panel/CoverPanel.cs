using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPanel : IView
{
    private TweenAlpha ta;

    public CoverPanel()
    {
        m_Layer = Layer.CoverUI;
    }

    protected override void OnStart()
    {
        ta = this.GetChild("cover").GetComponent<TweenAlpha>();
        ta.onFinished.Add(new EventDelegate(HidePanel));
    }

    protected override void OnShow()
    {
        ta.ResetToBeginning();
        ta.enabled=true;
    }

    protected override void OnDestroy()
    {
   
    }

    protected override void OnHide()
    {
       
    }


    void HidePanel()
    {
        GUIManager.HideView("CoverPanel");
    }

 

  
}
