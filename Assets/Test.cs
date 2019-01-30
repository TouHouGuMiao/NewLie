using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public List<ItemData> list = new List<ItemData>();
	// Use this for initialization
	void Start () {
      ItemDataManager.Instance.GetHasEquipList();
        //Debug.Log(list[0].itemType);
        Debug.Log(list.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
