using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBullet : BulletBase
{
    private float speed=15f;

    private GameObject target;


    private GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        FindEnemyInRadius();
    }

    private void Start()
    { 
        if (target != null)
        {

            Vector3 tempPos = transform.InverseTransformPoint(target.transform.position);
            float angle = Mathf.Atan2(tempPos.y, tempPos.x) * Mathf.Rad2Deg;
            transform.Rotate(new Vector3(0, 0, 1)*angle);
        }
    
    }



    private void Update()
    {
        transform.Translate(new Vector3(1,0,0)* speed*Time.deltaTime, Space.Self);
    }


    void FindEnemyInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 10, 1 << LayerMask.NameToLayer("enemy"));
        if (colliders.Length > 0)
        {
            GameObject go = colliders[0].gameObject;
            for (int i = 1; i < colliders.Length; i++)
            {
                float distance_Temp = Vector2.Distance(go.transform.position, player.transform.position);
                float distance_Now = Vector2.Distance(colliders[i].transform.position, player.transform.position);
                if (distance_Now < distance_Temp)
                {
                    go = colliders[i].gameObject;
                }
            }
            target = go;
        }
    }


}
