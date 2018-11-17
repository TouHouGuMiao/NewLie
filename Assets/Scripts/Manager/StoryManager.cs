using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class StoryManager
{
    private static StoryManager _instance = null;
    private Dictionary<int, StoryData> StoryDic;
    private Dictionary<int, StoryData> Stage0Dic;
    private Dictionary<int, StoryData> Stage0EventDic;
    private Dictionary<int, StoryData> NPCDic;

    public static StoryManager Instacne
    {
        get
        {
            if(_instance==null)
            {
                _instance = new StoryManager();
            }
            return _instance;
        }
    }

 
    

    public void ShowStoryList(List<StoryData> dataList)
    {
        //if (StoryDic == null)
        //{
        //    StoryDic = new Dictionary<int, StoryData>();
        //    LoadStoryXML("StoryConfig",StoryDic);
        //}

        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, global::StoryData>();
            LoadStoryXML("Stage0Config",Stage0Dic);
        }
      
        StoryPanel.dataList = dataList;
        
        GUIManager.ShowView("StoryPanel");

    }

    public void ShowEventStoryList(int id)
    {
       
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, global::StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
            InitEventStoryData();
        }

        List<StoryData> dataList = new List<StoryData>();
        dataList.Add(GetEventDataByID(id));

        EventStoryPanel.dataList = dataList;

        if (EventStoryPanel.isEventSpeak)
        {
            GUIManager.HideView("EventStoryPanel");
        }
        GUIManager.ShowView("EventStoryPanel");

    }

    public void ShowStoryPanel(StoryData data)
    {




        StoryPanel.data = data;
        GUIManager.ShowView("StoryPanel");


    }
    #region  获得对话数据
    //public StoryData GetStoryDataByID(int id)
    //{
    //    if (StoryDic == null)
    //    {
    //        StoryDic = new Dictionary<int, StoryData>();
    //        LoadStoryXML("StoryConfig",StoryDic);
    //    }
    //    StoryData data = null;
    //    if (!StoryDic.TryGetValue(id, out data))
    //    {
    //        Debug.LogError("not data in storyDic");
    //        return null;
    //    }
    //    return data;
    //}


    public StoryData GetState0DataByID(int id)
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config",Stage0Dic);
        }
        StoryData data = null;
        if (!Stage0Dic.TryGetValue(id, out data))
        {
            Debug.LogError("not data in storyDic");
            return null;
        }
        return data;
    }
    #endregion
    void LoadStoryXML(string pathName, Dictionary<int, StoryData> DataDic)
    {
        string path = "Config";

        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode node = xmlDoc.SelectSingleNode("Story");
        XmlNodeList nodeList = node.ChildNodes;
    
        foreach (XmlNode item in nodeList)
        {
            XmlNode id = item.SelectSingleNode("id");
            XmlNode index = item.SelectSingleNode("index");
            XmlNode state = item.SelectSingleNode("State");
            XmlNode name = item.SelectSingleNode("name");
            XmlNode cout = item.SelectSingleNode("cout");
            XmlNode speak = item.SelectSingleNode("Speak");
            XmlNode spriteName = item.SelectSingleNode("spriteName");

            StoryData data = new StoryData();
            data.id = CommonHelper.Str2Int(id.InnerText);
            data.state = CommonHelper.Str2Int(state.InnerText);
            data.name = name.InnerText;
            data.index = CommonHelper.Str2Int(index.InnerText);
            data.cout = CommonHelper.Str2Int(cout.InnerText);
            data.spriteName = spriteName.InnerText;

            foreach (XmlNode pair in speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            DataDic.Add(data.id, data);
        }
        
    }







    private StoryData GetEventDataByID(int id)
    {
        StoryData data = null;
        if (Stage0EventDic == null)
        {
            InitEventStoryData();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        if (!Stage0EventDic.TryGetValue(id, out data))
        {
            Debug.LogError("EventData has error!......" + id);
            return null;
        }
        return data;
    }


    #region 获得Stage0中单独的对话List，并且绑定了需要触发的方法
    public List<StoryData> GetStage0State0List()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config",Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int,StoryData> item in Stage0Dic)
        {
            if (item.Value.state == 0)
            {
                dataList.Add(item.Value);  
            }
        }
        dataList[dataList.Count - 1].Hander = WhatYouThinkAboutWhiteRabbit_1;
        return dataList;
    }

  

    public List<StoryData> GetStage0WhiteRabbitSpeak_1()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config", Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int, StoryData> item in Stage0Dic)
        {
            if (item.Value.id == 5)
            {
                dataList.Add(item.Value);
            }
        }
        return dataList;
    }

    public List<StoryData> GetStage0WhiteRabbitSpeak_2()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config", Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int, StoryData> item in Stage0Dic)
        {
            if (item.Value.id == 6)
            {
                dataList.Add(item.Value);
            }
        }
        return dataList;
    }

    public List<StoryData> GetStage0WhiteRabbitSpeak_3()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config", Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int, StoryData> item in Stage0Dic)
        {
            if (item.Value.id == 7)
            {
                dataList.Add(item.Value);
            }
        }
        return dataList;
    }

    public List<StoryData> GetStage0WhiteRabbitSpeak_4()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config", Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int, StoryData> item in Stage0Dic)
        {
            if (item.Value.id == 8)
            {
                dataList.Add(item.Value);
            }
        }
        return dataList;
    }

    public List<StoryData> GetStage0WhiteRabbitSpeak_5()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config", Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int, StoryData> item in Stage0Dic)
        {
            if (item.Value.id == 9)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += WhiteRabbitRun;
        return dataList;
    }

    #endregion




    private void InitEventStoryData()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        StoryData data = GetEventDataByID(0);
        data.Hander += ContiueGo;

        StoryData data1 = GetEventDataByID(1);
        data1.Hander += WhiteRabitSpeak;

        StoryData data2 = GetEventDataByID(2);
        data2.Hander += ObserveTheWhiteRabbitOrGiveUp_1;

        StoryData data3 = GetEventDataByID(3);
        data3.Hander += ObserveTheWhiteRabbitOrGiveUp_2;

        StoryData data4 = GetEventDataByID(4);
        data4.Hander += ObserveTheWhiteRabbitOrGiveUp_3;

        StoryData data5 = GetEventDataByID(5);
        data5.Hander += ObserveTheWhiteRabbitOrGiveUp_4;

        StoryData data6 = GetEventDataByID(6);
        data6.Hander += ObserveTheWhiteRabbitOrGiveUp_5;

        StoryData data7 = GetEventDataByID(7);
        data7.Hander += ObserveTheWhiteRabbitOrGiveUp_6;

        StoryData data8 = GetEventDataByID(8);
        data8.Hander += AskTheWhiteRabbit;

        StoryData data9 = GetEventDataByID(9);

        StoryData data10 = GetEventDataByID(10);
        data10.Hander += RabbitHouseEvent;
    }


    #region Stage0 对话所需方法


    /// <summary>
    /// 绑定在与白兔子第一次对话的最后一句，用于显示 热心的兔子 的首个Event
    /// </summary>
    void WhatYouThinkAboutWhiteRabbit_1()
    {
        ShowEventStoryList(2);
    }


    void WhiteRabbitRun()
    {
        ShowEventStoryList(9);
    }




    #endregion

    #region Stage0 Event所需的方法

    void ContiueGo()
    {
        ChoseManager.Instance.ShowChosePanel(0);
    }
    
    void WhiteRabitSpeak()
    {
        List<StoryData> dataList = GetStage0State0List();
        ShowStoryList(dataList);
    }


    /// <summary>
    /// Stage0 中首个观察或者放弃兔子  的选择
    /// </summary>
    void ObserveTheWhiteRabbitOrGiveUp_1()
    {
        ChoseManager.Instance.ShowChosePanel(1);
    }

    void ObserveTheWhiteRabbitOrGiveUp_2()
    {
        ChoseManager.Instance.ShowChosePanel(2);
    }

    void ObserveTheWhiteRabbitOrGiveUp_3()
    {
        ChoseManager.Instance.ShowChosePanel(3);
    }

    void ObserveTheWhiteRabbitOrGiveUp_4()
    {
        ChoseManager.Instance.ShowChosePanel(4);
    }

    void ObserveTheWhiteRabbitOrGiveUp_5()
    {
        ChoseManager.Instance.ShowChosePanel(5);
    }


    void ObserveTheWhiteRabbitOrGiveUp_6()
    {
        ChoseManager.Instance.ShowChosePanel(6);
    }

    void AskTheWhiteRabbit()
    {
        ShowStoryList(GetStage0WhiteRabbitSpeak_5());
     
    }

    void RabbitHouseEvent()
    {
        ChoseManager.Instance.ShowChosePanel(7);
    }


    #endregion









    #region NPC
    public void ShowNPCStory(int id)
    {
        if (NPCDic == null)
        {
            InitNPCDic();
        }

        StoryData data = GetNPCStroyDataById(id);
        if (data != null)
        {
            List<StoryData> dataList = new List<StoryData>();
            dataList.Add(data);
            ShowStoryList(dataList);
        }
        else
        {
            Debug.LogError("NPC Data is null");
            return;
        }
    }

    private void InitNPCDic()
    {
        NPCDic = new Dictionary<int, StoryData>();
        LoadStoryXML("NPCConfig", NPCDic);
    }

    private StoryData GetNPCStroyDataById(int id)
    {
        StoryData data = null;
        if(!NPCDic.TryGetValue(id,out data))
        {
            Debug.LogError("NPCData has error!......" + id);
            return null;
        }
        return data;
    }


    private void InitNPCData()
    {

    }


    #region NPC对话添加事件



    #endregion

    #endregion
}
