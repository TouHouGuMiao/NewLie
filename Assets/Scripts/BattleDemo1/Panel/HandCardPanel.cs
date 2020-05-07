using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandCardPanel : MonoBehaviour {
	private GameObject player;
	private bool isCreatBulle = false;
	private float speed=25;
	private static List<CardBase> cardGroundList = new List<CardBase>();
	public static List<CardBase> handList = new List<CardBase>();
	private static List<CardBase> keyCodeList = new List<CardBase>();
	private static UIGrid grid;
	private static GameObject costLabelPrefab;
	private static  Dictionary<KeyCode, CardBase> SpeicalCardBattleDic=new Dictionary<KeyCode, CardBase> ();
	private static Transform keyContainer;
	private static GameObject thisGamoObject;
	private static UIGrid costLabelWidgetGrid;
	private static GameObject choseLeveeUpGame;
	private static UIGrid levelUpGrid;
	private static CardBase currentUseKeyCodeData;
	private static  UIButton levelCover;
	private static GameObject addCardContainer;

	private static List<CardBase> currentCardList = new List<CardBase> ();
	private static List<CardBase> usedCardList = new List<CardBase> ();
	private static List<CardBase> currentKeyCodeList = new List<CardBase>();
	

	public static CardBase GetCurrentUseKeyCodeData()
	{
		return currentUseKeyCodeData;
	}

	private enum WhatKeyCodeDown
	{
		Q,
		W,
		E,
		R,
	}
	private WhatKeyCodeDown whatKeyCodeDown = WhatKeyCodeDown.Q;
	private WhatKeyCodeDown lastKeyCodeDown = WhatKeyCodeDown.Q;
	
	void Awake()
	{
		AudioManager.Instance.PlayBg_Source("miniaturegarden", true);
		grid = transform.Find("Grid").GetComponent<UIGrid>();
		//costLabelWidgetGrid = transform.FindRecursively("costLabelWidget").GetComponent<UIGrid>();
		//thisGamoObject = gameObject;
		//costLabelPrefab = transform.FindRecursively("costItem").gameObject;
		//SpeicalCardBattleDic.Clear();
		//grid = this.transform.Find("Grid").GetComponent<UIGrid>();
		//cardGroundList = BattleCardManager.intance.GetCardGround();
		//player = GameObject.FindWithTag("Player").gameObject;
		//keyContainer = transform.FindRecursively("KeyContainer");
		//levelUpGrid = transform.FindRecursively("levelUpGrid").GetComponent<UIGrid>();
	}

	/// <summary>
	/// 洗牌
	/// </summary>
	public void DushCardGround()
	{
		for (int i = 0; i < usedCardList.Count; i++)
		{
			currentCardList.Add(usedCardList[i]);
		}
		usedCardList.Clear();
		currentCardList = OtherHelper.Instance.Shuffle<CardBase>(currentCardList);
		for (int i = 0; i < currentCardList.Count; i++)
		{
			GameObject go = GameObject.Instantiate(currentCardList[i].prefab);
			go.transform.SetParent(transform, false);
			go.transform.localPosition = new Vector3(-1806f, -1770f, 493f);
			go.transform.localScale = new Vector3(180, 180, 180);
			go.transform.rotation = Quaternion.Euler(0, 0, 0);
			TweenPosition tp = go.GetComponent<TweenPosition>();
			tp.enabled = true;
			tp.from = go.transform.localPosition;
			tp.to = new Vector3(-2659f, -393f, 493);
			tp.duration = 0.8f;
			tp.delay = 0.2f + i * 0.1f;
			tp.onFinished.Clear();
			if (i == currentCardList.Count - 1)
			{
				tp.onFinished.Add(new EventDelegate(ResetKeyCodeToDic));
			}
			tp.onFinished.Add(new EventDelegate(DestoryOnFished));	
			tp.ResetToBeginning();
		}
	}

	/// <summary>
	/// 到达后销毁
	/// </summary>
	private void DestoryOnFished()
	{
		GameObject.Destroy(TweenPosition.current.gameObject);
	}

	/// <summary>
	/// 空格键绑定功能，更换两个新技能到技能栏.如果currentHandList 数量小于2，则洗牌
	/// </summary>
	private void SpaceChangeNewSkill()
	{
		if (currentCardList.Count <= 1)
		{
			DushCardGround();
		}
		else
		{
			ResetKeyCodeToDic();
		}
	}

	/// <summary>
	/// 将新的两个技能绑定到Q,W上。
	/// </summary>
	public void ResetKeyCodeToDic()
	{
		if (grid.transform.childCount != 0)
		{
			if (grid.transform.childCount == 1)
			{
				MoveCardFromLeft(KeyCode.Q);
			}
			else if(grid.transform.childCount == 2)
			{
				MoveCardFromLeft(KeyCode.Q);
				MoveCardFromLeft(KeyCode.W);
			}
		}
		CardBase skillQ = currentCardList[0];
		CardBase skillW = currentCardList[1];
		usedCardList.Add(skillQ);
		usedCardList.Add(skillW);
		AddCardToGrid(KeyCode.Q,skillQ);
		AddCardToGrid(KeyCode.W, skillW);
		currentCardList.Remove(skillQ);
		currentCardList.Remove(skillW);
		SpeicalCardBattleDic[KeyCode.Q] = skillQ;
		SpeicalCardBattleDic[KeyCode.W] = skillW;
		currentKeyCodeList.Clear();
		currentKeyCodeList.Add(skillQ);
		currentKeyCodeList.Add(skillW);
	}
	/// <summary>
	/// 将新的卡牌加入技能列表
	/// </summary>
	public void AddCardToGrid(KeyCode keyCode,CardBase data)
	{
		if(keyCode == KeyCode.Q)
		{
			GameObject go = GameObject.Instantiate(data.prefab);
			go.transform.SetParent(grid.transform, true);
			go.transform.localPosition = new  Vector3 (-494F,811,0);
			go.transform.localScale = new Vector3(180, 180, 180);
			go.name = data.id.ToString();
			TweenPosition tp = go.GetComponent<TweenPosition>();
			tp.enabled = true;
			tp.duration = 0.6F;
			tp.onFinished.Clear();
			tp.onFinished.Add(new EventDelegate(SetCanContorlToTure));
			tp.from = tp.transform.localPosition;
			tp.to = new Vector3(-133.55f, 0, 0);
			tp.ResetToBeginning();
		}
		else if(keyCode == KeyCode.W)
		{
			GameObject go = GameObject.Instantiate(data.prefab);
			go.transform.SetParent(grid.transform, true);
			go.transform.localPosition = new Vector3(-494F, 811, 0);
			go.transform.localScale = new Vector3(180, 180, 180);
			go.name = data.id.ToString();
			TweenPosition tp = go.GetComponent<TweenPosition>();
			tp.enabled = true;
			tp.duration = 0.5F;
			tp.onFinished.Clear();
			tp.onFinished.Add(new EventDelegate(SetCanContorlToTure));
			tp.from = tp.transform.localPosition;
			tp.to = new Vector3(133.55f, 0, 0);
			tp.ResetToBeginning();
		}
	}

	private void SetCanContorlToTure()
	{
		canContorl = true;
	}


	/// <summary>
	/// 判断是否可以操控
	/// </summary>
	private bool canContorl = true;


	/// <summary>
	/// 判断是否可以使用空格
	/// </summary>
	public static bool canUseSpace = true;

	/// <summary>
	/// 将技能牌移出屏幕
	/// </summary>
	public void MoveCardFromLeft(KeyCode keyCode)
	{
		GameObject go = null;
		if (keyCode == KeyCode.Q)
		{
			go= grid.transform.GetChild(0).gameObject;
		}

		else
		{
			go = grid.transform.GetChild(1).gameObject;
		}
	
		
		TweenPosition tp = go.GetComponent<TweenPosition>();
		tp.enabled = true;
		tp.delay = 0;
		tp.duration = 0.5f;
		tp.onFinished.Clear();
		tp.onFinished.Add(new EventDelegate(DestoryGameObject));
		tp.from = go.transform.localPosition;
		tp.to = tp.from + new Vector3(0, -797f, 0);
		tp.ResetToBeginning();
		go.AddComponent<RotateUpdate>().speed = 640;
	}

	/// <summary>
	/// 销毁物体
	/// </summary>
	private void DestoryGameObject()
	{
		GameObject go = TweenPosition.current.gameObject;
		GameObject.Destroy(go, 0.1f);
	}

	public void UseSkillFished(KeyCode keyCode,CardBase data)
	{
		GameObject go = null;
		if(keyCode == KeyCode.Q)
		{
			go = grid.transform.GetChild(0).gameObject;
		}
		else
		{
			go = grid.transform.GetChild(1).gameObject;
		}
		
		currentKeyCodeList.Remove(data);
		SpeicalCardBattleDic[keyCode] = null;
		usedCardList.Add(data);
		SkillCardRotate(go);
	}


	/// <summary>
	/// 使用过的技能卡牌翻面
	/// </summary>
	public void SkillCardRotate(GameObject go)
	{
		TweenRotation tr = go.GetComponent<TweenRotation>();
		tr.enabled = true;
		tr.delay = 0;
		tr.duration = 0.5f;
		tr.from = new Vector3(0, 180, 0);
		tr.to = new Vector3(0, 0, 0);
		tr.onFinished.Clear();
		tr.ResetToBeginning();
	}

	/// <summary>
	/// 为玩家开启时停
	/// </summary>
	public void ReduceTimeForPlayer()
	{
		PlayerBattleRule.Instance.ReduceTime(5);
	}

	public static void ClearAddKeyCodeContioner()
	{
		addCardContainer.transform.DestroyChildren();
	}


	void Start()
	{
		player = GameObject.FindWithTag("Player");
		SpeicalCardBattleDic.Add(KeyCode.Q, null);
		SpeicalCardBattleDic.Add(KeyCode.W, null);
		cardGroundList = BattleCardManager.intance.GetCardGround();
		currentCardList = cardGroundList;
		addCardContainer = transform.FindRecursively("AddCardContainer").gameObject;
		levelCover = transform.FindRecursively("levelCover").GetComponent<UIButton>();
		levelCover.onClick.Add(new EventDelegate(OnLevelCoverClick));
		SpaceChangeNewSkill();
		//List<int> initIdList = new List<int>();
		//for (int i = 0; i < cardGroundList.Count; i++)
		//{
		//	if (cardGroundList[i].isInit)
		//	{
		//		initIdList.Add(cardGroundList[i].id);
		//	}
		//}
		//for (int i = 0; i < initIdList.Count; i++)
		//{
		//	DrawCardById(initIdList[i]);
		//}

	}
	private void OnLevelCoverClick()
	{

	}

	public void DrawCardById(int id)
	{
		CardBase temp = null;
		for (int i = 0; i < cardGroundList.Count; i++)
		{
			if (cardGroundList[i].id == id)
			{
				temp = cardGroundList[i];
			}
		}
		cardGroundList.Remove(temp);
		handList.Add(temp);
		GridHandCard();
	}

	public static void AddDataToGroundList(int id)
	{
		CardBase data = BattleCardManager.intance.GetCardBaseDataById(id);
		cardGroundList.Add(data);
	}

	public static void RemoveDataFromHandList(int id)
	{
		CardBase data = null;
		for (int i = 0; i < handList.Count; i++)
		{
			if(handList[i].id == id)
			{
				data = handList[i];
			}
		}
		handList.Remove(data);

		for (int i = 0; i < grid.transform.childCount; i++)
		{
			GameObject go = grid.transform.GetChild(i).gameObject;
			if (go.name == data.id.ToString())
			{
				Destroy(go);
			}
		}
		GridHandCard();
	}

	public static List<CardBase> GetOneShotCardList()
	{
		List<CardBase> list = new List<CardBase>();
		for (int i = 0; i < handList.Count; i++)
		{
			if (handList[i].bulletType == CardBase.BulletType.One)
			{
				list.Add(handList[i]);
			}
		}
		return list;
	}
	public static List<CardBase> GetFiveShotCardList()
	{
		List<CardBase> list = new List<CardBase>();
		for (int i = 0; i < handList.Count; i++)
		{
			if (handList[i].bulletType == CardBase.BulletType.Five)
			{
				list.Add(handList[i]);
			}
		}
		return list;
	}
	public static List<CardBase> GetThreeShotCardList()
	{
		List<CardBase> list = new List<CardBase>();
		for (int i = 0; i < handList.Count; i++)
		{
			if (handList[i].bulletType == CardBase.BulletType.Three)
			{
				list.Add(handList[i]);
			}
		}
		return list;
	}


	public static List<CardBase> GetStrongCardBaseListFromHandAndGround()
	{
		List<CardBase> list = new List<CardBase>();
		for (int i = 0; i < handList.Count; i++)
		{
			if(handList[i].storyRank!= StoryRank.No)
			{
				list.Add(handList[i]);
			}
		}
		for (int i = 0; i < cardGroundList.Count; i++)
		{
			if (cardGroundList[i].storyRank != StoryRank.No)
			{
				list.Add(cardGroundList[i]);
			}
		}
		return list;
	}


	public static List<CardBase> GetWindUpCardBaseListFromHandAndGround()
	{
		List<CardBase> list = new List<CardBase>();
		for (int i = 0; i < handList.Count; i++)
		{
			if (handList[i].windRank != WindRank.No)
			{
				list.Add(handList[i]);
			}
		}
		for (int i = 0; i < cardGroundList.Count; i++)
		{
			if (cardGroundList[i].windRank != WindRank.No)
			{
				list.Add(cardGroundList[i]);
			}
		}
		return list;
	}

	public static CardBase GetHandCardData(int id)
    {
        CardBase data = null;
        for (int i = 0; i < handList.Count; i++)
        {
            if(handList[i].id == id)
            {
                data = handList[i];
            }
        }
        return data;
    }

	public static void CardJump(int id)
	{
		for (int i = 0; i < grid.transform.childCount; i++)
		{
			GameObject go = grid.transform.GetChild(i).gameObject;
			if(go.name == id.ToString())
			{
				TweenScale ts = go.GetComponent<TweenScale>();
				ts.enabled = true;
				ts.from = ts.transform.localScale;
				ts.to = ts.transform.localScale * 1.2f;
				ts.duration = 0.5f;
				ts.delay = 0;
				ts.ResetToBeginning();
			}
		}
	}
    public static CardBase GetKeyCodeCardData(int id)
    {
        CardBase data = null;
        for (int i = 0; i <currentKeyCodeList.Count; i++)
        {
            if (currentKeyCodeList[i].id == id)
            {
                data = currentKeyCodeList[i];
            }
        }
        return data;
		
    }


	static System.Random drawRandom = new System.Random();
    /// <summary>
    /// 从牌组中抽取卡牌
    /// </summary>
    /// <param name="count"></param>
    public static void DrawCard(int count)
	{
		if (!RoundRule.CompltePValue(-100))
		{
			return;
		}
		RoundRule.ChangePValue(-100);
		RoundRule.Instance.StartCoroutine(DrawCardIEnumator(count));
	}
	private static IEnumerator DrawCardIEnumator(int count)
	{
		CardBase temp = new CardBase();
		for (int i = 0; i < count; i++)
		{
			temp =cardGroundList[drawRandom.Next(cardGroundList.Count)];
			cardGroundList.Remove(temp);
			handList.Add(temp);
			GridHandCard();
			yield return new WaitForSeconds(0.6f);
		}
	}



	private static void GridHandCard()
	{
		int childCount = grid.transform.childCount;
		for (int i = childCount; i < handList.Count; i++)
		{
			GameObject go = GameObject.Instantiate(handList[i].prefab);
			go.transform.SetParent(grid.transform, false);
			go.transform.localPosition = new Vector3(1600, 0, 493);
			go.name = handList[i].id.ToString();
			DragSkillCard dsc = go.GetComponent<DragSkillCard>();
			dsc.dragDenpent = DragSkillCard.DragDenpent.byLockKeyCode;
			dsc.OnCardDragFished.Add(new EventDelegate(BattleCardEvent));
		}
		grid.Reposition();
	}
	

	public static void OnRoundBattleStart()
	{
		grid.gameObject.SetActive(false);
	}
	public static void ShowHandGrid()
	{
		KeyCodeCardMoveReturnToHandList();
		grid.gameObject.SetActive(true);
	}

	// Update is called once per frame
	public static GameObject tempBullet=null;
	private bool needReduceTime = false;
	void Update() {


		//if (levelUpGrid.gameObject.activeSelf)
		//{
		//	if (Input.GetMouseButtonDown(0))
		//	{
		//		levelUpGrid.gameObject.SetActive(false);
		//		costLabelWidgetGrid.transform.DestroyChildren();
		//	}
		//}
		if (canContorl)
		{
			if (canUseSpace)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					AudioManager.Instance.PauseBg_Source(1f);
					canContorl = false;
					canUseSpace = false;
					ReduceTimeForPlayer();
					SpaceChangeNewSkill();

				}
			}


			if (Input.GetKeyDown(KeyCode.Q))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.Q, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				if (data == null)
				{
					return;
				}
				currentUseKeyCodeData = data;
				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					if (data.count <= 0)
					{
						return;
					}
					EventDelegate.Execute(data.CardSpeicalEvent);
					UseSkillFished(KeyCode.Q, data);
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.atOnce)
				{
					EventDelegate.Execute(data.CardSpeicalEvent);
					currentUseKeyCodeData = data;
				}
				whatKeyCodeDown = WhatKeyCodeDown.Q;
				if (lastKeyCodeDown != whatKeyCodeDown)
				{

					lastKeyCodeDown = whatKeyCodeDown;
				}
			}

			if (whatKeyCodeDown == WhatKeyCodeDown.Q)
			{
				if (Input.GetKey(KeyCode.Q))
				{
					CardBase data = null;
					if (!SpeicalCardBattleDic.TryGetValue(KeyCode.Q, out data))
					{
						Debug.LogError("cardData is null");
						return;
					}
					if (data == null)
					{
						return;
					}
					if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
					{
						if (data.count <= 0)
						{
							//PlayerBattlePanel.HideBulletCount();
							return;
						}
						//PlayerBattlePanel.ShowBulletCount(data.count);
						//PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
						if (Input.GetMouseButtonDown(0))
						{
							EventDelegate.Execute(data.CardSpeicalEvent);
							isCreatBulle = true;
						}

						if (isCreatBulle)
						{
							if (Input.GetMouseButtonUp(0))
							{
								BulletBaseComponent bbc = tempBullet.GetComponent<BulletBaseComponent>();
								float angle = -(tempBullet.transform.eulerAngles.y) * Mathf.Deg2Rad;
								Rigidbody rgb = tempBullet.GetComponent<Rigidbody>();
								rgb.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
								rgb.mass = data.mass;
								bbc.speed = data.speed;
								bbc.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
								bbc.power = data.power;
								isCreatBulle = false;
								PlayerBattleRule.Instance.arrowHead.SetActive(false);
								//data.count--;
								tempBullet = null;

								//PlayerBattlePanel.ShowBulletCount(data.count);
								UseSkillFished(KeyCode.Q, data);
							}

						}
					}

					else if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
					{
						//PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
						if (Input.GetMouseButtonUp(0))
						{
							SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
							scb.isOn = true;
							data.count--;
						}

					}

					else if (data.speicalCardType == CardBase.SpeicalCardType.many)
					{
						if (Input.GetMouseButtonDown(0))
						{
							//PlayerBattlePanel.ShowBulletCount(data.count);
							//PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
						}
						if (Input.GetMouseButtonUp(0))
						{
							EventDelegate.Execute(data.CardSpeicalEvent);
							UseSkillFished(KeyCode.Q, data);
							//data.count--;
						}
					}
				}

				else if (Input.GetKeyUp(KeyCode.Q))
				{
					CardBase data = null;
					if (!SpeicalCardBattleDic.TryGetValue(KeyCode.Q, out data))
					{
						Debug.LogError("cardData is null");
						return;
					}
					if (data == null)
					{
						return;
					}
					if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
					{
						//PlayerBattlePanel.HideBulletCount();
						PlayerBattleRule.Instance.arrowHead.SetActive(false);
						if (tempBullet != null)
						{
							GameObject.Destroy(tempBullet);
						}
					}

					if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
					{
						SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
						if (!scb.isOn)
							GameObject.Destroy(tempBullet);
					}
					currentUseKeyCodeData = null;

				}


			}



			if (Input.GetKeyDown(KeyCode.W))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.W, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				if (data == null)
				{
					return;
				}
				currentUseKeyCodeData = data;
				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					if (data.count <= 0)
					{
						return;
					}
					EventDelegate.Execute(data.CardSpeicalEvent);
					UseSkillFished(KeyCode.W, data);
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.atOnce)
				{
					EventDelegate.Execute(data.CardSpeicalEvent);
					currentUseKeyCodeData = data;
				}
				whatKeyCodeDown = WhatKeyCodeDown.W;
				if (lastKeyCodeDown != whatKeyCodeDown)
				{

					lastKeyCodeDown = whatKeyCodeDown;
				}
			}

			if (whatKeyCodeDown == WhatKeyCodeDown.W)
			{
				if (Input.GetKey(KeyCode.W))
				{
					CardBase data = null;
					if (!SpeicalCardBattleDic.TryGetValue(KeyCode.W, out data))
					{
						Debug.LogError("cardData is null");
						return;
					}
					if (data == null)
					{
						return;
					}
					if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
					{
						if (data.count <= 0)
						{
							//PlayerBattlePanel.HideBulletCount();
							return;
						}
						//PlayerBattlePanel.ShowBulletCount(data.count);
						//PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
						if (Input.GetMouseButtonDown(0))
						{
							EventDelegate.Execute(data.CardSpeicalEvent);
							isCreatBulle = true;
						}

						if (isCreatBulle)
						{
							if (Input.GetMouseButtonUp(0))
							{
								BulletBaseComponent bbc = tempBullet.GetComponent<BulletBaseComponent>();
								float angle = -(tempBullet.transform.eulerAngles.y) * Mathf.Deg2Rad;
								Rigidbody rgb = tempBullet.GetComponent<Rigidbody>();
								rgb.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
								rgb.mass = data.mass;
								bbc.speed = data.speed;
								bbc.velocity = new Vector3(data.speed * Mathf.Cos(angle), 0, data.speed * Mathf.Sin(angle));
								bbc.power = data.power;
								isCreatBulle = false;
								PlayerBattleRule.Instance.arrowHead.SetActive(false);
								//data.count--;
								tempBullet = null;

								//PlayerBattlePanel.ShowBulletCount(data.count);
								UseSkillFished(KeyCode.W, data);
							}

						}
					}

					else if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
					{
						//PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
						if (Input.GetMouseButtonUp(0))
						{
							SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
							scb.isOn = true;
							data.count--;
						}

					}

					else if (data.speicalCardType == CardBase.SpeicalCardType.many)
					{
						if (Input.GetMouseButtonDown(0))
						{
							//PlayerBattlePanel.ShowBulletCount(data.count);
							//PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
						}
						if (Input.GetMouseButtonUp(0))
						{
							EventDelegate.Execute(data.CardSpeicalEvent);
							UseSkillFished(KeyCode.W, data);
							//data.count--;
						}
					}
				}

				else if (Input.GetKeyUp(KeyCode.W))
				{
					CardBase data = null;
					if (!SpeicalCardBattleDic.TryGetValue(KeyCode.W, out data))
					{
						Debug.LogError("cardData is null");
						return;
					}
					if (data == null)
					{
						return;
					}
					if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
					{
						//PlayerBattlePanel.HideBulletCount();
						PlayerBattleRule.Instance.arrowHead.SetActive(false);
						if (tempBullet != null)
						{
							GameObject.Destroy(tempBullet);
						}
					}

					if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
					{
						SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
						if (!scb.isOn)
							GameObject.Destroy(tempBullet);
					}
					currentUseKeyCodeData = null;
					

				}


			}
		}










		if (Input.GetKey(KeyCode.E))
		{
			Animator aniamtor = player.GetComponent<Animator>();
			aniamtor.SetInteger("AnimatorState", 1);
		}
		if (Input.GetKeyUp(KeyCode.E))
		{
			Animator aniamtor = player.GetComponent<Animator>();
			aniamtor.SetInteger("AnimatorState", 0);
			
		}

	
	}
	private static void AddCardToKeyCodeListFromHandList(int id)
	{
		for (int i = 0; i < handList.Count; i++)
		{
			if (handList[i].id == id)
			{
				keyCodeList.Add(handList[i]);
				handList.Remove(handList[i]);
				return;
			}
		}
	} 

	private void OnChangeKey()
	{
		//PlayerBattlePanel.HideBulletCount();
		tempBullet = null;
	}

	#region 卡牌委托
	static void OnBattleCardPress()
	{
		keyContainer.gameObject.SetActive(true);
	}


	private static void OnLevelUpBtnClick()
	{
		UIButton current = UIButton.current;
		for (int i = 0; i < handList.Count; i++)
		{

			if(handList[i].id == CommonHelper.Str2Int(choseLeveeUpGame.name))
			{
				int id = CommonHelper.Str2Int(current.gameObject.name);
				CardBase data = (BattleCardManager.intance.GetCardBaseDataById(id));
				if (!RoundRule.CompltePValue(-data.cost))
				{
					levelUpGrid.gameObject.SetActive(false);
					costLabelWidgetGrid.transform.DestroyChildren();
					return;
				}
				handList[i] = data;
				int index = choseLeveeUpGame.transform.GetSiblingIndex();
				choseLeveeUpGame.SetActive(false);
				Destroy(choseLeveeUpGame, 0.5f);
				current.transform.SetParent(grid.transform, true);
				current.transform.SetSiblingIndex(index);
				break;
			}
		}
		int tempId = CommonHelper.Str2Int(current.gameObject.name);
		CardBase tempData = BattleCardManager.intance.GetCardBaseDataById(tempId);
		RoundRule.ChangePValue(-(tempData.cost));
		costLabelWidgetGrid.transform.DestroyChildren();
		current.transform.GetComponent<DragSkillCard>().enabled = true;
		current.transform.GetComponent<DragSkillCard>().dragDenpent = DragSkillCard.DragDenpent.byLockKeyCode;
		current.transform.GetComponent<DragSkillCard>().OnCardDragFished.Add(new EventDelegate(BattleCardEvent));
		current.enabled = false;
		levelUpGrid.gameObject.SetActive(false);
		grid.Reposition();
	}

	static void BattleCardEvent()
	{
		DragSkillCard dragCard = DragSkillCard.current;
		if (dragCard.useByKeyCode)
		{
			if (DragSkillCard.current.transform.localPosition.y <= -221.8993f)
			{
				DragSkillCard.current.transform.SetParent(grid.transform, true);
				dragCard.useByKeyCode = false;
				handList.Add(SpeicalCardBattleDic[dragCard.keyCode]);
				SpeicalCardBattleDic.Remove(dragCard.keyCode);
				GridHandCard();
			}
			else
			{
				dragCard.ReturnStartVec();
			}
		}
		else
		{
			if (!keyContainer.gameObject.activeSelf)
			{
				CardBase tempData = BattleCardManager.intance.GetCardBaseDataById(CommonHelper.Str2Int(dragCard.gameObject.name));
				if (tempData.cardUseType == CardBase.CardUseType.roundUse)
				{
					SureUseCardPanel.ShowContainer(tempData);
					dragCard.ReturnStartVec();
				}
				else
				{
			
					//levelCover.gameObject.SetActive(true);
					levelUpGrid.transform.DestroyChildren();
					costLabelWidgetGrid.transform.DestroyChildren();
					choseLeveeUpGame = dragCard.gameObject;
					levelUpGrid.transform.DestroyChildren();
					int id = CommonHelper.Str2Int(dragCard.gameObject.name);
					CardBase data = BattleCardManager.intance.GetCardBaseDataById(id);
					levelUpGrid.transform.localPosition = new Vector3(dragCard.startPos.x, levelUpGrid.transform.localPosition.y, levelUpGrid.transform.localPosition.z);
					for (int i = 0; i < data.LevevUpList.Count; i++)
					{
						GameObject go = GameObject.Instantiate(data.LevevUpList[i].prefab);
						go.transform.SetParent(levelUpGrid.transform, false);
						go.name = data.LevevUpList[i].id.ToString();
						DragSkillCard drag = go.GetComponent<DragSkillCard>();
						drag.enabled = false;
						go.AddComponent<UIButton>();
						go.GetComponent<UIButton>().onClick.Add(new EventDelegate(OnLevelUpBtnClick));
						go.GetComponent<UIButton>().enabled = true;
						costLabelWidgetGrid.transform.localPosition = new Vector3(levelUpGrid.transform.localPosition.x, costLabelWidgetGrid.transform.localPosition.y, costLabelWidgetGrid.transform.localPosition.z);
						GameObject costLabel = GameObject.Instantiate(costLabelPrefab);
						costLabel.transform.SetParent(costLabelWidgetGrid.transform, true);
						costLabel.transform.localPosition = Vector3.zero;
						costLabel.transform.localScale = Vector3.one;
						UILabel label = costLabel.transform.Find("costLabel").GetComponent<UILabel>();
						label.text = data.LevevUpList[i].cost.ToString();
						int pValue = RoundRule.Instance.GetPValue();
						if (data.LevevUpList[i].cost > pValue)
						{
							UILabel reduceLable = costLabel.transform.Find("label").GetComponent<UILabel>();
							reduceLable.color = new Color(255, 0, 0);
							label.color = new Color(255, 0, 0);
						}
						costLabel.gameObject.SetActive(true);
					}
					levelUpGrid.Reposition();
					costLabelWidgetGrid.Reposition();
					levelUpGrid.gameObject.SetActive(true);
					dragCard.ReturnStartVec();

				}
				
			}


			else
			{


				string keyCodeName = dragCard.keyCodeName;
				CardBase cardBaseQ = null;
				CardBase cardBaseW = null;
				CardBase cardBaseE = null;
				CardBase cardBaseR = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.Q, out cardBaseQ))
				{
					if (keyCodeName == "Q")
					{
						dragCard.transform.SetParent(addCardContainer.transform, true);
						TweenPosition tp = dragCard.GetComponent<TweenPosition>();
						tp.enabled = true;
						tp.from = dragCard.transform.localPosition;
						tp.to = new Vector3(-732.9f, 131.7f, dragCard.transform.localPosition.z);
						tp.duration = 0.2f;
						tp.delay = 0;
						tp.onFinished.Clear();
						tp.ResetToBeginning();
						CardBase data = BattleCardManager.intance.GetCardBaseDataById(CommonHelper.Str2Int(dragCard.gameObject.name));
						SpeicalCardBattleDic.Add(KeyCode.Q, data);
						AddCardToKeyCodeListFromHandList(CommonHelper.Str2Int(dragCard.gameObject.name));
						dragCard.useByKeyCode = true;
						dragCard.keyCode = KeyCode.Q;
					}
				}

				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.W, out cardBaseW))
				{
					if (keyCodeName == "W")
					{
						dragCard.transform.SetParent(keyContainer, true);
						TweenPosition tp = dragCard.GetComponent<TweenPosition>();
						tp.enabled = true;
						tp.from = dragCard.transform.localPosition;
						tp.to = new Vector3(-244.3F, 131.7f, dragCard.transform.localPosition.z);
						tp.duration = 0.2f;
						tp.delay = 0;
						tp.onFinished.Clear();
						tp.ResetToBeginning();
						CardBase data = BattleCardManager.intance.GetCardBaseDataById(CommonHelper.Str2Int(dragCard.gameObject.name));
						SpeicalCardBattleDic.Add(KeyCode.W, data);
						AddCardToKeyCodeListFromHandList(CommonHelper.Str2Int(dragCard.gameObject.name));
						dragCard.useByKeyCode = true;
						dragCard.keyCode = KeyCode.W;
					}
				}



				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.E, out cardBaseE))
				{
					if (keyCodeName == "E")
					{
						dragCard.transform.SetParent(keyContainer, true);
						TweenPosition tp = dragCard.GetComponent<TweenPosition>();
						tp.enabled = true;
						tp.from = dragCard.transform.localPosition;
						tp.to = new Vector3(244f, 131.7f, dragCard.transform.localPosition.z);
						tp.duration = 0.2f;
						tp.delay = 0;
						tp.onFinished.Clear();
						tp.ResetToBeginning();
						CardBase data = BattleCardManager.intance.GetCardBaseDataById(CommonHelper.Str2Int(dragCard.gameObject.name));
						SpeicalCardBattleDic.Add(KeyCode.E, data);
						AddCardToKeyCodeListFromHandList(CommonHelper.Str2Int(dragCard.gameObject.name));
						dragCard.useByKeyCode = true;
						dragCard.keyCode = KeyCode.E;
					}
				}

				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.R, out cardBaseR))
				{
					if (keyCodeName == "R")
					{
						dragCard.transform.SetParent(keyContainer, true);
						TweenPosition tp = dragCard.GetComponent<TweenPosition>();
						tp.enabled = true;
						tp.from = dragCard.transform.localPosition;
						tp.to = new Vector3(732, 131.7f, dragCard.transform.localPosition.z);
						tp.duration = 0.2f;
						tp.delay = 0;
						tp.onFinished.Clear();
						tp.ResetToBeginning();
						CardBase data = BattleCardManager.intance.GetCardBaseDataById(CommonHelper.Str2Int(dragCard.gameObject.name));
						SpeicalCardBattleDic.Add(KeyCode.R, data);
						AddCardToKeyCodeListFromHandList(CommonHelper.Str2Int(dragCard.gameObject.name));
						dragCard.useByKeyCode = true;
						dragCard.keyCode = KeyCode.R;
					}
				}
				else
				{
					DragSkillCard.current.ReturnStartVec();
				}
			}
			GridHandCard();


		}
	   
	}


	

	 static void KeyCodeCardMoveReturnToHandList()
	{
		SpeicalCardBattleDic.Clear();
		for (int i = 0; i < keyCodeList.Count; i++)
		{
			CardBase data = BattleCardManager.intance.GetCardBaseDataById(keyCodeList[i].id);
			handList.Add(data);
		}
		GridHandCard();
		keyCodeList.Clear();
	}

    #endregion


	public void BuChouShotCount()
	{
		Debug.LogError(1);
		for (int i = 0; i < handList.Count; i++)
		{
			handList[i].count = handList[i].countMax;
		}
	}
}
