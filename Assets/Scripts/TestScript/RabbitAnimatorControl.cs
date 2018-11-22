using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimatorControl : MonoBehaviour {
    public Animator m_anim;
    float MoveSpeed = 5;
	// Use this for initialization
	void Start () {
        m_anim = this.GetComponent<Animator>();
    }
    //void Walk() {
    //    m_anim = this.GetComponent<Animator>();
    //    if (Input.GetKey(KeyCode.D)) {
    //        m_anim.SetBool("Walk", true);
    //        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * MoveSpeed);
    //    }
    //}
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
           // m_anim.SetBool("Walk", true);
            // m_anim.SetBool("Idle", false);
            if (transform.rotation.eulerAngles.y != 0) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * MoveSpeed,Space.World);
        }
        else if (Input.GetKey(KeyCode.A)) {

           // m_anim.SetBool("Walk", true);
            //  m_anim.SetBool("Idle", false);
            if (transform.rotation.eulerAngles.y != 180) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * MoveSpeed,Space.World);
        }
    }
}
