using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : IView
{
    List<ItemData> ItemDataList = new List<ItemData>();
    List<ItemData> MateriaDatalList = new List<ItemData>();
    private UIGrid itemGrid;
    private GameObject infoItem;
    private ItemData m_ItemData;

    private GameObject itemWidget;
    private Transform clickTrans;


    private UIButton equipBtn;
    private UIButton materialBtn;
    private UIButton itemBtn;

    private UIButton itemHandBtn;
    protected override void OnStart()
    {

        itemGrid = this.GetChild("ItemGrid").GetComponent<UIGrid>();
        infoItem = this.GetChild("Item").gameObject;

        materialBtn = this.GetChild("MaterialBtn").GetComponent<UIButton>();
        itemBtn = this.GetChild("ItemBtn").GetComponent<UIButton>();
        equipBtn = this.GetChild("EquipBtn").GetComponent<UIButton>();
        itemWidget = this.GetChild("ItemWidget").gameObject;
        itemHandBtn = this.GetChild("ItemHandBtn").GetComponent<UIButton>();

        AddEventInBtn();



    }

    protected override void OnShow()
    {
        ItemDataList = ItemDataManager.Instance.GetHasItemList();
        MateriaDatalList = ItemDataManager.Instance.GetHasMaterialList();
        OnItemBtnClick();
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {

    }

    #region 



    private void OnChildItemClick_Go(GameObject go)
    {
        clickTrans = go.transform;
    }

    private void OnChildItemClick_ID(int id)
    {
        m_ItemData = ItemDataManager.Instance.GetItemDataByID(id);
        if (m_ItemData == null)
        {
            Debug.LogError("Inventory ItemData is null");
            return;
        }
        if (m_ItemData.itemType == ItemType.material)
        {
            float width = clickTrans.GetComponent<UIWidget>().width;
            itemWidget.transform.position = new Vector3(itemWidget.transform.position.x, clickTrans.position.y, 0);
            itemWidget.SetActive(true);
        }
    }


    private void OnMaterialBtnClick()
    {
        if (itemWidget.activeInHierarchy)
        {
            itemWidget.SetActive(false);
        }
        UpdataItemGridData(MateriaDatalList);
    }


    private void OnItemBtnClick()
    {
        if (itemWidget.activeInHierarchy)
        {
            itemWidget.SetActive(false);
        }
        UpdataItemGridData(ItemDataList);
    }

    #endregion




    void UpdataItemGridData(List<ItemData> DataList)
    {
        if (itemGrid.transform.childCount <= DataList.Count)
        {
            for (int i = 0; i < itemGrid.transform.childCount; i++)
            {
                GameObject go = itemGrid.transform.GetChild(i).gameObject;
                UISprite icon = go.transform.Find("icon").GetComponent<UISprite>();
                icon.spriteName = DataList[i].icon;
                go.name = DataList[i].id.ToString();
                UILabel label = go.transform.Find("name").GetComponent<UILabel>();
                label.text = DataList[i].name;
                UILabel numLabel = go.transform.Find("num").GetComponent<UILabel>();
                numLabel.text = "x" + DataList[i].num.ToString();
                UIButton button = go.GetComponent<UIButton>();
                if (button.DelegateOnClickByGameObject == null)
                {
                    button.DelegateOnClickByGameObject += OnChildItemClick_Go;
                }
                if (button.DelegateOnClickByID == null)
                {
                    button.DelegateOnClickByID += OnChildItemClick_ID;
                }
                button.id = DataList[i].id;
                go.SetActive(true);
            }

            for (int i = itemGrid.transform.childCount; i < DataList.Count; i++)
            {
                GameObject go = GameObject.Instantiate(infoItem);

                UISprite icon = go.transform.Find("icon").GetComponent<UISprite>();
                icon.spriteName = DataList[i].icon;
                go.name = DataList[i].id.ToString();
                go.transform.SetParent(itemGrid.transform, false);
                UILabel label = go.transform.Find("name").GetComponent<UILabel>();
                label.text = DataList[i].name;
                UILabel numLabel = go.transform.Find("num").GetComponent<UILabel>();
                numLabel.text = "x" + DataList[i].num.ToString();
                UIButton button = go.GetComponent<UIButton>();
                if (button.DelegateOnClickByGameObject == null)
                {
                    button.DelegateOnClickByGameObject += OnChildItemClick_Go;
                }
                if (button.DelegateOnClickByID == null)
                {
                    button.DelegateOnClickByID += OnChildItemClick_ID;
                }
                button.id = DataList[i].id;
                go.SetActive(true);
            }
        }

        else
        {
            for (int i = itemGrid.transform.childCount - 1; i >= ItemDataList.Count; i--)
            {
                GameObject go = itemGrid.transform.GetChild(i).gameObject;
                go.SetActive(false);
            }

            for (int i = 0; i <= ItemDataList.Count; i++)
            {
                GameObject go = itemGrid.transform.GetChild(i).gameObject;
                UISprite icon = go.transform.Find("icon").GetComponent<UISprite>();
                icon.spriteName = DataList[i].icon;
                go.name = DataList[i].id.ToString();
                UILabel label = go.transform.Find("name").GetComponent<UILabel>();
                label.text = DataList[i].name;
                UIButton button = go.GetComponent<UIButton>();
                UILabel numLabel = go.transform.Find("num").GetComponent<UILabel>();
                numLabel.text = "x" + DataList[i].num.ToString();
                if (button.DelegateOnClickByGameObject == null)
                {
                    button.DelegateOnClickByGameObject += OnChildItemClick_Go;
                }
                if (button.DelegateOnClickByID == null)
                {
                    button.DelegateOnClickByID += OnChildItemClick_ID;
                }
                button.id = DataList[i].id;
                go.SetActive(true);
            }
        }

        itemGrid.Reposition();
    }








    private void AddEventInBtn()
    {
        EventDelegate ItemBtnEvent = new global::EventDelegate(OnItemBtnClick);
        EventDelegate materialEvent = new global::EventDelegate(OnMaterialBtnClick);


        itemBtn.onClick.Add(ItemBtnEvent);
        materialBtn.onClick.Add(materialEvent);

    }


    public override void Update()
    {

    }

}
