using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaDrawFiveStars
{
    private GameObject wuPrefab;

    public void Init()
    {
        wuPrefab = ResourcesManager.Instance.LoadParticleBullet("WuParitcle");
    }

    public void ShowSkill(Vector3 centerVec, float radius)
    {
        Vector3 centerPoint;
        centerPoint = centerVec;

        GameObject go = GameObject.Instantiate(wuPrefab);
        WuMoveBullet wuBullet = go.GetComponent<WuMoveBullet>();
        wuBullet.centerPoint = centerPoint;
        wuBullet.radius = radius;
    }
}
