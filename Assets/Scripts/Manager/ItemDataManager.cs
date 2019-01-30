using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ItemDataManager
{
    private static ItemDataManager _instance = null;
    public static ItemDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ItemDataManager();
            }
            return _instance;
        }
    }

    private Dictionary<int, ItemData> ItemDataDic;


    private List<ItemData> HasItemList = new List<ItemData>();
    private List<ItemData> HasMaterialList = new List<ItemData>();
    private List<ItemData> HasEquipList = new List<ItemData>();
    //private string pathName = "ItemConfig";

    public ItemData GetItemDataByBulletName(string name)
    {
        if (ItemDataDic == null)
        {
            InitTestItemDataDic();
        }

        foreach (ItemData item in ItemDataDic.Values)
        {
            if (item.bulletName == name)
            {
                return item;
            }
        }
        return null;
    }

    public List<ItemData> GetHasItemList()

    {
        List<ItemData> ItemDataList = new List<global::ItemData>();

        if (HasItemList == null)
        {
            TestInitItemlList();
        }



        ItemDataList = HasItemList;
        return ItemDataList;
    }
    public void AddItemToHasItemList(ItemData data)
    {
        if (HasItemList == null)
        {
            TestInitItemlList();
        }
        if (data == null)
        {
            Debug.LogError("data is null");
            return;
        }

        foreach (ItemData item in HasItemList)
        {
            if (item.id == data.id)
            {
                item.num++;
                return;
            }
        }
        data.num = 1;
        HasItemList.Add(data);
    }

    public void AddItemToHasMaterialList(ItemData data)
    {
        if (HasMaterialList == null)
        {
            TestInitMaterialList();
        }

        if (data == null)
        {
            Debug.LogError("data is null");
            return;
        }

        foreach (ItemData item in HasMaterialList)
        {
            if(item.id == data.id)
            {
                item.num++;
                return;
            }
        }
        HasMaterialList.Add(data);
    }


    public void CutItemToHasItemList(ItemData data)
    {
        if (HasItemList == null)
        {
            TestInitItemlList();
        }
        if (data == null)
        {
            Debug.LogError("data is null");
            return;
        }
        for (int i = 0; i < HasItemList.Count; i++)
        {
            if (HasItemList[i].id == data.id)
            {
                HasItemList[i].num--;
                if (HasItemList[i].num <= 0)
                {
                    HasItemList.Remove((HasItemList[i]));
                }
            }
        }
    }

    public void CutMaterialToHasMaterialList(ItemData data)
    {
        if (HasMaterialList == null)
        {
            TestInitMaterialList();
        }

        if (data == null)
        {
            Debug.LogError("data is null");
            return;
        }

        for (int i = 0; i < HasMaterialList.Count; i++)
        {
            if (HasMaterialList[i].id == data.id)
            {
                HasMaterialList[i].num--;
                if (HasMaterialList[i].num <= 0)
                {
                    HasMaterialList.Remove((HasMaterialList[i]));
                }  
            }
        }
            

    }



    public ItemData GetItemDataByID(int id)
    {


        if (ItemDataDic == null)
        {
            InitTestItemDataDic();
        }


        ItemData data = null;
        if(!ItemDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("not data in itemDataDic");
            return null;
        }
        return data;
    }

    //int count = 0;
    public List<ItemData> GetHasMaterialList()
    {
        List<ItemData> ItemDataList = new List<ItemData>();      
            LoadItemXml("ItemConfig", ItemDataList);
        // ItemDataList = HasMaterialList;
        //Debug.Log(ItemDataList.Count);
        int length = ItemDataList.Count;
       // Debug.Log(ItemDataList[1].itemType);
        for (int i = ItemDataList.Count-1; i >= 0; i--) {
            if (ItemDataList[i].itemType!=ItemType.material) {//解决方法，新建一个表，如果是这个数的话，加到这个表里，然后返回这个表
                ItemDataList.Remove(ItemDataList[i]);
                //Debug.Log(ItemDataList[i].name);
               // count++;
            }
        }
        //Debug.Log("count"+count);
        //Debug.Log(ItemDataList.Count);
        
        return ItemDataList;
    }
    public List<ItemData> GetHasEquipList() {
        List<ItemData> ItemDataList = new List<ItemData>();       
            LoadItemXml("ItemConfig", ItemDataList);
        //ItemDataList = HasEquipList;
        for (int i = ItemDataList.Count - 1; i >= 0; i--)
        {
            if (ItemDataList[i].itemType != ItemType.Equipment)
            {//解决方法，新建一个表，如果是这个数的话，加到这个表里，然后返回这个表
                ItemDataList.Remove(ItemDataList[i]);
                //Debug.Log(ItemDataList[i].name);
               // count++;
            }
        }

        //Debug.Log(HasEquipList.Count);
        return ItemDataList;
    }
    public List<ItemData> GetHasItemsList() {
        List<ItemData> ItemDataList = new List<ItemData>();
        LoadItemXml("ItemConfig", ItemDataList);
        for (int i = ItemDataList.Count - 1; i >= 0; i--) {
            if (ItemDataList[i].itemType != ItemType.Item) {
                ItemDataList.Remove(ItemDataList[i]);
            }
        }
        return ItemDataList;
    }



    //public ItemData GetHasItemDataById(int id)
    //{
    //    for (int i = 0; i < HasItemList.Count; i++)
    //    {
    //        if (HasItemList[i].id == id)
    //        {
    //            return HasItemList[i];
    //        }
    //    }
    //    return null;
    //}

    //public ItemData GetHasMaterialDataById(int id)
    //{
    //    for (int i = 0; i < HasMaterialList.Count; i++)
    //    {
    //        if (HasMaterialList[i].id == id)
    //        {
    //            return HasMaterialList[i];
    //        }
    //    }
    //    return null;
    //}

    void TestInitMaterialList()
    {
        HasMaterialList = new List<global::ItemData>();
        ItemData data1 = new ItemData();
        data1.id = 1;
        data1.itemType = ItemType.material;
        data1.name = "土元素";
        data1.icon = "1";

        ItemData data2 = new ItemData();
        data2.id = 2;
        data2.itemType = ItemType.material;
        data2.name = "木元素";
        data2.icon = "2";

        ItemData data3 = new ItemData();
        data3.id = 3;
        data3.itemType = ItemType.material;
        data3.name = "水元素";
        data3.icon = "3";

        HasMaterialList.Add(data1);
        HasMaterialList.Add(data2);
        HasMaterialList.Add(data3);
    }

    void TestInitItemlList()
    {
        HasItemList = new List<global::ItemData>();
        ItemData data1 = new ItemData();
        data1.id = 4;
        data1.itemType = ItemType.Item;
        data1.name = "怀表";
        data1.icon = "4";

        ItemData data2 = new ItemData();
        data2.id = 5;
        data2.itemType = ItemType.Item;
        data2.name = "月时记";
        data2.icon = "5";



        HasItemList.Add(data1);
        HasItemList.Add(data2);

    }

    void TestInitEquipmentList() {
        HasEquipList = new List<ItemData>();

        ItemData data1 = new ItemData();
        data1.id = 1;
        data1.itemType = ItemType.Equipment;
        data1.name = "大刀";
        data1.icon = "1";

        ItemData data2 = new ItemData();
        data2.id = 2;
        data2.itemType = ItemType.Equipment;
        data2.name = "大剑";
        data2.icon = "2";

        ItemData data3 = new ItemData();
        data3.id = 3;
        data3.itemType = ItemType.Equipment;
        data3.name = "大斧";
        data3.icon = "3";

        ItemData data4 = new ItemData();
        data4.id = 4;
        data4.itemType = ItemType.Equipment;
        data4.name = "大枪";
        data4.icon = "4";

        HasEquipList.Add(data1);
        HasEquipList.Add(data2);
        HasEquipList.Add(data3);
        HasEquipList.Add(data4);
    }




    private void InitTestItemDataDic()
    {
        ItemDataDic = new Dictionary<int, ItemData>();
        ItemData data1 = new ItemData();
        data1.id = 1;
        data1.itemType = ItemType.material;
        data1.name = "土元素";
        data1.icon = "1";

        ItemData data2 = new ItemData();
        data2.id = 2;
        data2.itemType = ItemType.material;
        data2.name = "木元素";
        data2.icon = "2";

        ItemData data3 = new ItemData();
        data3.id = 3;
        data3.itemType = ItemType.material;
        data3.name = "水元素";
        data3.icon = "3";


        ItemData data4 = new ItemData();
        data4.id = 4;
        data4.itemType = ItemType.material;
        data4.name = "胡萝卜";
        data4.icon = "4";
        data4.bulletName = "carrotBullet_Player";

      

        ItemDataDic.Add(data1.id, data1);
        ItemDataDic.Add(data2.id, data2);
        ItemDataDic.Add(data3.id, data3);
        ItemDataDic.Add(data4.id, data4);
       
    }
    

   public void LoadItemXml(string pathName, List<ItemData> list) {
        string path = "Config";
        string text = ResourcesManager.Instance.LoadConfig(path,pathName).text;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode xmlNode = xmlDoc.SelectSingleNode("Item");
        XmlNodeList nodeList = xmlNode.ChildNodes;

        foreach (XmlNode node in nodeList) {
            XmlNode id = node.SelectSingleNode("id");
            XmlNode name = node.SelectSingleNode("name");
            XmlNode sprite = node.SelectSingleNode("sprite");
            XmlNode atlas = node.SelectSingleNode("atlas");//哪个图集
            XmlNode Des = node.SelectSingleNode("Des");           
            XmlNode price = node.SelectSingleNode("price");
            XmlNode itemType = node.SelectSingleNode("ItemType");

            ItemData data = new ItemData();
            data.id = CommonHelper.Str2Int(id.InnerText);
            data.name = name.InnerText;
            data.icon = sprite.InnerText; 
            data.atlasName = atlas.InnerText;
            data.des = Des.InnerText;
            data.price = CommonHelper.Str2Int(price.InnerText);
            switch (int.Parse(itemType.InnerText)) {
                case 10:data.itemType = ItemType.material;
                    break;
                case 20:data.itemType = ItemType.Item;
                    break;
                case 30:data.itemType = ItemType.Equipment;
                    break;
            }
            //Debug.Log("xxx");                                                                   
                list.Add(data);
        }
        //Debug.Log(list.Count);
    }
}
