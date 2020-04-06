using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardManager:MonoBehaviour
{
    public static BattleCardManager intance;
    private Dictionary<int, CardBase> cardDic = new Dictionary<int, CardBase>();
    private GameObject player;
    void Awake()
    {
        intance = this;
        intance.Init();
    }

    private List<T> RandomSort<T>(List<T> list)
    {
        var random = new System.Random();
        var newList = new List<T>();
        foreach (var item in list)
        {
            newList.Insert(random.Next(newList.Count), item);
        }
        return newList;
    }
    /// <summary>
    /// 测试用临时数据
    /// </summary>
    private void Init()
    {
        player = GameObject.FindWithTag("Player").gameObject;

        CardBase data0 = new CardBase();
        data0.id = 0;
        data0.name = "灵扎";
        data0.des = "";
        data0.mass = 10;
        data0.power = 6;
        data0.speed = 10;
        data0.count = 600;
        data0.bulletColor = CardBase.BulletColor.red;
        data0.cost = 1;
        data0.CardSpeicalEvent.Add(new EventDelegate(Card0_LingZha));
        cardDic.Add(data0.id, data0);

        CardBase data1 = new CardBase();
        data1.id = 1;
        data1.name = "圆形弹幕.红";
        data1.des = "";
        data1.power = 4;
        data1.mass = 15;
        data1.speed = 15;
        data1.count = 600;
        data1.cost = 1;
        data1.CardSpeicalEvent.Add(new EventDelegate(Card1_SphereBulletRed));
        cardDic.Add(data1.id, data1);

        CardBase data2 = new CardBase();
        data2.id = 2;
        data2.name = "符札编织";
        data2.des = "记录：灵扎，10上限";
        data2.cost = 3;
        cardDic.Add(data2.id, data2);

        CardBase data3 = new CardBase();
        data3.id = 3;
        data3.name = "灵扎.散射";
        data3.des = "一次性射出3张灵扎";
        data3.power = 6;
        data3.mass = 10;
        data3.speed = 15;
        data3.count = 1000;
        data3.cost = 3;
        data3.CardSpeicalEvent.Add(new EventDelegate(Card3_BreakLingZhaBullet_3));
        cardDic.Add(data3.id, data3);


        CardBase data4 = new CardBase();
        data4.id = 4;
        data4.name = "红色爆发";
        data4.des = "当前友方所有颜色为红色的弹幕,Power+2";
        data4.cost = 2;
        cardDic.Add(data4.id, data4);

        CardBase data5 = new CardBase();
        data5.id = 5;
        data5.name = "子母弹.红";
        data5.des = "碰撞后消失。消减：以自身为圆心生成8颗小型红色弹幕。";
        data5.mass = 20;
        data5.speed = 5;
        data5.count = 200;
        data5.power = 6;
        data5.cost = 2;
        data5.CardSpeicalEvent.Add(new EventDelegate(Card5_BoomBulletRed));
        cardDic.Add(data5.id, data5);

        CardBase data6 = new CardBase();
        data6.id = 6;
        data6.name = "红色直击";
        data6.des = "在指定地点制造6颗弹幕，成直线扑向目标。";
        data6.mass = 15;
        data6.speed = 30;
        data6.count = 6;
        data6.cost = 3;
        data6.power = 4;
        cardDic.Add(data6.id, data6);

        CardBase data7 = new CardBase();
        data7.id = 7;
        data7.name = "二重结界";
        data7.des = "在一片区域展开空间结界经过该区域的我方弹幕速度提升，经过该区域的敌方弹幕变轻。";
        data7.cost = 3;
        data7.count = 1;
        data7.speicalCardType = CardBase.SpeicalCardType.layUp;
        data7.CardSpeicalEvent.Add(new EventDelegate(Card7_SecondJieJie));
        cardDic.Add(data7.id, data7);

        CardBase data8 = new CardBase();
        data8.id = 8;
        data8.name = "灵力解放";
        data8.des = "抽取2张牌";
        data8.cost = 3;
        data8.cardUseType = CardBase.CardUseType.roundUse;
        cardDic.Add(data8.id, data8);

        CardBase data9 = new CardBase();
        data9.id = 9;
        data9.name = "结界二重身";
        data9.des = "复制处于二重结界中的所有我方弹幕，复制的弹幕属性为原弹幕的50%。";
        data9.cost = 2;
        data9.speicalCardType = CardBase.SpeicalCardType.atOnce;
        data9.CardSpeicalEvent.Add(new EventDelegate(Card9_SpiecalWolrdCopy));
        cardDic.Add(data9.id, data9);

        CardBase data10 = new CardBase();
        data10.id = 10;
        data10.name = "八方龙杀阵";
        data10.des = "复制场上所有[灵扎]，复制的[灵扎]继承原有属性。所有弹幕移动方向改变为敌人方向。";
        data10.speicalCardType = CardBase.SpeicalCardType.atOnce;
        data10.CardSpeicalEvent.Add(new EventDelegate(Card10_EightVecSpeicalCard));
        cardDic.Add(data10.id, data10);


        data0.LevevUpList.Add(data3);
    }


    public CardBase GetCardBaseDataById(int id)
    {
        CardBase data = null;
        if(!cardDic.TryGetValue(id,out data))
        {
            Debug.LogError("cardData is null");
        }
        return data;
    }


    public List<CardBase> GetCardGround()
    {
        List<CardBase> list = new List<CardBase>();
        foreach (CardBase item in cardDic.Values)
        {
            list.Add(item);
        }
        List<CardBase> tempList = new List<CardBase>();
        tempList = list;
        //for (int i = 0; i < 10; i++)
        //{
        //    tempList = RandomSort(tempList);
        //}
        return tempList;
    }

    #region 符卡效果
    private void Card0_LingZha()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("initBullet");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>();
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }

    private void Card1_SphereBulletRed()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("redSphereBullet");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>();
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }

    private void Card3_BreakLingZhaBullet_3()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("breakInitBullet_3");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>();
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }

   private void Card5_BoomBulletRed()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("RedBoomBullet");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>();
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }

    private void Card7_SecondJieJie()
    {
        PlayerBattleRule.Instance.ReduceTime();
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("SecondJieJie");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        HandCardPanel.tempBullet = go;
    }

    private void Card9_SpiecalWolrdCopy()
    {
        Transform bulletParent = GameObject.FindWithTag("Bullet").transform;
        List<GameObject> goList = new List<GameObject>();
        List<SpeicalCardBase> scbList = new List<SpeicalCardBase>();
        for (int i = 0; i < bulletParent.childCount; i++)
        {
            GameObject go = bulletParent.GetChild(i).gameObject;
            if (go.tag == "SpeicalWorld")
            {
                scbList.Add(go.GetComponent<SpeicalCardBase>());
            }
        }
        for (int i = 0; i < scbList.Count; i++)
        {
            List<GameObject> list = scbList[i].GetBulletInTrigger_AtOnce(LayerMask.NameToLayer("PlayerBullet"));
            for (int j = 0; j <list.Count ; j++)
            {
                goList.Add(list[j]);
            }
        }

        for (int i = 0; i < goList.Count; i++)
        {
            GameObject go = GameObject.Instantiate(goList[i]);
            go.transform.SetParent(bulletParent, false);
        }

    }

    private void Card10_EightVecSpeicalCard()
    {
        GameObject bulletParent = GameObject.FindWithTag("Bullet");
        GameObject prefab = ResourcesManager.Instance.LoadBullet("initBullet");
        int count = bulletParent.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject go = bulletParent.transform.GetChild(i).gameObject;
            CanTouchScreen cs = go.GetComponent<CanTouchScreen>();
            if (go.tag == "LingZha")
            {
                if (cs == null)
                {
                    go.AddComponent<CanTouchScreen>();
                }
                GameObject clone = GameObject.Instantiate(prefab);
                clone.transform.SetParent(bulletParent.transform, false);
                clone.transform.position = go.transform.position;
                BulletBaseComponent cloneBBC = clone.AddComponent<BulletBaseComponent>();
                BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
                cloneBBC.velocity = bbc.velocity;
                cloneBBC.speed = bbc.speed;
                clone.AddComponent<CanTouchScreen>();
                float angle = Mathf.Atan2(cloneBBC.velocity.z, cloneBBC.velocity.x)*Mathf.Rad2Deg;
                angle += 15;
                cloneBBC.velocity = new Vector3(cloneBBC.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, cloneBBC.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
                cloneBBC.SetVelocity();
            }
        }
    }
    #endregion
}
