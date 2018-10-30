using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaMoPao
{
    private GameObject bulletPrefab;

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadParticleBullet("MoPaoParitcle");
    }

    public void ShowSkill(Transform pointTF,Vector3 targetPos)
    {
        GameObject go = GameObject.Instantiate(bulletPrefab);   
        go.transform.position = pointTF.position;
        Vector3 vecPos = go.transform.InverseTransformPoint(targetPos);
        float angle = Mathf.Atan2(vecPos.y, vecPos.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
