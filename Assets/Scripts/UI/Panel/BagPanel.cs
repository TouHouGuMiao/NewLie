using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : IView
{
    private List<ItemData> m_ItemList;
    private List<ItemData> EquipmentList = new List<ItemData>();
    private List<ItemData> MaterialList = new List<ItemData>();
    private GameObject m_ItemGo;
    private GameObject TrippleBtn;//是装备，材料等按钮的父物体
    private UIButton EquipmentBtn;
    private UIButton MaterialBtn;
    private UIButton UseBtn;
    //private GameObject secondLayer;
    private UIGrid ItemGrid;
    //private GameObject ItemWidget;
    //private GameObject ShowInfo;
    private GameObject ShowUI;//用于使整体显示的GameObject
                              // private int ItemID;
    private List<int> ItemIdList = new List<int>();
    static int ItemId;//记录点击的物品的ID的静态数据
    private UILabel ItemsName;//物品名称的控件
    private static int ButtonType;//记录背包更新的是什么表




    // Use this for initialization
    public override void Update() {
        ButtonControl();
    }

    protected override void OnDestroy()
    {

    }
    protected override void OnHide()
    {

    }
    protected override void OnShow()
    {

        EquipmentList = ItemDataManager.Instance.GetHasEquipList();
        MaterialList = ItemDataManager.Instance.GetHasMaterialList();
        //OnItemsClick();
        //ButtonControl();

    }
    protected override void OnStart()
    {
        
        ShowUI = this.GetChild("ItemShowUI").gameObject;
        m_ItemGo = this.GetChild("Item").gameObject;
        m_ItemGo.AddComponent<UIButton>();
        EquipmentBtn = this.GetChild("EquipBtn").GetComponent<UIButton>();
        MaterialBtn = this.GetChild("MaterialBtn").GetComponent<UIButton>();
        ItemGrid = this.GetChild("ItemGrid").GetComponent<UIGrid>();
        ItemsName = this.GetChild("Name").GetComponent<UILabel>();
        //ItemWidget = this.GetChild("ItemWidget").gameObject;
        //ShowInfo = this.GetChild("ShowInfo").gameObject;
        UseBtn = this.GetChild("ItemUseBtn").GetComponent<UIButton>();
        TrippleBtn = this.GetChild("Grid").gameObject;

        AddEventDelegate();
        //AddButtonInList();
        //Debug.Log(UIButton.current.name);
        //ButtonControl();
    }

    void OnClickEquipment()//点击装备按钮时的操作
    {
        if (ShowUI.activeInHierarchy == true)
        {
            ShowUI.SetActive(false);
        }       
        UpdataBg(EquipmentList);
        ButtonType = 0;

    }
    void OnClickMaterial() {//点击材料按钮时的操作
        if (ShowUI.activeInHierarchy == true) {
            ShowUI.SetActive(false);
        }       
        UpdataBg(MaterialList);
        ButtonType = 1;//材料按钮的类型是1
    }
    

    
    void OnItemsClick()//点击具体物品时的响应事件
    {

        if (ShowUI.activeInHierarchy == false)
        {
            ShowUI.SetActive(true);
        }
        else
        {
            ShowUI.SetActive(false);
        }
        ItemId = System.Int32.Parse(UIButton.current.name);
        
        switch (ButtonType) {
            case 0: UpdataInformation(EquipmentList, ItemId - 1);
            break;
            case 1:UpdataInformation(MaterialList, ItemId - 1);
            break;
        }
        
    }


    void UpdataBg(List<ItemData> DataList)
    {
       
         EventDelegate  OnItemClickBtn = new global:: EventDelegate(OnItemsClick);                         
            if (ItemGrid.transform.childCount <= DataList.Count)
            {
            
            for (int i = 0; i < ItemGrid.transform.childCount; i++)
            {
                GameObject m_Item = ItemGrid.transform.GetChild(i).gameObject;
                m_Item.name = DataList[i].id.ToString();
               // m_Item.transform.SetParent(ItemGrid.GetComponent<Transform>(), false);

                UISprite mIcon = m_Item.transform.Find("icon").GetComponent<UISprite>();
                mIcon.spriteName = DataList[i].icon;
                UILabel mName = m_Item.transform.Find("name").GetComponent<UILabel>();
                mName.text = DataList[i].name;
                UILabel itemNum = m_Item.transform.Find("num").GetComponent<UILabel>();
                itemNum.text = "x" + DataList[i].num.ToString();
                //m_Item.AddComponent<GetItemDataTools>();
                m_Item.GetComponent<UIButton>().onClick.Remove(OnItemClickBtn);
                m_Item.GetComponent<UIButton>().onClick.Add(OnItemClickBtn);
                //ItemID = m_Item.GetComponent<GetItemDataTools>().getMyGameObjectID();
                // ItemIdList.Add(ItemID);
                m_Item.SetActive(true);
            }
            for (int i = ItemGrid.transform.childCount; i < DataList.Count; i++) {
                GameObject m_Item = GameObject.Instantiate(m_ItemGo);
                m_Item.name = DataList[i].id.ToString();
                m_Item.transform.SetParent(ItemGrid.GetComponent<Transform>(), false);

                UISprite mIcon = m_Item.transform.Find("icon").GetComponent<UISprite>();
                mIcon.spriteName = DataList[i].icon;
                UILabel mName = m_Item.transform.Find("name").GetComponent<UILabel>();
                mName.text = DataList[i].name;
                UILabel itemNum = m_Item.transform.Find("num").GetComponent<UILabel>();
                itemNum.text = "x" + DataList[i].num.ToString();
                //m_Item.AddComponent<GetItemDataTools>();
                m_Item.GetComponent<UIButton>().onClick.Remove(OnItemClickBtn);
                m_Item.GetComponent<UIButton>().onClick.Add(OnItemClickBtn);
                //ItemID = m_Item.GetComponent<GetItemDataTools>().getMyGameObjectID();
                // ItemIdList.Add(ItemID);
                m_Item.SetActive(true);
            }

            }
            else
            {
            for (int i = ItemGrid.transform.childCount-1; i >= DataList.Count; i--) {
                GameObject m_Item = ItemGrid.transform.GetChild(i).gameObject;
                m_Item.SetActive(false);
            }
            for (int i = 0; i < DataList.Count; i++) {//注意这个等于号，不能写i<=DataList.Count!!!
                GameObject m_Item = ItemGrid.transform.GetChild(i).gameObject;
                m_Item.name = DataList[i].id.ToString();
                // m_Item.transform.SetParent(ItemGrid.GetComponent<Transform>(), false);

                UISprite mIcon = m_Item.transform.Find("icon").GetComponent<UISprite>();
                mIcon.spriteName = DataList[i].icon;
                UILabel mName = m_Item.transform.Find("name").GetComponent<UILabel>();
                mName.text = DataList[i].name;
                UILabel itemNum = m_Item.transform.Find("num").GetComponent<UILabel>();
                itemNum.text = "x" + DataList[i].num.ToString();
                //m_Item.AddComponent<GetItemDataTools>();
                m_Item.GetComponent<UIButton>().onClick.Remove(OnItemClickBtn);
                m_Item.GetComponent<UIButton>().onClick.Add(OnItemClickBtn);
                //ItemID = m_Item.GetComponent<GetItemDataTools>().getMyGameObjectID();
                // ItemIdList.Add(ItemID);
                m_Item.SetActive(true);
            }
            }
            ItemGrid.Reposition();                        
    }
    void UpdataInformation(List<ItemData> DataList,int Item_ID) {//更新背包右边显示窗口的方法
        ItemsName.text = DataList[Item_ID].name;       
    }




    void AddEventDelegate()
    {
        
        EventDelegate OnClickEquipmentBtn = new global::EventDelegate(OnClickEquipment);
        EventDelegate OnClickMaterialBtn = new global::EventDelegate(OnClickMaterial);
        EventDelegate OnClickUseBtn = new global::EventDelegate(ClickUse);

        EquipmentBtn.onClick.Add(OnClickEquipmentBtn);
        MaterialBtn.onClick.Add(OnClickMaterialBtn);
        UseBtn.onClick.Add(OnClickUseBtn);
      

    }
    void ClickUse() {//点击使用的按钮时的响应事件

    }
    List<UIButton> m_BtnList = new List<UIButton>();
    void AddButtonInList()//将Button存入数组中
    {
        for (int i = 0; i < TrippleBtn.transform.childCount; i++)
        {
            m_BtnList = ButtonManager.Instance.Add_Btn(TrippleBtn.transform.GetChild(i).GetComponent<UIButton>());
            // Debug.Log(m_BtnList[i].name);
        }
    }
    int index = 0; //记录是第几个Button
    UIButton btn = new UIButton();
    void ButtonControl()
    {
        AddButtonInList();
        btn = m_BtnList[index];
        for (int i = 0; i < m_BtnList.Count; i++)
        {
            m_BtnList[i].isActive_Button = false;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (btn == m_BtnList[2])
            {
                index = 0;
                btn = m_BtnList[index];
                Debug.Log(m_BtnList[index].name);
            }
            else
            {
                btn = m_BtnList[index++];
                btn.isActive_Button = true;
                Debug.Log(m_BtnList[index].name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) )
        {
            if (btn == m_BtnList[0])
            {
                index = 2;
                btn = m_BtnList[index];
                Debug.Log(m_BtnList[index].name);
            }
            else
            {
                btn = m_BtnList[index--];
                btn.isActive_Button = true;
                Debug.Log(m_BtnList[index].name);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            switch (index) {
                case 0:                                        
                  OnClickEquipment();                                    
                    break;
                case 1:                    
                  OnClickMaterial();                                        
                    break;
                case 2:
                    break;

            }

        }

    }
}



