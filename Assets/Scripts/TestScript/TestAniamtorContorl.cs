using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAniamtorContorl : MonoBehaviour {
    private Animator m_Animator;
    public float speed;
    private void Awake()
    {
        m_Animator = this.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return;
            }
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 0, this.transform.rotation.eulerAngles.z);
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.loopIdle") || stateInfo.IsName("Base Layer.startIdle"))
            {
                m_Animator.SetBool("isWalk", true);
            }


            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.Self);
            
        }

  


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 180, this.transform.rotation.eulerAngles.z);
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.loopIdle")|| stateInfo.IsName("Base Layer.startIdle"))
            {
                m_Animator.SetBool("isWalk", true);
            }


          
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.Self);
            

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            m_Animator.SetBool("isWalk", false);
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            m_Animator.SetBool("isWalk", false);
        }
    }
}
