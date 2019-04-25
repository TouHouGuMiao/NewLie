using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    private static SkillManager _instance = null;
    public static SkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SkillManager();
                _instance.InitSkillData();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 安抚
    /// </summary>
    private Skill Placate;
    /// <summary>
    /// 妖怪学
    /// </summary>
    private Skill MonsterTheory;
    /// <summary>
    /// 博物学
    /// </summary>
    private Skill NaturalTheory;
    /// <summary>
    /// 第三只眼
    /// </summary>
    private Skill ThridEye;
    /// <summary>
    /// 侦查
    /// </summary>
    private Skill Investigate;
    /// <summary>
    /// 聆听
    /// </summary>
    private Skill Listen;


    private Dictionary<int, Skill> SkillDic = new Dictionary<int, Skill>();
    private void InitSkillData()
    {
        Placate = new Skill(0,"Placate", "","Placate");
        Placate.canUse = true;
        MonsterTheory = new Skill(1, "MonsterTheory", "", "MonsterTheory");
        MonsterTheory.canUse = true;
        NaturalTheory = new Skill(2, "NaturalTheory", "", "NaturalTheory");
        NaturalTheory.canUse = true;
        ThridEye = new Skill(3, "ThridEye", "", "ThridEye");
        ThridEye.canUse = true;
        Investigate = new Skill(4, "Investigate", "", "Investigate");
        Investigate.canUse = true;
        Listen = new Skill(5, "Listen", "", "Listen");
        Listen.canUse = true;

        SkillDic.Add(Placate.data.ID, Placate);
        SkillDic.Add(MonsterTheory.data.ID, MonsterTheory);
        SkillDic.Add(NaturalTheory.data.ID, NaturalTheory);
        SkillDic.Add(ThridEye.data.ID, ThridEye);
        SkillDic.Add(Investigate.data.ID, Investigate);
        SkillDic.Add(Listen.data.ID, Listen);
    }

    private Skill GetSkillById(int id)
    {
        Skill skill = null;
        if(!SkillDic.TryGetValue(id,out skill))
        {
            Debug.LogError("skill is null___"+id);
        }
        return skill; 
    }

    public List<Skill> CanUseSkillList()
    {
        List<Skill> List = new List<Skill>();
        foreach (Skill item in SkillDic.Values)
        {
            if (item.canUse)
            {
                List.Add(item);
            }
        }
        return List;
    }

    public void ShowSkill()
    {

    }
    
}

