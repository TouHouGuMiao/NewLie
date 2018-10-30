using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHuXianBullet : BulletBase {


    private float radius = 3;
    private GameObject bulletPrefab;
    [HideInInspector]
    public float angle = 0;

    [HideInInspector]
    public Transform centerTF;
    [HideInInspector]
    public string spriteName;

    private Vector3 startPos;

    private Sprite sprite;

    protected override void Awake()
    {

        base.Awake();
        bulletPrefab = ResourcesManager.Instance.LoadBullet("zhiXianPauseStar");


    }

    private void Start()
    {
        sprite = ResourcesManager.Instance.LoadSpriteBullet(spriteName);
        startPos = centerTF.position;
        StartCoroutine(InstanteBullet());


    }

    private void Update()
    {
        radius += 4f * Time.deltaTime;
        if (radius >= 15f)
        {
            Destroy(gameObject);
        }
        this.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 120);
    }

    IEnumerator InstanteBullet()
    {
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(bulletPrefab);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad)+ startPos.x, radius * Mathf.Sin(angle * Mathf.Deg2Rad)+ startPos.y);
            transform.position = new Vector3((radius - 0.1f) * Mathf.Cos(angle * Mathf.Deg2Rad) + startPos.x, (radius - 0.1f) * Mathf.Sin(angle * Mathf.Deg2Rad)+ startPos.y);
            go.transform.position = pos;
            ZhiXianPauseBullet zhiXianBullet = go.GetComponent<ZhiXianPauseBullet>();
            zhiXianBullet.targetVec = new Vector2(-radius * Mathf.Cos((angle-7) * Mathf.Deg2Rad), -radius * Mathf.Sin((angle-7) * Mathf.Deg2Rad));
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
