using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {

    private float speed;
    private void Start()
    {
        speed = Random.Range(0.1f, 0.5f);
    }
    void Update ()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime, Space.World);
	}
}
