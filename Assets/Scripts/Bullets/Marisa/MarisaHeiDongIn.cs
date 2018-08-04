using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaHeiDongIn
{
    private GameObject bulletPrefab;
    private List<string> spriteNameList=new List<string> ();
    public void Init()
    {
        spriteNameList.Add("kingStar");
        spriteNameList.Add("purpleStar");
        spriteNameList.Add("redStar");
        spriteNameList.Add("greedStar");
        bulletPrefab = ResourcesManager.Instance.LoadParticleBullet("WuRotateIn");
    }

    int index = 0;
    float angle = 0;
    public void ShowSkill(Vector3 marisaVec)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            SphereRotateWithPause withPauseBullet = go.GetComponent<SphereRotateWithPause>();
            withPauseBullet.centerPoint = marisaVec;
            withPauseBullet.spriteName = spriteNameList[index];
            withPauseBullet.angle = angle;

            index++;
            if (index >= spriteNameList.Count)
            {
                index = 0;
            }
            angle += 60;
        }
    }
}
