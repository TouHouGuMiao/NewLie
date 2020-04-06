using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaSphere
{
    public GameObject bulletPrefab;
    public float radius=2.0f;
    private float angle=0;

    public void InitSkill()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("AroundBullet");
    }

    public void ShowSkill(Vector2 marisaVec)
    {
        angle = 0;
        for (int i = 0; i <20; i++)
        {
            angle += 18.0f;
            float point_x = (radius) * Mathf.Cos(angle*Mathf.Deg2Rad) + marisaVec.x;
            float point_y = (radius) * Mathf.Sin(angle*Mathf.Deg2Rad) + marisaVec.y;
            Vector2 pointVec = new Vector2(point_x, point_y);
            GameObject go = GameObject.Instantiate(bulletPrefab);
            go.transform.position = pointVec;
            Vector2 targetVec = new Vector2(radius * Mathf.Cos((angle+30)* Mathf.Deg2Rad), radius * Mathf.Sin((angle+30)*Mathf.Deg2Rad));
            ZhiXianBullet buttle = go.GetComponent<ZhiXianBullet>();

           
            buttle.targetVec = targetVec;
        }
    }
}
