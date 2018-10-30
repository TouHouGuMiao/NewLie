using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianPauseBullet : BulletBase
{
    private float speed = 0;
    [HideInInspector]
    public Vector2 targetVec;
    [HideInInspector]
    public float deltaTime=7;
    [HideInInspector]
    public bool isRange=false;
    [HideInInspector]
    public float pauseSpeed=10;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        StartCoroutine(SpeedChange());
        if (isRange)
        {
            targetVec = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }

    IEnumerator SpeedChange()
    {
        yield return new WaitForSeconds(deltaTime);
        speed = pauseSpeed;
 
    }

    void Update()
    {
        transform.Translate(targetVec.normalized * speed * Time.deltaTime, Space.World);
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 30);
    }

}
