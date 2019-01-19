using MathNet.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletBase
{
    float k = 0;
    float a = 0;
    float b = 0;
    float x = 0;
    float y = 0;
    float distance=0;

    float x0 = 0;
    float y0 = 0;
    float deltaTime = 0;
    bool isAdd_x=false;

    private float down;
    private float up;
    private bool isClick=false;

    private float speed=0;


    public bool isCW=true;
    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        StartCoroutine(TheBulletRun());
    }

    private void Update()
    {
  
    }

    

    IEnumerator TheBulletRun()
    {
        if (delayTime > 0)
        {
            yield return new WaitForSeconds(delayTime);
         
        }
        BeeLine();

    }



    void BeeLine()
    {
        Vector3 targetPos = ballteCollider.transform.position + new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad));
        k = (transform.position.y - targetPos.y) / (transform.position.x - targetPos.x);
        b = targetPos.y - k * targetPos.x;
        x = transform.position.x;
        distance = Vector3.Distance(transform.position, targetPos);

        if (transform.position.x < targetPos.x)
        {
            isAdd_x = true;
        }
        else
        {
            isAdd_x = false;
        }
        deltaTime = Mathf.Abs(transform.position.x - targetPos.x)/aritiveTime;
        StartCoroutine(BeeLineMove());
    }

    IEnumerator BeeLineMove()
    {
        y = k * x + b;  
        transform.position = new Vector3(x, y);
        yield return new WaitForSeconds(0.01f);
        if (isAdd_x)
        {
            x += (deltaTime * 0.01f);
        }
        else
        {
            x -= ((deltaTime * -0.01f));
        }
        StartCoroutine(BeeLineMove());
    }


    protected override void OnTriggerEnter(Collider other)
    {
        isClick = true;
        base.OnTriggerEnter(other);
    }

    protected  void OnTriggerStay(Collider other)
    {
        if (other.name== "BattleCollider")
        {
            StopAllCoroutines();
            
            Vector3 vecPos = Vector3.Normalize (ballteCollider.transform.position - transform.position);
            transform.Translate(vecPos * Time.deltaTime * 5, Space.World);
        }

    }


    private void OnMouseOver()
    {

        if (isClick)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(this.gameObject);
                AudioManager.Instance.PlayEffect_Source("fireAudio");
            }
        }
    }





    ///// <summary>
    ///// 返回为True则使用Sphere 否  为直线
    ///// </summary>
    ///// <returns></returns>


    //void SphereMove(bool isCW)
    //{
    //    if (isCW)
    //    {
    //        Vector3 targetPos = ballteCollider.transform.position + new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    //        float temp_k = -1 / ((transform.position.y - targetPos.y) / (transform.position.x - targetPos.x));
    //        float temp_Angle = Mathf.Atan(temp_k);
    //        Vector3 tempPos = targetPos + new Vector3(radius * Mathf.Cos(temp_Angle), radius * Mathf.Sin(temp_Angle));
    //        a = (Mathf.Abs(Vector2.Distance(transform.position, targetPos)) + radius) / 2;
    //        float c = (Mathf.Abs(Vector2.Distance(transform.position, tempPos)) - a);
    //        b = Mathf.Sqrt(Mathf.Pow(a, 2) - Mathf.Pow(c, 2));
    //        float temp_k2 = (transform.position.y - tempPos.y) / (transform.position.x - tempPos.x);
    //        float temp_Angle2 = Mathf.Atan(temp_k2);
    //        Vector3 centerPos = transform.position - new Vector3(a * Mathf.Cos(temp_Angle2), a * Mathf.Sin(temp_Angle2));

    //        x0 = centerPos.x;
    //        y0 = centerPos.y;
    //        k = (transform.position.y - centerPos.y) / (transform.position.x - centerPos.x);
    //        x = a;

    //    }
    //}

    //IEnumerator StartMove()
    //{
    //    if (Mathf.Abs(x) == a)
    //    {
    //        y = 0;
    //    }
    //    else
    //    {
    //        y = b * Mathf.Sqrt(1 - ((x * x) / (a * a)));
    //    }

    //    Debug.LogError(x0);
    //    Debug.LogError(y0);

    //    float temp_Angle = Mathf.Atan(k);
    //    float temp_x = ((x - x0) * Mathf.Cos(temp_Angle)) + ((y - y0) * Mathf.Sin(temp_Angle));
    //    float temp_y = ((y - y0) * Mathf.Cos(temp_Angle)) - ((x - x0) * Mathf.Sin(temp_Angle));
    //    float temp_k = (x - x0) * Mathf.Cos(temp_Angle);
    //    float temp_j = (x - x0) * Mathf.Sin(temp_Angle);
    //    float temp_a = Mathf.Pow(b, 2) * Mathf.Pow(Mathf.Sin(temp_Angle), 2) + Mathf.Pow(a, 2) * Mathf.Pow(Mathf.Cos(temp_Angle), 2);
    //    float temp_b = 2 * (b * b * Mathf.Sin(temp_Angle) * temp_k - b * b * y0 * Mathf.Pow(Mathf.Sin(temp_Angle), 2) - a * a * Mathf.Pow(Mathf.Cos(temp_Angle), 2) - a * a * Mathf.Cos(temp_Angle) * temp_j);
    //    float temp_c = temp_k * temp_k * b * b - 2 * temp_k * b * b * Mathf.Sin(temp_Angle) * y0 + b * b * Mathf.Pow(Mathf.Sin(temp_Angle), 2) * y0 * y0 + 2 * a * a * temp_j * Mathf.Cos(temp_Angle) * y0 + a * a * Mathf.Pow(Mathf.Cos(temp_Angle), 2) * y0 * y0 + a * a * temp_j * temp_j - a * a * b * b;
    //    transform.position = new Vector3(temp_x, temp_y);

    //    x -= 0.01f;
    //    yield return new WaitForSeconds(0.01f);
    //}


    //float FangCheng(float a, float b, float c, bool isX1)
    //{
    //    float de = b * b - 4 * a * c;
    //    if (a != 0)
    //    {
    //        if (de > 0)
    //        {
    //            float x1 = ((-b) + Mathf.Sqrt(de)) / 2 * a;
    //            float x2 = ((-b) - Mathf.Sqrt(de)) / 2 * a;
    //            if (isX1)
    //            {
    //                return x1;
    //            }

    //            return x2;
    //        }
    //    }
    //    else if (de == 0)
    //    {
    //        float x = (-b) / 2 * a;
    //        return x;

    //    }
    //    else
    //    {
    //        Debug.LogError("此方程无解！");
    //        return 0;
    //    }
    //    return 0;
    //}


}
