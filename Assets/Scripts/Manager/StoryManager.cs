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

    public void ShowEventStoryList(List<StoryData> dataList)
    {

        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, global::StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

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
    void LoadStoryXML(string pathName,Dictionary<int,StoryData> DataDic)
    {
        string filePath = Application.dataPath + @"/Resources/Config/StoryConfig/"+ pathName+".xml";
        if (!File.Exists(filePath))
        {
            Debug.LogError("not storyCofing");
            return;
        }

        if (File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

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
                DataDic.Add(data.id,data);
            }
        }
    }




    #region
 


    #endregion



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
        return dataList;
    }

    #endregion


    #region 获得Stage0中Event的对话List，并且绑定了需要触发的方法
    public List<StoryData> GetStage0TheMiLuGrilEvent()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 0)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander = ContiueGo;
        return dataList;
    }




    public List<StoryData> GetStage0TheFunnyRabitEvent()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 1)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander = WhiteRabitSpeak;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_1()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 2)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count -1].Hander+= ObserveTheWhiteRabbitOrGiveUp_1;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_2()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 3)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += ObserveTheWhiteRabbitOrGiveUp_2;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_3()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 4)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += ObserveTheWhiteRabbitOrGiveUp_3;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_4()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 5)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += ObserveTheWhiteRabbitOrGiveUp_4;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_5()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 6)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += ObserveTheWhiteRabbitOrGiveUp_5;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_6()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 7)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += ObserveTheWhiteRabbitOrGiveUp_6;
        return dataList;
    }

    public List<StoryData> GetStage0TheFunnyRabitEvent_7()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

        List<StoryData> dataList = new List<StoryData>();

        foreach (KeyValuePair<int, StoryData> item in Stage0EventDic)
        {
            if (item.Value.state == 8)
            {
                dataList.Add(item.Value);
            }
        }
        dataList[dataList.Count - 1].Hander += AskTheWhiteRabbit;
        return dataList;
    }

        #endregion

        #region Stage0 对话所需方法


        /// <summary>
        /// 绑定在与白兔子第一次对话的最后一句，用于显示 热心的兔子 的首个Event
        /// </summary>
        void WhatYouThinkAboutWhiteRabbit_1()
    {
        List<StoryData> dataList = GetStage0TheFunnyRabitEvent_1();
        ShowEventStoryList(dataList);
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


    #endregion
}
