using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    material=10,
    Item = 20,
    Equipment=30
}
public class ItemData
{
    public int id;
    public string icon;
    public string atlasName;
    public string des;//描述
    public string name;
    public int num=1;
    public ItemType itemType;
    public string bulletName;
    public int price;//价格
}
