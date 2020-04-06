using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour {
	private GameObject fireEffect;
	private BulletBaseComponent bbc;
	// Use this for initialization
	void Start () {
		bbc=this.GetComponent<BulletBaseComponent>();
		StartFire();
	}
	
	// Update is called once per frame
	void Update () {
		if (bbc.isDead)
		{
			StopAllCoroutines();
		}
	}

	private void StartFire()
	{
		GameObject prefab = ResourcesManager.Instance.LoadEffect("fireEffect");
		fireEffect = GameObject.Instantiate(prefab);
		fireEffect.transform.SetParent(gameObject.transform, false);
	}

	public void Light()
	{
		GameObject prefab = ResourcesManager.Instance.LoadEffect("fireEffect");
		fireEffect = GameObject.Instantiate(prefab);
		fireEffect.transform.SetParent(gameObject.transform, false);
		LightHarm();
	}

	private void LightHarm()
	{
		StartCoroutine(LightHarmIEnumerator());
	}

	IEnumerator LightHarmIEnumerator()
	{
		yield return new WaitForSeconds(0.5f);
		bbc.power -= 2.0f;
		Debug.LogError(bbc.power);
		StartCoroutine(LightHarmIEnumerator());
	}

	private void OnCollisionEnter(Collision collider)
	{
		FireBullet fb = collider.gameObject.GetComponent<FireBullet>();
		if (fb == null)
		{

			collider.gameObject.AddComponent<FireBullet>();
			collider.gameObject.GetComponent<FireBullet>().Light();
		}
	}

	private void OnTriggerEnter(Collider collider)
	{

		if(collider.gameObject.layer == this.gameObject.layer)
		{
			collider.gameObject.AddComponent<FireBullet>();
			collider.gameObject.GetComponent<FireBullet>().Light();
		}
	}

}
