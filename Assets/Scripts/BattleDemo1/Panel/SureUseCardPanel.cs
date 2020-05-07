using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureUseCardPanel : MonoBehaviour {
	private  static CardBase currentData;
	private UIButton sureBtn;
	private UIButton saleBtn;
	private static UILabel useNeedValueLabel;
	private static  UILabel saleValueLabel;
	private static GameObject container;
	// Use this for initialization
	void Start () {
		container = transform.Find("Container").gameObject;
		sureBtn = transform.FindRecursively("SureBtn").GetComponent<UIButton>();
		useNeedValueLabel = sureBtn.transform.Find("PValue").GetComponent<UILabel>();
		sureBtn.onClick.Add(new EventDelegate(OnSureBtnClick));
		saleBtn = transform.FindRecursively("SaleBtn").GetComponent<UIButton>();
		saleValueLabel = saleBtn.transform.Find("PValue").GetComponent<UILabel>();
		saleBtn.onClick.Add(new EventDelegate(OnSaleBtnClick));
	}

	public static void ShowContainer(CardBase data)
	{
		currentData = data;
		useNeedValueLabel.text = data.cost.ToString();
		saleValueLabel.text = ((int)(0.25f* data.cost)).ToString();
		container.gameObject.SetActive(true);
	}

	public static void CloseContainer()
	{
		currentData = null;
		container.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void OnSureBtnClick()
	{
		if (!RoundRule.CompltePValue(-currentData.cost))
		{
			return;
		}
		RoundRule.ChangePValue(-currentData.cost);
		EventDelegate.Execute(currentData.CardSpeicalEvent);
		CloseContainer();
	}

	private void OnSaleBtnClick()
	{
		RoundRule.ChangePValue((int)(0.25f* currentData.cost));
		HandCardPanel.RemoveDataFromHandList(currentData.id);
		CloseContainer();


	}

}
