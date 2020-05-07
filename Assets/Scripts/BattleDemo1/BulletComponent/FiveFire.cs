using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiveFire : MonoBehaviour {
	// Use this for initialization

	private Vector3 initPos;
	public bool isClone = false;
	private CardBase data;
	void Start () {
		if (isClone)
		{
			return;
		}
		BulletBaseComponent bbc=gameObject.GetComponent<BulletBaseComponent>();
		bbc.OnBulletInitFished.Add(new EventDelegate(Fire));
		initPos = transform.localPosition;
		data = bbc.cardBase;
	}
	System.Random rd = new System.Random();
	private void Fire()
	{
		StartCoroutine(FireIEnumerator());
	}

	private IEnumerator FireIEnumerator()
	{
		for (int i = 0; i < 10; i++)
		{
			GameObject go = GameObject.Instantiate(transform.gameObject);
			go.GetComponent<FiveFire>().isClone = true ;
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			bbc.cardBase = data;
			go.transform.SetParent(GameObject.FindWithTag("Bullet").gameObject.transform, false);
			go.transform.localPosition = initPos;
			bbc.OnBulletInitFished.Clear();
			Vector3 rotationVec = transform.rotation.eulerAngles;
			rotationVec += new Vector3(0, rd.Next(-50, 50), 0);
			go.transform.rotation = Quaternion.Euler(rotationVec);
			bbc.OnBulletInitFished.Clear();
			float angle = -(go.transform.eulerAngles.y) * Mathf.Deg2Rad;
			Rigidbody rgb = go.GetComponent<Rigidbody>();
			rgb.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
			rgb.mass = data.mass;
			bbc.speed = data.speed;
			bbc.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
			bbc.power = data.power;
			yield return new WaitForSeconds(0.01f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
