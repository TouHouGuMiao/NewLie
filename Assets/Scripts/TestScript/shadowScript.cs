using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowScript : MonoBehaviour {
    private GameObject player;
    private Animator m_Aninator;
    private float moveSpeed = 3;
    private float speed=3;
	void Start () {
        player = this.gameObject;
        m_Aninator = player.GetComponent<Animator>();
        ForbidComponents();       
    }
	
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Aninator.SetBool("isWalk", true);
            m_Aninator.Play("Walk", 0);
            player.transform.localRotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 0, this.transform.rotation.eulerAngles.z);
            player.GetComponent<SpriteRenderer>().flipX = false;           
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Aninator.SetBool("isWalk", true);
            m_Aninator.Play("Walk", 0);
            player.transform.localRotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 0, this.transform.rotation.eulerAngles.z);
            player.GetComponent<SpriteRenderer>().flipX = true;           
        }
        else
        {
            m_Aninator.SetBool("isWalk", false);           
        }      
    }
    void ForbidComponents()
    {
        if (player.GetComponent<PlayerControl>() != null)
        {
            player.GetComponent<PlayerControl>().enabled = false;
        }
        if (player.GetComponent<Rigidbody>() != null)
        {
            Destroy(player.GetComponent<Rigidbody>());
        }
        if (player.GetComponent<CapsuleCollider>() != null)
        {
            player.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
