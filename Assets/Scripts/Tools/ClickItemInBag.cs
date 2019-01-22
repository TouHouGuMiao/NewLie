using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItemInBag:MonoBehaviour  {
    public GameObject item;
    public UIButton itemBtn;
    public GameObject itemWidget;
    //public static int itemID;
    // Use this for initialization
    void Start() {
        item = GameObject.Find("Item");
        itemWidget = GameObject.Find("ItemWidget");
        item.AddComponent<UIButton>();
        AddOnClickEventDelegate();
    }
    void ClickItem()
    {
        if (itemWidget.activeInHierarchy == true)
        {
            itemWidget.SetActive(false);
        }
        else {
            itemWidget.SetActive(true);
        }

    }
    void AddOnClickEventDelegate() {
        EventDelegate OnClickItem = new global::EventDelegate(ClickItem);

        item.GetComponent<UIButton>().onClick.Add(OnClickItem);
    }

    void Updata() {

    }
}
