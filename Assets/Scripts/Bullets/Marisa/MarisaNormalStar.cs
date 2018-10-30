using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaNormalStar
{
    private GameObject prefab;

    public void InitSkill()
    {
        prefab = ResourcesManager.Instance.LoadBullet("StarBullet");
    }

    public void ShowSkill(Transform target,Transform point)
    {
        IEnumeratorManager.Instance.StartCoroutine(ShowSkillEnumtor(target,point));
    }

    IEnumerator ShowSkillEnumtor(Transform target,Transform point)
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.position = point.position;
            StarBullet starBullet = go.GetComponent<StarBullet>();
            //starBullet.target = target;
            starBullet.m_Type = BulletBase.BulletTpye.emptyBullet;
            starBullet.HP = 50;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
