using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeakData
{
    public delegate void NPCSpeakHander();
    /// <summary>
    /// 一个对话结束后所触发的委托
    /// </summary>
    public NPCSpeakHander Hander;

    public int Id
    {
        get;
        set;
    }

    public string Main
    {
        get;
        set;
    }

    public int SpeakCount
    {
        get;
        set;
    }


    public List<string> SpeakList = new List<string>();

 
}
