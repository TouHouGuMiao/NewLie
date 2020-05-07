using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
/// <summary>
/// 弹幕基本功能组件
/// PS：不作为基类
/// </summary>
public class BulletBaseComponent : MonoBehaviour
{
	public Vector3 velocity;
	public float power = 1;
	public bool notTouch = false;
	public bool isDead = false;
	public float speed;
	public bool rotateWithArrow=true;
	private Rigidbody rgb;
	private GameObject pValuePrefab;
	public static BulletBaseComponent current;
	public bool deadByChainBoom=false;
	private float leaveSpeed=0;
	private GameObject windPrefab;
	public CardBase cardBase;

	

	public enum GolabBulletState
	{
		init,
		fly,
		reduceTime,
	    dead,
	}
	public List<EventDelegate> OnBulletDead = new List<EventDelegate>();
	public GolabBulletState state = GolabBulletState.init;
	private GolabBulletState lastState = GolabBulletState.init;



	void Awake()
	{
		windPrefab = ResourcesManager.Instance.LoadBullet("wind");
		isDead = false;
		pValuePrefab = ResourcesManager.Instance.LoadEffect("Pvalue");
	}
	// Use this for initialization
	void Start()
	{
		//if(gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
		//{
		//	gameObject.AddComponent<CanTouchScreen>();
		//}

		if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet")&&cardBase==null)
		{
			cardBase = HandCardPanel.GetCurrentUseKeyCodeData();
		}
		rgb = this.GetComponent<Rigidbody>();
		if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
		{
			if(cardBase.storyRank!= StoryRank.No) 
			{
				gameObject.AddComponent<Strong>().rank = cardBase.storyRank;
			}
			if(cardBase.windRank != WindRank.No)
			{
				GameObject wind = GameObject.Instantiate(windPrefab);
				wind.transform.SetParent(transform, false);
				wind.transform.localPosition = Vector3.zero;
				wind.transform.localScale = Vector3.zero;
				wind.GetComponent<WindFly>().rank = cardBase.windRank;
			}
		}
	}
    void OnDestroy() 
	{
		if (deadByChainBoom)
		{
			GameObject pValue = GameObject.Instantiate(pValuePrefab);
			pValue.transform.position = gameObject.transform.position;
			TweenPosition tp = pValue.GetComponent<TweenPosition>();
			tp.enabled = true;
			tp.from = pValue.transform.position;
			tp.to = new Vector3(8.05f, pValue.transform.position.y, -4.99f);
			tp.onFinished.Clear();
			tp.duration = 0.6f;
			tp.onFinished.Add(new EventDelegate(RoundRule.Instance.ChangePValueForChainBoom));
			tp.ResetToBeginning();
		}
	}


	


	// Update is called once per frame
	void Update()
	{
		if(state == GolabBulletState.reduceTime)
		{
			//if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
			//{
			//	Collider m_collider = gameObject.GetComponent<Collider>();
			//	m_collider.isTrigger = true;
			//	SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
			//	render.color = new Color(render.color.r, render.color.g, render.color.b,0.4f);
			//}
			
		}
		if (isDead)
		{
			if (!deadByChainBoom)
			{
				gameObject.transform.Translate(0, Mathf.Abs(leaveSpeed) * Time.deltaTime, 0, Space.World);
				SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
				render.sortingOrder = 10;
				state = GolabBulletState.dead;
			}
			else
			{
				rgb.velocity = Vector3.zero;
			}
		}
		if (!isDead)
		{
			if (rotateWithArrow && this.velocity.magnitude <= 0)
			{
				transform.rotation = Quaternion.Euler(PlayerBattleRule.Instance.ReturnArrowHandRotate());
				state = GolabBulletState.init;
			}


			else if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
			{
				state = GolabBulletState.reduceTime;
			}
			
			else
			{
				state = GolabBulletState.fly;
			}
		}
	
		WhenStateChange();
		if (this.power <= 0||isDead)
		{
			this.GetComponent<Collider>().isTrigger = true;
			Rigidbody rgb = this.GetComponent<Rigidbody>();
			//rgb.useGravity = true;
			isDead = true;
		}
	}
	public List<EventDelegate> OnBulletInitFished = new List<EventDelegate>();
	void WhenStateChange()
	{
		if (lastState != state)
		{
			if(lastState== GolabBulletState.init)
			{
				velocity = rgb.velocity;
				current = this;
				EventDelegate.Execute(OnBulletInitFished);
				current = null;
				Collider collider = gameObject.GetComponent<Collider>();
				collider.isTrigger = false;
			}
	

			if (state != GolabBulletState.reduceTime && state != GolabBulletState.dead)
			{
				Collider m_collider = gameObject.GetComponent<Collider>();
				m_collider.isTrigger = false;
				SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
				render.color = new Color(render.color.r, render.color.g, render.color.b, 1.0f);
			}
		

			if (state == GolabBulletState.reduceTime)
			{
				PlayerBattleRule.Instance.ReduceTime_ForNewBullet(gameObject);
				lastState = state;
			}
			else if(lastState== GolabBulletState.reduceTime&&state== GolabBulletState.fly)
			{
				PlayerBattleRule.Instance.FromReudceToFly(gameObject);
				lastState = state;
			}

			else if(state== GolabBulletState.dead)
			{
				//float speed = velocity.sqrMagnitude;
				//velocity += new Vector3(0, speed*0.5f, 0);
				//rgb.velocity = velocity;
				if(gameObject.layer== LayerMask.NameToLayer("EnemyBullet"))
				{
					GameObject pValue = GameObject.Instantiate(pValuePrefab);
					pValue.transform.position = gameObject.transform.position;
					TweenPosition tp = pValue.GetComponent<TweenPosition>();
					tp.enabled = true;
					tp.duration = 0.6f;
					tp.from = pValue.transform.position;
					tp.to = new Vector3(8.05f, pValue.transform.position.y, -4.99f);
					tp.onFinished.Clear();
					tp.onFinished.Add(new EventDelegate(OnPValueMoveFished));
					tp.ResetToBeginning();
				}
				leaveSpeed = rgb.velocity.magnitude;
				if (leaveSpeed < 5)
				{
					leaveSpeed = 5;
				}
				lastState = state;
				if (OnBulletDead != null)
				{
					current = this;
					EventDelegate.Execute(OnBulletDead);
					current = null;
				}
			}

		}
	}

	private void OnPValueMoveFished()
	{
		RoundRule.ChangePValue(1);
        GameObject.Destroy(TweenPosition.current.gameObject, 0.1f);
	}

	/// <summary>
	/// 外部计算伤害的方法
	/// </summary>
	/// <param name="collider"></param>
	/// <param name="powerValue"></param>
	/// <returns></returns>
	public bool ComputerPower(float powerValue)
	{	
		this.power -= powerValue;

		if (this.power <= 0)
		{
			this.GetComponent<Collider>().isTrigger = true;
			Rigidbody rgb = this.GetComponent<Rigidbody>();
			//rgb.useGravity = true;
			isDead = true;
		}
		
		return isDead;
	}

	/// <summary>
	/// 判断死亡弹幕
	/// </summary>
	/// <param name="collider"></param>
	void OnCollisionEnter(Collision collider)
	{
		BulletBaseComponent bbc = collider.transform.GetComponent<BulletBaseComponent>();
		if (bbc != null)
		{
			if (gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
			{
				if (bbc.power <= 0)
				{
					return;
				}
				Strong strong = gameObject.GetComponent<Strong>();
				float temp=1;
				if (strong != null)
				{
					if(strong.rank ==StoryRank.D)
					{
						temp = 0.8f;
					}
				}
				else
				{
					temp = 1;
				}
				float enemyPower = bbc.power - this.power;
				float goPower = this.power - (bbc.power)*temp;
				this.power = goPower;
				bbc.power = enemyPower;

				if (this.power <= 0)
				{
					this.GetComponent<Collider>().isTrigger = true;
					Rigidbody rgb = this.GetComponent<Rigidbody>();
					rgb.useGravity = true;
					isDead = true;
				}

				ChainBullet chainBullet = collider.transform.GetComponent<ChainBullet>();
				if (chainBullet != null)
				{
					if (isDead)
					{
						chainBullet.ChainBomb(bbc.transform.gameObject, transform, gameObject.layer);
					}
				}
			}
	


		}
	}

	void OnCollisionExit(Collision collider)
	{
		if (notTouch)
		{
			return;
		}
		if (PlayerBattleRule.Instance.bulletState == BulletState.reduceTime)
		{
			return;
		}
		UpdateVelocity();
		
		//BulletBaseComponent bbc = collider.transform.GetComponent<BulletBaseComponent>();
		//if (bbc != null)
		//{
		//	if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
		//	{

		//		SetVelocityNormal();
		//	}
		//}
	}


    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "wind")
        {
			if (collider.transform.parent == transform)
				return;
			if(collider.transform.gameObject.layer == gameObject.layer)
			{
				return;
			}
            WindFly windFly = collider.gameObject.GetComponent<WindFly>();
            if(windFly.rank== WindRank.D)
            {
                BulletBaseComponent windBBC = collider.gameObject.transform.parent.GetComponent<BulletBaseComponent>();
				float force = (windBBC.speed- speed / rgb.mass)*20f;
				Vector3 vec = transform.position- collider.transform.parent.position;
				rgb.AddForce(vec.normalized * force);
				UpdateVelocity();
            }
        }
    }

    /// <summary>
    /// 更新记录的速度值，记录时机为:发射时，撞击后
    /// </summary>
    public void UpdateVelocity()
	{
		velocity = gameObject.GetComponent<Rigidbody>().velocity;
		SetVelocityNormal();

	}
	public void UpdateVelocity(Vector3 velocity)
	{
		this.velocity = velocity;
	}

	public void SetVelocity()
	{
		velocity = velocity.normalized * speed;
		gameObject.GetComponent<Rigidbody>().velocity = velocity;
		SetVelocityNormal();
	}

	private void SetVelocityNormal()
	{
		Rigidbody rgb = this.GetComponent<Rigidbody>();
		float angle = Mathf.Atan2(rgb.velocity.z,rgb.velocity.x);
		float angleZ = angle * Mathf.Rad2Deg;
		Vector3 rotateVec = rgb.transform.rotation.eulerAngles;
		if (rotateVec.x > 180)
		{
			rotateVec.x = rotateVec.x - 360;
		}
		if (rotateVec.y > 180)
		{
			rotateVec.y = rotateVec.y - 360;
		}
		rgb.transform.rotation = Quaternion.Euler(rotateVec.x, -angleZ,0 );
	}



	//物体离开屏幕
	void OnBecameInvisible()
	{
		Destroy(gameObject,0.1F);
	}
}
