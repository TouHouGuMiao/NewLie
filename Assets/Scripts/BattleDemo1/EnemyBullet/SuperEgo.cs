using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEgo : MonoBehaviour {
	private GameObject redHeadPrefab;
	private GameObject greenHeadPrefab;
	private GameObject blueHeadPrefab;
	private GameObject yellowPrefab;
	private GameObject enemy;
	private Transform bulletParent;
	private List<Vector3> pointList = new List<Vector3>();
	private SphereCollider collider;
	// Use this for initialization
	void Start () {
		
		bulletParent = GameObject.FindWithTag("Bullet").transform;
		greenHeadPrefab = ResourcesManager.Instance.LoadBullet("greenHead");
		yellowPrefab = ResourcesManager.Instance.LoadBullet("yellowHead");
		blueHeadPrefab = ResourcesManager.Instance.LoadBullet("blueHead");
		enemy = GameObject.FindWithTag("enemy");
		collider = enemy.transform.FindRecursively("chaoWo").GetComponent<SphereCollider>();
		float x1 = 3.73f;
		float x2 = 1.428f;
		float y = 14.12F;
		redHeadPrefab = ResourcesManager.Instance.LoadBullet("redHead");
		Vector3 point1 = new Vector3(enemy.transform.position.x - x1, -6.01f, 5.5f); ;
		Vector3 point2 = new Vector3(enemy.transform.position.x - x2, -6.01f,5.5f);
		Vector3 point3 = new Vector3(enemy.transform.position.x + x1, -6.01f, 5.5f); ;
		Vector3 point4 = new Vector3(enemy.transform.position.x + x2, -6.01f,5.5f);
		Vector3 point5 = new Vector3(enemy.transform.position.x - x1, -6.01f, 5f-y); ;
		Vector3 point6 = new Vector3(enemy.transform.position.x - x2, -6.01f, 5f-y);
		Vector3 point7 = new Vector3(enemy.transform.position.x + x1, -6.01f, 5f-y); ;
		Vector3 point8 = new Vector3(enemy.transform.position.x + x2, -6.01f, 5f-y);
		Vector3 point9 = new Vector3(enemy.transform.position.x - 8.46f, -6.01f, 5.93f);
		Vector3 point10 = new Vector3(enemy.transform.position.x + 8.46f, -6.01f,-9.05f);
		pointList.Add(point1);
		pointList.Add(point2);
		pointList.Add(point3);
		pointList.Add(point4);
		pointList.Add(point5);
		pointList.Add(point6);
		pointList.Add(point7);
		pointList.Add(point8);
		pointList.Add(point9);
		pointList.Add(point10);
		
	}

	public void Shot()
	{
		StartCoroutine(StartMoveToKoishi());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator StartMoveToKoishi()
	{
		int orderInLayer = 0;
		float aroundSpeed = 120;
		int count=0;
		for (int j = 0; j < 130; j++)
		{
		
			
			yield return new WaitForSeconds(0.15F);
			for (int i = 0; i < pointList.Count; i++)
			{
				count++;
				GameObject go = null;
				if (i == 0 || i == 4)
				{
					go = GameObject.Instantiate(redHeadPrefab);
				}
				else if (i == 1 || i == 5)
				{
					go = GameObject.Instantiate(blueHeadPrefab);
				}
				else if (i == 2 || i == 6)
				{
					go = GameObject.Instantiate(yellowPrefab);
				}
				else if (i == 3 || i == 7)
				{
					go = GameObject.Instantiate(greenHeadPrefab);
				}
				else if (i == 8)
				{
					go = GameObject.Instantiate(yellowPrefab);
				}

				else if (i == 9)
				{
					go = GameObject.Instantiate(greenHeadPrefab);
				}
				go.GetComponent<Rigidbody>().isKinematic = true;
				go.transform.SetParent(bulletParent, false);
				go.transform.position = pointList[i];
				BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
				
				bbc.rotateWithArrow = false;

				MoveAndAround maa = go.AddComponent<MoveAndAround>();
				if (i == 0 || i == 4)
				{
					maa.rotate = 42.5f;
					maa.aroundSgin = 1;
					if (i == 4)
					{
						maa.rotate = -42.5f;
						maa.aroundSgin = -1;
						go.transform.rotation = Quaternion.Euler(90, -90, 0);
					}
					else
					{
						go.transform.rotation = Quaternion.Euler(90, 90, 0);
					}
					
				}
				else if (i == 1 || i == 5)
				{
					maa.rotate = -2.5f;
					maa.aroundSgin = -1;
					if (i == 5)
					{
						maa.aroundSgin = 1;
						maa.rotate = 2.5f;
						go.transform.rotation = Quaternion.Euler(90, -90, 0);
					}
					else
					{
						go.transform.rotation = Quaternion.Euler(90, 90, 0);
					}
				}

				else if (i == 2 || i == 6)
				{
					maa.rotate = -42.5f;
					maa.aroundSgin = -1;
					if (i == 6)
					{
						maa.aroundSgin = 1;
						maa.rotate = 42.5f;
						go.transform.rotation = Quaternion.Euler(90, -90, 0);
					}
					else
					{
						go.transform.rotation = Quaternion.Euler(90, 90, 0);
					}
				}
				else if (i == 3 || i == 7)
				{
					maa.rotate = 2.5f;
					maa.aroundSgin = 1;
					if (i == 7)
					{
						maa.rotate = -2.5f;
						maa.aroundSgin = -1;
						go.transform.rotation = Quaternion.Euler(90, -90, 0);
					}
					else
					{
						go.transform.rotation = Quaternion.Euler(90, 90, 0);
					}
				}
				else if (i == 8 || i == 9)
				{
					maa.rotate = 45f;
					maa.aroundSgin = 1;
					if (i == 9)
					{
						
						go.transform.rotation = Quaternion.Euler(90, -90, 0);
					}
					else
					{
						go.transform.rotation = Quaternion.Euler(90, 90, 0);
					}
				}
				bbc.power = 1000;
				bbc.notTouch = true;
				SpriteRenderer render = go.GetComponent<SpriteRenderer>();
				render.sortingOrder = orderInLayer;

				orderInLayer++;
				maa.aroundSpeed = aroundSpeed;
				collider.radius += 0.001f;
				aroundSpeed += 0.01f;
			}
			
		}
		
	}
}
