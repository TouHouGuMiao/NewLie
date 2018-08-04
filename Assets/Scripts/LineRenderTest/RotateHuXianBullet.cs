using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHuXianBullet : BulletBase {


    private float radius = 3;
    private GameObject bulletPrefab;
    [HideInInspector]
    public float angle = 0;

    [HideInInspector]
    public Vector3 centerPoint;
    [HideInInspector]
    public string spriteName;

    private Sprite sprite;

    private void Awake()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("zhiXianPauseStar");


    }

    private void Start()
    {
        sprite = ResourcesManager.Instance.LoadSpriteBullet(spriteName);
        StartCoroutine(InstanteBullet());

    }

    private void Update()
    {
        radius += 4f * Time.deltaTime;
        if (radius >= 15f)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator InstanteBullet()
    {
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(bulletPrefab);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad)+centerPoint.x, radius * Mathf.Sin(angle * Mathf.Deg2Rad)+centerPoint.y);
            transform.position = new Vector3((radius - 0.1f) * Mathf.Cos(angle * Mathf.Deg2Rad+centerPoint.x) + centerPoint.x, (radius - 0.1f) * Mathf.Sin(angle * Mathf.Deg2Rad)+centerPoint.y);
            go.transform.position = pos;
            ZhiXianPauseBullet zhiXianBullet = go.GetComponent<ZhiXianPauseBullet>();
            zhiXianBullet.targetVec = new Vector2(-radius * Mathf.Cos((angle-7) * Mathf.Deg2Rad), -radius * Mathf.Sin((angle-7) * Mathf.Deg2Rad));
            angle += 6;

            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            render.sprite = sprite;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(InstanteBullet());
    }
}
