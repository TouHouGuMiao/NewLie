using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaXiaoSan
{
    private GameObject bulletPrefab;
    private List<string> spriteNameList = new List<string>();

    public void Init()
    {
        spriteNameList.Add("kingStar");
        spriteNameList.Add("purpleStar");
        spriteNameList.Add("redStar");
        spriteNameList.Add("greedStar");
        bulletPrefab = ResourcesManager.Instance.LoadParticleBullet("Wu_XiaoSanBullet");
    }
    float angle=0;

    private int index = 0;
    public void ShowSkill(Vector3 marisaVec)
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            SphereRotateBullet rotateBullet = go.GetComponent<SphereRotateBullet>();
            rotateBullet.spriteName = spriteNameList[index];
            rotateBullet.centerPoint = marisaVec;
            rotateBullet.angle = angle;
            angle += 30;
            index++;
            if (index >= spriteNameList.Count)
            {
                index = 0;
            }
        }
    }
}
