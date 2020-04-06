using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakShotBullet : MonoBehaviour {

	public int shotCount;
	public float rateAngle;
	private Vector3 center;
	private float radius = 0.5f;
	private Transform bulletParent;
	private bool isShot=false;
	private BulletBaseComponent bbc;
	private bool ShotFished = false;
	void Awake()
	{
		bulletParent = GameObject.FindWithTag("Bullet").transform;
		
	
	}
	void Start () {
		bbc = this.GetComponent<BulletBaseComponent>();
	}
	
	void Shot()
	{
		int count = transform.childCount;
		for (int i = 0; i < count; i++)
		{
			GameObject child = transform.GetChild(0).gameObject;
			child.transform.SetParent(bulletParent, true);
			BulletBaseComponent childBBC = child.GetComponent<BulletBaseComponent>();
			float angle = -(child.transform.eulerAngles.y) * Mathf.Deg2Rad;
			Rigidbody rgb = child.GetComponent<Rigidbody>();
			rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
			rgb.mass = transform.GetComponent<Rigidbody>().mass;
			
			childBBC.speed = bbc.speed;
			childBBC.power = bbc.power;
			childBBC.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
		}
		ShotFished = true;
	}

	// Update is called once per frame
	void Update () {
		if(transform.GetComponent<Rigidbody>().velocity.magnitude>0){
			isShot = true;
		}

		if (isShot&&!ShotFished)
		{
			Shot();
		}
	}
}
