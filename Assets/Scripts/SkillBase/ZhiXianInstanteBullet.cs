using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianInstanteBullet : BulletBase
{
    public bool isInstate=false;

    public Vector3 movetVec;

    public Vector3 targetVec;

    public bool isRange;

    private float speed=20;

    private GameObject bulletPrefab;

    protected override void Awake()
    {
        explosionEffect = ResourcesManager.Instance.LoadEffect("explosionEffect");
        Destroy(transform.gameObject, 5);
        bulletPrefab = ResourcesManager.Instance.LoadBullet("zhiXianPauseStar");
    }

    private void Start()
    {
        if (isInstate)
        {
            StartCoroutine(InstateBullet());
        }
    }
    private void Update()
    {
        transform.Translate(movetVec.normalized * Time.deltaTime * speed,Space.World);
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 360);
    }

    IEnumerator InstateBullet()
    {
        GameObject go = Instantiate(bulletPrefab);
        go.transform.position = transform.position;
        ZhiXianPauseBullet pauseBullet = go.GetComponent<ZhiXianPauseBullet>();
        pauseBullet.targetVec = go.transform.position - targetVec;
        pauseBullet.isRange = isRange;
        pauseBullet.pauseSpeed = Random.Range(2, 10);
        pauseBullet.injured = 30;
        pauseBullet.HP = 30;
        pauseBullet.m_Type = BulletTpye.emptyBullet;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(InstateBullet());
    }
}
