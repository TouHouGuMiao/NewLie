using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaGroundCardManager
{
    private static BaGroundCardManager _instance = null;
    public static BaGroundCardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BaGroundCardManager();
            }
            return _instance;
        }
    }

    private List<CardData> m_BaGroundCardsList=new List<CardData> ();
    private List<CardData> HandCardList;
    private UIGrid m_Grid;
    private GameObject m_Item;
    public List<CardData> InPlaneHeroDataList = new List<CardData>();
   



    public void Init()
    {
        HandCardList = new List<global::CardData>();
        //m_BaGroundCardsList = GroundCardsManager.Instance.GetGroundCardsList();
        GameObject panel = GUIManager.FindPanel("BattleUIPanel");
        m_Item = panel.transform.FindRecursively("Item").gameObject;
        m_Grid = panel.transform.FindRecursively("Grid").GetComponent<UIGrid>();
    }

    public void UpdataHandCardData()
    {


        int childCount = m_Grid.transform.childCount;
        for (int i = 0; i < HandCardList.Count; i++)
        {
            GameObject go = null;
            if (i < childCount)
            {
                go = m_Grid.transform.GetChild(i).gameObject;

            }
            else
            {
                go = GameObject.Instantiate(m_Item) as GameObject;
                go.transform.SetParent(m_Grid.transform, false);
                BaGroundCardManager.Instance.SetDepth(go);
            }
            go.name = HandCardList[i].heroData.id.ToString();
            UISprite sprite = go.transform.Find("icon").GetComponent<UISprite>();
            sprite.spriteName = HandCardList[i].heroData.spriteName;
            sprite.MakePixelPerfect();
            go.SetActive(true);
        }


        for (int i = HandCardList.Count; i < childCount; i++)
        {
            GameObject go = m_Grid.transform.GetChild(i).gameObject;
            if (go != null)
            {
                go.SetActive(false);
            }
        }
        m_Grid.Reposition();
    }

    public void ChouCard()
    {
        CardData data = GetCard();
        if (data == null)
        {
            return;
        }
        HandCardList.Add(data);
    }

    public List<CardData> GetHandList()
    {
        return HandCardList;
    }




    public void DeleteHandCardDataById(int id)
    {
        for (int i = 0; i < HandCardList.Count; i++)
        {
            if (HandCardList[i].heroData.id == id)
            {
                HandCardList.Remove(HandCardList[i]);
                break;
            }
        }
        UpdataHandCardData();
    }



    
    private CardData GetCard()
    {
        int count = m_BaGroundCardsList.Count;
     
        int index = (int)Random.Range(0, count -0.1f);

        CardData  data= new CardData();
        data.heroData = m_BaGroundCardsList[index].heroData;
        data.isHas = true;
        data.num = 1;
        if (HandCardList.Count >= 5)return null;
        if (HandCardList.Count + InPlaneHeroDataList.Count >= count)
            return null;
        while (!SpecilRuler(data.heroData.id))
        {
            index = (int)Random.Range(0, count -0.1f);
            data.heroData = m_BaGroundCardsList[index].heroData;
        }
        return data;
    }



    int depth = 2;
   /// <summary>
   /// 在创建的时候设置depth
   /// </summary>
   /// <param name="go"></param>
    public void SetDepth(GameObject go)
    {
        UISprite bg = go.GetComponent<UISprite>();
        UISprite icon = go.transform.Find("icon").GetComponent<UISprite>();
        bg.depth = depth;
        depth++;
        icon.depth = depth;
        depth++;
        
    }


    /// <summary>
    /// 抽取手牌的特殊规则 相同卡牌数由卡组数定（场上+手牌），如带了一张⑨那只允许有两张⑨存在手牌或场上。
    /// </summary>
    bool SpecilRuler(int id)
    {
        int count = 0;
        for (int i = 0; i < HandCardList.Count; i++)
        {
            if (HandCardList[i].heroData.id == id)
            {
                count++;
            }
        }

        for (int i = 0; i < InPlaneHeroDataList.Count; i++)
        {
            if (InPlaneHeroDataList[i].heroData.id == id)
            {
                count++;
            }
        }
        count++;

        int groundCount=0;
        for (int i = 0; i < m_BaGroundCardsList.Count; i++)
        {
            if (m_BaGroundCardsList[i].heroData.id == id)
            {
                groundCount++;

            }
        }



        if (count > groundCount)
        {
            return false;
        }

        return true;
    }

}
