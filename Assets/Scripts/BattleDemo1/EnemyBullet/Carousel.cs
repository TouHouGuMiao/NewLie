using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour 
{
    private GameObject prefab;
    private GameObject bulletParent;
    public int count;
    private float radius=0.5f;
    private List<GameObject> pointList = new List<GameObject>();
    private float deltaAngle=1.0f;
    private GameObject redAndBlackPrefab;
    void Start()
    {
        transform.position = GameObject.FindWithTag("enemy").transform.position;
        prefab = ResourcesManager.Instance.LoadBullet("smallBullet_red");
        redAndBlackPrefab = ResourcesManager.Instance.LoadBullet("smallBullet_redAndBlack");
        count = gameObject.transform.childCount;
        bulletParent = GameObject.FindWithTag("Bullet");
        float angle = 0;
        float deltaAngle = (360.0f / count);
        for (int i = 0; i < count; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.transform.localPosition = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(angle * Mathf.Deg2Rad));
            angle += deltaAngle;
            pointList.Add(go);
        }
        
    }

    void Update()
    {
       
        
    }
    public void Shot_Easy()
    {
        shotCount = 0;
        StartCoroutine(ShotEasyIEnumator());
    }


    public void Shot_Diffcult()
    {
        shotCount = 0;
        StartCoroutine(ShotDiffcultIEnumator());
    }


    IEnumerator ShotDiffcultIEnumator()
    {
        if (shotCount >= 1000)
        {
            yield break;
        }

        if (shotCount % 100 == 0 && shotCount >= 100)
        {
            yield return new WaitForSeconds(1f);
        }

        if (shotCount % temp == 0)
        {
            yield return new WaitForSeconds(0.05f);
            index = 0;
        }
        yield return new WaitForSeconds(0.01F);
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject point = pointList[i];
            GameObject go = GameObject.Instantiate(redAndBlackPrefab);
            go.transform.position = new Vector3(point.transform.position.x, GameObject.FindWithTag("enemy").transform.position.y, point.transform.position.z);
            go.transform.SetParent(bulletParent.transform, false);

            float angle = Mathf.Atan2(point.transform.localPosition.z, point.transform.localPosition.x);

            go.transform.rotation = Quaternion.Euler(90, angle * Mathf.Rad2Deg, 0);
            go.layer = LayerMask.NameToLayer("EnemyBullet");
            go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
            BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();

            bbc.speed = 3f + 0.3f * index;
            bbc.power = 5;
            bbc.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
            Rigidbody rgb = go.GetComponent<Rigidbody>();
            rgb.mass = 5;
            rgb.velocity = bbc.velocity;
            bbc.SetVelocity();

        }
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject point = pointList[i];
            float angle = Mathf.Atan2(point.transform.localPosition.z, point.transform.localPosition.x) * Mathf.Rad2Deg;
            angle += deltaAngle;
            point.transform.localPosition = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        shotCount++;
        index++;


        StartCoroutine(ShotDiffcultIEnumator());
    }



    IEnumerator ShotEasyIEnumator()
    {
        if (shotCount >= 600)
        {
            yield break;
        }

        if (shotCount % 100==0&&shotCount>=100)
        {
            yield return new WaitForSeconds(1.5f);
        }

        if (shotCount % temp == 0)
        {
            yield return new WaitForSeconds(0.05f);
            index = 0;
        }
        yield return new WaitForSeconds(0.01F);
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject point = pointList[i];
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.position = new Vector3(point.transform.position.x, GameObject.FindWithTag("enemy").transform.position.y, point.transform.position.z);
            go.transform.SetParent(bulletParent.transform, false);

            float angle = Mathf.Atan2(point.transform.localPosition.z, point.transform.localPosition.x);

            go.transform.rotation = Quaternion.Euler(90, angle * Mathf.Rad2Deg, 0);
            go.layer = LayerMask.NameToLayer("EnemyBullet");
            go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
            BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();

            bbc.speed = 2.5f + 0.25f * index;
            bbc.power = 5;
            bbc.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
            Rigidbody rgb = go.GetComponent<Rigidbody>();
            rgb.mass = 5;
            rgb.velocity = bbc.velocity;
            bbc.SetVelocity();

        }
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject point = pointList[i];
            float angle = Mathf.Atan2(point.transform.localPosition.z, point.transform.localPosition.x) * Mathf.Rad2Deg;
            angle += deltaAngle;
            point.transform.localPosition = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        shotCount++;
        index++;


        StartCoroutine(ShotEasyIEnumator());
    }

    void Shot()
    {
        StartCoroutine(ShotIEnumator());
    }
    int temp = 8;
    int index = 0;
    int shotCount = 0;

    IEnumerator ShotIEnumator()
    {
        if (shotCount >= 200)
        {
            yield break;
        }
        if (shotCount % temp == 0)
        {
            yield return new WaitForSeconds(0.05f);
            index = 0;
        }
        yield return new WaitForSeconds(0.01F);
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject point = pointList[i];
            GameObject go = GameObject.Instantiate(prefab);
            go.transform.position = new Vector3(point.transform.position.x, GameObject.FindWithTag("enemy").transform.position.y, point.transform.position.z);
            go.transform.SetParent(bulletParent.transform, false);

            float angle = Mathf.Atan2(point.transform.localPosition.z, point.transform.localPosition.x);
            
            go.transform.rotation = Quaternion.Euler(90, angle * Mathf.Rad2Deg, 0);
            go.layer = LayerMask.NameToLayer("EnemyBullet");
            go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
            BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();

            bbc.speed = 5.0f+0.5f*index;
            bbc.power = 5;
            bbc.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
            Rigidbody rgb = go.GetComponent<Rigidbody>();
            rgb.mass = 5;
            rgb.velocity = bbc.velocity;
            bbc.SetVelocity();
           
        }
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject point = pointList[i];
            float angle = Mathf.Atan2(point.transform.localPosition.z, point.transform.localPosition.x) * Mathf.Rad2Deg;
            angle += deltaAngle;
            point.transform.localPosition = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), 0, radius * Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        shotCount++;
        index++;
        
    
        StartCoroutine(ShotIEnumator());
    }

    

}
