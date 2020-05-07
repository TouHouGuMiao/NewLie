using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoTableInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		CreatDemoTable();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreatDemoTable()
	{
		TableMoveManager.Instance.InitTable(3, 10);
		List<TableCardData> uplist = new List<TableCardData>();
		uplist.Add(TableCardDataManager.Instance.GetDateById(0));
		uplist.Add(TableCardDataManager.Instance.GetDateById(0));
		uplist.Add(TableCardDataManager.Instance.GetDateById(6));
		List<TableCardData> tempEventList = new List<TableCardData>();
		tempEventList = TableCardDataManager.Instance.GetRandemEvent(2);
		for (int i = 0; i < tempEventList.Count; i++)
		{
			uplist.Add(tempEventList[i]);
		}
		uplist = OtherHelper.Instance.Shuffle<TableCardData>(uplist);
		int initIndex = 2;
		for (int i = 0; i < uplist.Count; i++)
		{
			TableMoveManager.Instance.AddGameObjectToTable(new Vector2(0, initIndex), uplist[i]);
			initIndex++;
		}

		tempEventList.Clear();
		tempEventList = TableCardDataManager.Instance.GetRandemEvent(1);
		List<TableCardData> downlist = new List<TableCardData>();
		downlist.Add(TableCardDataManager.Instance.GetDateById(0));
		downlist.Add(TableCardDataManager.Instance.GetDateById(0));
		downlist.Add(tempEventList[0]);
		downlist.Add(TableCardDataManager.Instance.GetDateById(6));
		downlist = OtherHelper.Instance.Shuffle<TableCardData>(downlist);
		downlist.Add(TableCardDataManager.Instance.GetDateById(7));

		initIndex = 2;
		for (int i = 0; i < downlist.Count; i++)
		{
			TableMoveManager.Instance.AddGameObjectToTable(new Vector2(2, initIndex), downlist[i]);
			initIndex++;
		}

	}
}
