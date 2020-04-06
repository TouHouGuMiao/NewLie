using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondJieJie : SpeicalCardBase
{
	private GameObject player;
	private bool IsGetNewBulletList = false;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		base.Update();
		if (!isOn)
			TogatherWithMouse();
		WhenStateChange();
	}

	void TogatherWithMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.y - player.transform.position.y)));
		transform.localPosition = worldPos;
	}


	void WhenStateChange()
	{
		if (lastState != state)
		{
			if (lastState == SpeicalCardState.init && state == SpeicalCardState.on)
			{
				StartCoroutine(BulletSpeedUp());
				//lastState = SpeicalCardState.on;
			}
		}
	}

	IEnumerator BulletSpeedUp()
	{
		IsGetNewBulletList = true;
		yield return new WaitForSeconds(0.1f);
		IsGetNewBulletList = false;
		for (int i = 0; i < list.Count; i++)
		{
			BulletBaseComponent bbc = list[i].GetComponent<BulletBaseComponent>();
			bbc.speed += 1;
			bbc.SetVelocity();
		}
		list.Clear();

		StartCoroutine(BulletSpeedUp());

	}

	private List<GameObject> list = new List<GameObject>();


	 public void OnTriggerStay(Collider collider)
	{
		base.OnTriggerStay(collider);
		if (!isOn)
		{
			return;
		}
		if(collider.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
		{
			BulletBaseComponent bbc = collider.gameObject.GetComponent<BulletBaseComponent>();
			bbc.speed += 5;
			bbc.SetVelocity();
		}
		

		//if (IsGetNewBulletList)

		//	if (!list.Contains(collider.gameObject))
		//		list.Add(collider.gameObject);
	}
}

