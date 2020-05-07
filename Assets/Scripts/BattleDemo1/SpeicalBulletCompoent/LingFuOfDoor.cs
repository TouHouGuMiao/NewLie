using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LingFuOfDoor : MonoBehaviour {
	public float delay=0.5f;
	private GameObject prefab;
	// Use this for initialization
	void Start () {
		prefab = ResourcesManager.Instance.LoadBullet("lingFuKuBullet");
		
	}	

	public void StartFire()
	{
		StartCoroutine(Fire());
	}

    public void StopFire()
	{
		StopAllCoroutines();
	}	
	private IEnumerator Fire()
	{
		CardBase data = RoundRule.Instance.GetDateFormLingFuKu();
		GameObject go = Instantiate(prefab);
		go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
		go.transform.position = transform.GetChild(0).transform.position;
		LingFuKuBullet lfb = go.GetComponent<LingFuKuBullet>();
		lfb.data = data;
		yield return new WaitForSeconds(delay);
		StartCoroutine(Fire());
	}

	// Update is called once per frame
	void Update () {
		
	}
}
