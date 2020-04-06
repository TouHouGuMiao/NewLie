using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弧形移动，并且环绕恋恋。PS:并不是通用，只为超我服务
/// </summary>
public class MoveAndAround : MonoBehaviour {
    private GameObject enmey;
    private bool aroundWithEnemy = false;
    private Rigidbody rgb;
    public float rotate = 100;
    public float speed = 6f;
    public int aroundSgin = 1;
    public float aroundSpeed;
    public bool isShot = false;
    // Use this for initialization
    void Start () {
        enmey = GameObject.FindWithTag("enemy");
        rgb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isShot)
        {
            return;
        }
        if (!aroundWithEnemy)
        {
            transform.Rotate(0, -rotate * Time.deltaTime, 0, Space.World);
            transform.Translate(speed*Time.deltaTime, 0, 0);
            //float angle = transform.rotation.eulerAngles.y;
            //rgb.velocity = new Vector3(speed * Mathf.Cos(-angle * Mathf.Deg2Rad), 0, speed * Mathf.Sin(-angle * Mathf.Deg2Rad));
        }
        else
        {
            transform.RotateAround(enmey.transform.position, new Vector3(0, aroundSgin, 0), aroundSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "chaoWo")
        {
            if (!aroundWithEnemy)
            {
                Rigidbody rgb = gameObject.GetComponent<Rigidbody>();
                rgb.velocity = new Vector3(0, 0, 0);
                Vector3 tempVec = enmey.transform.position - transform.position;
                float angle = Mathf.Atan2(tempVec.z, tempVec.x) * Mathf.Rad2Deg;
                angle = -angle;
                angle = angle - 90;
                if (aroundSgin == -1)
                {
                    transform.rotation = Quaternion.Euler(90, angle, 180);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(90, angle, 0);
                }
                aroundWithEnemy = true;
            }
        }
    }
}
