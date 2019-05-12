using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPropBase:MonoBehaviour
{
    public int HP
    {
        get
        {
            return (int)(1.5f* VIT+Stature*0.75f);
        }
    }



    /// <summary>
    /// 体型
    /// </summary>
    public float Stature;
    /// <summary>
    /// 灵力
    /// </summary>
    public float Power;
    /// <summary>
    /// 体质
    /// </summary>
    public float VIT;//体质
    public float Lucky;
    public float IQ;
    public int SkillPoint;
    public int Money;
    /// <summary>
    /// 灵感
    /// </summary>
    public int Idea;
    /// <summary>
    /// 威严
    /// </summary>
    public int WeiYan;

    public float preesure;
}
