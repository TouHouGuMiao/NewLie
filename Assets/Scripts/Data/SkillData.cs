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
    public SkillData data = new SkillData();
    public Skill(int id,string name,string des,string altasName,string iconName)
    {
        data.Name = name;
        data.ID = id;
        data.Des = des;
        data.IconName = iconName;
    }
    public int point;
    public virtual void Init()
    {

    }
    
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

    public string AltasName
    {
        get;set;
    }

    public string IconName
    {
        get;
        set;
    }
}
