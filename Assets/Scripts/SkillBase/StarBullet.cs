using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet : BulletBase
{

    public Transform target;
    private float speed=15f;
    private Vector2 m_MoveVec;
    private GameObject item;

    //private float speedCenter;
    //private float speedOneGround;
    //private float speedTwoGround;
    //float x = 0;

    private void Awake()
    {
        Destroy(this.gameObject, 10);
    }
    private void Start()
    {
        m_MoveVec = target.position - transform.position;
        //float index_x = transform.position.x - target.position.x;
        //if (index_x > 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 180, 0);
        //}
        m_MoveVec.Normalize();
        Vector3 vecPos = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        StartCoroutine(SpeedUpdata());
      
    }

    private void Update()
    {
        LockToTarget();
    }

    float time=0;
    private IEnumerator SpeedUpdata()
    {
        yield return new WaitForSeconds(1);
        time += 1;
        speed = 15f - 2.0f * time;
        StartCoroutine(SpeedUpdata()); 
    }

   
  

    public void LockToTarget()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
    




        transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
