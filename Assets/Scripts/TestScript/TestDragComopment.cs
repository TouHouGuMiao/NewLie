using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDragComopment : MonoBehaviour {
	private float speed;
	private Vector3 startPos;
	private Vector3 endPos;
	float time = 0;
	private bool startAddTime = false;
	private bool isDrag = false;
	private void StartTime()
	{
		time = 0;
		startAddTime = true;
    }
    public float EndAddTimeAndGet()
    {
        startAddTime = false;
        return time;
    }

    void Update()
	{
		if (startAddTime)
		{
			time += Time.deltaTime;
		}
	}
	// Use this for initialization
	void Start () {
		
	}

    private Vector3 LastMousePos;
    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKey(KeyCode.Q) && Input.GetMouseButtonDown(0) && !isDrag)
        {
            if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("EnemyBullet")))
            {
                if (hit.transform.gameObject == transform.gameObject)
                {
                    Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + 6);
                    Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
                    startPos = targetPos;
                    isDrag = true;
                    LastMousePos = mousePos;
                    StartTime();
                    PlayerBattleRule.Instance.ReduceTime();
                }
            }
        }
    
       
        if (Input.GetMouseButton(0) && isDrag)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + 6);
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 vec = mousePos - LastMousePos;
            if ((mousePos - LastMousePos).magnitude >= 2F)
            {
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -angle, transform.rotation.eulerAngles.z);
                LastMousePos = mousePos;
            }

           transform.position = new Vector3(targetPos.x, -6, targetPos.z);
        }
        else if (Input.GetMouseButtonUp(0)&& isDrag)
        {

            endPos = transform.position;
            float distance = Mathf.Abs(Vector3.Distance(startPos, endPos));
            speed = distance / (EndAddTimeAndGet());

            Rigidbody rgb = transform.GetComponent<Rigidbody>();
            float angle = -(transform.eulerAngles.y) * Mathf.Deg2Rad;
            rgb.velocity = new Vector3(speed * Mathf.Cos(angle), 0, speed * Mathf.Sin(angle));
            isDrag = false;
        }
    }

    // Update is called once per fra
}
