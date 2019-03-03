using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class NPCSpeakManager
{
    private static NPCSpeakManager _instance=null;

    public static NPCSpeakManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NPCSpeakManager();
                _instance.Init();
            }
            return _instance;
        }
    }
    /// <summary>
    /// 词典查找:0-魔理沙
    /// </summary>
    private Dictionary<int, Dictionary<int, NPCSpeakData>> NPCCollectionDic=new Dictionary<int, Dictionary<int, NPCSpeakData>> ();

    private Dictionary<int, NPCSpeakData> MarisaDic=new Dictionary<int, NPCSpeakData> ();

    void Init()
    {
        LoadNPCSpeakData("MarisaConfig",MarisaDic);
        NPCCollectionDic.Add(0, MarisaDic);
        InitHnader();
    }


    public void ShowNPCSpeakPanel(int id)
    {
        InputPanel.NPCId = id;
        GUIManager.ShowView("InputPanel");
    }

    public Dictionary<int, NPCSpeakData> GetNPCDicById(int id)
    {
        Dictionary<int, NPCSpeakData> speakDic = null;
        if(!NPCCollectionDic.TryGetValue(id,out speakDic))
        {
            Debug.LogError("NPCSpeakManager:id has error!!");
        }
        return speakDic;
    }
    /// <summary>
    /// 第一个参数代表NPC字典编号，第二个参数代表NPC的
    /// </summary>
    /// <param name="dicIndex"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    NPCSpeakData GetDataByID(int dicIndex,int id)
    {
        Dictionary<int, NPCSpeakData> speakDic = GetNPCDicById(dicIndex);
        NPCSpeakData data = null;
        if (!speakDic.TryGetValue(0, out data))
        {
            Debug.LogError("NPCSpeakManager data has error");
        }
        return data;
    }


    private void LoadNPCSpeakData(string pathName,Dictionary<int,NPCSpeakData> DataDic)
    {
        string path = "Config"+"/"+"NPC";

        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode node = xmlDoc.SelectSingleNode("NPCConfig");
        XmlNodeList nodeList = node.ChildNodes;

        foreach (XmlNode item in nodeList)
        {
            XmlNode id = item.SelectSingleNode("id");
            XmlNode main = item.SelectSingleNode("Main");
            XmlNode SpeakCout = item.SelectSingleNode("SpeakCout");
            XmlNode Speak = item.SelectSingleNode("Speak");
            NPCSpeakData data = new NPCSpeakData();
           

            foreach (XmlNode pair in Speak)
            {
                data.storyData.SpeakList.Add(pair.InnerText);
            }

            foreach (XmlNode pair in main)
            {
                data.MainList.Add(pair.InnerText);
            }
            data.Id = CommonHelper.Str2Int(id.InnerText);
            data.SpeakCount = CommonHelper.Str2Int(SpeakCout.InnerText);
            DataDic.Add(data.Id, data);
        }
    }

    void InitHnader()
    {
        NPCSpeakData data = GetDataByID(0,0);
        data.OnEnterDownDic.Add(0, MarisaEnterEventOneSpeak0);
        data.OnEnterDownDic.Add(1, MarisaEnterEventOneSpeak1);
        data.OnEnterDownDic.Add(2, MarisaEnterEventOneSpeak1);
        data.OnEnterDownDic.Add(3, MarisaEnterEventOneSpeak2);
        data.OnEnterDownDic.Add(4, MarisaEnterEventOneSpeak2);
        data.OnEnterDownDic.Add(5, MarisaEnterEventOneSpeak2);
        data.OnEnterDownDic.Add(6, MarisaEnterEventOneSpeak2);
        data.OnEnterDownDic.Add(7, MarisaEnterEventOneSpeak3);
        data.OnEnterDownDic.Add(8, MarisaEnterEventOneSpeak3);
        data.OnEnterDownDic.Add(9, MarisaEnterEventOneSpeak6);
        data.OnEnterDownDic.Add(10, MarisaEnterEventOneSpeak4);
        data.OnEnterDownDic.Add(11, MarisaEnterEventOneSpeak4);
        data.OnEnterDownDic.Add(12, MarisaEnterEventOneSpeak4);
        data.OnEnterDownDic.Add(13, MarisaEnterEventOneSpeak7);
        data.OnEnterDownDic.Add(14, MarisaEnterEventOneSpeak7);
        data.OnEnterDownDic.Add(15, MarisaEnterEventOneSpeak8);
        data.OnEnterDownDic.Add(16, MarisaEnterEventOneSpeak9);
        data.OnEnterDownDic.Add(17, MarisaEnterEventOneSpeak9);
        data.OnEnterDownDic.Add(18, MarisaEnterEventOneSpeak12);
        data.OnEnterDownDic.Add(19, MarisaEnterEventOneSpeak11);
        data.OnEnterDownDic.Add(20, MarisaEnterEventOneSpeak10);
        data.OnEnterDownDic.Add(21, MarisaEnterEventOneSpeak12);
        data.OnEnterDownDic.Add(22, MarisaEnterEventOneSpeak13);
        data.OnEnterDownDic.Add(23, MarisaEnterEventOneSpeak13);
        data.OnEnterDownDic.Add(24, MarisaEnterEventOneSpeak13);
        data.OnEnterDownDic.Add(25, MarisaEnterEventOneSpeak14);

        data.storyData.StoryHanderDic.Add(0, MarisaTalkChapterOne0);
        data.storyData.StoryHanderDic.Add(1, MarisaTalkChapterOne1);
        data.storyData.StoryHanderDic.Add(2, MarisaTalkChapterOne2);
        data.storyData.StoryHanderDic.Add(3, MarisaTalkChapterOne3);
        data.storyData.StoryHanderDic.Add(4, MarisaTalkChapterOne4);
        data.storyData.StoryHanderDic.Add(5, MarisaTalkChapterOne5);
        data.storyData.StoryHanderDic.Add(6, MarisaTalkChapterOne6);
        data.storyData.StoryHanderDic.Add(7, MarisaTalkChapterOne7);
        data.storyData.StoryHanderDic.Add(8, MarisaTalkChapterOne8);
        data.storyData.StoryHanderDic.Add(9, MarisaTalkChapterOne9);
        data.storyData.StoryHanderDic.Add(10, MarisaTalkChapterOne10);
        data.storyData.StoryHanderDic.Add(11, MarisaTalkChapterOne11);
        data.storyData.StoryHanderDic.Add(12, MarisaTalkChapterOne12);
        data.storyData.StoryHanderDic.Add(13, MarisaTalkChapterOne13);
        data.storyData.StoryHanderDic.Add(14, MarisaTalkChapterOne14);
    }


    #region 第一章确定输入后的方法
    #region 事件一
    void MarisaEnterEventOneSpeak0()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData,0);
    }

    void MarisaEnterEventOneSpeak1()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 1);
    }

    void MarisaEnterEventOneSpeak2()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 2);
    }

    void MarisaEnterEventOneSpeak3()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 3);
    }

    void MarisaEnterEventOneSpeak4()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 4);
    }

    void MarisaEnterEventOneSpeak5()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 5);
    }

    void MarisaEnterEventOneSpeak6()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 6);
    }

    void MarisaEnterEventOneSpeak7()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 7);
    }

    void MarisaEnterEventOneSpeak8()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 8);
    }

    void MarisaEnterEventOneSpeak9()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 9);
    }

    void MarisaEnterEventOneSpeak10()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 10);
    }

    void MarisaEnterEventOneSpeak11()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 11);
    }

    void MarisaEnterEventOneSpeak12()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 12);
    }


    void MarisaEnterEventOneSpeak13()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 13);
    }
    void MarisaEnterEventOneSpeak14()
    {
        GUIManager.HideView("InputPanel");
        NPCSpeakData data = GetDataByID(0, 0);
        TalkManager.Instance.ShowTalkPanel(data.storyData, 14);
    }



    #endregion
    #endregion

    #region  第一章，对话结束后会触发的方法
    #region 事件一
    void MarisaTalkChapterOne0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 15);
    }

    void MarisaTalkChapterOne1()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 16);
    }

    void MarisaTalkChapterOne2()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 17);
    }

    void MarisaTalkChapterOne3()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 18);
    }


    void MarisaTalkChapterOne4()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 19);
    }



    void MarisaTalkChapterOne5()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 20);
    }



    void MarisaTalkChapterOne6()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 21);
    }



    void MarisaTalkChapterOne7()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 15);
    }

    void MarisaTalkChapterOne8()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 15);
    }

    void MarisaTalkChapterOne9()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 22);
    }

    void MarisaTalkChapterOne10()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 22);
    }

    void MarisaTalkChapterOne11()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 22);
    }

    void MarisaTalkChapterOne12()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 22);
    }

    void MarisaTalkChapterOne13()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 23);
    }


    void MarisaTalkChapterOne14()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 24);
    }

    #endregion
    #endregion

}
