using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : IView
{
    private List<ItemData> ItemsList;
    private List<ItemData> EquipmentList;
    private List<ItemData> MaterialList;
    private List<UIButton> ItemsBtnControlList = new List<UIButton>();
    private List<UIButton> m_BtnList = new List<UIButton>();
    private List<UIButton> ItemWidgetList = new List<UIButton>();//功能按钮的存储表

    private GameObject m_ItemGo;
    private GameObject TrippleBtn;//是装备，材料等按钮的父物体
    private GameObject ShowUI;//用于使整体显示的GameObject
    private GameObject ChoiceMark;//按钮选择标记 
    private GameObject FunctionChoice;//三个功能选择
    private GameObject ScrollItems;

    private UIButton EquipmentBtn;
    private UIButton MaterialBtn;
    private UIButton ItemsBtn;//物品按钮
    private UIButton UseBtn;
  
    //private GameObject secondLayer;
    private UIGrid ItemGrid;
    //private GameObject ItemWidget;
    //private GameObject ShowInfo;

    private List<int> ItemIdList = new List<int>();
    public static int ItemId;//记录点击的物品的ID的静态数据
    private UILabel ItemsName;//物品名称的控件
    public static int ButtonType;//记录背包更新的是什么表
    private bool isActiveLeftArrow = false;
    private bool isActiveWhenShowUI = false;



    public bool isActive_1 = false;//用于记录背包是否update的变量
  
    public BagPanel()
    {
        m_Layer = Layer.System;
    }

    // Use this for initialization
    public override void Update()
    {
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

        //ItemDataManager.Instance.LoadItemXml(pathName,m_ItemList);
        // ItemDataManager.Instance.LoadItemXml("ItemConfig",m_ItemList);
        SystemPanel.Bg_IsActive = true;
        EquipmentList = ItemDataManager.Instance.GetHasEquipList();
        MaterialList = ItemDataManager.Instance.GetHasMaterialList();
        ItemsList = ItemDataManager.Instance.GetHasItemsList();
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
        ItemsBtn = this.GetChild("ItemBtn").GetComponent<UIButton>();
        ItemGrid = this.GetChild("ItemGrid").GetComponent<UIGrid>();
        ItemsName = this.GetChild("Name").GetComponent<UILabel>();
        //ItemWidget = this.GetChild("ItemWidget").gameObject;
        //ShowInfo = this.GetChild("ShowInfo").gameObject;
        UseBtn = this.GetChild("ItemUseBtn").GetComponent<UIButton>();
        TrippleBtn = this.GetChild("Grid").gameObject;
        ChoiceMark = this.GetChild("ChoiceMark").gameObject;
        FunctionChoice = this.GetChild("ItemWidget").gameObject;
       // ScrollItems = this.GetChild("ScrolItemView").gameObject;

        AddButtonInList();
        //AddButton();
        AddEventDelegate();

        // Debug.Log(ItemWidgetList[0].name);
        // Debug.Log(FunctionChoice.name);
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
        //UIButton btn = UIButton.current;        
        UpdataBg(EquipmentList);
        ButtonType = 0;

    }

    void OnClickMaterial()
    {//点击材料按钮时的操作
        if (ShowUI.activeInHierarchy == true)
        {
            ShowUI.SetActive(false);
        }
        UpdataBg(MaterialList);
        ButtonType = 1;//材料按钮的类型是1
    }
    void OnClickItems()
    {//当点击物品按钮时的操作
        if (ShowUI.activeInHierarchy == true)
        {
            ShowUI.SetActive(false);
        }
        UpdataBg(ItemsList);
        ButtonType = 2;
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
        switch (ButtonType)
        {
            case 0:
                UpdataInformation(EquipmentList, ItemId);
                break;
            case 1:
                UpdataInformation(MaterialList, ItemId);
                break;
            case 2:
                UpdataInformation(ItemsList, ItemId);
                break;
        }


    }

    void OnItemsClick_2()//按键具体物品时的响应事件
    {

        if (ShowUI.activeInHierarchy == false)
        {
            ShowUI.SetActive(true);
        }

        ItemId = System.Int32.Parse(ItemsBtnControlList[index].name);
        //int.TryParse(m_BtnList[index].name, out ItemId);
        // Debug.Log(ItemId);

        switch (ButtonType)
        {
            case 0:
                UpdataInformation(EquipmentList, ItemId);
                break;
            case 1:
                UpdataInformation(MaterialList, ItemId);
                break;
            case 2:
                UpdataInformation(ItemsList, ItemId);
                break;
        }

    }


    void UpdataBg(List<ItemData> DataList)
    {
        ItemsBtnControlList.Clear();
        EventDelegate OnItemClickBtn = new global::EventDelegate(OnItemsClick);
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
                ItemsBtnControlList.Add(m_Item.GetComponent<UIButton>());
                //ItemID = m_Item.GetComponent<GetItemDataTools>().getMyGameObjectID();
                // ItemIdList.Add(ItemID);
                m_Item.SetActive(true);
            }
            for (int i = ItemGrid.transform.childCount; i < DataList.Count; i++)
            {
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
                ItemsBtnControlList.Add(m_Item.GetComponent<UIButton>());
                //ItemID = m_Item.GetComponent<GetItemDataTools>().getMyGameObjectID();
                // ItemIdList.Add(ItemID);
                m_Item.SetActive(true);
            }

        }
        else
        {

            for (int i = ItemGrid.transform.childCount - 1; i >= DataList.Count; i--)
            {
                GameObject m_Item = ItemGrid.transform.GetChild(i).gameObject;
                m_Item.SetActive(false);
            }
            for (int i = 0; i < DataList.Count; i++)
            {//注意这个等于号，不能写i<=DataList.Count!!!
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
                ItemsBtnControlList.Add(m_Item.GetComponent<UIButton>());
                //ItemID = m_Item.GetComponent<GetItemDataTools>().getMyGameObjectID();
                // ItemIdList.Add(ItemID);
                m_Item.SetActive(true);
            }
        }
       // ItemGrid.Reposition();
        ItemGrid.repositionNow = true;
    }
    void UpdataInformation(List<ItemData> DataList, int Item_ID)
    {//更新背包右边显示窗口的方法
        //foreach (ItemData data in DataList) {
        //    if (data.id == Item_ID)
        //    {
        //        ItemsName.text = data.name;
        //    }
        //    else {
        //        Debug.LogError("data.id" + data.id + "is not equals Item_ID,please check it!");
        //        break; ;
        //    }
        //}
        for (int i = 0; i < DataList.Count; i++)
        {
            if (DataList[i].id == Item_ID)
            {
                ItemsName.text = DataList[i].name;
            }
            //else
            //{
            //    Debug.LogError("data.id" + DataList[i].id + "is not equals Item_ID,please check it!");
            //    continue;
            //}
        }
    }




    void AddEventDelegate()
    {

        EventDelegate OnClickEquipmentBtn = new global::EventDelegate(OnClickEquipment);
        EventDelegate OnClickMaterialBtn = new global::EventDelegate(OnClickMaterial);
        EventDelegate OnClickitemsBtn = new global::EventDelegate(OnClickItems);
        EventDelegate OnClickUseBtn = new global::EventDelegate(ClickUse);

        EquipmentBtn.onClick.Add(OnClickEquipmentBtn);
        MaterialBtn.onClick.Add(OnClickMaterialBtn);
        ItemsBtn.onClick.Add(OnClickitemsBtn);
        UseBtn.onClick.Add(OnClickUseBtn);


    }
    void ShowItemsName(List<ItemData> list)
    {
        foreach (ItemData data in list)
        {
            if (data.id == ItemId)
            {
                Debug.Log("当前使用的物体的名字是" + data.name);
            }
        }
    }
    void ClickUse()
    {//点击使用的按钮时的响应事件
        switch (ButtonType)
        {
            case 0:
                ShowItemsName(EquipmentList);
                break;
            case 1:
                ShowItemsName(MaterialList);
                break;
            case 2:
                ShowItemsName(ItemsList);
                break;
        }
        //Debug.Log(ItemId);

    }
    void ClickUse_2()
    {//点击使用的按钮时的响应事件
        switch (ButtonType)
        {
            case 0:
                Debug.Log("当前使用的物体的名字是" + EquipmentList[index].name);
                break;
            case 1:
                Debug.Log("当前使用的物体的名字是" + MaterialList[index].name);
                break;
            case 2:
                Debug.Log("当前使用的物体的名字是" + ItemsList[index].name);
                break;
        }

    }

    void AddButtonInList()//将Button存入数组中
    {
        m_BtnList = ButtonManager.Instance.Add_Btn(TrippleBtn);
        ItemWidgetList = ButtonManager.Instance.Add_Btn(FunctionChoice);
    }

    public static int index = 0; //记录是第几个Button
    int twoTothreeIndex = 0;//记录从第一层进入第二层的时候的第一层的index
    int oneTotwoIndex = 0;
    int ItemWidgetIndex = 0;//在使用按钮进行上下选择时不干扰index对物品ID的索引
    UIButton btn = new UIButton();
    void trasver()
    {
        for (int i = 0; i < ItemsBtnControlList.Count; i++)
        {
            Debug.Log(ItemsBtnControlList[i].name);

        }
    }
    void TransformChoicePos(UIButton btn)
    {

        ChoiceMark.transform.position = btn.transform.position;//+ vt;
                                                               // Debug.Log(btn.transform.localPosition);
       // Debug.Log("indexBtn=" + index);

    }
    void ButtonControl()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) && ShowUI.activeInHierarchy == true)
        {
            if (btn == ItemWidgetList[2])
            {
                ItemWidgetIndex = 0;
                btn = ItemWidgetList[ItemWidgetIndex];

                TransformChoicePos(btn);
                Debug.Log(ItemWidgetList[ItemWidgetIndex].name);
            }
            else
            {
                ItemWidgetIndex += 1;
                btn = ItemWidgetList[ItemWidgetIndex];
                //Debug.Log(btn.transform.position);
                TransformChoicePos(btn);
                //btn.isActive_Button = true;
                Debug.Log(ItemWidgetList[ItemWidgetIndex].name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isActiveWhenShowUI == true) {
            Debug.Log("xxx");
            int length = ItemsBtnControlList.Count;
            if (ItemsBtnControlList.Count == 1)
            {
                index = 0;
                btn = ItemsBtnControlList[index];
                TransformChoicePos(btn);
            }
            else
            {
                if (btn == ItemsBtnControlList[length - 1])
                {
                   // Debug.Log("Error");
                    index = 0;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
                else
                {
                    index += 1;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isActive_1 == true)//ShowUI.activeInHierarchy == true)
        {
            int length = ItemsBtnControlList.Count;
            if (ItemsBtnControlList.Count == 1)
            {
                index = 0;
                btn = ItemsBtnControlList[index];
                TransformChoicePos(btn);
            }
            else
            {
                if (btn == ItemsBtnControlList[length - 1])
                {
                    //Debug.Log("Error");
                    index = 0;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
                else
                {
                    index += 1;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isActive_1 == false)
        {
            if (btn == m_BtnList[2])
            {
                index = 0;
                btn = m_BtnList[index];

                TransformChoicePos(btn);
                Debug.Log(m_BtnList[index].name);
            }
            else
            {
                index += 1;
                btn = m_BtnList[index];
                //Debug.Log(btn.transform.position);
                TransformChoicePos(btn);
                //btn.isActive_Button = true;
                Debug.Log(m_BtnList[index].name);
            }
        }
         if (Input.GetKeyDown(KeyCode.UpArrow) && ShowUI.activeInHierarchy == true)
        {
            if (btn == ItemWidgetList[0])
            {
                ItemWidgetIndex = 2;
                btn = ItemWidgetList[ItemWidgetIndex];
                TransformChoicePos(btn);
                Debug.Log(ItemWidgetList[ItemWidgetIndex].name);
            }
            else
            {
                ItemWidgetIndex = ItemWidgetIndex - 1;
                btn = ItemWidgetList[ItemWidgetIndex];
                TransformChoicePos(btn);
                Debug.Log(ItemWidgetList[ItemWidgetIndex].name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isActiveWhenShowUI == true) {
            if (ItemsBtnControlList.Count == 1)
            {
                index = 0;
                btn = ItemsBtnControlList[index];
            }
            else
            {
                if (btn == ItemsBtnControlList[0])
                {
                    index = ItemsBtnControlList.Count - 1;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
                else
                {
                    index = index - 1;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
            }
        }       
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isActive_1 == true)
        {
            if (ItemsBtnControlList.Count == 1)
            {
                index = 0;
                btn = ItemsBtnControlList[index];

            }
            else
            {
                if (btn == ItemsBtnControlList[0])
                {
                    index = ItemsBtnControlList.Count - 1;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
                else
                {
                    index = index - 1;
                    btn = ItemsBtnControlList[index];
                    TransformChoicePos(btn);
                    Debug.Log(ItemsBtnControlList[index].name);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isActive_1 == false)
        {
            if (btn == m_BtnList[0])
            {
                index = 2;
                btn = m_BtnList[index];
                TransformChoicePos(btn);
                Debug.Log(m_BtnList[index].name);
            }
            else
            {
                index = index - 1;
                btn = m_BtnList[index];
                TransformChoicePos(btn);
                //btn.isActive_Button = true;
                Debug.Log(m_BtnList[index].name);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && ShowUI.activeInHierarchy == true) {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isActiveWhenShowUI == true)
        {
            twoTothreeIndex = index;
            OnItemsClick_2();
            btn = ItemWidgetList[0];
            TransformChoicePos(btn);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isActiveLeftArrow == true)//开启第三层，到第三层
        {

            twoTothreeIndex = index;
            OnItemsClick_2();
            btn = ItemWidgetList[0];
            TransformChoicePos(btn);
            Debug.Log("index=" + index);

            // isActiveWhenShowUI = true;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isActive_1 == true)//到第二层
        {
            isActiveLeftArrow = true;
            if (ItemsBtnControlList.Count == 1)
            {
                index = 0;
                btn = ItemsBtnControlList[index];
                TransformChoicePos(btn);
            }
            else
            {
                btn = ItemsBtnControlList[index];
                TransformChoicePos(btn);
            }


            //OnItemsClick_2();


            //isActive_1 = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isActive_1 == false)//开启第二层
        {
            ItemGrid.gameObject.SetActive(true);
            oneTotwoIndex = index;
            Debug.Log("oneTotwoIndex=" + oneTotwoIndex);
            Debug.Log("index=" + index);
            switch (index)
            {
                case 0:
                    OnClickEquipment();
                    isActive_1 = true;
                    break;
                case 1:
                    OnClickMaterial();
                    isActive_1 = true;
                    break;
                case 2:
                    OnClickItems();
                    isActive_1 = true;
                    break;
            }

           
        }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && ShowUI.activeInHierarchy==true)
            {
            Debug.Log("xxx");
            isActiveWhenShowUI = true;
            isActiveLeftArrow = false;
            if (ShowUI.activeInHierarchy == true)
                {
                    ShowUI.SetActive(false);
                }
                index = twoTothreeIndex;
                Debug.Log(twoTothreeIndex + "twoTothreeIndex");
                btn = ItemsBtnControlList[index];
                TransformChoicePos(btn);
                
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow) && isActive_1 == true)
            {

            isActive_1 = false;
            isActiveLeftArrow = false;
            isActiveWhenShowUI = false;
            ItemGrid.gameObject.SetActive(false);
            index = oneTotwoIndex;
                btn = m_BtnList[index];
                TransformChoicePos(btn);
                //if (ShowUI.activeInHierarchy == true)
                //{
                //    ShowUI.SetActive(false);
                //}
               
                // TransformChoicePos(btn);
            }

            if (Input.GetKeyDown(KeyCode.Return) && btn.transform.position == ItemWidgetList[0].transform.position)
            {
                ItemUseData data = new ItemUseData();
                ItemUseManager.Instance.addDelegate_Use(data);
                if (data.u_Hander != null)
                {
                    data.u_Hander();
                }
            }


        }
    }




