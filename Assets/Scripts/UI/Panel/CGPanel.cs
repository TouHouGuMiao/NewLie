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

    public CGPanel()
    {
        m_Layer = Layer.normal;
    }

    protected override void OnStart()
    {
        mainCG = this.GetChild("MainCG").GetComponent<UITexture>();
        cgPanel = GUIManager.FindPanel("CGPanel");
     
    }

    protected override void OnShow()
    {
        if (camera == null)
        {
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            camera.gameObject.SetActive(false);
        }

        mainCG.mainTexture = CGTexutre;
        mainCG.MakePixelPerfect();
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        camera.gameObject.SetActive(true);
    }

    public override void Update()
    {
      
    }
}
