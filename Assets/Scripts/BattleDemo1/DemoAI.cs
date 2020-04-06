using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAI : MonoBehaviour {
	private GameObject prefab;
	public Transform point;
	public float speed=-5;
	private BoomOfRose boomOfRose;
	private Self self;
	private SuperEgo superEgo;

	private bool zhiXianBulletsOver = true;
	private BoomOfHead boh;

	private Carousel carousel;
	private FatherDream fatherDream;

	// Use this for initialization
	void Start () {
		boh = gameObject.AddComponent<BoomOfHead>();
		carousel = gameObject.transform.Find("carousel").GetComponent<Carousel>();
		fatherDream = gameObject.AddComponent<FatherDream>();
		self = gameObject.AddComponent<Self>();
		superEgo = gameObject.AddComponent<SuperEgo>();
		boomOfRose = gameObject.AddComponent<BoomOfRose>();
		//StartTestAI();
	}

	public void UseSuperEgo()
	{
		superEgo.Shot();
	
	}

	public void UseSelf()
	{
		self.StartShot();
	}
	public void UseBoomOfHeadAndCarousl()
	{
		boh.UseThisNotSpeicalCard_Easy();
		carousel.Shot_Diffcult();
	}

	public void UseBoomOfRose()
	{
		boomOfRose.Shot();
	}
	public void UseDiffcultBoomOfHead()
	{
		boh.UseThisNotSpeicalCard_Diffcult();
	}


	public void UseBoomOfHeadEasy()
	{
		boh.UseThisNotSpeicalCard_Easy();
		
	}

	public void UseCarouselNormal()
	{
		carousel.Shot_Easy();
	}

	public void UseFatherDream()
	{
		fatherDream.UseFatherDream();
	}


	public void UseCarouselDiffcult()
	{
		carousel.Shot_Diffcult();
	}
	// Update is called once per frame
	void Update () {
		
	}

	int lineBulletIndex = 0;
	void ShowLineBullets()
	{
		lineBulletIndex = 0;
		zhiXianBulletsOver = false;
		StartCoroutine(ShowLineBulletsIEnumerator());
	}
	/// <summary>
	/// 正常情况下的直线弹幕发射
	/// </summary>
	/// <returns></returns>
	IEnumerator ShowLineBulletsIEnumerator()
	{
		GameObject prefab = ResourcesManager.Instance.LoadBullet("zhiXianBullet");
		for (lineBulletIndex=0; lineBulletIndex < 5; lineBulletIndex++)
		{
			yield return new WaitForSeconds(0.8f);
			if (!NotReduceTime())
			{
				StartCoroutine(ShowLineBulletsWhenReduceTime());
			}
			yield return new WaitUntil(NotReduceTime);
			GameObject go = Instantiate(prefab);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
			go.transform.position = point.position;
			Rigidbody rb = go.GetComponent<Rigidbody>();
			rb.velocity = new Vector3(speed, 0, 0);
			//go.AddComponent<CanTouchScreen>();
			go.AddComponent<BulletBaseComponent>();
			go.AddComponent<CanTouchScreen>();
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			bbc.power = 10;
			bbc.velocity = rb.velocity;
			bbc.speed = speed;
		}
			zhiXianBulletsOver = true;
	}
	/// <summary>
	/// 减速时的直线弹幕发射
	/// </summary>
	/// <returns></returns>
	IEnumerator ShowLineBulletsWhenReduceTime()
	{
		GameObject prefab = ResourcesManager.Instance.LoadBullet("zhiXianBullet");
		for (; lineBulletIndex < 5; lineBulletIndex++)
		{
			yield return new WaitForSeconds(0.8f/PlayerBattleRule.Instance.timeScale);
			if (NotReduceTime())
			{				
				yield break;
			}
			GameObject go = Instantiate(prefab);
			go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
			go.transform.position = point.position;
			Rigidbody rb = go.GetComponent<Rigidbody>();
			rb.velocity = new Vector3(speed, 0, 0);
			//go.AddComponent<CanTouchScreen>();
			go.AddComponent<BulletBaseComponent>();
			go.AddComponent<CanTouchScreen>();
			BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
			bbc.velocity = rb.velocity;
			bbc.speed = Mathf.Abs(speed);
			bbc.power = 10;
			PlayerBattleRule.Instance.ReduceTime_ForNewBullet(go);
		}
		zhiXianBulletsOver = true;
	}

	bool NotReduceTime()
	{
		if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	/// <summary>
	/// 不同发射角度（同一圆上）的弹幕
	/// </summary>
	void ShowSpheresFourBullets(float bulletSpeed)
	{
		float angle = 120;		
		GameObject prefab = ResourcesManager.Instance.LoadBullet("StarBullet");
		if (NotReduceTime())
		{
			for (int i = 0; i < 4; i++)
			{
				GameObject go = GameObject.Instantiate(prefab);
				go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
				go.transform.position = point.position;
				Rigidbody rgb = go.GetComponent<Rigidbody>();
				rgb.velocity = -new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * bulletSpeed;
				angle += 30;
				go.AddComponent<BulletBaseComponent>();
				BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
				bbc.speed = Mathf.Abs(bulletSpeed);
				bbc.velocity = rgb.velocity;
				bbc.power = 1;
				go.AddComponent<CanTouchScreen>();
			}
		}
		
		else
		{
			for (int i = 0; i < 4; i++)
			{
				GameObject go = GameObject.Instantiate(prefab);
				go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, false);
				go.transform.position = point.position;
				Rigidbody rgb = go.GetComponent<Rigidbody>();
				rgb.velocity = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * bulletSpeed;
				angle += 30;
				go.AddComponent<BulletBaseComponent>();
				BulletBaseComponent bbc = go.GetComponent<BulletBaseComponent>();
				bbc.speed = Mathf.Abs(speed);
				PlayerBattleRule.Instance.ReduceTime_ForNewBullet(go);
				bbc.speed = Mathf.Abs(bulletSpeed);
				bbc.velocity = rgb.velocity;
				bbc.power = 5;
				go.AddComponent<CanTouchScreen>();
			}
		}
	}

	/// <summary>
	/// 开始AI测试
	/// </summary>
	void StartTestAI()
	{
		StartCoroutine(AIDontThinkEnumerator());
	}

	/// <summary>
	/// 无须思考，AI无意识动作
	/// </summary>
	/// <returns></returns>
	IEnumerator AIDontThinkEnumerator()
	{
		yield return new WaitForSeconds(1);
		//StartCoroutine(UseZhiXianBulletsIEnumerator());
		StartCoroutine(UseSphereZhiXianBulletsIEnumerator());
	}


	IEnumerator UseZhiXianBulletsIEnumerator()
	{
		yield return new WaitUntil(ZhiXianBulletsIsOver);
		ShowLineBullets();
		float radom = Random.Range(13, 16);
		yield return new WaitForSeconds(radom);
		StartCoroutine(UseZhiXianBulletsIEnumerator());
	}

	bool ZhiXianBulletsIsOver()
	{
		if (zhiXianBulletsOver)
		{
			return true;
		}
		else
		{
			return false;
		}
	}



	IEnumerator UseSphereZhiXianBulletsIEnumerator()
	{
		float radom_Sec = Random.Range(3f, 4f);
		if (!NotReduceTime())
		{
			overTime = Time.time + (radom_Sec/PlayerBattleRule.Instance.timeScale);
			yield return new WaitUntil(isOverTime);
		}
		else
		{
			yield return new WaitForSeconds(radom_Sec);
		}
		ShowSpheresFourBullets(7);
		StartCoroutine(UseSphereZhiXianBulletsIEnumerator());
	}

	private float overTime=0;
	/// <summary>
	/// 判断减缓的时间是否结束（同时只能给一个技能做判断）
	/// </summary>
	/// <returns></returns>
	bool isOverTime()
	{
	    if(Time.time>=overTime)
		{
			return true;
		}
		else
		{
			if (NotReduceTime())
			{
				return true;
			}
			return false;
		}
	}
}
