using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWithPlayer
{
    private static HandWithPlayer _Instance = null;

    public static HandWithPlayer Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new HandWithPlayer();
            }
            return _Instance;
        }
    }


    private Transform player;
    private float radius=2;
    private GameObject handPrefab;

    Dictionary<GameObject, GameObject> EnemyAndHandDic=new Dictionary<GameObject, GameObject> ();
    public void Init(Transform player)
    {
        handPrefab = ResourcesManager.Instance.LoadBullet("hand");
        this.player = player;
        IEnumeratorManager.Instance.StartCoroutine(FindInTargetIEnumerator());
        IEnumeratorManager.Instance.StartCoroutine(StartHandMove());
    }


    

    IEnumerator FindInTargetIEnumerator()
    {
        
        FindTargetInRadius();
        yield return new WaitForSeconds(0.5f);
        IEnumeratorManager.Instance.StartCoroutine(FindInTargetIEnumerator());
    }
    IEnumerator StartHandMove()
    {
        foreach (KeyValuePair<GameObject,GameObject> pair in EnemyAndHandDic)
        {
            Vector3 vecPos = player.InverseTransformPoint(pair.Key.transform.position);
            if (player.rotation.eulerAngles.y == 180)
            {
                vecPos = new Vector3(-vecPos.x, vecPos.y);
            }
            float initAngle = pair.Value.transform.rotation.eulerAngles.z;
            if (initAngle > 180)
            {
                initAngle = initAngle - 360;
            }
            float tempAngle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;
            float needAngle = tempAngle - initAngle;
            float onceAngle = needAngle / 40;

            
           
            float angle = initAngle;
            for (int i = 0; i < 40; i++)
            {
                pair.Value.transform.position = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad) + player.transform.position.x, radius * Mathf.Sin(angle * Mathf.Deg2Rad) + player.transform.position.y);
                pair.Value.transform.Rotate(0, 0, onceAngle);
                angle += onceAngle;
                yield return new WaitForSeconds(0.01f);
            }
        }
      
        IEnumeratorManager.Instance.StartCoroutine(StartHandMove());

    }


    public void FindTargetInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(player.position, 100, 1 << LayerMask.NameToLayer("enemy"));
        foreach (Collider c in colliders)
        {                
            GameObject getValueGo;
            if(!EnemyAndHandDic.TryGetValue(c.gameObject,out getValueGo))
            {
                GameObject go = GameObject.Instantiate(handPrefab);
                go.transform.position = player.position + new Vector3(radius, 0, 0);
                EnemyAndHandDic.Add(c.gameObject, go);
            }

            else
            {
                continue;
            } 
        }
    }

}
