using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsPanel:IView
{
    private List<CardData> m_heroDataList;
    private GameObject item;
    private UIGrid m_Grid;
    private int startIndex;
    private int endIndex;
    private UIButton nextBtn;
    private UIButton upBtn;
    private UIButton closeBtn;

    public CardsPanel()
    {
        m_Layer = Layer.UI;
    }
    protected override void OnStart()
    {
        m_heroDataList = new List<CardData>();
        GameObject panel = GUIManager.FindPanel("CardsPanel");
        GameObject groundPanel =panel.transform.Find("Scroll View").gameObject;
        groundPanel.AddComponent<GroundCardsManager>();
        Transform HasGridTF = panel.transform.FindRecursively("Grid");
        Transform itemTF = panel.transform.FindRecursively("Item");
        if (itemTF == null)
        {
            Debug.LogError("itemTF is null");
            return;
        }
        if (HasGridTF == null)
        {
            Debug.LogError("HasGridTF is null");
            return;
        }
    
        item = itemTF.gameObject;
        m_Grid = HasGridTF.GetComponent<UIGrid>();
        nextBtn = panel.transform.FindRecursively("NextBtn").GetComponent<UIButton>();
        upBtn = panel.transform.FindRecursively("UptBtn").GetComponent<UIButton>();
        closeBtn = panel.transform.FindRecursively("closeBtn").GetComponent<UIButton>();
        CardsContiny.Instance.cardCollider = panel.transform.FindRecursively("Grid").GetComponent<BoxCollider>();
        ButtonAdd();
    }

    protected override void OnShow()
    {
        int cout=0;
      
  
        startIndex = 0;
        endIndex = 10;
        m_heroDataList = XMLDataManager.Instance.GetHasList();
        for (int i = 0; i < m_heroDataList.Count; i++)
        {
            if (m_heroDataList[i].isHas)
                cout++;
        }

        if (cout <= 10)
        {
            nextBtn.gameObject.SetActive(false);
        }
        UpdataCardsData();
        CardsContiny.Instance.UpDataCardItem(m_Grid.gameObject);
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        for (int i = 1; i < m_Grid.transform.childCount; i++)
        {
            GameObject go = m_Grid.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }

 
        nextBtn.gameObject.SetActive(true);
        upBtn.gameObject.SetActive(false);
        CardsContiny.Instance.OnHide();
    }

    public override void Update()
    {
        base.Update();
    }

    private void UpdataCardsData()
    {
        int childCount = m_Grid.transform.childCount;
        int childIndex = 0;
        for (int i = startIndex; i < endIndex; i++)
        {
            GameObject go = null;
            if (!m_heroDataList[i].isHas) continue;
            if (childIndex < childCount)
            {
                go = m_Grid.transform.GetChild(childIndex).gameObject;
            }
            else
            {
                go = GameObject.Instantiate(item) as GameObject;
                go.transform.SetParent(m_Grid.transform, false);
            }
            go.name = m_heroDataList[i].heroData.id.ToString();
            CardsItem m_Item = go.GetComponent<CardsItem>();
            m_Item.UpdataItem();
            go.SetActive(true);
            childIndex++;
        }

        for (int i = endIndex-startIndex; i < childCount; i++)
        {
            GameObject go = m_Grid.transform.GetChild(i).gameObject;
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        m_Grid.Reposition();
    }

    private void OnNextBtnClick()
    {
        int count = 0;
        for (int i = 0; i < m_heroDataList.Count; i++)
        {
            if (m_heroDataList[i].isHas)
            {
                count++;
            }
        }
        startIndex = endIndex;
        endIndex = endIndex + 10;
        if (count < endIndex)
        {
            int moreIndex = count % 10;
            endIndex = endIndex - 10 + moreIndex;
            nextBtn.gameObject.SetActive(false);
        }
        upBtn.gameObject.SetActive(true);
        UpdataCardsData();
        CardsContiny.Instance.UpDataCardItem(m_Grid.gameObject);
    }

    private void OnUPBtnClick()
    {
        endIndex = startIndex;
        startIndex = startIndex - 10;
        if (startIndex == 0)
        {
            upBtn.gameObject.SetActive(false);
        }
        nextBtn.gameObject.SetActive(true);
        UpdataCardsData();
        CardsContiny.Instance.UpDataCardItem(m_Grid.gameObject);
    }

    private void OnCloseBtnClick()
    {
        GUIManager.HideView("CardsPanel");
    }

    private void ButtonAdd()
    {

        EventDelegate NextBtnClick = new global::EventDelegate(OnNextBtnClick);
        nextBtn.onClick.Add(NextBtnClick);
        EventDelegate UPBtnClick = new EventDelegate(OnUPBtnClick);
        upBtn.onClick.Add(UPBtnClick);
        EventDelegate CloseBtnClick = new global::EventDelegate(OnCloseBtnClick);
        closeBtn.onClick.Add(CloseBtnClick);
    }



}
