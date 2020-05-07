using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeFire : MonoBehaviour {
	// Use this for initialization

	private Vector3 initPos;
	public bool isClone = false;
	void Start()
	{
		if (isClone)
		{
			return;
		}
		BulletBaseComponent bbc = gameObject.GetComponent<BulletBaseComponent>();
		bbc.OnBulletInitFished.Add(new EventDelegate(Fire));
		initPos = transform.localPosition;
	}
	System.Random rd = new System.Random();
	private void Fire()
	{
		StartCoroutine(FireIEnumerator());
	}

	private IEnumerator FireIEnumerator()
	{
		CardBase data = transform.GetComponent<BulletBaseComponent>().cardBase;
		for (int i = 0; i < 2; i++)
		{
			GameObject go = GameObject.Instantiate(transform.gameObject);
			go.GetComponent<FiveFire>().isClone = true;
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			bbc.cardBase = data;
			go.transform.SetParent(GameObject.FindWithTag("Bullet").gameObject.transform, false);
			go.transform.localPosition = initPos;
			bbc.OnBulletInitFished.Clear();
			Vector3 rotationVec = transform.rotation.eulerAngles;
			rotationVec += new Vector3(0, rd.Next(-25, 25), 0);
			go.transform.rotation = Quaternion.Euler(rotationVec);
			bbc.OnBulletInitFished.Clear();
			float angle = -(go.transform.eulerAngles.y) * Mathf.Deg2Rad;
			Rigidbody rgb = go.GetComponent<Rigidbody>();
			rgb.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
			rgb.mass = data.mass;
			bbc.speed = data.speed;
			bbc.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
			bbc.power = data.power;
			yield return new WaitForSeconds(0.1f);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
