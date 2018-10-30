using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase:MonoBehaviour
{
    [HideInInspector]
    public GameObject explosionEffect;
    public enum BulletTpye
    {
        playerBullet=0,
        emptyBullet=10,
    }

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
        explosionEffect = ResourcesManager.Instance.LoadEffect("explosionEffect");
        Destroy(transform.gameObject, 15);
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(explosionEffect);
        go.transform.position = transform.position;
    }

    
}
