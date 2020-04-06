using System.Collections;
using System.Collections.Generic;
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

	private float leaveSpeed=0;
	
	private enum GolabBulletState
	{
		init,
		fly,
		reduceTime,
	    dead,
	}

	private GolabBulletState state = GolabBulletState.init;
	private GolabBulletState lastState = GolabBulletState.init;
	void Awake()
	{
		isDead = false;
		pValuePrefab = ResourcesManager.Instance.LoadEffect("Pvalue");
	}
	// Use this for initialization
	void Start()
	{
	    rgb = this.GetComponent<Rigidbody>();
		Destroy(gameObject, 8);
		
	}

	// Update is called once per frame
	void Update()
	{
		if(state == GolabBulletState.reduceTime||state== GolabBulletState.init)
		{
			if (gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
			{
				Collider m_collider = gameObject.GetComponent<Collider>();
				m_collider.isTrigger = true;
				SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
				render.color = new Color(render.color.r, render.color.g, render.color.b,0.4f);
			}
		}
		if (isDead)
		{
			gameObject.transform.Translate(0, Mathf.Abs(leaveSpeed) *Time.deltaTime, 0,Space.World);
			SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
			render.sortingOrder = 10;
			state = GolabBulletState.dead;
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

	void WhenStateChange()
	{
		if (lastState != state)
		{
			if(lastState== GolabBulletState.init)
			{
				velocity = rgb.velocity;
				
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
			}

		}
	}

	private void OnPValueMoveFished()
	{
		RoundRule.Instance.ChangePValue(1);
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

				float enemyPower = bbc.power - this.power;
				float goPower = this.power - bbc.power;
				this.power = goPower;
				bbc.power = enemyPower;
					
			}

			if (this.power <= 0)
			{
				this.GetComponent<Collider>().isTrigger = true;
				Rigidbody rgb = this.GetComponent<Rigidbody>();
				rgb.useGravity = true;
				isDead = true;
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


}
