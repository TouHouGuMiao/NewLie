using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBullet : BulletBase
{
    public Vector3 targetVec;
    private float speed=2;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
    
    }

    private void Update()
    {
        transform.Translate(targetVec.normalized * Time.deltaTime*speed, Space.World);

        transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);

    }

}
