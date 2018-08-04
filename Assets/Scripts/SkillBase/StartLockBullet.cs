using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLockBullet : BulletBase
{
    public float speed;
    public Vector3 target;
    private bool isRotateOver;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LockToTarget();
    }

    public void LockToTarget()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        Vector3 vecPos = transform.InverseTransformPoint(target);
        float angle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;
        if (Vector2.Distance(transform.position,target)<=1.0f)
        {
            isRotateOver = true;
        }

        if (!isRotateOver)
        {
            transform.Rotate(new Vector3(0, 0, 1) * angle * Time.deltaTime * speed);
        }




        transform.Translate(new Vector2(1, 0) * Time.deltaTime * 20); 
    }
}

