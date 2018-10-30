using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : IView
{
    private UISlider m_slider;
    private float loadPos;
    private float delta;

    public LoadingPanel()
    {
        m_Layer = Layer.bottom;
    }

    protected override void OnStart()
    {
        GameObject panel = GUIManager.FindPanel("LoadingPanel");
       

        if (panel)
        {
            Transform t = panel.transform.FindRecursively("Container");
            if (t)
            {
                m_slider = t.GetComponent<UISlider>();
                if (m_slider == null)
                {
                    Debug.LogError("m_slider is null");
                    return;
                }
            }
            m_slider.value = 0;
        }
    }

    protected override void OnShow()
    {

        delta = Time.time + 0.01f;
        loadPos = 0;
        deltaPos = 0;
    }

    protected override void OnHide()
    {
        m_slider.value = 0;
     
    }

    protected override void OnDestroy()
    {
       
    }
    float deltaPos=0;
    bool isOpen
    {
        get
        {
            if (deltaPos < 1) return true;

            if (deltaPos >= 1) return false;

            return false;
        }
    }

    public static string LoadingName;
    public override void Update()
    {
        if (isOpen)
        {
            if (Time.time > delta)
            {
                m_slider.value = deltaPos;
                delta = Time.time + 0.01f;
                deltaPos += 0.01f;
            }
        }

        if (isOpen == false && DownLoadManager.Instance.isDo)
        {
            GUIManager.HideView("LoadingPanel");
            if (LoadingName != null)
            {
                GUIManager.ShowView(LoadingName);
                LoadingName = null;
            }
        }
    }
}
