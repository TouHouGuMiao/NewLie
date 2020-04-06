using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTouchScreen : MonoBehaviour {

	public float angle;
    private GameObject up;
    private GameObject down;
    private GameObject left;
    private GameObject right;
    private BulletBaseComponent bbc;
	public ScreenCheckManager.TouchState touchState
	{
		get;
		set;
	}
	void Start () {
        GameObject xiangKuang = GameObject.FindWithTag("XiangKuang");
        up = xiangKuang.transform.Find("Up").gameObject;
        down = xiangKuang.transform.Find("Down").gameObject;
        left = xiangKuang.transform.Find("Left").gameObject;
        right = xiangKuang.transform.Find("Right").gameObject;
      

    }
	
	// Update is called once per frame
	void Update () {
		//if (!this.GetComponent<BulletBaseComponent>().isDead)
		//{
		//	ScreenCheckManager.Instance.ObjectCheckTouchScreen(gameObject);
		//}
	}


    void OnTriggerEnter(Collider collider)
    {
        bbc = this.GetComponent<BulletBaseComponent>();
        if (bbc.isDead)
        {

            return;
        }
        if (collider.gameObject == down)
        {
            BeBound(gameObject, true);
        }

        else if(collider.gameObject == up)
        {
            BeBound(gameObject, true);
        }

        else if (collider.gameObject == left)
        {
            BeBound(gameObject, false);
        }

        else if (collider.gameObject == right)
        {
            BeBound(gameObject, false);
        }
    }

    private void BeBound(GameObject go, bool isHorizontal)
    {
        float vecZ = go.transform.rotation.eulerAngles.y;
        if (vecZ > 180)
        {
            vecZ = vecZ - 360;
        }
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (isHorizontal)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, -rb.velocity.z);
            if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
            {
                BulletBaseComponent cts = go.GetComponent<BulletBaseComponent>();
                cts.velocity = new Vector3(cts.velocity.x, 0, -cts.velocity.z);
            }
            else
            {
                go.GetComponent<BulletBaseComponent>().UpdateVelocity();
            }
        }
        else
        {
            rb.velocity = new Vector3(-rb.velocity.x, 0, rb.velocity.z);
            if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
            {
                BulletBaseComponent cts = go.GetComponent<BulletBaseComponent>();
                cts.velocity = new Vector3(-cts.velocity.x, 0, cts.velocity.z);
            }
            else
            {
                go.GetComponent<BulletBaseComponent>().UpdateVelocity();
            }
        }


        go.transform.rotation = Quaternion.Euler(90, -vecZ, 0);
    }



}
