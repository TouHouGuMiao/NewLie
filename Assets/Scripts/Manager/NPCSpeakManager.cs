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

        XmlNode node = xmlDoc.SelectSingleNode("MarisaConfig");
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
        data.Hander = MarisaEnterChapterOne0;
        data.storyData.StoryHanderDic.Add(0, MarisaTalkChapterOne0);

    }


    #region 第一章确定输入后的方法
    void MarisaEnterChapterOne0()
    {
     
    }


    #endregion

    #region  第一章对话方法
    void MarisaTalkChapterOne0()
    {
        GUIManager.HideView("InputPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 1);
    }

    #endregion


}
