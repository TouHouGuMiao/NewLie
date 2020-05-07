using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LingFuKuBullet : MonoBehaviour {
	private Rigidbody rgb;
	public CardBase data;
	public Animator animator;
	// Use this for initialization
	void Start () {
		rgb = gameObject.GetComponent<Rigidbody>();
		animator = transform.GetComponent<Animator>();
		animator.speed = RoundRule.Instance.animaotrSpeed;
		Vector3 tempVec= (GameObject.FindWithTag("enemy").transform.position - transform.position).normalized;

		float angle = Mathf.Atan2(tempVec.z, tempVec.x);
		angle = Mathf.Rad2Deg * angle;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire()
	{
        gameObject.AddComponent<BulletBaseComponent>().cardBase =data;
		BulletBaseComponent bbc = gameObject.GetComponent<BulletBaseComponent>();
		bbc.cardBase = data;
		float angle = -(transform.eulerAngles.y) * Mathf.Deg2Rad;
		rgb.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
		rgb.mass = data.mass;
		bbc.speed = data.speed;
		bbc.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
		bbc.power = data.power;
	}
}
