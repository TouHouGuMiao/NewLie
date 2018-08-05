using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLockRotateBullet : BulletBase {

    private float speed=20;
    public Transform target;
    private bool isRotateOver=false;
    private GameObject boomStar;
    [HideInInspector]
    public float rotateZ;
    private Vector3 LockVec;

    

    private void Awake()
    {
        boomStar = ResourcesManager.Instance.LoadParticleBullet("StarBoomParticle");
    }
    // Use this for initialization
    void Start()
    {
        LockVec = new Vector3(target.position.x, target.position.y, 0);
        transform.eulerAngles = new Vector3(0, 0, rotateZ);
    }

    // Update is called once per frame
    void Update()
    {
        LockToTarget();
        if (isRotateOver)
        {
            Destroy(gameObject);
        }
    }

    public void LockToTarget()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        Vector3 vecPos = transform.InverseTransformPoint(LockVec);
        float angle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;
        if (Vector2.Distance(transform.position, LockVec) <= 1.5f)
        {
            isRotateOver = true;
        }

        if (!isRotateOver)
        {
            transform.Rotate(new Vector3(0, 0, 1) * angle * Time.deltaTime * 7);
        }

        transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(boomStar);
        go.transform.position = this.transform.position;
        ParitcleReturn paritcle = go.GetComponent<ParitcleReturn>();
        paritcle.rotateZ = rotateZ;
        paritcle.target = target;
    }
}
