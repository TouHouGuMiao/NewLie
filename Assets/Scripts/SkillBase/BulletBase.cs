using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase:MonoBehaviour
{
    protected float radius = 2.5f;

    [HideInInspector]
    public GameObject explosionEffect;
    /// <summary>
    /// 到达击打点所需时间
    /// </summary>
    public float aritiveTime = 0;

   /// <summary>
   /// 到达击打圈的角度位置
   /// </summary>
    public float angle=0;

    /// <summary>
    /// 延迟时间
    /// </summary>
    public float delayTime = 0;
    public enum BulletTpye
    {
        playerBullet=0,
        emptyBullet=10,
    }

    protected Transform ballteCollider;

    public BulletTpye m_Type;

    public float injured { get; set; }
    public float HP { get; set; }

    protected virtual void OnTriggerEnter(Collider other)
    {

        BulletBase m_base = other.transform.GetComponent<BulletBase>();
        if (m_base == null)
        {
         
            return;
        }

        if (m_base.m_Type != this.m_Type)
        {
            this.HP -= m_base.injured;

        }

        if (this.HP <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    protected virtual void Awake()
    {
        ballteCollider = GameObject.FindWithTag("BattleCollider").transform;
       
        //Destroy(transform.gameObject, 15);
    }

    private void OnDestroy()
    {

    }

    
}
