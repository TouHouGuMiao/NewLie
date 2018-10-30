using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaTailLockBullet
{
    private GameObject bulletPrefab;

    private List<Sprite> spriteList=new List<Sprite> ();

    public void Init()
    {
        bulletPrefab = ResourcesManager.Instance.LoadBullet("tailLockBullet");
        Sprite star_tail_red = ResourcesManager.Instance.LoadSpriteBullet("star_tail_red",512,128);
        spriteList.Add(star_tail_red);
        Sprite star_tail_green = ResourcesManager.Instance.LoadSpriteBullet("star_tail_green",512,128);
        spriteList.Add(star_tail_green);
        Sprite star_tail_purple = ResourcesManager.Instance.LoadSpriteBullet("star_tail_purple",512,128);
        spriteList.Add(star_tail_purple);
        Sprite star_tail_yellow = ResourcesManager.Instance.LoadSpriteBullet("star_tail_yellow",512,128);
        spriteList.Add(star_tail_yellow);
        Sprite star_tail_blue = ResourcesManager.Instance.LoadSpriteBullet("star_tail_blue",512,128);
        spriteList.Add(star_tail_blue);
    }

    public void ShowSkill(Transform marisaTF, Transform target)
    {
        IEnumeratorManager.Instance.StartCoroutine(ShowSkillIEnumator_AC(marisaTF,target));
        IEnumeratorManager.Instance.StartCoroutine(ShowSkillIEnumator_CW(marisaTF, target));

    }

    IEnumerator ShowSkillIEnumator_CW(Transform marisaTF, Transform target)
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            render.sprite = spriteList[(int)Random.Range(0, spriteList.Count - 0.1f)];
            LockBullet lockBullet = go.GetComponent<LockBullet>();
            lockBullet.target = target;
            lockBullet.speed = Random.Range(4, 16);
            lockBullet.injured = 60;
            lockBullet.HP = 50;
            lockBullet.m_Type = BulletBase.BulletTpye.emptyBullet;
            if (marisaTF.rotation.eulerAngles.y == 0)
            {
                go.transform.rotation = Quaternion.Euler(0, 0, 180);
                go.transform.position = new Vector3(marisaTF.position.x - 2.0f, marisaTF.position.y);
            }

            else
            {
                go.transform.position = new Vector3(marisaTF.position.x + 2.0f, marisaTF.position.y);
            }
            go.transform.rotation = Quaternion.Euler(0, 0, 15);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ShowSkillIEnumator_AC(Transform marisaTF, Transform target)
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject go = GameObject.Instantiate(bulletPrefab);
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            render.sprite = spriteList[(int)Random.Range(0, spriteList.Count - 0.1f)];
            LockBullet lockBullet = go.GetComponent<LockBullet>();
            lockBullet.target = target;
            lockBullet.speed = Random.Range(4, 16);
            lockBullet.m_Type = BulletBase.BulletTpye.emptyBullet;
            lockBullet.injured = 60;
            lockBullet.HP = 50;
            if (marisaTF.rotation.eulerAngles.y == 0)
            {
                go.transform.rotation = Quaternion.Euler(0, 0, 180);
                go.transform.position = new Vector3(marisaTF.position.x - 2.0f, marisaTF.position.y);
            }

            else
            {
                go.transform.position = new Vector3(marisaTF.position.x + 2.0f, marisaTF.position.y);
            }
            go.transform.rotation = Quaternion.Euler(0, 0, -15);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
