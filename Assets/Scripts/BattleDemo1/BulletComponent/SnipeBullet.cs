using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeBullet : MonoBehaviour
{
	private Transform target;
	private BulletBaseComponent bbc;
	private List<Transform> notSnipeList=new List<Transform> ();
	private bool snipeOver = false;
	void Start()
	{
		bbc = this.GetComponent<BulletBaseComponent>();
	    FindTarget();
	}

	// Update is called once per frame
	void Update()
	{
		if (bbc.isDead)
		{
			StopAllCoroutines();
		}
			
		
	}

	private void FindTarget()
	{
		if (bbc.isDead)
			return;
		Rigidbody rgb = this.GetComponent<Rigidbody>();
		rgb.velocity = Vector3.zero;
		Collider[] colliderList = Physics.OverlapSphere(transform.position, 100, 1<<LayerMask.NameToLayer("EnemyBullet"));
		if (colliderList.Length == 0)
		{
			bbc.isDead=true;
		}
		float distance = 0;
		distance = Mathf.Abs(Vector2.Distance(colliderList[0].transform.position, this.transform.position));
		GameObject target = null;
		for (int i = 0; i < colliderList.Length; i++)
		{
			if (colliderList[i].gameObject.GetComponent<BulletBaseComponent>().isDead)
			{
				continue;
			}
			bool notSnipe = false;
			for (int j = 0; j < notSnipeList.Count; j++)
			{
				
				if (colliderList[i].transform == notSnipeList[j])
				{
					notSnipe = true;
				}
			}
			if (notSnipe)
			{
				continue;
			}
			float tempDistance = Mathf.Abs(Vector2.Distance(colliderList[i].transform.position, transform.position));
			if (tempDistance <= distance)
			{
				target = colliderList[i].gameObject;
				distance = tempDistance;
			}
		}
		if (target != null)
		{
			this.target = target.transform;
		}
		else
		{
			this.target = null;
		}
		MoveToTarget();
	}

	IEnumerator MoveToTargetIEnumerator()
	{
		yield return new WaitForSeconds(0.05f);

		Rigidbody rgb = gameObject.GetComponent<Rigidbody>();
		if (target != null)
		{
			if (target.gameObject.GetComponent<BulletBaseComponent>().isDead||target==null)
			{
				StopAllCoroutines();
				FindTarget();
			}
			if (target == null)
			{
				rgb.velocity = bbc.velocity;
			}
			else
			{
				Vector3 targetVec = (target.transform.position - transform.position).normalized;
				float angle = Mathf.Atan2(targetVec.y, targetVec.x);

				rgb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), bbc.speed * Mathf.Sin(angle));
				bbc.velocity = rgb.velocity;
			}

		}
		else
		{
			rgb.velocity = bbc.velocity;
		}
		StartCoroutine(MoveToTargetIEnumerator());
	}
	private void MoveToTarget()
	{
		StartCoroutine(MoveToTargetIEnumerator());
	}

	private void OnCollisionEnter(Collision collider)
	{
		notSnipeList.Add(collider.transform);

	}
	void OnCollisionExit(Collision collider)
	{
		StopAllCoroutines();
		FindTarget();
	}

}
