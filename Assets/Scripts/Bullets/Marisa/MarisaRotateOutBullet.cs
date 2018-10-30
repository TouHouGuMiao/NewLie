using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaRotateOutBullet
{
    private GameObject bulletPrefab;
    private List<Sprite> SpriteList = new List<Sprite>();
    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("RotateOutBullet");
        Sprite redStar = ResourcesManager.Instance.LoadSpriteBullet("redStar");
        Sprite purpleStar = ResourcesManager.Instance.LoadSpriteBullet("purpleStar");
        Sprite kingStar = ResourcesManager.Instance.LoadSpriteBullet("kingStar");
        Sprite greedStar = ResourcesManager.Instance.LoadSpriteBullet("greedStar");
        SpriteList.Add(redStar);
        SpriteList.Add(purpleStar);
        SpriteList.Add(kingStar);
        SpriteList.Add(greedStar);
    }

    public void ShowSkill(Vector3 centerVec)
    {
        float angle = 0;
        int index=0;
        for (int i = 0; i < 12; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            RotateOutBullet outBullet = go.GetComponent<RotateOutBullet>();
            outBullet.centerPoint = centerVec;
            outBullet.angle = angle;
            angle += 30;
            outBullet.sprite = SpriteList[2];
            index++;
            if (index >= SpriteList.Count)
            {
                index=0;
            }
        }
    }

}
