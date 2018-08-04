using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBoomBullet : BulletBase
{
    private Rigidbody rigidbody;
    public Vector2 forceVec;
    private GameObject particle;

    private void Awake()
    {
        Destroy(gameObject, 1.5f);
        particle = ResourcesManager.Instance.LoadParticleBullet("PingBoom");
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 150 * Time.deltaTime));
    }

    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.AddForce(forceVec);
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(particle);
        go.transform.position = transform.position;
    }
}
