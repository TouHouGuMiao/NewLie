using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomOfHead : MonoBehaviour {
	private Transform enemy;
	private GameObject prefab;
	private GameObject diffcultPrefab;
	// Use this for initialization
	void Start () {
		enemy = GameObject.FindWithTag("enemy").transform;
		prefab = ResourcesManager.Instance.LoadBullet("smallBullet_red");
		diffcultPrefab = ResourcesManager.Instance.LoadBullet("greenSmallBullet");
	}

	// Update is called once per frame
	void Update () {
		
	}

	

	public void UseThisNotSpeicalCard_Diffcult()
	{
		StartCoroutine(BoomOfHandDiffcult());
	}

	public void UseThisNotSpeicalCard_Easy()
	{
		StartCoroutine(BoomOfHandEasy());
	}

	private IEnumerator BoomOfHandDiffcult()
	{
		int sign = 1;


		for (int i = 0; i < 2; i++)
		{

			sign = sign * -1;

			StartCoroutine(UseThisNotSpeicalCardDiffcult_IEnumerator(sign));
			yield return new WaitForSeconds(5);
		}


	}


	IEnumerator UseThisNotSpeicalCardDiffcult_IEnumerator(int sign)
	{
		float radius = 0.5f;
		float deltaAngle = (-25 * sign);
		float angle = 0;

		float tempLerp = 0;

		int count = 20;
		float delayTime = 0.05f;
		float deltaLerp = (1.0F / count);
		for (int i = 0; i < 20; i++)
		{
			yield return new WaitForSeconds(delayTime);
			Vector3 boomVec = new Vector3(enemy.transform.position.x + (radius * Mathf.Cos(angle * Mathf.Deg2Rad)), enemy.transform.position.y, (enemy.transform.position.z + (radius * Mathf.Sin(angle * Mathf.Deg2Rad))));
			Boom(boomVec,diffcultPrefab);
			angle += deltaAngle;

			radius = Mathf.Lerp(0.5f, 6.0f, tempLerp);
			tempLerp += deltaLerp;
		}
	}

	private IEnumerator BoomOfHandEasy()
	{
		int sign = 1;
		
			
			for (int i = 0; i < 2; i++)
			{
				
				sign = sign * -1;

				StartCoroutine(UseThisNotSpeicalCard_IEnumerator(sign));
			yield return new WaitForSeconds(5);
		}
		
	
	}


	IEnumerator UseThisNotSpeicalCard_IEnumerator(int sign)
	{
		float radius = 0.5f;
		float deltaAngle = (-25* sign);
		float angle = 0;

		float tempLerp = 0;

		int count = 20;
		float delayTime = 0.05f;
		float deltaLerp = (1.0F / count);
		for (int i = 0; i < 20; i++)
		{
			yield return new WaitForSeconds(delayTime);
			Vector3 boomVec = new Vector3(enemy.transform.position.x + (radius* Mathf.Cos(angle * Mathf.Deg2Rad)), enemy.transform.position.y, (enemy.transform.position.z + (radius*Mathf.Sin(angle * Mathf.Deg2Rad))));
			Boom(boomVec,prefab);
			angle += deltaAngle;
			
			radius=Mathf.Lerp(0.5f, 6.0f, tempLerp);
			tempLerp += deltaLerp;
		}
	}
	private void Boom(Vector3 boomVec,GameObject prefab)
	{
		int count = 32;
		float tempAngle = 360 / count;
		for (int i = 0; i < count; i++)
		{
			GameObject go = GameObject.Instantiate(prefab);
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
}
