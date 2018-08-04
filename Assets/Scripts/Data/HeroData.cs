using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroData
{
    public int id;
    public string name;
    public string des;
    public string spriteName;
    public int starLv;//0白 1蓝 2紫 3橙
}



public class CardData
{
    public HeroData heroData;
    public int num;
    public bool isHas;
   

    public CardData()
    {
        heroData = new global::HeroData();
        isHas = false;
        num = 0;
    }
}

public class CardDataManager
{
    private static CardDataManager _Instance = null;
    public static CardDataManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CardDataManager();
            }
            return _Instance;
        }
    }
    private List<CardData> m_CardsList=new List<CardData> ();
    private List<CardData> GroundCardList;

    public List<CardData> GetInitCardList()
    {
        m_CardsList.Clear();
        m_CardsList= InitCardList();
        return m_CardsList;
    }

    public List<CardData> GetGroundCardList()
    {
        if (GroundCardList == null)
        {
            GetGroundList();
        }
        return GroundCardList;
    }

    private void GetGroundList()
    {
        GroundCardList = new List<CardData>();
        List<HeroData> heroDataList = new List<global::HeroData>();
        heroDataList = HeroDataManager.Instance.GetAllHeroDataList();
        foreach (HeroData item in heroDataList)
        {
            CardData m_CardData = new CardData();
            m_CardData.heroData = item;
            m_CardData.isHas = false;
            m_CardData.num = 0;
            GroundCardList.Add(m_CardData);

        }
    }

    private List<CardData> InitCardList()
    {
        List<HeroData> heroDataList = new List<global::HeroData>();
        List<CardData> m_CardDataList = new List<global::CardData>();
        heroDataList = HeroDataManager.Instance.GetAllHeroDataList();
        foreach (HeroData item in heroDataList)
        {
            CardData m_CardData = new CardData();
            m_CardData.heroData = item;
            m_CardData.isHas = false;
            m_CardData.num = 0;
            m_CardDataList.Add(m_CardData);

        }
        return m_CardDataList;
    }
    }

public class HeroDataManager
{
    private static HeroDataManager _Instance = null;

    public static HeroDataManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new HeroDataManager();
            }
            return _Instance;
        }
    }

    private Dictionary<int, HeroData> heroDataDic;
    private List<HeroData> AllDataList;




    public List<HeroData> GetAllHeroDataList()
    {
        if (heroDataDic == null)
        {
            heroDataDic = new Dictionary<int, HeroData>();
            InitHeroData();
        }

        if (AllDataList == null)
        {
            AllDataList = new List<HeroData>();

            foreach (HeroData item in heroDataDic.Values)
            {
                AllDataList.Add(item);
            }
        }

        return AllDataList;
    }

    public HeroData GetHeroData(int id)
    {
        HeroData m_HeroData = new HeroData();
        if (heroDataDic.TryGetValue(id, out m_HeroData))
        {
            return m_HeroData;
        }
        return null;
    }

    public HeroData GetChouKaData()
    {
        if (heroDataDic == null)
        {
            InitHeroData();
        }

        List<HeroData> ChoukaList = new List<HeroData>();
        float pro = Random.Range(0.01f, 1);

        if (pro < 0)
        {
            return null;
        }
        if (pro <= 0.5)
        {
            foreach (KeyValuePair<int, HeroData> pair in heroDataDic)
            {
                if (pair.Value.starLv == 0)
                {
                    ChoukaList.Add(pair.Value);
                }
            }
            int num = (int)Random.Range(0, ChoukaList.Count  -0.1F);
            return ChoukaList[num];
        }

        else if (pro >= 0.5 && pro <= 0.9)
        {
            foreach (KeyValuePair<int, HeroData> pair in heroDataDic)
            {
                if (pair.Value.starLv == 1)
                {
                    ChoukaList.Add(pair.Value);
                }
            }
            int num = (int)Random.Range(0, ChoukaList.Count - 0.1F);
            return ChoukaList[num];
        }
        else if (pro >= 0.9 && pro <= 0.99)
        {
            foreach (KeyValuePair<int, HeroData> pair in heroDataDic)
            {
                if (pair.Value.starLv == 2)
                {
                    ChoukaList.Add(pair.Value);
                }
            }
            int num = (int)Random.Range(0, ChoukaList.Count - 0.1F);
            Debug.Log(num);
            Debug.Log(ChoukaList.Count - 1);
            return ChoukaList[num];
        }
        else if (pro >= 0.99 && pro <= 1)
        {
            foreach (KeyValuePair<int, HeroData> pair in heroDataDic)
            {
                if (pair.Value.starLv == 3)
                {
                    ChoukaList.Add(pair.Value);
                }
            }
            int num = (int)Random.Range(0, ChoukaList.Count-0.1F);
            return ChoukaList[num];
        }
        return null;
    }


    private void InitHeroData()
    {
        heroDataDic = new Dictionary<int, HeroData>();
        HeroData m_HeroData0 = new HeroData();
        m_HeroData0.id = 0;
        m_HeroData0.name = "八云紫";
        m_HeroData0.spriteName = "0";
        m_HeroData0.starLv = 3;

        heroDataDic.Add(m_HeroData0.id, m_HeroData0);

      

        HeroData m_HeroData1 = new HeroData();
        m_HeroData1.id = 1;
        m_HeroData1.name = "博丽灵梦";
        m_HeroData1.spriteName = "1";
        m_HeroData1.starLv = 2;



        heroDataDic.Add(m_HeroData1.id, m_HeroData1);

        HeroData m_HeroData2 = new HeroData();
        m_HeroData2.id = 2;
        m_HeroData2.name = "大妖精";
        m_HeroData2.spriteName = "2";
        m_HeroData2.starLv = 1;

        heroDataDic.Add(m_HeroData2.id, m_HeroData2);

        HeroData m_HeroData3 = new HeroData();
        m_HeroData3.id = 3;
        m_HeroData3.name = "琪露诺";
        m_HeroData3.spriteName = "3";
        m_HeroData3.starLv = 0;

   
        heroDataDic.Add(m_HeroData3.id, m_HeroData3);

        HeroData m_HeroData4 = new HeroData();
        m_HeroData4.id = 4;
        m_HeroData4.name = "露米娅";
        m_HeroData4.spriteName = "4";
        m_HeroData4.starLv = 1;

    
        heroDataDic.Add(m_HeroData4.id, m_HeroData4);

        HeroData m_HeroData5 = new HeroData();
        m_HeroData5.id = 5;
        m_HeroData5.name = "红美铃";
        m_HeroData5.spriteName = "5";
        m_HeroData5.starLv = 1;


 
        heroDataDic.Add(m_HeroData5.id, m_HeroData5);

        HeroData m_HeroData6 = new HeroData();
        m_HeroData6.id = 6;
        m_HeroData6.name = "河城荷取";
        m_HeroData6.spriteName = "6";
        m_HeroData6.starLv = 1;

   
        heroDataDic.Add(m_HeroData6.id, m_HeroData6);

        HeroData m_HeroData7 = new HeroData();
        m_HeroData7.id = 7;
        m_HeroData7.name = "米斯蒂娅";
        m_HeroData7.spriteName = "7";
        m_HeroData7.starLv = 0;


        heroDataDic.Add(m_HeroData7.id, m_HeroData7);

        HeroData m_HeroData8 = new HeroData();
        m_HeroData8.id = 8;
        m_HeroData8.name = "八云橙";
        m_HeroData8.spriteName = "8";
        m_HeroData8.starLv = 1;

        heroDataDic.Add(m_HeroData8.id, m_HeroData8);

        HeroData m_HeroData9 = new HeroData();
        m_HeroData9.id = 9;
        m_HeroData9.name = "蕾迪";
        m_HeroData9.spriteName = "9";
        m_HeroData9.starLv = 0;

        heroDataDic.Add(m_HeroData9.id, m_HeroData9);

        HeroData m_HeroData10 = new HeroData();
        m_HeroData10.id = 10;
        m_HeroData10.name = "泄矢诹访子";
        m_HeroData10.spriteName = "10";
        m_HeroData10.starLv = 2;

    
        heroDataDic.Add(m_HeroData10.id, m_HeroData10);

        HeroData m_HeroData11 = new HeroData();
        m_HeroData11.id =11;
        m_HeroData11.name = "因幡帝";
        m_HeroData11.spriteName = "11";
        m_HeroData11.starLv = 1;

        m_HeroData11.name = "泄矢诹访子";
        m_HeroData11.spriteName = "10";
        m_HeroData11.starLv = 2;

      
        heroDataDic.Add(m_HeroData11.id, m_HeroData11);

       
    }
}

