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
                //return _instance;
                _instance = new SkillManager();
            }
            return _instance;
        }
    }
    //  private SkillData data=new SkillData();
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
        m_SkillDataDic.Add(data2.ID, s2);

        SkillData data3 = new SkillData();
        data3.ID = 3;
        data3.Name = "医药学";
        data3.Des = "治病";
        data3.ModelName = "name";
        data3.SkillPoints = 0;
        Skill s3 = new Skill(data3.ID, data3.Name, data3.Des, data3.ModelName);
        m_SkillDataDic.Add(data3.ID, s3);

        SkillData data4 = new SkillData();
        data4.ID = 4;
        data4.Name = "法律";
        data4.Des = "精通幻想乡的法律";
        data4.ModelName = "name";
        data4.SkillPoints = 0;
        Skill s4 = new Skill(data4.ID, data4.Name, data4.Des, data4.ModelName);
        m_SkillDataDic.Add(data4.ID, s4);

        SkillData data5 = new SkillData();
        data5.ID = 5;
        data5.Name = "博物学";
        data5.Des = "随着等级的提升可以增加对新事物的了解程度";
        data5.ModelName = "name";
        data5.SkillPoints = 0;
        Skill s5 = new Skill(data5.ID, data5.Name, data5.Des, data5.ModelName);
        m_SkillDataDic.Add(data5.ID, s5);

        SkillData data6 = new SkillData();
        data6.ID = 6;
        data6.Name = "妖怪学";
        data6.Des = "对妖怪的了解程度";
        data6.ModelName = "name";
        data6.SkillPoints = 0;
        Skill s6 = new Skill(data6.ID, data6.Name, data6.Des, data6.ModelName);
        m_SkillDataDic.Add(data6.ID, s6);

        SkillData data7 = new SkillData();
        data7.ID = 7;
        data7.Name = "图书馆使用";
        data7.Des = "对幻想乡的常识和为少数人知道的知识的掌握";
        data7.ModelName = "name";
        data7.SkillPoints = 0;
        Skill s7 = new Skill(data7.ID, data7.Name, data7.Des, data7.ModelName);
        m_SkillDataDic.Add(data7.ID, s7);

        SkillData data8 = new SkillData();
        data8.ID = 8;
        data8.Name = "跟踪";
        data8.Des = "某些特殊时候会起到意想不到的作用";
        data8.ModelName = "name";
        data8.SkillPoints = 0;
        Skill s8 = new Skill(data8.ID, data8.Name, data8.Des, data8.ModelName);
        m_SkillDataDic.Add(data8.ID, s8);

        SkillData data9 = new SkillData();
        data9.ID = 9;
        data9.Name = "急救";
        data9.Des = "在关键的时候能使自己免于失血过多死亡";
        data9.ModelName = "name";
        data9.SkillPoints = 0;
        Skill s9 = new Skill(data9.ID, data9.Name, data9.Des, data9.ModelName);
        m_SkillDataDic.Add(data9.ID, s9);

        SkillData data10 = new SkillData();
        data10.ID = 10;
        data10.Name = "心理学";
        data10.Des = "与人交谈时有一定几率洞穿对话者的真实意图";
        data10.ModelName = "name";
        data10.SkillPoints = 0;
        Skill s10 = new Skill(data10.ID, data10.Name, data10.Des, data10.ModelName);
        m_SkillDataDic.Add(data10.ID, s10);
    }
    public Dictionary<int,Skill> GetSkillDataInDic() {
        Dictionary<int,Skill> SkillDataDic = new Dictionary<int, Skill>();
        InitSkillData();
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
}

