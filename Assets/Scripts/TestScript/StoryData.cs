using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void StoryHander();
public class StoryData
{


    /// <summary>
    /// 一个对话结束后所触发的委托，将对话与对话需要的方法通过字典绑定在一起
    /// </summary>
    public List<string> SpeakList = new List<string>();
    public Dictionary<int, StoryHander> StoryHanderDic=new Dictionary<int, StoryHander> ();
    public int id { get; set; }

    public int state { get; set; }

    public string name { get; set; }

    public int index { get; set; }

    public int cout { get; set; }

    public string spriteName { get; set; }

   
}
