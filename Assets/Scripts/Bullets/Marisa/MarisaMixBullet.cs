using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaMixBullet
{
    private GameObject red_Bullet;
    private GameObject greed_Bullet;
    private GameObject blue_Bullet;

    public float radius=3.0f;

    public void init()
    {
        red_Bullet = ResourcesManager.Instance.LoadBullet("red_ZhiXian");
        greed_Bullet = ResourcesManager.Instance.LoadBullet("greed_ZhiXian");
        blue_Bullet = ResourcesManager.Instance.LoadBullet("blue_ZhiXian");
    }

    public void ShowSkill(Transform mairsaTF)
    {
        IEnumeratorManager.Instance.StartCoroutine(ShowSkill_IEnumerator(mairsaTF));
    }

    IEnumerator ShowSkill_IEnumerator(Transform marisaTF)
    {
        for (int i = 0; i < 8; i++)
        {
            ShowRedBullet(marisaTF);
            ShowGreedBullet(marisaTF);
            ShowBlueBullet(marisaTF);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void ShowRedBullet(Transform marisaTF)
    {
        if (marisaTF.transform.rotation.eulerAngles.y != 0)
        {
            float point_x = marisaTF.position.x + radius;
            float point_y = marisaTF.position.y;
            GameObject redBullet = GameObject.Instantiate(red_Bullet);
            red_Bullet.transform.position = new Vector2(point_x, point_y);
            ZhiXianBullet red_UpBullet = red_Bullet.GetComponent<ZhiXianBullet>();
            Vector2 red_target = new Vector2(-3, Mathf.Sqrt(3));
            red_UpBullet.targetVec = red_target.normalized;

            GameObject redBullet_Down = GameObject.Instantiate(red_Bullet);
            redBullet_Down.transform.position = new Vector3(point_x, point_y);
            ZhiXianBullet red_DownBullet = redBullet_Down.GetComponent<ZhiXianBullet>();
            red_DownBullet.targetVec = new Vector2(-3, -Mathf.Sqrt(3)).normalized;
        }


        else
        {
            float point_x = marisaTF.position.x - radius;
            float point_y = marisaTF.position.y;
            GameObject redBullet = GameObject.Instantiate(red_Bullet);
            red_Bullet.transform.position = new Vector2(point_x, point_y);
            ZhiXianBullet red_UpBullet = red_Bullet.GetComponent<ZhiXianBullet>();
            Vector2 red_target = new Vector2(3, Mathf.Sqrt(3));
            red_UpBullet.targetVec = red_target.normalized;

            GameObject redBullet_Down = GameObject.Instantiate(red_Bullet);
            redBullet_Down.transform.position = new Vector3(point_x, point_y);
            ZhiXianBullet red_DownBullet = redBullet_Down.GetComponent<ZhiXianBullet>();
            red_DownBullet.targetVec = new Vector2(3, -Mathf.Sqrt(3)).normalized;
        }
    }

    private void ShowGreedBullet(Transform marisaTF)
    {
        if (marisaTF.rotation.eulerAngles.y != 0)
        {
            float point_x = marisaTF.position.x - 1.0f;
            float point_y = marisaTF.position.y;
            GameObject greedBullet_Up = GameObject.Instantiate(greed_Bullet);
            greedBullet_Up.transform.position = new Vector2(point_x, point_y);
            ZhiXianBullet greed_UpBullet = greedBullet_Up.GetComponent<ZhiXianBullet>();
            greed_UpBullet.targetVec = new Vector2(-3, Mathf.Sqrt(3)).normalized;


            GameObject greedBullet_Down = GameObject.Instantiate(greed_Bullet);
            greedBullet_Down.transform.position = new Vector2(point_x, point_y);
            ZhiXianBullet greed_DownBullet = greedBullet_Down.GetComponent<ZhiXianBullet>();
            greed_DownBullet.targetVec = new Vector2(-3, -Mathf.Sqrt(3)).normalized;
        }

        else
        {
            float point_x = marisaTF.position.x + 1.0f;
            float point_y = marisaTF.position.y;
            GameObject greedBullet_Up = GameObject.Instantiate(greed_Bullet);
            greedBullet_Up.transform.position = new Vector2(point_x, point_y);
            ZhiXianBullet greed_UpBullet = greedBullet_Up.GetComponent<ZhiXianBullet>();
            greed_UpBullet.targetVec = new Vector2(3, Mathf.Sqrt(3)).normalized;


            GameObject greedBullet_Down = GameObject.Instantiate(greed_Bullet);
            greedBullet_Down.transform.position = new Vector2(point_x, point_y);
            ZhiXianBullet greed_DownBullet = greedBullet_Down.GetComponent<ZhiXianBullet>();
            greed_DownBullet.targetVec = new Vector2(3, -Mathf.Sqrt(3)).normalized;
        }
    }

    private void ShowBlueBullet(Transform marisaTF)
    {
        if (marisaTF.rotation.eulerAngles.y != 0)
        {
            float point_x = (radius) * Mathf.Cos(165 * Mathf.Deg2Rad) + marisaTF.position.x;
            float point_y = (radius) * Mathf.Sin(165 * Mathf.Deg2Rad) + marisaTF.position.y;
            GameObject blueBullet_Up = GameObject.Instantiate(blue_Bullet);
            blueBullet_Up.transform.position = new Vector3(point_x, point_y);
            ZhiXianBullet blue_UpBullet = blueBullet_Up.GetComponent<ZhiXianBullet>();
            blue_UpBullet.targetVec = new Vector2(-1, 0).normalized;

            GameObject blueBullet_Down = GameObject.Instantiate(blue_Bullet);
            blueBullet_Down.transform.position = new Vector2(point_x, -point_y);
            ZhiXianBullet blue_DownBullet = blueBullet_Down.GetComponent<ZhiXianBullet>();
            blue_DownBullet.targetVec = new Vector2(-1, 0).normalized;
        }

        else
        {
            float point_x = (radius) * Mathf.Cos(165 * Mathf.Deg2Rad) + marisaTF.position.x;
            float point_y = (radius) * Mathf.Sin(165 * Mathf.Deg2Rad) + marisaTF.position.y;
            GameObject blueBullet_Up = GameObject.Instantiate(blue_Bullet);
            blueBullet_Up.transform.position = new Vector3(-point_x, point_y);
            ZhiXianBullet blue_UpBullet = blueBullet_Up.GetComponent<ZhiXianBullet>();
            blue_UpBullet.targetVec = new Vector2(1, 0).normalized;

            GameObject blueBullet_Down = GameObject.Instantiate(blue_Bullet);
            blueBullet_Down.transform.position = new Vector2(-point_x, -point_y);
            ZhiXianBullet blue_DownBullet = blueBullet_Down.GetComponent<ZhiXianBullet>();
            blue_DownBullet.targetVec = new Vector2(1, 0).normalized;
        }
    }
}
