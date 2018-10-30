using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaZhiXianInstateBullet
{
    private GameObject bulletPrefab;

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("ZhiXianInstateBullet");
    }

    public void ShowSkill(Vector3 instatePos, Vector3 movetVec, Vector3 targetVec, bool isRange,bool isInstate = false)
    {
        GameObject go = GameObject.Instantiate(bulletPrefab);
        ZhiXianInstanteBullet instateBullet = go.GetComponent<ZhiXianInstanteBullet>();
        instateBullet.movetVec = movetVec;
        instateBullet.isInstate = isInstate;
        instateBullet.targetVec = targetVec;
        instateBullet.isRange = isRange;
        go.transform.position = instatePos;
    }
}
