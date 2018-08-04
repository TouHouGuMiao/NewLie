using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianBullet : BulletBase{

    private float speed=6.0f;
    public Vector2 targetVec;

    private void Awake()
    {
        
    }
    void Start () {
		
	}
	

	void Update ()
    {
        transform.Translate(targetVec.normalized*speed*Time.deltaTime,Space.World);
	}
}
