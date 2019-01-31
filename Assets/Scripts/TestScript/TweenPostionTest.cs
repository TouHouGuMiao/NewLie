using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPostionTest : MonoBehaviour {
    Transform battleCollider;


    private void Awake()
    {
        battleCollider = GameObject.FindWithTag("BattleCollider").transform;
    }


    protected void OnTriggerStay(Collider other)
    {
        if (other.name == "BattleCollider")
        {
            StopAllCoroutines();

            Vector3 vecPos = Vector3.Normalize(battleCollider.transform.position - transform.position);
            transform.Translate(vecPos * Time.deltaTime * 5, Space.World);
        }

    }



}
