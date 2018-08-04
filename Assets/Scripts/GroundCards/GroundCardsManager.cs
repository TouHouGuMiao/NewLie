using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCardsManager:MonoBehaviour
{
    public static GroundCardsManager Instance;

    private List<CardData> GroundsCardList=new List<CardData> ();
    private GameObject groundItem;
    private UIGrid m_Grid;
    private UIScrollView m_ScrollView;
    public BoxCollider m_Collider;

    private void Awake()
    {
        Instance = this;
        Transform gridTF = this.transform.Find("GroundGrid");
        if (gridTF == null)
        {
            Debug.LogError("grid is null");
            return;
        }
        m_Grid = gridTF.GetComponent<UIGrid>();
        groundItem = m_Grid.transform.GetChild(0).gameObject;
        GroundsCardList = CardDataManager.Instance.GetGroundCardList();
        Transform SVTF = this.gameObject.transform.FindRecursively("Scroll View");
        if (SVTF == null)
        {
            Debug.LogError("SVTF is null");
            return;
        }
        m_ScrollView = SVTF.GetComponent<UIScrollView>();
        m_Collider = m_Grid.GetComponent<BoxCollider>();

    }


  

    private void Start()
    {
        UpdataData();
    }

    private void Update()
    {
   
    }

 


    public void DelteCardData(CardData data)
    {
        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            if (GroundsCardList[i].heroData.id == data.heroData.id)
            {
                GroundsCardList[i].num--;

            }
            if (GroundsCardList[i].num == 0)
            {
                GroundsCardList[i].isHas = false;
            }
        }
        GroundsCardList.Sort(Tools.CompareByRank);
        UpdataData();
    }


    public void AddCardData(CardData data)
    {

        int cout = 0;
        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            cout += GroundsCardList[i].num;
        }

        if (cout > 30)
        {
            return;
        }



        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            if (GroundsCardList[i].heroData.id == data.heroData.id)
            {
                if (GroundsCardList[i].heroData.starLv <3)
                {
                    if (GroundsCardList[i].num < 2)
                    {
                        GroundsCardList[i].num++;
                       
                    }
                    else
                        return;
                }

                if (GroundsCardList[i].heroData.starLv == 3)
                {
                    if (GroundsCardList[i].num < 1)
                    {
                     
                        GroundsCardList[i].num++;
                    }
                    else
                        return;
                }
                GroundsCardList[i].isHas = true;
            }
        }
        GroundsCardList.Sort(Tools.CompareByRank);
        UpdataData();
    }


    public void UpdataData()
    {
        int childCount = m_Grid.transform.childCount;
        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            GameObject go = null;
            if (!GroundsCardList[i].isHas) continue;
            if (i < childCount)
            {
                go = m_Grid.transform.GetChild(i).gameObject;
            }
            else
            {
                go = GameObject.Instantiate(groundItem) as GameObject;
                go.transform.SetParent(m_Grid.transform, false);
            }
            go.name = GroundsCardList[i].heroData.id.ToString();
            GroundItem item = new GroundItem();
            item.Init(go);
            int id = CommonHelper.Str2Int(go.name);
            item.Show(id);
            go.SetActive(true);
        }
        int hasCoun = 0;
        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            if (GroundsCardList[i].isHas)
            {
                hasCoun++;
            }
        }

        for (int i = hasCoun; i < childCount; i++)
        {
            GameObject go = m_Grid.transform.GetChild(i).gameObject;
            if (go != null)
            {
                go.SetActive(false);
            }
        }
            m_Grid.Reposition();

    }


    //不获取数量
    public List<CardData> GetGroundCardsList()
    {
        List<CardData> list = new List<CardData>();
        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            if (GroundsCardList[i].isHas == false)
            {
                continue;
            }
            for (int j = 0; j < GroundsCardList[i].num; j++)
            {
                CardData data = new CardData();
                data.num = 1;
                data.isHas = GroundsCardList[i].isHas;
                data.heroData = GroundsCardList[i].heroData;

                list.Add(data);
            }
        }
        return list;
    }






    public CardData GetGroundDataById(int id)
    {
        for (int i = 0; i < GroundsCardList.Count; i++)
        {
            if (GroundsCardList[i].heroData.id == id)
            {
                return GroundsCardList[i];
            }
        }
        return null;

    }



}

public class GroundItem
{
    private UISprite icon;
    private GameObject gameObject;
    private UILabel num;
    private UILabel name;
    private CardData data;

    public void Init(GameObject go)
    {
        gameObject = go;
        icon = go.transform.Find("GroundIcon").GetComponent<UISprite>();
        name = go.transform.Find("name").GetComponent<UILabel>();
        num = go.transform.Find("num").GetComponent<UILabel>();
    }

    public void Show(int id)
    {
        data = GroundCardsManager.Instance.GetGroundDataById(id);
        if (data == null)
        {
            Debug.LogError(" date is null");
            return;
        }
        icon.spriteName = data.heroData.spriteName;
        icon.MakePixelPerfect();
        name.text = data.heroData.name;
        num.text ="x"+ data.num.ToString();
    }
}
