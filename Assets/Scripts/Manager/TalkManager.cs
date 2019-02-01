using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TalkManager
{
    private static TalkManager _Instance = null;
    public static TalkManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new TalkManager();
                _Instance.InitLoad();
            }
            return _Instance;
        }
    }

    private Dictionary<int, StoryData> StoryDataDic=new Dictionary<int, StoryData> ();

    public void ShowTalkPanel(int id,int index=0)
    {
        StoryData data = GetStoryDataById(id);
        data.index = index;
        TalkPanel.data = data;
        GUIManager.ShowView("TalkPanel");
    }

    void InitLoad()
    {
       LoadStoryXML("StoryConfig",StoryDataDic);
        InitHander();
    }
  
    public StoryData GetStoryDataById(int id)
    {
        StoryData data;
        if(!StoryDataDic.TryGetValue(id, out data))
        {
            Debug.LogError("id has error" + data.id + "      " + "TalkManager");
        }
        return data;
    }


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


            foreach (XmlNode pair in speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            DataDic.Add(data.id, data);
        }
    }


    #region 第一章事件绑定
    void InitHander()
    {
        StoryData data = GetStoryDataById(0);
        data.StoryHanderDic.Add(0, KongWuGuaiTan0);
        data.StoryHanderDic.Add(1, KongWuGuaiTan1);
    }


    #endregion


    #region 空鹜怪谈
    void KongWuGuaiTan0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 9);
    }

    void KongWuGuaiTan1()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 11);
    }
    #endregion
}
