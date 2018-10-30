using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianBullet : BulletBase{

    private float speed=6.0f;
    public Vector3 targetVec = Vector3.zero;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start () {
        Transform playerTF = GameObject.FindWithTag("Player").transform;
        if (playerTF.rotation.eulerAngles.y == 0)
        {
            targetVec = new Vector3(1, 0, 0);
        }

        else
        {
            targetVec = new Vector3(-1, 0, 0);
        }
	}
	

	void Update ()
    {
        transform.Translate(targetVec * speed*Time.deltaTime,Space.World);
	}
}
