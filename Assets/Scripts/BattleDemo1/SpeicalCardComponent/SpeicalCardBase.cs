using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 放置结界类符卡的基类，继承关系
/// </summary>
public class SpeicalCardBase : MonoBehaviour {
    public bool isOn=false;
    public SpeicalCardState state= SpeicalCardState.init;
    public SpeicalCardState lastState = SpeicalCardState.init;
    public enum SpeicalCardState
    {
        init,
        on,
    }

    public void Update()
    {
        if (!isOn)
        {
            state = SpeicalCardState.init;
        }
        if (isOn)
        {
            state = SpeicalCardState.on;
        }        
    }

    public List<GameObject> GetBulletInTrigger_AtOnce(LayerMask layerMask)
    {
        List<GameObject> list = new List<GameObject>();
        Collider[] colliderList = Physics.OverlapSphere(transform.position, gameObject.GetComponent<SphereCollider>().radius, 1 << layerMask);
        for (int i = 0; i < colliderList.Length; i++)
        {
            list.Add(colliderList[i].gameObject);
        }
        return list;
    }
    
    public void OnTriggerStay(Collider colldier)
    {
      
    }
}
