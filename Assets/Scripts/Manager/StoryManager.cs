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
     
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, global::StoryData>();
            LoadStoryXML("Stage0Config",Stage0Dic);
        }
      
        //StoryPanel.dataList = dataList;
        
        GUIManager.ShowView("StoryPanel");

    }

    public void ShowEventStoryList(int id)
    {
       
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, global::StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
            //InitEventStoryData();
        }

        List<StoryData> dataList = new List<StoryData>();
        //dataList.Add(GetEventDataByID(id));

        EventStoryPanel.data = dataList[0];

        if (EventStoryPanel.isEventSpeak)
        {
            GUIManager.HideView("EventStoryPanel");
        }
        GUIManager.ShowView("EventStoryPanel");

    }

    public void ShowStoryPanel(StoryData data)
    {




        //StoryPanel.data = data;
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





    private void InitEventStoryData()
    {
        if (Stage0EventDic == null)
        {
            Stage0EventDic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0EventConfig", Stage0EventDic);
        }

    }


    //#region Stage0 对话所需方法


    ///// <summary>
    ///// 绑定在与白兔子第一次对话的最后一句，用于显示 热心的兔子 的首个Event
    ///// </summary>
    //void WhatYouThinkAboutWhiteRabbit_1()
    //{
    //    ShowEventStoryList(2);
    //}


    //void WhiteRabbitRun()
    //{
    //    ShowEventStoryList(9);
    //}




    //#endregion

    //#region Stage0 Event所需的方法

    //void ContiueGo()
    //{
    //    ChoseManager.Instance.ShowChosePanel(0);
    //}

    //void WhiteRabitSpeak()
    //{
    //    List<StoryData> dataList = GetStage0State0List();
    //    ShowStoryList(dataList);
    //}


    ///// <summary>
    ///// Stage0 中首个观察或者放弃兔子  的选择
    ///// </summary>
    //void ObserveTheWhiteRabbitOrGiveUp_1()
    //{
    //    ChoseManager.Instance.ShowChosePanel(1);
    //}

    //void ObserveTheWhiteRabbitOrGiveUp_2()
    //{
    //    ChoseManager.Instance.ShowChosePanel(2);
    //}

    //void ObserveTheWhiteRabbitOrGiveUp_3()
    //{
    //    ChoseManager.Instance.ShowChosePanel(3);
    //}

    //void ObserveTheWhiteRabbitOrGiveUp_4()
    //{
    //    ChoseManager.Instance.ShowChosePanel(4);
    //}

    //void ObserveTheWhiteRabbitOrGiveUp_5()
    //{
    //    ChoseManager.Instance.ShowChosePanel(5);
    //}


    //void ObserveTheWhiteRabbitOrGiveUp_6()
    //{
    //    ChoseManager.Instance.ShowChosePanel(6);
    //}

    //void AskTheWhiteRabbit()
    //{
    //    ShowStoryList(GetStage0WhiteRabbitSpeak_5());

    //}

    //void Rabbit_AHouseEvent()
    //{
    //    ChoseManager.Instance.ShowChosePanel(7);
    //}

    //void RabbitAsDiray()
    //{
    //    ChoseManager.Instance.ShowChosePanel(8);
    //}

    //void RabbitAsDirayFirstPage()
    //{
    //    ChoseManager.Instance.ShowChosePanel(9);
    //}

    //void RabbitAsDiraySecondPage()
    //{
    //    ChoseManager.Instance.ShowChosePanel(10);
    //}

    //void RabbitAsDirayThirdPage()
    //{
    //    ChoseManager.Instance.ShowChosePanel(11);
    //}

    //void RabbitAsDirayFourthPage()
    //{
    //    ChoseManager.Instance.ShowChosePanel(12);
    //}

    //void RabbitAsDirayFivePage()
    //{
    //    ChoseManager.Instance.ShowChosePanel(13);
    //}

    //void RabbitAsDiraySixthPage()
    //{
    //    ChoseManager.Instance.ShowChosePanel(14);
    //}

    //void RabbitBsHouse()
    //{
    //    ChoseManager.Instance.ShowChosePanel(15);
    //}

    //void RabbitBDoorIsOpen()
    //{
    //    ChoseManager.Instance.ShowChosePanel(16);
    //}


    //void RabbitB_BookCase()
    //{
    //    ShowEventStoryList(22);
    //}

    //void RabbitB_ReadBookFirst()
    //{
    //    ChoseManager.Instance.ShowChosePanel(17);
    //}
    //#endregion









    //#region NPC
    //public void ShowNPCStory(int id)
    //{
    //    if (NPCDic == null)
    //    {
    //        InitNPCDic();
    //    }

    //    StoryData data = GetNPCStroyDataById(id);
    //    if (data != null)
    //    {
    //        List<StoryData> dataList = new List<StoryData>();
    //        dataList.Add(data);
    //        ShowStoryList(dataList);
    //    }
    //    else
    //    {
    //        Debug.LogError("NPC Data is null");
    //        return;
    //    }
    //}

    //private void InitNPCDic()
    //{
    //    NPCDic = new Dictionary<int, StoryData>();
    //    LoadStoryXML("NPCConfig", NPCDic);
    //}

    //private StoryData GetNPCStroyDataById(int id)
    //{
    //    StoryData data = null;
    //    if(!NPCDic.TryGetValue(id,out data))
    //    {
    //        Debug.LogError("NPCData has error!......" + id);
    //        return null;
    //    }
    //    return data;
    //}


    //private void InitNPCData()
    //{

    //}


    //#region NPC对话添加事件



    //#endregion

    //#endregion
}
