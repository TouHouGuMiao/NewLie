using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoishiSkillOne: Skill
{
    public SkillType m_tpye = SkillType.skill1;
    private GameObject lockBullet;
    public float injured=0.02f;

    public override void Init()
    {
        animatorName = "skill1";
        canUse = true;
        data.ID = 1;
        data.Name = "普通攻击";
        data.ColdTime = 5;
        data.Des = "";
        lockBullet = ResourcesManager.Instance.LoadBullet("lock")as GameObject;
    }

    public override void Show(Transform shotPoint, Transform parent, Transform target = null, int sign = 1)
    {
        float angle = -135;  
        for (int i = 0; i < 7; i++)
        {
            GameObject go = GameObject.Instantiate(lockBullet);
            LockBullet bullet = go.GetComponent<LockBullet>();
            bullet.injured = injured;
            bullet.HP = 0.2f;
            bullet.target = target;
            go.transform.SetParent(parent, false);
            go.transform.position = shotPoint.position;
            go.transform.eulerAngles = new Vector3(0, 0, angle);
            angle += 45;
        }
    }
}
