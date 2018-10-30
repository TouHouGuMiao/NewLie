using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaRotateBullet
{
    private GameObject bulletPrefab;

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("rotateFireBullet");
    }

    public void ShowSkill(Transform marisaTF,Transform targetTF)
    {
        GameObject go_Up = GameObject.Instantiate(bulletPrefab);
        go_Up.transform.position = new Vector3(marisaTF.position.x, marisaTF.position.y + 16.0f, 0);

        RotateFireBullet wuRotate_Up = go_Up.GetComponent<RotateFireBullet>();
        wuRotate_Up.targetTF = targetTF;
        wuRotate_Up.marisaTF = marisaTF;
        wuRotate_Up.HP = 150;

        GameObject go_Down = GameObject.Instantiate(bulletPrefab);
        go_Down.transform.position = new Vector3(marisaTF.position.x, marisaTF.position.y - 16.0f, 0);

        RotateFireBullet wuRotate_Down = go_Down.GetComponent<RotateFireBullet>();
        wuRotate_Down.marisaTF = marisaTF;
        wuRotate_Down.targetTF = targetTF;
        wuRotate_Down.HP = 150;

    }
}
