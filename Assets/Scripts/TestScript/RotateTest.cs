using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour {
    public GameObject go;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        go.transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);
	}
}
