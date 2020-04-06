using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherDream : MonoBehaviour {

	// Use this for initialization
	private GameObject enemy;
	private List<Transform> pointList = new List<Transform>();
	private GameObject blueLockBulletPrefab;
	private GameObject boomBullet;
	private float radius=0f;
	private GameObject lockPrefab;


	private Transform bulletParent;

	void Start () {
		bulletParent = GameObject.FindWithTag("Bullet").transform;
		enemy = GameObject.FindWithTag("enemy");
		boomBullet = ResourcesManager.Instance.LoadBullet("KingGuangBullet");
		blueLockBulletPrefab = ResourcesManager.Instance.LoadBullet("blueLockBullet");

		lockPrefab = ResourcesManager.Instance.LoadBullet("DaYu_Red");

		GameObject lockPointList = enemy.transform.Find("blueLockPoint").gameObject;
		for (int i = 0; i < lockPointList.transform.childCount; i++)
		{
			GameObject point = lockPointList.transform.GetChild(i).gameObject;
			pointList.Add(point.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UseFatherDream()
	{
		StartCoroutine(UseFatherDream_IEnumerator());
	}

	private IEnumerator UseFatherDream_IEnumerator()
	{
		for (int i = 0; i < 4; i++)
		{
			StartCoroutine(ShotZhiXianBuleBullet());
			yield return new WaitForSeconds(7.0f);
		}
	}


	IEnumerator ShotZhiXianBuleBullet()
	{
		for (int i = 0; i < pointList.Count; i++)
		{		
			GameObject go = GameObject.Instantiate(blueLockBulletPrefab);
			go.transform.position = pointList[i].transform.position;
			go.GetComponent<BlueZhiXianBullet>().point = pointList[i];
			yield return new WaitForSeconds(0.5f);
		}
		StartCoroutine(ShotBoomBullet());
	}

	IEnumerator ShotBoomBullet()
	{
		StartCoroutine( ShotSphereThenLockBullet_IEnumerator(1.0f));
		StartCoroutine(ShotSphereThenLockBullet_IEnumerator(4.0f));
		for (int i = 0; i < 10; i++)
		{
			Boom(enemy.transform.position);
			yield return new WaitForSeconds(0.5f);
		}
	}


	private void Boom(Vector3 boomVec)
	{
		int count = 16;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(boomBullet);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
			
			go.AddComponent<BulletBaseComponent>();
			go.SetActive(true);
			go.layer = LayerMask.NameToLayer("EnemyBullet");
			go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			Rigidbody rgb = go.GetComponent<Rigidbody>();

			bbc.speed = 8;
			bbc.power = 10;
			float angle = i * tempAngle;

			go.transform.position = boomVec+new Vector3 (radius * Mathf.Cos(angle * Mathf.Deg2Rad),0, radius * Mathf.Sin(angle * Mathf.Deg2Rad));
			rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
			bbc.velocity = rgb.velocity;
			rgb.mass = 40;

			bbc.SetVelocity();
		}
	}


	private void ShotSphereThenLockBullet()
	{

	}

	private IEnumerator ShotSphereThenLockBullet_IEnumerator(float delay)
	{
		yield return new WaitForSeconds(delay);
		int count = 16;
		float deltaAngle = 720f / count;
		float angle = 60;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(lockPrefab);
			go.transform.SetParent(bulletParent, false);
			go.layer = LayerMask.NameToLayer("EnemyBullet");
			go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			go.transform.position = enemy.transform.position;
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			bbc.power = 10;
			bbc.speed = 3f;
			bbc.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
			bbc.SetVelocity();
			LockPlayerMouse lpm = go.AddComponent<LockPlayerMouse>();
			lpm.speed = 10;
			lpm.delayTime = 0.85f;
			angle += deltaAngle;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
