using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData
{

    public delegate void StoryHander();
    /// <summary>
    /// 一个对话结束后所触发的委托
    /// </summary>
    public StoryHander Hander;
    public int id { get; set; }

    public int state { get; set; }

    public string name { get; set; }

    public int index { get; set; }

    public int cout { get; set; }

    public string spriteName { get; set; }

    public List<string> SpeakList = new List<string>();
}
