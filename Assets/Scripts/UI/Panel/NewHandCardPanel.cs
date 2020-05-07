using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHandCardPanel : MonoBehaviour {
	private UIGrid handGird;
	private List<CardBase> handCardList = new List<CardBase>();
	private List<CardBase> groundCardList = new List<CardBase>();
	// Use this for initialization
	void Start () {
		handGird = transform.Find("grid").GetComponent<UIGrid>();
		
	}
	
	void GridHandList()
	{
		List<int> idList = new List<int>();
		List<int> tempList = new List<int>();
		for (int i = 0; i < handGird.transform.childCount; i++)
		{
			tempList.Add(CommonHelper.Str2Int( handGird.transform.GetChild(i).gameObject.name));
		}
		for (int i = 0; i < handCardList.Count; i++)
		{
			idList.Add( handCardList[i].id);
		}
		for (int i = 0; i < handCardList.Count; i++)
		{
			for (int j = 0; j < tempList.Count; j++)
			{
				if (tempList[i] == handCardList[i].id)
				{
					tempList.Remove(tempList[i]);
					idList.Remove(tempList[i]);
					break;
				}
			}
		}
		for (int i = 0; i < idList.Count; i++)
		{
			GameObject go =GameObject.Instantiate( BattleCardManager.intance.GetCardBaseDataById(idList[i]).prefab);
			go.transform.SetParent(handGird.transform, false);
			go.transform.localPosition = new Vector3(1600, 0, 493);
			go.name = idList.ToString();
		}
		handGird.Reposition();
	}


	// Update is called once per frame
	void Update () {
		
	}


}
