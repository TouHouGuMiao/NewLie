using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaCurveBullet : MonoBehaviour
{
    public bool isSpecialBullet = false;

    [HideInInspector]
    public Vector3 targetPos;

    private Rigidbody m_rigidbody;

    private void Awake()
    {
        Destroy(this.gameObject,10);
        m_rigidbody = transform.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartParaCurveMove();
    }


    private void StartParaCurveMove()
    {
        float x = Mathf.Abs(transform.position.x - targetPos.x);
        float h = targetPos.y - transform.position.y;
        float v = (Mathf.Sqrt(10.0f) * x) / Mathf.Sqrt(x - h);

        float temp_X = transform.position.x - targetPos.x;

        if (temp_X > 0)
        {
            m_rigidbody.velocity = Vector3.Normalize(new Vector3(-1, 1, 0)) * v;
        }

        else
        {
            m_rigidbody.velocity = Vector3.Normalize(new Vector3(1, 1, 0)) * v;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isSpecialBullet)
        {
            if (collision.gameObject.name.Contains("BG"))
            {
                this.gameObject.tag = "specialBullet";
            }
        }
    
    }
}
