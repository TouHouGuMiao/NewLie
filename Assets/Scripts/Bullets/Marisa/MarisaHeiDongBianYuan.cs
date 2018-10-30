using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaHeiDongBianYuan
{
    private GameObject bulletPrefab;
    private List<string> spriteNameList = new List<string>();

    private int index = 0;

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("HuXianBullet");
        spriteNameList.Add("kingStar");
        spriteNameList.Add("purpleStar");
        spriteNameList.Add("redStar");
        spriteNameList.Add("greedStar");
    }

    float angle = 0;
    public void ShowSkill(Transform marisaTF)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            RotateHuXianBullet huXianBullet = go.GetComponent<RotateHuXianBullet>();
            huXianBullet.angle = angle;
            huXianBullet.centerTF = marisaTF;
            huXianBullet.spriteName = spriteNameList[index];
      

            index++;
            angle += 60;
            if (index >= spriteNameList.Count)
            {
                index = 0;
            }
        }
    } 
}
