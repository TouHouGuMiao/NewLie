using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoishiPuGong:Skill
{
    public SkillType m_type = SkillType.PuGong;
    private GameObject sinGo;
    private GameObject cosGo;
    public float injured = 0.01f;

    public override void Init()
    {
        animatorName = "PuGong";
        canUse = true;
        data.ID = 0;
        data.Name = "普通攻击";
        data.ColdTime = 0;
        data.Des = "";
        sinGo = ResourcesManager.Instance.LoadBullet("sin");
        cosGo = ResourcesManager.Instance.LoadBullet("cos");
    }

    public override void Show(Transform shotPoint, Transform parent, Transform target = null, int sign = 1)
    {
        GameObject m_sinGo = GameObject.Instantiate(sinGo) as GameObject;
        SinBullet m_sinBullet = m_sinGo.GetComponent<SinBullet>();
        m_sinBullet.m_Sign = sign;
        m_sinBullet.injured = injured;
        m_sinBullet.HP = 0.5f;


        GameObject m_CosGo = GameObject.Instantiate(cosGo) as GameObject;
        CosBullet m_CosBullet = m_CosGo.GetComponent<CosBullet>();
        m_CosBullet.m_Sign = sign;
        m_CosBullet.injured = injured;
        m_CosBullet.HP = 0.5f;

        m_sinGo.transform.SetParent(parent);
        m_CosGo.transform.SetParent(parent);

        m_sinGo.transform.position = shotPoint.position;
        m_CosGo.transform.position = shotPoint.position;

    }



}
