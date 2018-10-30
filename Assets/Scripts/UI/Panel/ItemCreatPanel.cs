using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreatPanel : IView
{
    private List<ItemData> ItemDataList = new List<ItemData>();
    private UIGrid itemGrid;
    private GameObject infoItem;

    private GameObject itemNext;
    private UIGrid materialGrid;
    private GameObject materialItem;

    public ItemCreatPanel()
    {
        m_Layer = Layer.UI;
    }
    protected override void OnStart()
    {
        itemGrid = this.GetChild("ItemGrid").GetComponent<UIGrid>();
        infoItem = this.GetChild("infoItem").gameObject;
        itemNext = this.GetChild("ItemNext").gameObject;
        materialItem = this.GetChild("materialItem").gameObject;
        materialGrid = this.GetChild("MaterialGrid").GetComponent<UIGrid>();
    }
    protected override void OnShow()
    {
        ItemDataList = ItemDataManager.Instance.GetHasItemList();
        ItemData data = new ItemData();
        data.id = 0;
        data.name = "破旧的怀表";
        data.icon = "0";
        ItemDataList.Add(data);

        ItemDataList = ItemDataManager.Instance.GetHasItemList();
        ItemData data1 = new ItemData();
        data1.id = 8;
        data1.name = "破旧的怀表2";
        data1.icon = "8";
        ItemDataList.Add(data1);

        UpdataItemGridData();


    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        ItemDataList.Clear();
    }




    void UpdataItemGridData()
    {

        if (itemGrid.transform.childCount <= ItemDataList.Count)
        {
            for (int i = 0; i < itemGrid.transform.childCount; i++)
            {
                GameObject go = itemGrid.transform.GetChild(i).gameObject;
                UISprite sprite = go.transform.Find("Sprite").GetComponent<UISprite>();
                sprite.spriteName = ItemDataList[i].icon;
                go.name = ItemDataList[i].id.ToString();
                UILabel label = go.transform.Find("name").GetComponent<UILabel>();
                label.text = ItemDataList[i].name;
                UIButton button = go.GetComponent<UIButton>();
                if (button.DelegateOnClickByID == null)
                {
                    button.DelegateOnClickByID += UpDataNextItemData;
                }
                button.id = ItemDataList[i].id;
                go.SetActive(true);
            }

            for (int i = itemGrid.transform.childCount; i < ItemDataList.Count; i++)
            {
                GameObject go = GameObject.Instantiate(infoItem);

                UISprite sprite = go.transform.Find("Sprite").GetComponent<UISprite>();
                sprite.spriteName = ItemDataList[i].icon;
                go.name = ItemDataList[i].id.ToString();
                go.transform.SetParent(itemGrid.transform, false);
                UILabel label = go.transform.Find("name").GetComponent<UILabel>();
                label.text = ItemDataList[i].name;
                UIButton button = go.GetComponent<UIButton>();
                if (button.DelegateOnClickByID == null)
                {
                    button.DelegateOnClickByID += UpDataNextItemData;
                }
                button.id = ItemDataList[i].id;
                go.SetActive(true);
            }
        }

        else
        {
            for (int i = itemGrid.transform.childCount - 1; i > ItemDataList.Count; i--)
            {
                GameObject go = itemGrid.transform.GetChild(i).gameObject;
                go.SetActive(false);
            }
        }

        itemGrid.Reposition();
    }


    void UpdataMateriralItemData(int id)
    {
        ItemCreatData data = ItemCreatManager.Instance.GetItemCreatDataByLastID(id);

        if (materialGrid.transform.childCount <= data.NeedItemList.Count)
        {
            for (int i = 0; i < materialGrid.transform.childCount; i++)
            {
                GameObject go = materialGrid.transform.GetChild(i).gameObject;
                UISprite sprite = go.transform.Find("icon").GetComponent<UISprite>();
                sprite.spriteName = data.NeedItemList[i].icon;
                go.name = data.NeedItemList[i].id.ToString();
                UILabel label = go.transform.Find("Label").GetComponent<UILabel>();
                label.text = "x" + data.NeedItemNumList[i].ToString();
                go.SetActive(true);
            }

            for (int i = materialGrid.transform.childCount; i < data.NeedItemList.Count; i++)
            {
                GameObject go = GameObject.Instantiate(materialItem);

                UISprite sprite = go.transform.Find("icon").GetComponent<UISprite>();
                sprite.spriteName = data.NeedItemList[i].icon;
                go.name = data.NeedItemList[i].id.ToString();
                UILabel label = go.transform.Find("Label").GetComponent<UILabel>();
                label.text = "x" + data.NeedItemNumList[i].ToString();
                go.transform.SetParent(materialGrid.transform, false);
                go.SetActive(true);
            }
        }

        else
        {
            for (int i = materialGrid.transform.childCount - 1; i > data.NeedItemList.Count; i--)
            {
                GameObject go = materialGrid.transform.GetChild(i).gameObject;
                go.SetActive(false);
            }
        }

        materialGrid.Reposition();
    }



 
    #region//按钮绑定方法
    void UpDataNextItemData(int id)
    {
        ItemCreatData data = ItemCreatManager.Instance.GetItemCreatDataByLastID(id);
        UISprite sprite = itemNext.transform.Find("icon").GetComponent<UISprite>();
        sprite.spriteName = data.itemData.icon;
        itemNext.name = data.itemData.id.ToString();
        UILabel label = itemNext.transform.Find("Label").GetComponent<UILabel>();
        label.text = data.itemData.name;
        itemNext.SetActive(true);
        UpdataMateriralItemData(id);
    }
    #endregion


}
