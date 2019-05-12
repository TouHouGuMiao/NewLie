using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGPanel : IView
{
    public static bool IsCGPlay
    {
        get
        {
            if (cgPanel == null)
            {
                return false;
            }

            if (cgPanel.activeSelf)
            {

                return true;

            }
            return false;
        }
    }
    private static GameObject cgPanel;
    private UITexture mainCG;
    public static Texture2D CGTexutre;
    private Camera camera;
    private Transform cover;
    public static bool isNeedBlackCover=false;
    public CGPanel()
    {
        m_Layer = Layer.normal;
    }

    protected override void OnStart()
    {
        mainCG = this.GetChild("MainCG").GetComponent<UITexture>();
        cgPanel = GUIManager.FindPanel("CGPanel");
        cover = this.GetChild("Cover");
    }

    protected override void OnShow()
    {
        if (camera == null)
        {
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    
        }
        if (isNeedBlackCover)
        {
            cover.gameObject.SetActive(true);
            return;
        }
        else
        {
            cover.gameObject.SetActive(false);
            mainCG.mainTexture = CGTexutre;
        }
      
        //mainCG.MakePixelPerfect();
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        camera.gameObject.SetActive(true);
        isNeedBlackCover = false; 
    }

    public override void Update()
    {
      
    }
}
