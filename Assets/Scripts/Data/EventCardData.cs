using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCardData  {
    public int id;
    public string name;//事件的标题
    public string Des;
    public string spriteName;//加载时显示界面显示的图片
    public EventCardData(int id, string name, string Des,string spriteName) {
        this.id = id;
        this.name = name;
        this.Des = Des;
        this.spriteName = spriteName;
    }
    public EventCardData() {

    }
}
