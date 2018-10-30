using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBullet : BulletBase
{
    
    public Transform target;
    public float speed;
    /// <summary>
	void Start () {
        
	}
	
	void Update () {
        LockToTarget();
    }
    public void LockToTarget()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        Vector3 vecPos = transform.InverseTransformPoint(target.position);
        float angle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;

        transform.Rotate(new Vector3(0, 0, 1) * angle * Time.deltaTime); 
        transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);

       
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
