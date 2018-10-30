using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaDemoSphere 
{
    private GameObject bulletPrefab;

    private float radius = 30;

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("demoSphereBullet");
    }

    public void ShowSkill(Vector3 marisaVec)
    {
        IEnumeratorManager.Instance.StartCoroutine(ShowSkillIEnumerator(marisaVec));
    }

    IEnumerator ShowSkillIEnumerator(Vector3 centerVec)
    {
        float angle =0;
        for (int i = 0; i < 360; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            go.transform.position = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad) + centerVec.x, radius * Mathf.Sin(angle * Mathf.Deg2Rad) + centerVec.y);
            angle += 1;
            DemoRotateBullet demoBullet = go.GetComponent<DemoRotateBullet>();
            demoBullet.m_Type = BulletBase.BulletTpye.emptyBullet;
            demoBullet.injured = 1000;
            demoBullet.HP = 1000000;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
