using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZhiXianBullet : MonoBehaviour{

    private float speed=6.0f;
    public Vector3 targetVec = Vector3.zero;


    void Start () {
        Transform playerTF = GameObject.FindWithTag("Player").transform;
    
        targetVec = new Vector3(-1, 0, 0);
    
	}
	

	void Update ()
    {
        transform.Translate(targetVec * speed*Time.deltaTime,Space.World);
	}
}
