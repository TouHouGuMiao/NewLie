using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuMoveBullet:BulletBase
{

    List<Vector3> MoveList;
    [HideInInspector]
    public float radius=12;
    private GameObject bulletPrefab;
    public Vector3 centerPoint;

    protected override void Awake()
    {
        base.Awake();
        MoveList = new List<Vector3>();
        bulletPrefab = ResourcesManager.Instance.LoadBullet("pointTarget");
    }

    private void Start()
    {
        int angle_Wai = 90;
        int angle_Nel = 126;
        for (int i = 0; i < 5; i++)
        {
            Vector2 point_wai = new Vector2(radius * Mathf.Cos(angle_Wai * Mathf.Deg2Rad)+centerPoint.x, radius * Mathf.Sin(angle_Wai * Mathf.Deg2Rad) + centerPoint.y);
            Vector2 point_nei = new Vector2(radius / 2 * Mathf.Cos(angle_Nel * Mathf.Deg2Rad)+centerPoint.x, radius / 2 * Mathf.Sin(angle_Nel * Mathf.Deg2Rad) + centerPoint.y);
            MoveList.Add(point_wai);
            MoveList.Add(point_nei);
            angle_Wai += 72;
            angle_Nel += 72;
        }


        transform.position = MoveList[0];
        StartCoroutine(InstanteBullet());
    }
    // Update is called once per frame
    void Update () {
        MoveToMoreTarget();
    }

    private int index = 1;
    private bool isDestory = false;
    public void MoveToMoreTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, MoveList[index], Time.deltaTime * 30);
        if (transform.position == MoveList[index])
        {
            if (isDestory)
            {
                Destroy(gameObject);
            }
            index++;
            if (index == MoveList.Count)
            {
                index = 0;
                isDestory = true;
            }

            
        }
    }

    IEnumerator InstanteBullet()
    {
        GameObject go = Instantiate(bulletPrefab);
        go.transform.position = transform.position;
        PointTargetBullet targetBullet = go.GetComponent<PointTargetBullet>();
        targetBullet.targetPoint = centerPoint;
        targetBullet.targetType = PointTargetBullet.BulletTarget.In;
        targetBullet.injured = 0.03f * MarisaSkillManager.Instance.marisaPro.bulletPower;
        targetBullet.HP = 150;
        targetBullet.m_Type = BulletTpye.emptyBullet;
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(InstanteBullet());
    }
}
