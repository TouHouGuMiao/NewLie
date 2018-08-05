using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaLockStarBoom
{
    private GameObject bulletPrafab;

    public void Init()
    {
        bulletPrafab = ResourcesManager.Instance.LoadBullet("WeiBaLockBullet");
    }

    public void ShowSkill(Transform point,Transform target)
    {
        float angle = 0;
        for (int i = 0; i < 6; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrafab);
            StarLockRotateBullet lockBullet = go.GetComponent<StarLockRotateBullet>();
            lockBullet.rotateZ = angle;
            lockBullet.target = target;
            go.transform.position = point.position;
            go.transform.eulerAngles = new Vector3(0, 0, angle);
            angle += 60;
        }
    }
}
