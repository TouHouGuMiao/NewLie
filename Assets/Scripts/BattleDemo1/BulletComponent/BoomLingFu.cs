using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomLingFu : MonoBehaviour
{
    GameObject prefab;
    public Vector3 boomVec;
    private CardBase data;
    void Start()
    {
        data = HandCardPanel.GetCurrentUseKeyCodeData();
        prefab = ResourcesManager.Instance.LoadBullet("initBullet");
        Boom(boomVec,prefab);
    }


    void Update()
    {

    }



    public void Boom(Vector3 boomVec, GameObject prefab)
    {
        int count = 32;
        float tempAngle = 360 / count;
       
        for (int i = 0; i < count; i++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
            go.transform.position = boomVec;
            go.SetActive(true);
            go.layer = LayerMask.NameToLayer("PlayerBullet");
            go.transform.SetChildLayer(LayerMask.NameToLayer("PlayerBullet"));
            BulletBaseComponent bbc = go.AddComponent<BulletBaseComponent>();
            Rigidbody rgb = go.GetComponent<Rigidbody>();
            rgb.mass = data.mass;

            bbc.speed = data.speed;
            bbc.power = data.power;
            float angle = i * tempAngle;
            rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
            bbc.velocity = rgb.velocity;

            bbc.SetVelocity();
        }
        Destroy(gameObject);
    }
}
