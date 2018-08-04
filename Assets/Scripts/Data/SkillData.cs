using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkillType
{
    PuGong = 0,
    skill1 = 1,
    skill2 = 2,
    skill3 = 3,
}

public class Skill
{
    public SkillData data=new SkillData ();
    /// <summary>
    /// 涉及到升级后是否可以使用该技能;
    /// </summary>
    public bool canUse;
    public string animatorName;

    public bool isCold=false;


    public virtual void Init() { }
    public virtual void Show(Transform shotPoint, Transform parent,Transform target =null,int sign = 1) { }
}


public class SkillData
{
    public int ID
    {
        get;set;
    }

    public string Name
    { get; set; }

    public string Des
    {
        get;set;
    }

    public float ColdTime
    {
        get;
        set;
    }
}
