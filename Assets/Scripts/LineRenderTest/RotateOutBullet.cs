using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOutBullet : MonoBehaviour
{
    [HideInInspector]
    public float radius = 1;
    private GameObject bulletPrefab;
    //[HideInInspector]
    public float angle = 0;
    private GameObject wuEffect;
    [HideInInspector]
    public Vector3 centerPoint;

    public string spriteName;

    private Sprite sprite;



    private void Awake()
    {
        spriteName = "kingStar";
        wuEffect = this.gameObject;
        bulletPrefab = ResourcesManager.Instance.LoadBullet("starZhiXianBullet");
    }

    private void Start()
    {
        sprite = ResourcesManager.Instance.LoadSpriteBullet(spriteName);
        StartCoroutine(InstanteBullet());
    }

    private void Update()
    {
        radius += 1f * Time.deltaTime;
        //if (radius <= 1f)
        //{
        //    Destroy(gameObject);
        //}
    }

    IEnumerator InstanteBullet()
    {
        for (int i = 0; i < 60; i++)
        {
            GameObject go = Instantiate(bulletPrefab);
            Vector3 pos = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad));
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
