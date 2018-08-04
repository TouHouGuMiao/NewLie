using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class KoishiSkillManager
{
    private static KoishiSkillManager _instance=null;
    public static KoishiSkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new KoishiSkillManager();
            }
            return _instance;
        }
    }

    private KoishiPuGong puGong;
    private KoishiSkillOne skillOne;
    private float lastTime;
    private List<Skill> SkillList=new List<Skill> ();
    public void Init()
    {
        puGong = new global::KoishiPuGong();
        puGong.Init();
        SkillList.Add(puGong);

        skillOne = new global::KoishiSkillOne();
        skillOne.Init();
        SkillList.Add(skillOne);
    }


    //public string GetUseSkillAnmimatorName()
    //{
    //    if (!skillOne.isCold)
    //    {
    //        return skillOne.animatorName;
    //    }

    //    return puGong.animatorName;
    //}

    //public void ShowSkill(Transform shotPoint, Transform parent,string name,int sign = 1,Transform target = null)
    //{
    //    for (int i = 0; i < SkillList.Count; i++)
    //    {
    //        if (SkillList[i].animatorName == name)
    //        {
    //            SkillList[i].Show(shotPoint, parent, target,sign);
    //            SkillList[i].isCold = true;
    //        }
    //    }
    //}


    public void ShowPuGong(Transform shotPoint, Transform parent, string name, int sign = 1, Transform target = null)
    {
        puGong.Show(shotPoint,parent,target,sign);
    }

    public void ShowSkillOne(Transform shotPoint, Transform parent, string name, int sign = 1, Transform target = null)
    {
        skillOne.Show(shotPoint, parent, target, sign);
        skillOne.isCold = true;
    }


    //int skillOneCout = 0;
    public void SkillColdSet()
    {
        //if (Time.time > lastTime + 1.0f)
        //{
        //    lastTime = Time.time;
        //    if (skillOne.isCold)
        //    {
        //        skillOneCout++;
        //        if (skillOneCout >= skillOne.data.ColdTime)
        //        {
        //            skillOne.isCold = false;
        //            skillOneCout = 0;
        //        }
        //    }

        //}
    }

    public void Updata()
    {
        //SkillColdSet();
    }






}
