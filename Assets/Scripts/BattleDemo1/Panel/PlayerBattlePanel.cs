using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattlePanel : MonoBehaviour {
	private static UISlider hpSlider;
	private static UISlider mpSlider;
	private static UIButton startBattleBtn;
	private UILabel currentCost;
	private UILabel maxCost;
	private static UILabel countLabel;
	private static AttributeBase playerAttirbute;
	private Camera uiCamera;
	public static UILabel pvalueLabel;
	public static UILabel timeLabel;

	private UIButton drawBtn;
	// Use this for initialization
	void Awake()
	{
		//drawBtn = transform.FindRecursively("drawBtn").GetComponent<UIButton>();
		//drawBtn.onClick.Add(new EventDelegate(OnDrawBtnClick));
		playerAttirbute = GameObject.FindWithTag("Player").GetComponent<AttributeBase>();
		hpSlider = transform.Find("HPSlider").GetComponent<UISlider>();
		mpSlider = transform.Find("MPSlider").GetComponent<UISlider>();
		//startBattleBtn = transform.FindRecursively("battleStart").GetComponent<UIButton>();
		//startBattleBtn.onClick.Add(new EventDelegate(OnRoundStartBtnClick));
		currentCost = transform.FindRecursively("currentCost").GetComponent<UILabel>();
		maxCost = transform.FindRecursively("costMax").GetComponent<UILabel>();
		countLabel = transform.FindRecursively("countLabel").GetComponent<UILabel>();
		uiCamera = transform.parent.FindRecursively("2DUICamera").GetComponent<Camera>();
		pvalueLabel = gameObject.transform.FindRecursively("pvalueLabel").GetComponent<UILabel>();
		timeLabel = gameObject.transform.FindRecursively("timeLabel").GetComponent<UILabel>();
	
	}

	public static void CloseTimelabel()
	{
		timeLabel.gameObject.SetActive(false);
	}

	public static void ShowTimeLabel()
	{
		timeLabel.gameObject.SetActive(true);
	}

	public static void ShowBulletCount(int count)
	{
		countLabel.text = count.ToString();
		countLabel.gameObject.SetActive(true);
	}

	public static void HideBulletCount()
	{
		countLabel.gameObject.SetActive(false);
	}


	void Start () {
	 	
	}


	// Update is called once per frame
	void Update () {
		
		if (countLabel.gameObject.activeSelf)
		{
			CountLabelTogatherWithMouse();
		}
	}





	private void CountLabelTogatherWithMouse()
	{
		Vector3 tempVec = uiCamera.ScreenToWorldPoint(Input.mousePosition+new Vector3 (25,0,0));
		countLabel.transform.position = tempVec;
	}

	public void OnRoundStartBtnClick()
	{
		HandCardPanel.OnRoundBattleStart();
		//startBattleBtn.gameObject.SetActive(false);
		GameObject enemy = GameObject.FindWithTag("enemy");
		DemoAI ai = enemy.GetComponent<DemoAI>();
		if (RoundRule.Instance.roundCount == 1)
		{
			ai.UseBoomOfHeadEasy();
			RoundRule.Instance.SetNextRoundTime(10);
		}
		else if (RoundRule.Instance.roundCount == 2)
		{
			ai.UseCarouselNormal();
			RoundRule.Instance.SetNextRoundTime(19);
		}

		else if (RoundRule.Instance.roundCount == 3)
		{
			ai.UseFatherDream();
			RoundRule.Instance.SetNextRoundTime(22);
		}
		else if (RoundRule.Instance.roundCount == 4)
		{

			ai.UseCarouselDiffcult();
			RoundRule.Instance.SetNextRoundTime(20);
		}

		else if (RoundRule.Instance.roundCount == 5)
		{
			ai.UseDiffcultBoomOfHead();
			RoundRule.Instance.SetNextRoundTime(25);
		}

		else if (RoundRule.Instance.roundCount == 6)
		{
			ai.UseBoomOfRose();
			RoundRule.Instance.SetNextRoundTime(35);
		}


		else if (RoundRule.Instance.roundCount == 7)
		{
			ai.UseBoomOfHeadAndCarousl();
			RoundRule.Instance.SetNextRoundTime(12);
		}

		else if (RoundRule.Instance.roundCount == 8)
		{
			ai.UseSelf();
			RoundRule.Instance.SetNextRoundTime(45);
		}

		else if (RoundRule.Instance.roundCount == 9)
		{
			ai.UseSuperEgo();
			RoundRule.Instance.SetNextRoundTime(50);
		}
	}
	public void OnDrawBtnClick()
	{
		if (HandCardPanel.handList.Count >= 7)
		{
			return;
		}
		HandCardPanel.DrawCard(1);
	}


	public static void ShowHandCardPanel()
	{
		HandCardPanel.ShowHandGrid();
		//startBattleBtn.gameObject.SetActive(true);
	}



	public static void UpdateHPSlider(float value)
	{
		float deta = value / playerAttirbute.MaxHP;
		hpSlider.value += deta;
	}

	public static void UpdateMPSlider(float value)
	{
		float deta = value / playerAttirbute.MaxMP;
		mpSlider.value += deta;
	}



	public static void UpdatePValueLabel(int value)
	{
		pvalueLabel.text = value.ToString();
	}
}
