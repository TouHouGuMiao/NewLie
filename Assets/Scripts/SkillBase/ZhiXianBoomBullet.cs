using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianBoomBullet : BulletBase
{
    public Vector3 targetPos;
    public float speed=2.5f;
    public float radius=1.0f;


    private void Awake()
    {
        Destroy(this.gameObject, 1);
        this.m_Type = BulletTpye.emptyBullet;
      
    }

    private void Update()
    {
        this.transform.Translate(targetPos * Time.deltaTime * speed);
    }



    private void OnDestroy()
    {
        GameObject effectPrefab = ResourcesManager.Instance.LoadEffect("starBoom");
        GameObject bulletPrefab = ResourcesManager.Instance.LoadBullet("boom_small");

        GameObject effect = Instantiate(effectPrefab);
        effect.transform.position = transform.position;
        float angle = 0;
        for (int i = 0; i < 8; i++)
        {
            angle += 45;
            float point_x = (radius) * Mathf.Cos(angle * Mathf.Deg2Rad) + transform.position.x;
            float point_y = (radius) * Mathf.Sin(angle * Mathf.Deg2Rad) + transform.position.y;
            Vector2 pointVec = new Vector2(point_x, point_y);
            GameObject go = GameObject.Instantiate(bulletPrefab);
            go.transform.position = pointVec;
           
            ZhiXianBullet buttle = go.GetComponent<ZhiXianBullet>();
            buttle.m_Type = BulletBase.BulletTpye.emptyBullet;

            buttle.targetVec = targetPos;
        }
    }
}
