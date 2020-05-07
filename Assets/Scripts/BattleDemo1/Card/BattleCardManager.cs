using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        data0.name = "灵符.根源";
        data0.des = "";
        data0.mass = 10;
        data0.power = 10;
        data0.speed = 5;     
        data0.count = 400;
        data0.countMax = data0.count;
        data0.isInit = true;
        data0.bulletColor = CardBase.BulletColor.red;
        data0.cost = 1;
        data0.CardSpeicalEvent.Add(new EventDelegate(Card0_LingZha));
        data0.bulletType = CardBase.BulletType.One;
        data0.LoadPrefab();
        cardDic.Add(data0.id, data0);

        CardBase data1 = new CardBase();
        data1.id = 1;
        data1.name = "绮丽灵符.五连";
        data1.des = "";
        data1.power = 20;
        data1.mass = 10;
        data1.speed = 10;
        data1.count = 30;
        data1.countMax = data1.count;
        data1.cost = 500;
        data1.bulletType = CardBase.BulletType.One;
        data1.CardSpeicalEvent.Add(new EventDelegate(Card1_SphereBulletRed));
        data1.LoadPrefab();
        cardDic.Add(data1.id, data1);

        CardBase data2 = new CardBase();
        data2.id = 2;
        data2.name = "灵力附加";
        data2.des = "之后的战斗中，你的弹幕威力上升10%";
        data2.cost = 100;
        data2.CardSpeicalEvent.Add(new EventDelegate(Card2_LingLiAdd));
        data2.LoadPrefab();
        //cardDic.Add(data2.id, data2);

        CardBase data3 = new CardBase();
        data3.id = 3;
        data3.name = "绮丽灵符.散";
        data3.des = "一次性射出3张灵扎";
        data3.power = 15;
        data3.mass = 20;
        data3.speed = 15;
        data3.count = 50;
        data3.countMax = data3.count;
        data3.cost = 150;
        data3.CanDraw = false;
        data3.bulletType = CardBase.BulletType.Three;
        data3.CardSpeicalEvent.Add(new EventDelegate(Card3_BreakLingZhaBullet_3));
        data3.LoadPrefab();
        cardDic.Add(data3.id, data3);


        CardBase data4 = new CardBase();
        data4.id = 4;
        data4.name = "灵符.幻想";
        data4.des = "击落弹幕时，被击落弹幕发生爆炸，爆炸伤害为灵符威力的100%";
        data4.power = 15;
        data4.mass = 20;
        data4.speed = 10;
        data4.count = 50;
        data4.cost = 300;
        data4.countMax = data4.count;
        data4.bulletType = CardBase.BulletType.Three;
        data4.CardSpeicalEvent.Add(new EventDelegate(Card4_LingFuFansty));
        data4.LoadPrefab();
        cardDic.Add(data4.id, data4);

        CardBase data5 = new CardBase();
        data5.id = 5;
        data5.name = "子母弹.红";
        data5.des = "碰撞后消失。消减：以自身为圆心生成8颗小型红色弹幕。";
        data5.mass = 20;
        data5.speed = 5;
        data5.count = 200;
        data5.countMax = data5.count;
        data5.power = 15;
        data5.cost = 2;
        data5.CardSpeicalEvent.Add(new EventDelegate(Card5_BoomBulletRed));
        data5.LoadPrefab();
        cardDic.Add(data5.id, data5);

        CardBase data6 = new CardBase();
        data6.id = 6;
        data6.name = "快速强化";
        data6.des = "当前弹幕威力增加5";
        data6.cost = 30;
        data6.cardUseType = CardBase.CardUseType.roundUse;
        data6.CardSpeicalEvent.Add(new EventDelegate(Card6_QuickAddPower));
        data6.LoadPrefab();
        cardDic.Add(data6.id, data6);
        cardDic.Remove(6);

        CardBase data7 = new CardBase();
        data7.id = 7;
        data7.name = "二重结界";
        data7.des = "在一片区域展开空间结界经过该区域的我方弹幕速度提升，经过该区域的敌方弹幕变轻。";
        data7.cost = 3;
        data7.count = 1;
        data7.speicalCardType = CardBase.SpeicalCardType.layUp;
        data7.CardSpeicalEvent.Add(new EventDelegate(Card7_SecondJieJie));
        data7.LoadPrefab();
        cardDic.Add(data7.id, data7);
        cardDic.Remove(7);

        CardBase data8 = new CardBase();
        data8.id = 8;
        data8.name = "万宝槌的魔力";
        data8.des = "之后的战斗中你的弹幕大小增加20%";
        data8.cost = 150;
        data8.cardUseType = CardBase.CardUseType.roundUse;
        data8.CardSpeicalEvent.Add(new EventDelegate(Card8_WanBaoChui_MoreBig));
        data8.LoadPrefab();
        cardDic.Add(data8.id, data8);
        cardDic.Remove(8);

        CardBase data9 = new CardBase();
        data9.id = 9;
        data9.name = "结界二重身";
        data9.des = "复制处于二重结界中的所有我方弹幕，复制的弹幕属性为原弹幕的50%。";
        data9.cost = 2;
        data9.speicalCardType = CardBase.SpeicalCardType.atOnce;
        data9.CardSpeicalEvent.Add(new EventDelegate(Card9_SpiecalWolrdCopy));
        data9.LoadPrefab();
        cardDic.Add(data9.id, data9);
        cardDic.Remove(9);

        CardBase data10 = new CardBase();
        data10.id = 10;
        data10.name = "八方龙杀阵";
        data10.des = "复制场上所有[灵扎]，复制的[灵扎]继承原有属性。所有弹幕移动方向改变为敌人方向。";
        data10.speicalCardType = CardBase.SpeicalCardType.atOnce;
        data10.CardSpeicalEvent.Add(new EventDelegate(Card10_EightVecSpeicalCard));
        data10.LoadPrefab();
        cardDic.Add(data10.id, data10);
        cardDic.Remove(10);

        CardBase data11 = new CardBase();
        data11.id = 11;
        data11.name = "灵符散华";
        data11.mass = 15;
        data11.speed = 10;
        data11.count = 6;
        data11.cost = 3;
        data11.power = 40;
        data11.des = "";
        data11.speicalCardType = CardBase.SpeicalCardType.many;
        data11.CardSpeicalEvent.Add(new EventDelegate(Card11_LingFuBoomSpeicalCard));
        data11.LoadPrefab();
        cardDic.Add(data11.id, data11);


        CardBase data12 = new CardBase();
        data12.id = 12;
        data12.name = "效率转化";
        data12.des = "灵符库的射击效率提高20%";
        data12.CardSpeicalEvent.Add(new EventDelegate(Card12_LingFuSpeedCard));
        data12.cardUseType = CardBase.CardUseType.roundUse;
        data12.cost = 200;
        data12.LoadPrefab();
        cardDic.Add(data12.id, data12);


        CardBase data13 = new CardBase();
        data13.id = 13;
        data13.name = "绮丽灵符.集";
        data13.mass = 30;
        data13.speed = 15;
        data13.count = 40;
        data13.countMax = data13.count;
        data13.cost = 150;
        data13.power = 40;
        data13.CanDraw = false;
        data13.des = "";
        data13.speicalCardType = CardBase.SpeicalCardType.bullet;
        data13.CardSpeicalEvent.Add(new EventDelegate(Card13_LingFuJi));
        data13.LoadPrefab();
        cardDic.Add(data13.id, data13);


        //CardBase data16 = new CardBase();
        //data16.id = 16;
        //data16.name = "勇仪王的赏识";
        //data16.des = "随机使手牌中一张的[单发]符卡的总数提升40%,并且提高其30%的质量。";
        //data16.cost = 300;
        //data16.cardUseType = CardBase.CardUseType.roundUse;
        //data16.CardSpeicalEvent.Add(new EventDelegate(Card16_YongYiOfShangShi));
        //data16.LoadPrefab();
        //cardDic.Add(data16.id,data16);

        //CardBase data17 = new CardBase();
        //data17.id = 17;
        //data17.name = "华狭间的战场";
        //data17.des = "手牌中所有[单发]符卡获得坚毅D，并且质量提高10，伤害提高5，速度提高5。将两张[鬼的决意]加入牌库。";
        //data17.cost = 300;
        //data17.cardUseType = CardBase.CardUseType.roundUse;
        //data17.LoadPrefab();
        //data17.CardSpeicalEvent.Add(new EventDelegate(Card17_HuaXiaJian));
        //cardDic.Add(data17.id, data17);


        //CardBase data18 = new CardBase();
        //data18.id = 18;
        //data18.name = "鬼的决意";
        //data18.des = "所有符卡的坚毅等级上升。";
        //data18.cost = 50;
        //data18.cardUseType = CardBase.CardUseType.roundUse;
        //data18.LoadPrefab();
        //data18.CanDraw = false;
        //data18.CardSpeicalEvent.Add(new EventDelegate(Card18_GuiOfJueYi));
        //cardDic.Add(data18.id, data18);

        //CardBase data19 = new CardBase();
        //data19.id = 19;
        //data19.name = "起风了";
        //data19.des = "随机使手牌中的两张[单发]符卡获得，吹飞D。并且将3张【诹访神的祝福】洗入牌库。";
        //data19.cost = 200;
        //data19.cardUseType = CardBase.CardUseType.roundUse;
        //data19.LoadPrefab();
        //data19.CardSpeicalEvent.Add(new EventDelegate(Card19_WindFly));
        //cardDic.Add(data19.id, data19);

        //CardBase data20 = new CardBase();
        //data20.id = 20;
        //data20.name = "诹访神的祝福";
        //data20.des = "所有符卡的吹飞等级上升。";
        //data20.cost = 50;
        //data20.CanDraw = false;
        //data20.cardUseType = CardBase.CardUseType.roundUse;
        //data20.LoadPrefab();
        //data20.CardSpeicalEvent.Add(new EventDelegate(Card20_WindUp));
        //cardDic.Add(data20.id, data20);

        //CardBase data21 = new CardBase();
        //data21.id = 21;
        //data21.name = "月战后遗症";
        //data21.des = "手牌中所有[三连发]符卡获得30%速度提升，以及基础伤害增加5。但质量下降10%。";
        //data21.cost = 150;
        //data21.cardUseType = CardBase.CardUseType.roundUse;
        //data21.LoadPrefab();
        //data21.CardSpeicalEvent.Add(new EventDelegate(Card21_MoonHouYiZhen));
        //cardDic.Add(data21.id, data21);

        //CardBase data22 = new CardBase();
        //data22.id = 22;
        //data22.name = "散射强化";
        //data22.des = "随机时手牌中的一张[三连发]或[五连发]提升20%的伤害。";
        //data22.cost = 100;
        //data22.cardUseType = CardBase.CardUseType.roundUse;
        //data22.LoadPrefab();
        //data22.CardSpeicalEvent.Add(new EventDelegate(Card22_SanShePowerUp));
        //cardDic.Add(data22.id, data22);


        //CardBase data23 = new CardBase();
        //data23.id = 23;
        //data23.name = "流星驱动器";
        //data23.des = "从牌库中抽取[金平糖],手牌中的[散华]类符卡质量提升10,伤害提升10";
        //data23.cost = 100;
        //data23.cardUseType = CardBase.CardUseType.roundUse;
        //data23.LoadPrefab();
        ////cardDic.Add(data23.id, data23);

        //CardBase data24 = new CardBase();
        //data24.id = 24;
        //data24.name = "浓缩魔法炸弹";
        //data24.des = "随机使手牌中的一张[散华]符卡获得[爆炸]。";
        //data24.cost = 100;
        //data24.cardUseType = CardBase.CardUseType.roundUse;
        //data24.LoadPrefab();
        ////cardDic.Add(data24.id, data24);


        //CardBase data25 = new CardBase();
        //data25.id = 25;
        //data25.name = "天邪鬼的作弊道具";
        //data25.des = "从手牌中选择一张[单发]符卡，保留其[单发]的强化效果，并将其升级为[三连发]";
        //data25.cost = 100;
        //data25.cardUseType = CardBase.CardUseType.roundUse;
        //data25.LoadPrefab();
        ////cardDic.Add(data25.id, data25);

        //CardBase data26 = new CardBase();
        //data26.id = 26;
        //data26.name = "Yahoooooo~!";
        //data26.des = "随机使手牌中的一张[单发]符卡获得[回弹]。";
        //data26.cost = 100;
        //data26.cardUseType = CardBase.CardUseType.roundUse;
        //data26.LoadPrefab();
        ////cardDic.Add(data26.id, data26);

        //CardBase data27 = new CardBase();
        //data27.id = 27;
        //data27.name = "星之器";
        //data27.des = "提升星星弹幕30%的威力。";
        //data27.cost = 100;
        //data27.cardUseType = CardBase.CardUseType.roundUse;
        //data27.LoadPrefab();
        ////cardDic.Add(data27.id, data27);

        //data0.LevevUpList.Add(data3);
        //data0.LevevUpList.Add(data13);
    }


    public CardBase GetCardBaseDataById(int id)
    {
        CardBase data = new CardBase();
        CardBase temp = null;
        if(!cardDic.TryGetValue(id,out temp))
        {
            Debug.LogError("cardData is null");
        }
        //data.id = temp.id;
        //data.isInit = temp.isInit;
        //data.bulletColor = temp.bulletColor;
        //data.bulletType = temp.bulletType;
        //data.CanDraw = temp.CanDraw;
        //data.cost = temp.cost;
        //data.des = temp.des;
        //data.name = temp.name;
        //data.mass = temp.mass;
        //data.speed = temp.speed;
        //data.speicalCardType = temp.speicalCardType;
        //data.windRank = temp.windRank;
        //data.CardSpeicalEvent += temp.CardSpeicalEvent;
        //data.speicalCardType = temp.speicalCardType;
        //data.prefab = temp.prefab;
        //data.power = temp.power;
        //data.cardUseType = temp.cardUseType;
        //data.LevevUpList = temp.LevevUpList;
        return temp;
    }


    public List<CardBase> GetCardGround()
    {
        List<CardBase> list = new List<CardBase>();
        list.Add(GetCardBaseDataById(0));
        list.Add(GetCardBaseDataById(0));
        list.Add(GetCardBaseDataById(1));
        list.Add(GetCardBaseDataById(5));
        list.Add(GetCardBaseDataById(4));
        list.Add(GetCardBaseDataById(11));
        list.Add(GetCardBaseDataById(12));
        return list;
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

    private void MoveToLingFuKu()
    {

        RoundRule.Instance.AddLingFuToKu(BulletBaseComponent.current.cardBase);
    }




    private void Card1_SphereBulletRed()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("initBullet");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>().OnBulletDead.Add(new EventDelegate(MoveToLingFuKu));
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        go.AddComponent<FiveFire>();
        HandCardPanel.tempBullet = go;
      
    }
    private void Card2_LingLiAdd()
    {
        BattlePropManager.instance.powerValue += 0.1f;
    }


    private void Card3_BreakLingZhaBullet_3()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("breakInitBullet_3");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>().OnBulletDead.Add(new EventDelegate(MoveToLingFuKu));
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }

    private void Card4_LingFuFansty()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("initBullet");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>().OnBulletDead.Add(new EventDelegate(MoveToLingFuKu));
        go.AddComponent<ChainBullet>();
        go.GetComponent<ChainBullet>().useChain=true; 
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

    private void Card6_QuickAddPower()
    {
        List<CardBase> tempList = HandCardPanel.handList;
        for (int i = 0; i < tempList.Count; i++)
        {
            if (tempList[i].speicalCardType != CardBase.SpeicalCardType.layUp && tempList[i].speicalCardType != CardBase.SpeicalCardType.atOnce)
            {
                BattlePropManager.instance.ChangeBattleCard_Power(tempList[i], 5);
            }
        }
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

    private void Card8_WanBaoChui_MoreBig()
    {
        BattlePropManager.instance.scale += 0.3f;
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

    private void Card11_LingFuBoomSpeicalCard()
    {
        Transform bulletParent = GameObject.FindWithTag("Bullet").transform;
        GameObject prefab = ResourcesManager.Instance.LoadBullet("BoomLingZha");
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(bulletParent, false);
        BoomLingFu boom = go.GetComponent<BoomLingFu>();
        boom.boomVec = worldPos;
    }

    private void Card12_LingFuSpeedCard()
    {
        RoundRule.Instance.ChangeLingFuKuXiaoLv_Per(0.2f);
    }

    private void Card13_LingFuJi()
    {
        Vector3 worldPos = player.transform.Find("firePoint").transform.position;
        GameObject prefab = ResourcesManager.Instance.LoadBullet("initBullet");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;



        BulletBaseComponent bbc = go.AddComponent<BulletBaseComponent>();
        bbc.cardBase = GetCardBaseDataById(13);
        Rigidbody rgb = go.GetComponent<Rigidbody>();
        rgb.velocity = new Vector3(bbc.cardBase.speed , 0, 0);
        rgb.mass = bbc.cardBase.mass;
        bbc.speed = bbc.cardBase.speed;
        bbc.velocity = new Vector3(bbc.cardBase.speed , 0, 0);
        bbc.power = bbc.cardBase.power;
        //PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
    }

    private void Card14_LingFu_Big()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("breakInitBullet_3_Big");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>();
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }

    private void Card15_LingFu_NotMove()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
        GameObject prefab = ResourcesManager.Instance.LoadBullet("initBulletNotMove");
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
        go.transform.position = worldPos;
        go.AddComponent<BulletBaseComponent>();
        go.AddComponent<Strong>();
        PlayerBattleRule.Instance.ShowSkillArrowHead(worldPos);
        HandCardPanel.tempBullet = go;
    }
    System.Random listRandom = new System.Random();
    private void Card16_YongYiOfShangShi()
    {
        List<CardBase> list = HandCardPanel.GetOneShotCardList();
     
        int index = listRandom.Next(list.Count);
        CardBase data = list[index];
        BattlePropManager.instance.ChangeBattleCard_MassPercentage(data, 0.3f);
        BattlePropManager.instance.ChangeBattleCard_CountPercentage(data, 0.4f);
        HandCardPanel.CardJump(data.id);
    }

    private void Card17_HuaXiaJian()
    {
        List<CardBase> list = HandCardPanel.GetOneShotCardList();
        for (int i = 0; i < list.Count; i++)
        {
            list[i].storyRank = StoryRank.D;
            BattlePropManager.instance.ChangeBattleCard_Speed(list[i], 5);
            BattlePropManager.instance.ChangeBattleCard_Mass(list[i], 10);
            BattlePropManager.instance.ChangeBattleCard_Power(list[i], 5);
            HandCardPanel.CardJump(list[i].id);
        }
        for (int i = 0; i < 2; i++)
        {
            HandCardPanel.AddDataToGroundList(18);
        }
    }

    private void Card18_GuiOfJueYi()
    {

        List<CardBase> list  = HandCardPanel.GetStrongCardBaseListFromHandAndGround();
        for (int i = 0; i < list.Count; i++)
        {
            StrongUp(list[i]);
        }
    }

    private void Card19_WindFly()
    {
        List<CardBase> list = HandCardPanel.GetOneShotCardList();

        int index = listRandom.Next(list.Count);
        CardBase data = list[index];
        data.windRank = WindRank.D;
    }

    private void Card20_WindUp()
    {

        List<CardBase> list= HandCardPanel.GetStrongCardBaseListFromHandAndGround();
        for (int i = 0; i < list.Count; i++)
        {
            WindUp(list[i]);
        }
    }

    private void Card21_MoonHouYiZhen()
    {
        List<CardBase> list = HandCardPanel.GetThreeShotCardList();
        for (int i = 0; i < list.Count; i++)
        {
            BattlePropManager.instance.ChangeBattleCard_Power(list[i],5);
            BattlePropManager.instance.ChangeBattleCard_SpeedPercentage(list[i], 0.3f);
            BattlePropManager.instance.ChangeBattleCard_MassPercentage(list[i], -0.1f);
        }
    }

    private void Card22_SanShePowerUp()
    {
        List<CardBase> tempList = HandCardPanel.GetThreeShotCardList();
        List<CardBase> tempList1 = HandCardPanel.GetFiveShotCardList();
        for (int i = 0; i < tempList1.Count; i++)
        {
            tempList.Add(tempList1[i]);
        }
        int index = listRandom.Next(tempList.Count);
        CardBase data = tempList[index];
        BattlePropManager.instance.ChangeBattleCard_PowerPercentage(data, 0.2f);
    }

    #endregion
    public void WindUp(CardBase data)
    {
        if (data.windRank == WindRank.D)
        {
            data.windRank = WindRank.C;
        }
        else if (data.windRank == WindRank.C)
        {
            data.windRank = WindRank.B;
        }
        else if (data.windRank == WindRank.B)
        {
            data.windRank = WindRank.A;
        }
    }


    public void StrongUp(CardBase data)
    {
        if (data.storyRank == StoryRank.D)
        {
            data.storyRank = StoryRank.C;
        }
        else if(data.storyRank == StoryRank.C)
        {
            data.storyRank = StoryRank.B;
        }
        else if (data.storyRank == StoryRank.B)
        {
            data.storyRank = StoryRank.A;
        }  
    }
}
