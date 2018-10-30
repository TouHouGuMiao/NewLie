using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 根据一个点，和自己的位置进行判断，会向点相反的方向移动
/// </summary>
public class PointTargetBullet : BulletBase
{
    public enum BulletTarget
    {
        Out,
        In
    }
    [HideInInspector]
    public BulletTarget targetType = BulletTarget.Out;

    public Vector3 targetPoint;
    private Vector3 moveVec;
    [HideInInspector]
    public float speed=0.1f;
    private float rotateSpeed = 30;

    protected override void Awake()
    {
        explosionEffect = ResourcesManager.Instance.LoadEffect("explosionEffect");
        Destroy(gameObject, 20);
        StartCoroutine(TimeDepent());
   
    }

    private void Start()
    {
        if (targetType == BulletTarget.Out)
        {
            moveVec = transform.position - targetPoint;
        }

        else
        {
            moveVec = targetPoint-transform.position ;
        }
    }
    private void Update()
    {
        transform.Translate(moveVec.normalized * Time.deltaTime * speed, Space.World);
        if (time > 13)
        {
            speed = 5.0f;
            rotateSpeed = 360;
        }
        transform.transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
    }

    private int time = 0;
    IEnumerator TimeDepent()
    {
        yield return new WaitForSeconds(1);
        time++;
        StartCoroutine(TimeDepent());
    }

}
