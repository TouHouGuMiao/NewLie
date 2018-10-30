using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreatManager
{
    private static ItemCreatManager _instance = null;
    public static ItemCreatManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ItemCreatManager();
            }
            return _instance;
        }
    }

    /// <summary>
    /// int值为其合成的前置物品的ID，例如 怀表的前置物品为破旧的怀表，那么int值是 破旧的怀表的id，通过这个id找到 怀表的itemCreat数据.
    /// </summary>
    private Dictionary<int, ItemCreatData> ItemCreatDic;

    public void Init()
    {
        if (ItemCreatDic == null)
        {
            ItemCreatDicInit();
        }
    }


    void ItemCreatDicInit()
    {
        ItemCreatDic = new Dictionary<int, global::ItemCreatData>();
        ItemCreatData creatData = new global::ItemCreatData();
        ItemData data1 = new ItemData();
        data1.id = 1;
        data1.itemType = ItemType.material;
        data1.name = "木材";
        data1.icon = "1";


        ItemData data2 = new ItemData();
        data2.id = 2;
        data2.itemType = ItemType.material;
        data2.name = "蘑菇";
        data2.icon = "2";

        ItemData data3 = new ItemData();
        data3.id = 3;
        data3.itemType = ItemType.Item;
        data3.name = "月时记";
        data3.icon = "3";

        creatData.itemData = data3;
        creatData.NeedItemList.Add(data1);
        creatData.NeedItemList.Add(data2);

        creatData.NeedItemNumList.Add(10);
        creatData.NeedItemNumList.Add(30);


        ItemCreatDic = new Dictionary<int, global::ItemCreatData>();
        ItemCreatData creatData2 = new global::ItemCreatData();
        ItemData data5 = new ItemData();
        data5.id = 4;
        data5.itemType = ItemType.material;
        data5.name = "木材1";
        data5.icon = "4";


        ItemData data6 = new ItemData();
        data6.id = 5;
        data6.itemType = ItemType.material;
        data6.name = "蘑菇2";
        data6.icon = "5";

        ItemData data8 = new ItemData();
        data8.id = 7;
        data8.itemType = ItemType.material;
        data8.name = "蘑菇3";
        data8.icon = "7";

        ItemData data7 = new ItemData();
        data7.id = 6;
        data7.itemType = ItemType.Item;
        data7.name = "月时记2";
        data7.icon = "6";

        creatData2.itemData = data7;
        creatData2.NeedItemList.Add(data5);
        creatData2.NeedItemList.Add(data6);

        creatData2.NeedItemNumList.Add(20);
        creatData2.NeedItemNumList.Add(30);


        ItemCreatDic.Add(0, creatData);
        ItemCreatDic.Add(8, creatData2);
    }


    public ItemCreatData GetItemCreatDataByLastID(int id)
    {
        if (ItemCreatDic == null)
        {
            ItemCreatDicInit();
        }
        ItemCreatData data = null;
        if(!ItemCreatDic.TryGetValue(id,out data))
        {
            Debug.LogError("not data in dic");
            return null;
        }
        return data;
    }



}
