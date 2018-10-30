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
            transform.Rotate(new Vector3(0, 0, 1) * angle * Time.deltaTime*30);
        }




        transform.Translate(new Vector2(1, 0) * Time.deltaTime *speed); 
    }

    protected override void OnTriggerEnter(Collider other)
    {
        BulletBase m_base = other.transform.GetComponent<BulletBase>();
        if (m_base == null)
        {

            return;
        }

        if (m_base.m_Type != this.m_Type)
        {
            this.HP -= m_base.injured;
            Destroy(gameObject);

        }

        if (this.HP <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }

    }



}

