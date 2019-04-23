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
                return _instance;
                _instance.InitSkillData();
            }
            return _instance;
        }
    }

    private Dictionary<int, Skill> SkillDic = new Dictionary<int, Skill>();

    /// <summary>
    /// 妖怪学
    /// </summary>
    private Skill MonsterTheory;
    /// <summary>
    /// 博物学
    /// </summary>
    private Skill NaturalThrory;
    /// <summary>
    /// 第三只眼
    /// </summary>
    private Skill ThridEyes;
    /// <summary>
    /// 安抚
    /// </summary>
    private Skill Placate;
    /// <summary>
    /// 威胁
    /// </summary>
    private Skill Threaten;
    /// <summary>
    /// 聆听
    /// </summary>
    private Skill Listen;
    /// <summary>
    /// 侦查
    /// </summary>
    private Skill Investigate;
    /// <summary>
    /// 话术
    /// </summary>
    private Skill Speechcraft;


    private void InitSkillData()
    {
        string monster_Des = "";
        MonsterTheory = new global::Skill(0, "MonsterTheory", monster_Des,"MonsterTheory");
        string natural_Des = "";
        NaturalThrory = new global::Skill(1, "NaturalThrory", natural_Des, "NaturalThrory");
        string thridEyes_Des = "";
        ThridEyes = new Skill(2, "ThridEyes", thridEyes_Des, "ThridEyes");
        string placate_Des = "";
        Placate = new Skill(3, "Placate", placate_Des, "Placate");
        string threaten_Des = "";
        Threaten = new Skill(4, "Threaten", threaten_Des, "Threaten");
        string listen_Des = "";
        Listen = new Skill(5, "Listen", listen_Des, "Listen");
        string investigate_Des = "";
        Investigate = new Skill(6, "Investigate", investigate_Des, "Investigate");
        string speechcraft_Des = "";
        Speechcraft = new Skill(7, "Speechcraft", speechcraft_Des, "Speechcraft");

        SkillDic.Add(MonsterTheory.data.ID, MonsterTheory);
        SkillDic.Add(NaturalThrory.data.ID, NaturalThrory);
        SkillDic.Add(ThridEyes.data.ID, ThridEyes);
        SkillDic.Add(Placate.data.ID, Placate);
        SkillDic.Add(Threaten.data.ID, Threaten);
        SkillDic.Add(Listen.data.ID, Listen);
        SkillDic.Add(Investigate.data.ID, Investigate);
        SkillDic.Add(Speechcraft.data.ID, Speechcraft);
    }

    private Skill GetDataByID(int id)
    {
        Skill skill;
        if(!SkillDic.TryGetValue(id,out skill))
        {
            Debug.LogError("skill is null____" + id);
        }
        return skill;
    }

    public void ShowSkill()
    {

    }
    
}

