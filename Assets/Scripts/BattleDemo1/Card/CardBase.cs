using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase 
{
    public CardBase()
    {
        prefab = ResourcesManager.Instance.LoadBattleCard(id.ToString());
    }
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
    }

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
    public BulletColor bulletColor;
    public CardUseType cardUseType = CardUseType.battleUse;
    public int cost;
    public List<EventDelegate> CardSpeicalEvent=new List<EventDelegate> ();
    public SpeicalCardType speicalCardType = SpeicalCardType.bullet;
    public List<CardBase> LevevUpList = new List<CardBase>();
}
