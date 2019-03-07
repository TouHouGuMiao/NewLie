using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
