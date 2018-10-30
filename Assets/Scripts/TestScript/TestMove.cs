using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    public float speed;
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.rotation.eulerAngles.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            this.transform.Translate(1.0f * Time.deltaTime * speed, 0, 0,Space.World) ;
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (transform.rotation.eulerAngles.y != 180)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            this.transform.Translate(-1.0f * Time.deltaTime * speed, 0,0,Space.World) ;
        }

      
    }
}
