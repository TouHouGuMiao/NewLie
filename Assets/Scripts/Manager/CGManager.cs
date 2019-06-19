using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGManager
{
    private static CGManager _Instance = null;
    public static CGManager instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CGManager();
                _Instance.Init();
            }
            return _Instance;
        }
    }

    private GameObject UICamera;
    private GameObject cgPanel;
    void Init()
    {
        UICamera = GameObject.FindWithTag("UIRoot").transform.Find("Camera").gameObject;
    }

    public void ShowCGPanel(string name)
    {
        Texture2D texture = ResourcesManager.Instance.LoadCG(name);
        CGPanel.CGTexutre = texture;
        CGPanel.isNeedBlackCover = false;
        GUIManager.ShowView("CGPanel");
    }

    public void ShowBlackCover(float fadeTime=0)
    {
        CGPanel.isNeedBlackCover = true;
        GUIManager.ShowView("CGPanel");
    }

    public void ChangeCG(string name)
    {
        GameObject panel = GUIManager.FindPanel("CGPanel");
        TweenAlpha ta = panel.transform.FindRecursively("Cover").GetComponent<TweenAlpha>();
        ta.ResetToBeginning();
        ta.enabled = true;
        ShowCGPanel(name);
    }

    public void HideCGPanel()
    {
        GameObject panel = GUIManager.FindPanel("CGPanel");
        TweenAlpha ta = panel.transform.FindRecursively("Cover").GetComponent<TweenAlpha>();
    }

    

   

}
