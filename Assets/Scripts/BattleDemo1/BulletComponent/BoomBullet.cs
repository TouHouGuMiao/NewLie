using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : MonoBehaviour {
	private GameObject prefab;
	private BulletBaseComponent m_Bbc;
	public bool useByEnemy=false;
	void Start() {
		prefab = this.transform.GetChild(0).gameObject;
		m_Bbc = this.GetComponent<BulletBaseComponent>();
	}


	void Update() {

	}

	private void Boom()
	{
		int count = 16;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(prefab);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
			if (useByEnemy)
			{
				go.layer = LayerMask.NameToLayer("EnemyBullet");
				go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			}
			go.transform.position = this.transform.position;
			go.AddComponent<BulletBaseComponent>();
			go.transform.localScale = Vector3.one;
			go.SetActive(true);
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			Rigidbody rgb = go.GetComponent<Rigidbody>();
			bbc.speed = m_Bbc.speed * 3f;
			bbc.power = m_Bbc.power/2;
			float angle = i * tempAngle;
			rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle * Mathf.Deg2Rad), 0, bbc.speed * Mathf.Sin(angle * Mathf.Deg2Rad));
			bbc.velocity = rgb.velocity;
		}
		Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision collider)
	{
		BulletBaseComponent bbc = collider.transform.GetComponent<BulletBaseComponent>();
		if (bbc != null)
		{
			if (useByEnemy)
			{
				if (gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
				{
					Boom();
				}
			}
			else
			{
				if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
				{
					Boom();
				}
			}
			
		}
	}
}
