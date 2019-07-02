using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlerp : MonoBehaviour {
    public Transform t1;
    public Transform t2;
    Vector3 v3 = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < 100; i++) {
        //    transform.position=Vector3.Slerp(t1,t2,)
        //    Debug.DrawLine(v3, Vector3.Slerp(t1.position, t2.position, i * 0.1f), Color.red);
        //}
	}
}
