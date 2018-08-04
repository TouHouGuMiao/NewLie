using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaBoom
{
    private GameObject bulletPrefab;

    public void InitSkill()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("DaYu_Red");
    }

    public void ShowSkill(Vector2 marisaVec)
    {
        float angle = 45f;
        for (int i = 0; i < 4; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            float point_x = (2.0f) * Mathf.Cos(angle * Mathf.Deg2Rad) + marisaVec.x;
            float point_y = (2.0f) * Mathf.Sin(angle * Mathf.Deg2Rad) + marisaVec.y;
            Vector2 pointVec = new Vector2(point_x, point_y);
            go.transform.position = pointVec;
            Vector2 targetVec = new Vector2(2.0f * Mathf.Cos((angle) * Mathf.Deg2Rad), 2.0f * Mathf.Sin((angle) * Mathf.Deg2Rad));
            ZhiXianBoomBullet buttle = go.GetComponent<ZhiXianBoomBullet>();
            buttle.m_Type = BulletBase.BulletTpye.emptyBullet;

            buttle.targetPos = targetVec;
            angle += 90;
        }
    }
}

