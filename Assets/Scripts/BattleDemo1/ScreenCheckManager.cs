using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCheckManager
{
    private static ScreenCheckManager _instance;
    public static ScreenCheckManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScreenCheckManager();
                _instance.Init();
            }
            return _instance;
        }
    }
    private Rect RectUp;
    private Rect RectDown;
    private Rect RectLeft;
    private Rect RectRight;
    private float temp=10;
    private float RectSize = 20f;
    private void Init()
    {
        RectUp = new Rect(0, Screen.height - RectSize, Screen.width+temp, Screen.height+temp);
        RectDown = new Rect(0, 0, Screen.width + temp, RectSize);
        RectLeft = new Rect(0, 0, RectSize, Screen.height + temp);
        RectRight = new Rect(Screen.width - RectSize, 0, RectSize, Screen.height + temp);
    }

    public enum TouchState
    {
        Enter,
        Leava,
    }
   

    private bool touchStart = false;
    /// <summary>
    /// 对弹幕与屏幕的上下左右碰撞进行检测以及相应事件
    /// </summary>
    /// <param name="go"></param>
    public void ObjectCheckTouchScreen(GameObject go)
    {
     
        Vector3 go_ScreenVec = Camera.main.WorldToScreenPoint(go.transform.position);
        if (!RectUp.Contains(go_ScreenVec)&& !RectDown.Contains(go_ScreenVec) && !RectLeft.Contains(go_ScreenVec) && !RectRight.Contains(go_ScreenVec))
        {
            go.GetComponent<CanTouchScreen>().touchState = TouchState.Leava;
        }
        if(go.GetComponent<CanTouchScreen>().touchState == TouchState.Enter)
        {
            return;
        }
        

        if (RectUp.Contains(go_ScreenVec))
        {

            go.GetComponent<CanTouchScreen>().touchState = TouchState.Enter;
            BeBound(go,true);
        }
   

        if (RectDown.Contains(go_ScreenVec) )
        {

            go.GetComponent<CanTouchScreen>().touchState = TouchState.Enter;
            BeBound(go,true);
        }



        if (RectLeft.Contains(go_ScreenVec) )
        {

            go.GetComponent<CanTouchScreen>().touchState = TouchState.Enter;
            BeBound(go,false);
        }

    

        if (RectRight.Contains(go_ScreenVec))
        {
            go.GetComponent<CanTouchScreen>().touchState = TouchState.Enter;
            BeBound(go,false);
        }


    }

    private void BeBound(GameObject go,bool isHorizontal)
    {
        float vecZ = go.transform.rotation.eulerAngles.z;
        if (vecZ > 180)
        {
            vecZ = vecZ - 360;
        }
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (isHorizontal)
        {
            rb.velocity = new Vector3(rb.velocity.x,0, -rb.velocity.z);
            if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
            {
                BulletBaseComponent cts = go.GetComponent<BulletBaseComponent>();
                cts.velocity = new Vector3(cts.velocity.x,0, -cts.velocity.z);
            }
            else
            {
                go.GetComponent<BulletBaseComponent>().UpdateVelocity();
            }
        }
        else
        {
            rb.velocity = new Vector3(-rb.velocity.x, 0,rb.velocity.z);
            if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
            {
                BulletBaseComponent cts = go.GetComponent<BulletBaseComponent>();
                cts.velocity = new Vector3(-cts.velocity.x,0, cts.velocity.z);
            }
            else
            {
                go.GetComponent<BulletBaseComponent>().UpdateVelocity();
            }
        }  
   

        go.transform.rotation = Quaternion.Euler(90, -vecZ,0 );
    }


    //private void ReBoundUpAndDown(GameObject go)
    //{

    //    float vecZ = go.transform.rotation.eulerAngles.z;
    //    if (vecZ > 180) 
    //    {
    //        vecZ = vecZ - 360;
    //    }
    //    Rigidbody rb = go.GetComponent<Rigidbody>();
    //    if (PlayerBattleRule.Instance.bulletState== BulletState.reduceTime)
    //    {
    //        rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y);
    //        go.transform.rotation = Quaternion.Euler(0, 0, -vecZ);
    //    }
    //    else
    //    {
    //        rb.isKinematic = true;
    //        rb.isKinematic = false;
    //        go.GetComponent<CanTouchScreen>().angle = -go.GetComponent<CanTouchScreen>().angle;
    //        float force = go.GetComponent<CanTouchScreen>().force;
    //        float angle = go.GetComponent<CanTouchScreen>().angle;
    //         rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y);
    //        go.transform.rotation = Quaternion.Euler(0, 0, -vecZ);
    //    }

 
        
    //}

    //private void ReBoundLeftAndRight(GameObject go)
    //{

    //    float vecZ = go.transform.rotation.eulerAngles.z;
    //    if (vecZ > 180)
    //    {
    //        vecZ = vecZ - 360;
    //    }
    //    Rigidbody rb = go.GetComponent<Rigidbody>();
    //    float tempAngle = go.GetComponent<CanTouchScreen>().angle;
    //    float m_Angle = 0;
    //    float m_Angle_ToRag = tempAngle * (Mathf.Rad2Deg);
    //    if (m_Angle_ToRag > 0)
    //    {
    //        m_Angle_ToRag = 180 - m_Angle_ToRag;
    //    }
    //    else
    //    {
    //        m_Angle_ToRag = -180 - m_Angle_ToRag;
    //    }
    //    m_Angle = m_Angle_ToRag  * Mathf.Deg2Rad;
    //    go.GetComponent<CanTouchScreen>().angle = m_Angle;
    //    if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
    //    {
    //        float force = go.GetComponent<CanTouchScreen>().force;
    //        float angle = go.GetComponent<CanTouchScreen>().angle;
    //        rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y);
    //        go.transform.rotation = Quaternion.Euler(0, 0, -vecZ);
    //    }
    //    else
    //    {
           
    //        rb.isKinematic = true;
    //        rb.isKinematic = false;
    //        float force = go.GetComponent<CanTouchScreen>().force;
    //        float angle = go.GetComponent<CanTouchScreen>().angle;
    //        rb.AddForce(new Vector3(force * Mathf.Cos(angle), force * Mathf.Sin(angle)), 0);
    //        go.transform.rotation = Quaternion.Euler(0, 0, -vecZ);
    //    }

    //}
}
