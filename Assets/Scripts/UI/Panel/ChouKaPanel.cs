using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChouKaPanel : IView
{
    public ChouKaPanel()
    {
        m_Layer = Layer.UI;
    }

    private UIButton chouKaBtn;
    private UIButton closeBtn;
    private UISprite heroItem;
    private GameObject instanteItem;


    private UISprite icon;
    protected override void OnStart()
    {
        GameObject panel = GUIManager.FindPanel("ChouKaPanel");
        Transform ChouKaTF = panel.transform.FindRecursively("ChouKaBtn");
        Transform closeTF = panel.transform.FindRecursively("Close");
        Transform itemTF = panel.transform.FindRecursively("heroItem");
        if (ChouKaTF == null)
        {
            Debug.LogError("ChoukaTF is null");
            return;
        }

        if (closeTF == null)
        {
            Debug.LogError("Close is null");
            return;
        }
        if (itemTF == null)
        {
            Debug.LogError("itemTF is null");
            return;
        }
        Transform iconTF = itemTF.transform.Find("icon");

        if (iconTF == null)
        {
            Debug.LogError("item is null");
            return;
        }
        chouKaBtn = ChouKaTF.GetComponent<UIButton>();
        closeBtn = closeTF.GetComponent<UIButton>();
        heroItem = itemTF.GetComponent<UISprite>();
        icon = iconTF.GetComponent<UISprite>();

        EventDelegate ChouKaClick = new EventDelegate(OnChouKaBtnClick);
        chouKaBtn.onClick.Add(ChouKaClick);
        EventDelegate CloseClick = new EventDelegate(CloseBtnClick);
        closeBtn.onClick.Add(CloseClick);
    }

    protected override void OnShow()
    {
   
    }

    protected override void OnHide()
    {
        GameObject.Destroy(instanteItem);
    }

    protected override void OnDestroy()
    {
     
    }



    public override void Update()
    {
   
    }

    private void OnChouKaBtnClick()
    {
        GameObject.Destroy(instanteItem);
        InsitanteItem();
    }
     
     private void CloseBtnClick()
    {
        GUIManager.HideView("ChouKaPanel");
    }


    private void InsitanteItem()
    {
        if (heroItem == null)
        {
            Debug.LogError("heroItem is null");
            return;
        }
        GameObject panel = GUIManager.FindPanel("ChouKaPanel");
        instanteItem = GameObject.Instantiate(heroItem).gameObject;
        instanteItem.transform.SetParent(panel.transform, false);
        CardData m_ChouKaData = new global::CardData();
        m_ChouKaData.heroData = HeroDataManager.Instance.GetChouKaData();
        UISprite itemSprite = instanteItem.transform.GetComponent<UISprite>();
        UISprite icon = instanteItem.transform.Find("icon").GetComponent<UISprite>();
        icon.spriteName = m_ChouKaData.heroData.spriteName;
        icon.MakePixelPerfect();
        icon.transform.localScale = new Vector3(1.5F, 1.5F, 1.5F);
        if (m_ChouKaData.heroData.starLv == 0)
        {

        }
        else if (m_ChouKaData.heroData.starLv == 1)
        {
            itemSprite.color = new Color(65/255f, 105/255f, 225/255f);
        }
        else if (m_ChouKaData.heroData.starLv == 2)
        {
            itemSprite.color = new Color(160/255f, 32/255f, 240/255f);
        }
        else if (m_ChouKaData.heroData.starLv == 3)
        {
            itemSprite.color = new Color(255/255f, 255/255f, 0/255f);
        }

        XMLDataManager.Instance.CraetOrSaveXml(m_ChouKaData);
        instanteItem.SetActive(true);
    } 

  


    
}
