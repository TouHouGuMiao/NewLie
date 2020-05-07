using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomOfRose : MonoBehaviour {
	private Transform enemy;
	private GameObject boomRoseprefab;
	private GameObject normlBlueRose;
	// Use this for initialization
	void Start () {
		boomRoseprefab = ResourcesManager.Instance.LoadBullet("roseYellow");
		normlBlueRose = ResourcesManager.Instance.LoadBullet("roseBlue");
		enemy = GameObject.FindWithTag("enemy").transform;
	
	}

	public void Shot()
	{
		StartCoroutine(ShotIEnumerator());
	}

	IEnumerator ShotIEnumerator()
	{
		for (int i = 0; i < 30; i++)
		{
			yield return new WaitWhile(PlayerBattleRule.Instance.IsReduceTime);
			if (i % 2 == 0)
			{
				ShotYellowBoom(enemy.transform.position);
			}
			else
			{
				ShotBlueRose(enemy.transform.position);
			}
			yield return new WaitForSeconds(1.0f);
		}
	}


	void ShotYellowBoom(Vector3 boomVec)
	{
		int count = 24;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(boomRoseprefab);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
			go.transform.position = boomVec;
			go.SetActive(true);
			go.layer = LayerMask.NameToLayer("EnemyBullet");
			go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			BoomBullet boom = go.AddComponent<BoomBullet>();
			boom.useByEnemy = true;
			Rigidbody rgb = go.GetComponent<Rigidbody>();
		   
			bbc.speed = 2;
			bbc.power = 10;
			float angle = i * tempAngle;
			rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
			bbc.velocity = rgb.velocity;
			bbc.SetVelocity();
		}
	}


	void ShotBlueRose(Vector3 boomVec)
	{
		int count = 24;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(normlBlueRose);
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

	// Update is called once per frame
	void Update () {
		
	}
}
