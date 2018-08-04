using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaRotateBullet
{
    private GameObject bulletPrefab;

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadParticleBullet("WuRotateParitcle");
    }

    public void ShowSkill(Transform marisaTF)
    {
        GameObject go_Up = GameObject.Instantiate(bulletPrefab);
        go_Up.transform.position = new Vector3(marisaTF.position.x, marisaTF.position.y + 3.0f, 0);
 
        WuRotate wuRotate_Up = go_Up.GetComponent<WuRotate>();
        wuRotate_Up.marisaTF = marisaTF;
        wuRotate_Up.HP = 150;

        GameObject go_Down = GameObject.Instantiate(bulletPrefab);
        go_Down.transform.position = new Vector3(marisaTF.position.x, marisaTF.position.y - 3.0f, 0);

        WuRotate wuRotate_Down = go_Down.GetComponent<WuRotate>();
        wuRotate_Down.marisaTF = marisaTF;
        wuRotate_Down.HP = 150;

    }
}
