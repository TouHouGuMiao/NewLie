using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverPanel : IView
{
    private TweenAlpha ta;
    public static float duration=1.0f;
    public static bool needAuteHide = true;
    public static bool fadeIn=false;
    public static EventDelegate OnCoverFished;
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

        ta.enabled = true;
        ta.duration = duration;

        if (OnCoverFished != null)
        {
            ta.onFinished.Clear();
            ta.onFinished.Add(OnCoverFished);
        }

        else
        {
            ta.onFinished.Clear();
            ta.onFinished.Add(new EventDelegate(HidePanel));
        }

        if (fadeIn)
        {
            ta.from = 0f;
            ta.to = 1.0f;
        }

        else
        {
            ta.from = 1.0f;
            ta.to = 0;
        }
        ta.ResetToBeginning();
     
    }

    protected override void OnDestroy()
    {
   
    }

    protected override void OnHide()
    {
       
    }


    void HidePanel()
    {
        if (!needAuteHide)
        {
            return;
        }
        GUIManager.HideView("CoverPanel");
    }

 

  
}
