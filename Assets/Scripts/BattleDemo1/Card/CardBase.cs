using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase 
{
    public enum CardUseType
    {
        roundUse,
        battleUse,
    }
    public enum SpeicalCardType
    {
        bullet,
        layUp,
        atOnce,
        many,
    }

    public enum BulletType
    {
        One,
        Three,
        Five,
        SanHua,
        NotBullet,
    }

    public bool CanDraw = true;
    public bool isInit = false;
    public enum BulletColor
    {
        red,
        yellow,
        blue,
        no,
    }

    public enum rank 
    {
        N,
        R,
        SR,
        SSR,
    }

  



    public int id;
    public string name;
    public string des;
    public GameObject prefab;
    public float speed;
    public float mass;
    public float power;
    public int count;
    public int countMax;
    public BulletColor bulletColor;
    public CardUseType cardUseType = CardUseType.battleUse;
    public int cost;
    public float scale = 1;
    public List<EventDelegate> CardSpeicalEvent=new List<EventDelegate> ();
    public SpeicalCardType speicalCardType = SpeicalCardType.bullet;
    public List<CardBase> LevevUpList = new List<CardBase>();
    public BulletType bulletType = BulletType.NotBullet;
    public StoryRank storyRank = StoryRank.No;
    public WindRank windRank = WindRank.No;

    public void LoadPrefab()
    {
        prefab = ResourcesManager.Instance.LoadBattleCard(id.ToString());
    }
}
