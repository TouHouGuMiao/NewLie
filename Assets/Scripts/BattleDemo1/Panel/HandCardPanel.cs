using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardPanel : MonoBehaviour {
	private GameObject player;
	private bool isCreatBulle = false;
	private float speed=25;
	private static List<CardBase> cardGroundList = new List<CardBase>();
	private static List<CardBase> handList = new List<CardBase>();
	private static List<CardBase> keyCodeList = new List<CardBase>();
	private static UIGrid grid;
	private static GameObject costLabelPrefab;
	private static  Dictionary<KeyCode, CardBase> SpeicalCardBattleDic=new Dictionary<KeyCode, CardBase> ();
	private static Transform keyContainer;
	private static GameObject thisGamoObject;
	private static Transform costLabelWidget;
	private static GameObject choseLeveeUpGame;

	private static  UIGrid levelUpGrid;

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
		costLabelWidget = transform.FindRecursively("costLabelWidget");
		thisGamoObject = gameObject;
		costLabelPrefab = transform.FindRecursively("costLabel").gameObject;
		SpeicalCardBattleDic.Clear();
		grid = this.transform.Find("Grid").GetComponent<UIGrid>();
		cardGroundList = BattleCardManager.intance.GetCardGround();
		player = GameObject.FindWithTag("Player").gameObject;
		keyContainer = transform.FindRecursively("KeyContainer");
		levelUpGrid = transform.FindRecursively("levelUpGrid").GetComponent<UIGrid>();
	}
	void Start()
	{
		DrawCard(11);
	}



	/// <summary>
	/// 从牌组中抽取卡牌
	/// </summary>
	/// <param name="count"></param>
	public static void DrawCard(int count)
	{
		List<CardBase> tempList = new List<CardBase>();
		for (int i = 0; i < count; i++)
		{
			tempList.Add(cardGroundList[i]);
		}
		for (int i = 0; i < tempList.Count; i++)
		{
			cardGroundList.Remove(tempList[i]);
			handList.Add(tempList[i]);
		}
		GridHandCard();
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
			if(handList[i].cardUseType== CardBase.CardUseType.roundUse)
			{
				dsc.dragDenpent = DragSkillCard.DragDenpent.byY;
				dsc.OnCardDragFished=handList[i].CardSpeicalEvent;
			}
			else if(handList[i].cardUseType == CardBase.CardUseType.battleUse)
			{
				dsc.dragDenpent = DragSkillCard.DragDenpent.byLockKeyCode;
				dsc.OnCardDragFished.Add(new EventDelegate (BattleCardEvent));
			}
		}
		grid.Reposition();
	}
	

	public static void OnRoundBattleStart()
	{
		grid.gameObject.SetActive(false);
	}
	public static void ShowHandGrid()
	{
		grid.gameObject.SetActive(true);
	}

	// Update is called once per frame
	public static GameObject tempBullet=null;
	private bool needReduceTime = false;
	void Update() {

		if (levelUpGrid.gameObject.activeSelf)
		{
			if (Input.GetMouseButtonDown(1))
			{
				levelUpGrid.gameObject.SetActive(false);
				costLabelWidget.DestroyChildren();
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
			if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
			{
				if (data.count <= 0)
				{
					return;
				}
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			if(data.speicalCardType== CardBase.SpeicalCardType.atOnce)
			{
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			whatKeyCodeDown = WhatKeyCodeDown.Q;
			if (lastKeyCodeDown != whatKeyCodeDown)
			{
				
				lastKeyCodeDown = whatKeyCodeDown;
			}
		}
	
		if(whatKeyCodeDown== WhatKeyCodeDown.Q)
		{
			if (Input.GetKey(KeyCode.Q))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.Q, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					if (data.count <= 0)
					{
						PlayerBattlePanel.HideBulletCount();
						return;
					}
					PlayerBattlePanel.ShowBulletCount(data.count);
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
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
							data.count--;
							tempBullet = null;
							PlayerBattlePanel.ShowBulletCount(data.count);
						}

					}
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
					if (Input.GetMouseButtonUp(0))
					{
						SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
						scb.isOn = true;
						data.count--;
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
				
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					PlayerBattlePanel.HideBulletCount();
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

				PlayerBattleRule.Instance.bulletState = BulletState.nomal;

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
			if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
			{
				if (data.count <= 0)
				{
					return;
				}
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			if (data.speicalCardType == CardBase.SpeicalCardType.atOnce)
			{
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			whatKeyCodeDown = WhatKeyCodeDown.W;
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
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					if (data.count <= 0)
					{
						PlayerBattlePanel.HideBulletCount();
						return;
					}
					PlayerBattlePanel.ShowBulletCount(data.count);
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
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
							isCreatBulle = false;
							PlayerBattleRule.Instance.arrowHead.SetActive(false);
							data.count--;
							PlayerBattlePanel.ShowBulletCount(data.count);
						}

					}
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
					if (Input.GetMouseButtonUp(0))
					{
						SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
						scb.isOn = true;
						data.count--;
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
				PlayerBattleRule.Instance.bulletState = BulletState.nomal;
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					PlayerBattlePanel.HideBulletCount();
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
					if (!scb.isOn)
						GameObject.Destroy(tempBullet);
				}



			}
		}












		if (Input.GetKeyDown(KeyCode.E))
		{
			CardBase data = null;
			if (!SpeicalCardBattleDic.TryGetValue(KeyCode.E, out data))
			{
				Debug.LogError("cardData is null");
				return;
			}
			if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
			{
				if (data.count <= 0)
				{
					return;
				}
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			if (data.speicalCardType == CardBase.SpeicalCardType.atOnce)
			{
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			whatKeyCodeDown = WhatKeyCodeDown.E;
		}
		if (whatKeyCodeDown == WhatKeyCodeDown.E)
		{

			if (Input.GetKey(KeyCode.E))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.E, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					if (data.count <= 0)
					{
						PlayerBattlePanel.HideBulletCount();
						return;
					}
					PlayerBattlePanel.ShowBulletCount(data.count);
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
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
							isCreatBulle = false;
							PlayerBattleRule.Instance.arrowHead.SetActive(false);
							data.count--;
							PlayerBattlePanel.ShowBulletCount(data.count);
						}

					}
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
					if (Input.GetMouseButtonUp(0))
					{
						SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
						scb.isOn = true;
						data.count--;
					}

				}
			}

			else if (Input.GetKeyUp(KeyCode.E))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.E, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				PlayerBattleRule.Instance.bulletState = BulletState.nomal;
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					PlayerBattlePanel.HideBulletCount();
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
					if (!scb.isOn)
						GameObject.Destroy(tempBullet);
				}



			}
		}


		if (Input.GetKeyDown(KeyCode.R))
		{
			CardBase data = null;
			if (!SpeicalCardBattleDic.TryGetValue(KeyCode.R, out data))
			{
				Debug.LogError("cardData is null");
				return;
			}
			if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
			{
				if (data.count <= 0)
				{
					return;
				}
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			if (data.speicalCardType == CardBase.SpeicalCardType.atOnce)
			{
				EventDelegate.Execute(data.CardSpeicalEvent);
			}
			whatKeyCodeDown = WhatKeyCodeDown.R;
		}
		if (whatKeyCodeDown == WhatKeyCodeDown.R)
		{


			

			if (Input.GetKey(KeyCode.R))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.R, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					if (data.count <= 0)
					{
						PlayerBattlePanel.HideBulletCount();
						return;
					}
					PlayerBattlePanel.ShowBulletCount(data.count);
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
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
							isCreatBulle = false;
							PlayerBattleRule.Instance.arrowHead.SetActive(false);
							data.count--;
							PlayerBattlePanel.ShowBulletCount(data.count);
						}

					}
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					PlayerBattleRule.Instance.bulletState = BulletState.reduceTime;
					if (Input.GetMouseButtonUp(0))
					{
						SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
						scb.isOn = true;
						data.count--;
					}

				}
			}

			else if (Input.GetKeyUp(KeyCode.R))
			{
				CardBase data = null;
				if (!SpeicalCardBattleDic.TryGetValue(KeyCode.R, out data))
				{
					Debug.LogError("cardData is null");
					return;
				}
				PlayerBattleRule.Instance.bulletState = BulletState.nomal;
				if (data.speicalCardType == CardBase.SpeicalCardType.bullet)
				{
					PlayerBattlePanel.HideBulletCount();
				}

				if (data.speicalCardType == CardBase.SpeicalCardType.layUp)
				{
					SpeicalCardBase scb = tempBullet.GetComponent<SpeicalCardBase>();
					if (!scb.isOn)
						GameObject.Destroy(tempBullet);
				}



			}


		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			keyContainer.gameObject.SetActive(true);
		}
		if (Input.GetKeyUp(KeyCode.Tab))
		{
			keyContainer.gameObject.SetActive(false);

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
		PlayerBattlePanel.HideBulletCount();
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
		for (int i = 0; i < handList[i].count; i++)
		{
			if(handList[i].id == CommonHelper.Str2Int(choseLeveeUpGame.name))
			{
				int id = CommonHelper.Str2Int(current.gameObject.name);
				CardBase data = BattleCardManager.intance.GetCardBaseDataById(id);
				handList[i] = data;
				int index = choseLeveeUpGame.transform.GetSiblingIndex();
				choseLeveeUpGame.SetActive(false);
				Destroy(choseLeveeUpGame, 0.5f);
				current.transform.SetParent(grid.transform, true);
				current.transform.SetSiblingIndex(index);
			}
		}
		int tempId = CommonHelper.Str2Int(current.gameObject.name);
		CardBase tempData = BattleCardManager.intance.GetCardBaseDataById(tempId);
		RoundRule.Instance.ChangePValue(-(tempData.cost));
		costLabelWidget.DestroyChildren();
		current.transform.GetComponent<DragSkillCard>().enabled = true;
		current.transform.GetComponent<DragSkillCard>().OnCardDragFished.Add(new EventDelegate(BattleCardEvent));
		current.enabled = false;
		grid.Reposition();
	}

	static void BattleCardEvent()
	{
		DragSkillCard dragCard = DragSkillCard.current;
		if (!keyContainer.gameObject.activeSelf)
		{
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
				GameObject costLabel = GameObject.Instantiate(costLabelPrefab);
				costLabel.transform.SetParent(costLabelWidget, false);
				costLabel.transform.localPosition = new Vector3(levelUpGrid.transform.localPosition.x + go.transform.localPosition.x, -307.2f, 480f);
				UILabel label = costLabel.GetComponent<UILabel>();
				label.text = data.LevevUpList[i].cost.ToString();
				int pValue = RoundRule.Instance.GetPValue();
				if (data.LevevUpList[i].cost > pValue)
				{
					label.color = new Color(255, 0, 0);
				}
				costLabel.gameObject.SetActive(true);
			}
			levelUpGrid.Reposition();
			levelUpGrid.gameObject.SetActive(true);
			dragCard.ReturnStartVec();
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
					dragCard.transform.SetParent(keyContainer, true);
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
				}
			}

			else
			{
				DragSkillCard.current.ReturnStartVec();
			}
			GridHandCard();
		}

	
	}

    #endregion
}
