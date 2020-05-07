using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 先祖之梦 锁定弹幕
/// </summary>
public class BlueZhiXianBullet : MonoBehaviour {
	public Transform point;
	private GameObject prefab;
	private Vector3 targetVec;
	private Transform enemy;
	private Transform bulletParent;
	// Use this for initialization
	void Start () {
		prefab = ResourcesManager.Instance.LoadBullet("BlueGuangBullet");
		enemy = GameObject.FindWithTag("enemy").transform;
		bulletParent = GameObject.FindWithTag("Bullet").transform;
		StartCoroutine(FindTarget());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator FindTarget()
	{
		yield return new WaitForSeconds(0.2f);
		//Collider[] colliderList = Physics.OverlapSphere(enemy.transform.position, 30, 1 << LayerMask.NameToLayer("PlayerBullet"));
		//float tempDistance = 0;
		//GameObject tempGo = null;
		//for (int i = 0; i < colliderList.Length; i++)
		//{
		//	GameObject go = colliderList[i].gameObject;
		//	BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
		//	if (bbc == null)
		//	{
		//		continue;
		//	}
		//	if (bbc.isDead)
		//	{
		//		continue;
		//	}
		//	float distance = Mathf.Abs(Vector3.Distance(go.transform.position, point.transform.position));
		//	if (tempDistance == 0)
		//	{
		//		tempDistance = distance;
		//		continue;
		//	}
		//	if (distance < tempDistance)
		//	{
		//		tempGo = go;
		//		tempDistance = distance;
		//	}
		//}
		//if (tempGo == null)
		//{
		//	tempGo = GameObject.FindWithTag("Player");
		//}
		Vector3 mouseVec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, (Mathf.Abs(Camera.main.transform.position.y - enemy.transform.position.y))));
		
		targetVec = mouseVec - point.transform.position;
		StartCoroutine(ShotIEnumerator());
	}


	IEnumerator ShotIEnumerator()
	{
		for (int i = 0; i < 2; i++)
		{
			yield return new WaitForSeconds(0.3f);
			GameObject go = GameObject.Instantiate(prefab);
			go.transform.SetParent(bulletParent, false);
			go.transform.position = point.transform.position;
			go.layer = LayerMask.NameToLayer("EnemyBullet");
			go.transform.SetChildLayer(LayerMask.NameToLayer("EnemyBullet"));
			go.AddComponent<TestDragComopment>();
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			float angle = Mathf.Atan2(targetVec.z, targetVec.x);
			bbc.speed = 2;
			bbc.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
			bbc.power = 1;
			Rigidbody rgb = go.GetComponent<Rigidbody>();
			rgb.mass = 20;
			bbc.SetVelocity();
		}
	}
	
}
