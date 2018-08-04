using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosBullet : BulletBase {

    public float speed;
    public int m_Sign = 1;
    private float x = -0.78f;
    public float rate;

    void Start()
    {
        if (m_Sign == -1)
        {
            transform.Rotate(new Vector3(0, 0, 180), 180);
        }
    }


    void BulletUpdata(int sign)
    {
        this.gameObject.transform.Translate(new Vector2(1, 0) * speed * sign * Time.deltaTime, Space.World);
  
        float cosY = Mathf.Cos(x);
        x += rate;
        this.gameObject.transform.Translate(new Vector2(0, cosY) * sign * Time.deltaTime * speed*1.5f, Space.World);

    }


    void Update()
    {
        BulletUpdata(m_Sign);

    }
}
