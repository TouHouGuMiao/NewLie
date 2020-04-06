using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlayerMouse : MonoBehaviour
{
	public float speed = 0;
	public float delayTime = 0;
	private GameObject enemy;
	// Use this for initialization
	void Start () {
		enemy = GameObject.FindWithTag("enemy");
		StartCoroutine(Lock());
	}

	IEnumerator Lock()
	{
		yield return new WaitForSeconds(delayTime);
	
		Vector3 mouseVec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, (Mathf.Abs(Camera.main.transform.position.y - enemy.transform.position.y))));

		Vector3 targetVec = mouseVec - transform.position;
		float angle = Mathf.Atan2(targetVec.z, targetVec.x) ;
		BulletBaseComponent bbc = gameObject.GetComponent<BulletBaseComponent>();
		bbc.speed = speed;
		bbc.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));

		bbc.SetVelocity();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
