using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOutBullet : MonoBehaviour
{
    [HideInInspector]
    private float radius = 3;
    private GameObject bulletPrefab;
    //[HideInInspector]
    public float angle = 0;
    private GameObject wuEffect;
    [HideInInspector]
    public Vector3 centerPoint;
    [HideInInspector]
    public Sprite sprite;



    private void Awake()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("starZhiXianBullet");
    }

    private void Start()
    {
        StartCoroutine(InstanteBullet());
    }

    private void Update()
    {
        radius += 1f * Time.deltaTime;
        if (radius >= 12f)
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
            transform.position = new Vector3((radius - 0.1f) * Mathf.Cos(angle * Mathf.Deg2Rad) + centerPoint.x, (radius - 0.1f) * Mathf.Sin(angle * Mathf.Deg2Rad));
            go.transform.position = pos;
            ZhiXianBullet zhiXianBullet = go.GetComponent<ZhiXianBullet>();
            zhiXianBullet.targetVec = new Vector2(-radius * Mathf.Cos((angle - 30) * Mathf.Deg2Rad), -radius * Mathf.Sin((angle - 30) * Mathf.Deg2Rad));
            angle += 6;

            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            render.sprite = sprite;
            yield return new WaitForSeconds(0.02f);
        }
        StartCoroutine(InstanteBullet());
    }
}
