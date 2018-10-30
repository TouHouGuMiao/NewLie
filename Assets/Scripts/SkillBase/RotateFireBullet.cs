using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFireBullet : BulletBase {
    private GameObject bulletPrefab;
    public Transform marisaTF;
    public Transform targetTF;
    private float radius;
    private string spriteName;
    private List<string> StarNameList;
    private Sprite sprite;

    private bool isOverAround=false;
    private Vector3 moveVec;
    protected override void Awake()
    {
        base.Awake();
        StarNameList = new List<string>();
        StarNameList.Add("kingStar");
        StarNameList.Add("purpleStar");
        StarNameList.Add("redStar");
        StarNameList.Add("greedStar");
        bulletPrefab = ResourcesManager.Instance.LoadBullet("rotateTargetBullet");
    }
    // Update is called once per frame


    private float angle;
    float initAngle;
    private void Start()
    {
      
        radius = Vector2.Distance(marisaTF.position, transform.position);
        Vector3 depentPos = marisaTF.InverseTransformPoint(transform.position);
        initAngle = Mathf.Atan2(depentPos.y, depentPos.x) * Mathf.Rad2Deg;
        angle = initAngle;
        spriteName = StarNameList[(int)Random.Range(0, StarNameList.Count - 0.1f)];
        sprite= ResourcesManager.Instance.LoadSpriteBullet(spriteName);
        StartCoroutine(InstanteBullet());
    }
    void Update ()
    {
        //transform.Rotate(new Vector3(0, 0, 700 * Time.deltaTime));
        if (!isOverAround)
        {
            MoveAroudMarisa();
            moveVec = targetTF.position - transform.position;
        }

      

        if (angle - initAngle >= 360)
        {
            isOverAround = true;
          
            transform.Translate(moveVec.normalized * Time.deltaTime * 20, Space.World);
        }


        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 90);
    }

    private float rotateAngle = 0;

    IEnumerator InstanteBullet()
    {
        GameObject go = Instantiate(bulletPrefab);
        go.transform.position = transform.position;
        SpriteRenderer render = go.GetComponent<SpriteRenderer>();
        render.sprite = sprite;
        RotateBullet rotateBullet = go.GetComponent<RotateBullet>();
        rotateBullet.targetVec = new Vector3(1 * Mathf.Cos(rotateAngle * Mathf.Deg2Rad), 1 * Mathf.Sin(rotateAngle * Mathf.Deg2Rad));
        rotateBullet.m_Type = BulletTpye.emptyBullet;
        rotateBullet.injured = 0.01f * MarisaSkillManager.Instance.marisaPro.bulletPower;
        rotateBullet.HP = 30;
        yield return new WaitForSeconds(0.01f);
        rotateAngle += 12;
        StartCoroutine(InstanteBullet());
    }

    void MoveAroudMarisa()
    {
        angle += 60 * Time.deltaTime;
        transform.position = new Vector2(radius * Mathf.Cos(angle * Mathf.Deg2Rad) + marisaTF.position.x, radius * Mathf.Sin(angle * Mathf.Deg2Rad)+marisaTF.position.y);

    }
}
