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
                _instance.InitSkillData_1();
                _instance.InitSkillData();
            }
            return _instance;
        }
    }

    public Skill Cur_Skill
    {
        get
        {
            return SkillUsePanel.curSkill;   
        }
    }

    private Dictionary<int, Skill> m_SkillDataDic=new Dictionary<int, Skill>();
    private void InitSkillData()
    {
       
        SkillData data1 = new SkillData();
        data1.ID = 1;
        data1.Name = "侦察";
        data1.Des = "可以观察附近的地形";
        data1.ModelName = "name";
        data1.SkillPoints = 0;
        Skill s1 = new Skill(data1.ID, data1.Name, data1.Des, data1.ModelName);
        m_SkillDataDic.Add(data1.ID,s1);

        SkillData data2 = new SkillData();
        data2.ID = 2;
        data2.Name = "说服";
        data2.Des = "进行劝说";
        data2.ModelName = "name";
        data2.SkillPoints = 0;
        Skill s2 = new Skill(data2.ID, data2.Name, data2.Des, data2.ModelName);
        m_SkillDataDic.Add(data2.ID,s2);

        SkillData data3 = new SkillData();
        data3.ID = 3;
        data3.Name = "医药学";
        data3.Des = "治病";
        data3.ModelName = "name";
        data3.SkillPoints = 0;
        Skill s3 = new Skill(data3.ID, data3.Name, data3.Des, data3.ModelName);
        m_SkillDataDic.Add(data3.ID,s3);

        SkillData data4 = new SkillData();
        data4.ID = 4;
        data4.Name = "法律";
        data4.Des = "精通幻想乡的法律";
        data4.ModelName = "name";
        data4.SkillPoints = 0;
        Skill s4 = new Skill(data4.ID, data4.Name, data4.Des, data4.ModelName);
        m_SkillDataDic.Add(data4.ID,s4);

        SkillData data5 = new SkillData();
        data5.ID = 5;
        data5.Name = "博物学";
        data5.Des = "随着等级的提升可以增加对新事物的了解程度";
        data5.ModelName = "name";
        data5.SkillPoints = 0;
        Skill s5 = new Skill(data5.ID, data5.Name, data5.Des, data5.ModelName);
        m_SkillDataDic.Add(data5.ID,s5);



    }
    public Dictionary<int,Skill> GetSkillDataInDic() {
        Dictionary<int,Skill> SkillDataDic = new Dictionary<int, Skill>();
        if (m_SkillDataDic == null)
        {
            InitSkillData();
        }
        SkillDataDic = m_SkillDataDic;
        return SkillDataDic;
    }
    public Skill GetSkillDataById(int id) {
        if (m_SkillDataDic == null) {
            InitSkillData();
        }
        Skill skill = null;
        Dictionary<int,Skill> SkillDataDic=new Dictionary<int, Skill>();
        SkillDataDic = m_SkillDataDic;
        if (!SkillDataDic.TryGetValue(id, out skill)) {
            Debug.LogError("no data of "+id+" in DIC");
            return null;
        }
        return skill;
    }
    public void UpDataSkillData(Skill s) {
        if (m_SkillDataDic == null) {
            InitSkillData();
        }
        for (int i = 0; i < m_SkillDataDic.Count; i++) {
            if (s.data.ID == m_SkillDataDic[i+1].data.ID) {
                m_SkillDataDic[i+1] = s;
            }
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

    /// <summary>
    /// 灵感
    /// </summary>
    private Skill Idea;

    private Skill Pressure;
    private Dictionary<int, Skill> SkillDic = new Dictionary<int, Skill>();
    private void InitSkillData_1()
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
        Idea = new Skill(6, "Idea", "", "Idea");       
        Idea.canUse = true;
        Idea.data.SkillPoints = 13;
        Pressure = new Skill(7, "Pressure", "", "Pressure");
        Pressure.canUse = true;
        SkillDic.Add(Placate.data.ID, Placate);
        SkillDic.Add(MonsterTheory.data.ID, MonsterTheory);
        SkillDic.Add(NaturalTheory.data.ID, NaturalTheory);
        SkillDic.Add(ThridEye.data.ID, ThridEye);
        SkillDic.Add(Investigate.data.ID, Investigate);
        SkillDic.Add(Listen.data.ID, Listen);
        SkillDic.Add(Idea.data.ID, Idea);
        SkillDic.Add(Pressure.data.ID, Pressure);
    }

    public Skill GetSkillById(int id)
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

    public void ClearSkillUsePanel()
    {
        SkillUsePanel.UpdataUseSkill(null);
    }

    public void UpdataAndShowSkillUsePanel(Dictionary<int,Dictionary<string, EventDelegate>> SkillIdWithHanderDic,bool mustCheck=false)
    {
        List<Skill> skillList = new List<Skill>();
        GUIManager.ShowView("SkillUsePanel");   
        if (SkillIdWithHanderDic.Count <=0)
        {
            SkillUsePanel.UpdataUseSkill(null);
            return;
        }

        foreach (KeyValuePair<int, Dictionary<string, EventDelegate>> item in SkillIdWithHanderDic)
        {
            Skill skill = null;
            if (!SkillDic.TryGetValue(item.Key, out skill))
            {
                Debug.LogError("not has this skill" + "__" + item.Key);
            }
            if (skill != null)
            {
                skill.TargetWithHanderDic = item.Value;
                skillList.Add(skill);
            }
          
        }
        SkillUsePanel.UpdataUseSkill(skillList,mustCheck);

    }
    
}

