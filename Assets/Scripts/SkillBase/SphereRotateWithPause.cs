using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotateWithPause : BulletBase
{

    private float radius = 15;
    private GameObject bulletPrefab;
    [HideInInspector]
    public float angle = 0;

    [HideInInspector]
    public Vector3 centerPoint;

    public string spriteName;

    private Sprite sprite;





    protected override void Awake()
    {
        base.Awake();
        bulletPrefab = ResourcesManager.Instance.LoadBullet("zhiXianPauseStar");
    }

    private void Start()
    {
        sprite = ResourcesManager.Instance.LoadSpriteBullet(spriteName);
        StartCoroutine(InstanteBullet());
    }

    private void Update()
    {
        radius -= 4f  * Time.deltaTime;
        if (radius <= 1f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator InstanteBullet()
    {
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(bulletPrefab);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad) + centerPoint.x, radius * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPoint.y);
            transform.position = new Vector3((radius - 0.1f) * Mathf.Cos(angle * Mathf.Deg2Rad) + centerPoint.x, (radius - 0.1f) * Mathf.Sin(angle * Mathf.Deg2Rad) + centerPoint.y);
            go.transform.position = pos;
            ZhiXianPauseBullet zhiXianBullet = go.GetComponent<ZhiXianPauseBullet>();
            zhiXianBullet.targetVec = new Vector2(radius * Mathf.Cos((angle) * Mathf.Deg2Rad), radius * Mathf.Sin((angle) * Mathf.Deg2Rad));
            angle += 6;
            zhiXianBullet.injured = 60;
            zhiXianBullet.HP = 90;
            zhiXianBullet.m_Type = BulletTpye.emptyBullet;
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            render.sprite = sprite;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(InstanteBullet());
    }
}
