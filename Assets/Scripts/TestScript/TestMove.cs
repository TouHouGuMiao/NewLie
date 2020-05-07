using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private GameObject prefab;
    private float speed;
    private Vector3 startPos;
    private Vector3 endPos;

    float time =0;
    private bool startAddTime = false;
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

    void Awake()
    {

    }
    void Start()
    {
        prefab = ResourcesManager.Instance.LoadBullet("initBullet");
    }

    void Update()
    {
        if (startAddTime)
        {
            time += Time.deltaTime;
        }
    }
    private bool isDrag = false;
    private GameObject tempBullet=null;
    private Vector3 LastMousePos;
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q)&&Input.GetMouseButtonDown(0)&&!isDrag)
        {
            GameObject go = GameObject.Instantiate(prefab);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y +6);
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            startPos = targetPos;
            go.transform.position = targetPos;
            go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
            go.AddComponent<BulletBaseComponent>();
            go.GetComponent<BulletBaseComponent>().cardBase = BattleCardManager.intance.GetCardBaseDataById(0);
            BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
            bbc.rotateWithArrow = false;
            bbc.power = 10000;
            tempBullet = go;
            isDrag = true;
            LastMousePos = mousePos;
            StartTime();
        }
        if (Input.GetMouseButton(0)&& isDrag)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y + 6);
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 vec = mousePos - LastMousePos;
            if ((mousePos - LastMousePos).magnitude >= 2F)
            {
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                tempBullet.transform.rotation = Quaternion.Euler(tempBullet.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,angle );
                LastMousePos = mousePos;
            }
           
            tempBullet.transform.position = new Vector3(targetPos.x, -6, targetPos.z);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (tempBullet == null)
            {
                return;
            }
            endPos = tempBullet.transform.position;
            float distance = Mathf.Abs(Vector3.Distance(startPos, endPos));
            speed = distance / (EndAddTimeAndGet());
            Rigidbody rgb = tempBullet.GetComponent<Rigidbody>();
            float angle = -(tempBullet.transform.eulerAngles.y) * Mathf.Deg2Rad;
            rgb.velocity = new Vector3(speed * Mathf.Cos(angle), 0, speed * Mathf.Sin(angle));
            tempBullet = null;
            isDrag = false;
        }
    }

  








}
