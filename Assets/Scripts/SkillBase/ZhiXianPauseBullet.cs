using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianPauseBullet : BulletBase
{
    private float speed = 0;
    [HideInInspector]
    public Vector2 targetVec;

    private void Awake()
    {

    }
    void Start()
    {
        StartCoroutine(SpeedChange());
    }

    IEnumerator SpeedChange()
    {
        yield return new WaitForSeconds(5);
        speed = 2;
    }

    void Update()
    {
        transform.Translate(targetVec.normalized * speed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 30);
    }
}
