using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : MonoBehaviour {
	private GameObject redHeadPrefab;
	private GameObject greenHeadPrefab;
	private GameObject enemy;
	// Use this for initialization
	void Start () {
		redHeadPrefab = ResourcesManager.Instance.LoadBullet("redHead");
		greenHeadPrefab = ResourcesManager.Instance.LoadBullet("greenHead");
		enemy = GameObject.FindWithTag("enemy");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartShot()
	{
		StartCoroutine(ShotIEnumerator());
	}

	private void Shot()
	{
		Boom_GreenBullet(enemy.transform.position);
		Boom_RedBullet(enemy.transform.position);
	}

	private IEnumerator ShotIEnumerator()
	{
		for (int i = 0; i < 80; i++)
		{
			yield return new WaitWhile(PlayerBattleRule.Instance.IsReduceTime);
			yield return new WaitForSeconds(0.5f);
			Shot();
		}
	}
	private void Boom_GreenBullet(Vector3 boomVec)
	{
		int count = 20;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(greenHeadPrefab);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
			go.transform.position = boomVec;
			go.SetActive(true);
			go.layer = LayerMask.NameToLayer("EnemyBullet");
			go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			Rigidbody rgb = go.GetComponent<Rigidbody>();
			rgb.mass = 20;
			bbc.speed = 2;
			bbc.power = 10;
			float angle = (i * tempAngle)+6;
			rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
			bbc.velocity = rgb.velocity;

			bbc.SetVelocity();
		}
	}
	private void Boom_RedBullet(Vector3 boomVec)
	{
		int count = 20;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(redHeadPrefab);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
			go.transform.position = boomVec;
			go.SetActive(true);
			go.layer = LayerMask.NameToLayer("EnemyBullet");
			go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			Rigidbody rgb = go.GetComponent<Rigidbody>();

			bbc.speed = 2;
			bbc.power = 10;
			float angle = i * tempAngle;
			rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
			bbc.velocity = rgb.velocity;

			bbc.SetVelocity();
		}
	}


	//private IEnumerator ShotIEnumerator()
	//{

	//}
}
