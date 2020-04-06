using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    public float speed;
    public Rigidbody rgb;
    public float rotate;
    private GameObject enmey;
    private bool aroundWithEnemy=false;
    void Start()
    {
        enmey = GameObject.FindWithTag("enemy");
    }
	// Update is called once per frame
	void Update ()
    {
        if (!aroundWithEnemy)
        {
            transform.Rotate(0, -rotate * Time.deltaTime, 0, Space.World);
            float angle = transform.rotation.eulerAngles.y;
            rgb.velocity = new Vector3(speed * Mathf.Cos(-angle * Mathf.Deg2Rad), 0, speed * Mathf.Sin(-angle * Mathf.Deg2Rad));
        }
        else
        {
            transform.RotateAround(enmey.transform.position, new Vector3(0, 1, 0),30*Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "chaoWo")
        {
            if (!aroundWithEnemy)
            {
                Rigidbody rgb = gameObject.GetComponent<Rigidbody>();
                Vector3 tempVec = enmey.transform.position - transform.position;
                float angle = Mathf.Atan2(tempVec.z, tempVec.x)*Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(90, angle, 0);
                rgb.velocity = new Vector3(0, 0, 0);
                aroundWithEnemy = true;
            }          
        }
    }



    

}
