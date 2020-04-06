using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BullteType
{
	NormalBullet,
}
public enum BulletState
{
	nomal,
	reduceTime,
}
public class PlayerBattleRule : MonoBehaviour {
	public float timeScale=1;
	public BulletState bulletState = BulletState.nomal;
	private GameObject player;
	private Animator animator;
	public GameObject arrowHead;
	private BullteType bullteType;
	private Transform firePoint;
	private GameObject bulletPrefab;
	public GameObject bulletParent;
	public float speed = 500;

	public static PlayerBattleRule Instance;
	public Vector3 targetVec;
	// Use this for initialization

    void Awake()
	{
		Instance = this;
	}
	void Start () {

		player = this.gameObject;
		animator = this.GetComponent<Animator>();
		firePoint = gameObject.transform.Find("firePoint");
	}

	public bool IsReduceTime()
	{
		if(bulletState== BulletState.nomal)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	// Update is called once per frame
	private bool isUseSkill = false;
	private bool isUseA=false;
	void Update () {
		if(bulletState == BulletState.nomal)
		{
			timeScale = 1;
		}
		else if(bulletState == BulletState.reduceTime)
		{
			timeScale = 0.1f;
		}
		//if (!isUseSkill)
		//{
		//	Vector3 mouseVec = Input.mousePosition;
		//	lastMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseVec.x, mouseVec.y, Mathf.Abs(Camera.main.transform.position.z)));
		//	//if (Input.GetKeyDown(KeyCode.Q))
		//	//{
		//	//	isUseA = true;
		//	//	isUseSkill = true;
		//	//	bullteType = BullteType.NormalBullet;
		//	//	bulletPrefab = ResourcesManager.Instance.LoadBullet("initBullet");
		//	//	arrowHead.transform.localScale = new Vector3(0.3f, 0.4F, 0.4F);
		//	//	ShowSkillArrowHead(firePoint.position);
		//	//}
		//}

		//else
		//{
			if (arrowHead.activeSelf)
			{
				UpdateArrowAroundTarget();
			}

			//if (Input.GetMouseButtonDown(0))
			//{
			//	StopAllCoroutines();
			//	bulletState = BulletState.nomal;
			//	FromReduceTimeStateToNomalState();
			//	if (isUseA)
			//	{
			//		StartUseBoom();
			//	}
			//	isUseSkill = false;
			//	isUseA = false;
			//	arrowHead.SetActive(false);

				
			//}

		//	if (Input.GetMouseButtonDown(1))
		//	{
		//		StopAllCoroutines();
		//		bulletState = BulletState.nomal;
		//		FromReduceTimeStateToNomalState();
		//		isUseSkill = false;
		//		isUseA = false;
		//		arrowHead.SetActive(false);				
		//	}
		//}
	}

	private void FromReduceTimeStateToNomalState()
	{
		for (int i = 0; i < bulletParent.transform.childCount; i++)
		{
			GameObject go = bulletParent.transform.GetChild(i).gameObject;
			Rigidbody rb = go.GetComponent<Rigidbody>();
			rb.drag = 0;

			BulletBaseComponent cts = go.GetComponent<BulletBaseComponent>();
			if (cts != null)
			{
				Vector3 velocity = cts.velocity;
				rb.velocity = velocity;
			}	
		}
		Time.timeScale = 1;
		timeScale = 1;
	}

	public void FromReudceToFly(GameObject go)
	{
		StopAllCoroutines();
		Rigidbody rb = go.GetComponent<Rigidbody>();
		rb.drag = 0;

		BulletBaseComponent cts = go.GetComponent<BulletBaseComponent>();
		if (cts != null)
		{
			Vector3 velocity = cts.velocity;
			rb.velocity = velocity;
		}
	}

	/// <summary>
	/// 时刻跟随鼠标更新箭头指向
	/// </summary>
	/// <param name="targetVec"></param>
	private void UpdateArrowAroundTarget()
	{
		float radius = 1.0F;
		Vector3 mouseVec = Input.mousePosition;
		Vector3 tempVec = Camera.main.ScreenToWorldPoint(new Vector3(mouseVec.x, mouseVec.y, Mathf.Abs(Camera.main.transform.position.y-player.transform.position.y)));
		//UpdateArrowScale();
		//Vector3 vecPos = transform.InverseTransformPoint(tempVec);
		Vector3 vecPos = targetVec - tempVec;
		float angle = Mathf.Atan2(-vecPos.z, -vecPos.x);
		float angleZ = angle * Mathf.Rad2Deg;
		Vector3 rotateVec = arrowHead.transform.rotation.eulerAngles;
		if (rotateVec.x > 180)
		{
			rotateVec.x = rotateVec.x - 360;
		}
		if (rotateVec.y > 180)
		{
			rotateVec.y = rotateVec.y - 360;
		}
		arrowHead.transform.rotation = Quaternion.Euler(90, -angleZ,0 );
		arrowHead.transform.position = new Vector3(targetVec.x + (radius * Mathf.Cos(angle)), targetVec.y, targetVec.z + (radius * Mathf.Sin(angle)));
	}

	Vector3 lastMousePos=Vector3.zero;
	/// <summary>
	/// 时刻改变箭头长度
	/// </summary>
	private void UpdateArrowScale()
	{
		Vector3 mouseVec = Input.mousePosition;
		Vector3 mouseWolrdVec = Camera.main.ScreenToWorldPoint(new Vector3(mouseVec.x, mouseVec.y, Mathf.Abs(Camera.main.transform.position.z)));
		if(lastMousePos == mouseWolrdVec)
		{
			return;
		}
		Vector3 forwardVec = mouseWolrdVec - lastMousePos;
		
		Vector3 needVec = arrowHead.transform.InverseTransformDirection(forwardVec);
		float scaleDepentValue = needVec.sqrMagnitude;
		if (needVec.x >= 0)
		{
			scaleDepentValue = Mathf.Abs(scaleDepentValue);
		}
		else
		{
			scaleDepentValue = -scaleDepentValue;
		}
		if (scaleDepentValue > 0.1f)
		{
			scaleDepentValue = 0.1f;
		}
		if (scaleDepentValue < -0.1f)
		{
			scaleDepentValue = -0.1f;
		}
	    Vector3 arrowVec =	arrowHead.transform.localScale;
		arrowVec = arrowVec+ new Vector3(scaleDepentValue, 0, 0);
		if (arrowVec.x >=1.0f)
		{
			arrowVec.x = 1.0f;
		}
		else if(arrowVec.x<=0.15f)
		{
			arrowVec.x = 0.15f;
		}
		arrowHead.transform.localScale = arrowVec;
		lastMousePos = mouseWolrdVec;


	}
	/// <summary>
	/// 减缓弹幕速度
	/// </summary>
	public void ReduceTime()
	{
		bulletState = BulletState.reduceTime;
		Time.timeScale = 0.1f;
		StartCoroutine(LerpReduceTime());
	}

	IEnumerator LerpReduceTime()
	{
		float lerpTemp = 0;

		for (int i = 0; i < 50; i++)
		{
			lerpTemp += 0.02f;
			for (int j = 0; j < bulletParent.transform.childCount; j++)
			{
				GameObject go = bulletParent.transform.GetChild(j).gameObject;
				Rigidbody rb = go.GetComponent<Rigidbody>();
				rb.drag = Mathf.Lerp(10, 0, lerpTemp);
			}
			yield return new WaitForSeconds(0.01f);
			
		}
	}

	public void ReduceTime_ForNewBullet(GameObject go)
	{
		StartCoroutine(LerpReduceTime_ForNewBullet(go));
	}

	private IEnumerator LerpReduceTime_ForNewBullet(GameObject go)
	{
		float lerpTemp = 0;

		for (int i = 0; i < 50; i++)
		{
			lerpTemp += 0.02f;
			Rigidbody rb = go.GetComponent<Rigidbody>();
			rb.drag = Mathf.Lerp(10, 0, lerpTemp);

			yield return new WaitForSeconds(0.01f);
			
		}
	}

	public Vector3 ReturnArrowHandRotate()
	{
		Vector3 euler = arrowHead.transform.rotation.eulerAngles;
		if (euler.x > 180)
		{
			euler.x = euler.x - 360;
		}
		if (euler.y > 180)
		{
			euler.y = euler.y - 360;
		}

		if (euler.z > 180)
		{
			euler.z = euler.z - 360;
		}

		return euler;
	}

	/// <summary>
	/// 显示箭头
	/// </summary>
	/// <param name="pointVec"></param>
	public void ShowSkillArrowHead(Vector3 pointVec)
	{
		targetVec = pointVec;
		arrowHead.transform.localPosition = pointVec;
		arrowHead.SetActive(true);

		timeScale = 0.1f;
	
	}

	/// <summary>
	/// 使用普通弹幕（平A）
	/// </summary>
	/// 
	private void StartUseA ()
	{
		GameObject go = GameObject.Instantiate(bulletPrefab);
		go.transform.SetParent(bulletParent.transform, false);
		go.transform.position = arrowHead.transform.position;
		float angleZ = arrowHead.transform.rotation.eulerAngles.z;
		go.transform.rotation = Quaternion.Euler(0, 0, angleZ);
		Rigidbody rb = go.GetComponent<Rigidbody>();
		Vector3 angleVec = transform.InverseTransformPoint(arrowHead.transform.position);
		
		go.AddComponent<CanTouchScreen>();
		go.AddComponent<BulletBaseComponent>();
		BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
		float angle = Mathf.Atan2(angleVec.y, angleVec.x);
		bbc.speed = speed;
		rb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0 , bbc.speed * Mathf.Sin(angle));
		bbc.velocity = rb.velocity;
		bbc.power = 5;
	}

	private void StartUseBoom()
	{
		GameObject prefab = ResourcesManager.Instance.LoadBullet("RedBoomBullet");
		GameObject go = GameObject.Instantiate(prefab);
		go.transform.SetParent(bulletParent.transform, false);
		go.transform.position = arrowHead.transform.position;
		float angleZ = arrowHead.transform.rotation.eulerAngles.z;
		go.transform.rotation = Quaternion.Euler(90, angleZ, 0);
		Rigidbody rb = go.GetComponent<Rigidbody>();
		Vector3 angleVec = transform.InverseTransformPoint(arrowHead.transform.position);
		go.AddComponent<BulletBaseComponent>();
		BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
		float angle = Mathf.Atan2(angleVec.y, angleVec.x);
		bbc.speed = 10;
		rb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), 0, bbc.speed * Mathf.Sin(angle));
		bbc.velocity = rb.velocity;
		bbc.power = 15;
	}

	private void StartUseSnipe()
	{
		GameObject prefab = ResourcesManager.Instance.LoadBullet("Lock");
		GameObject go = GameObject.Instantiate(prefab);
		go.transform.SetParent(bulletParent.transform, false);
		go.transform.position = arrowHead.transform.position;
		float angleZ = arrowHead.transform.rotation.eulerAngles.z;
		go.transform.rotation = Quaternion.Euler(0, 0, angleZ);
	
		Rigidbody rb = go.GetComponent<Rigidbody>();
		Vector3 angleVec = transform.InverseTransformPoint(arrowHead.transform.position);
		go.AddComponent<BulletBaseComponent>();
		BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
		float angle = Mathf.Atan2(angleVec.y, angleVec.x);
		bbc.speed = 20;
		rb.velocity = new Vector3(bbc.speed * Mathf.Cos(angle), bbc.speed * Mathf.Sin(angle), 0);
		bbc.velocity = rb.velocity;
		bbc.power = 20;
	}

	
		 




	private void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
		{
			BulletBaseComponent bbc = collider.gameObject.GetComponent<BulletBaseComponent>();
			float value = bbc.power;
			PlayerBattlePanel.UpdateHPSlider(-value);
			bbc.isDead = true;
		}
	}


}
