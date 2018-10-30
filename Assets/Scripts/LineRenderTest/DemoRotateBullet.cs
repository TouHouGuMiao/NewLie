using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRotateBullet : BulletBase
{
    protected override void Awake()
    {
        
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 180);
    }


    private void OnDestroy()
    {
        
    }
}
