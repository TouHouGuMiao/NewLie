using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class StoryEventManager
{
    private static StoryEventManager _Instance=null;
    public static StoryEventManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new StoryEventManager();
                _Instance.InitLoad();
            }
            return _Instance;
        }
    }

    private Dictionary<int, StoryData> ChapterOneDic = new Dictionary<int, StoryData>();

    private void InitLoad()
    {
        LoadStoryXML("ChapterOneEventConfig", ChapterOneDic);
        InitHander();
    }

    private StoryData GetChapterOneEventDataById(int id)
    {
        StoryData data = null;
        if(!ChapterOneDic.TryGetValue(id,out data))
        {
            Debug.LogError("chapterOneDic id has error!"+id);
        }
        return data;
    }


    public void ShowEventPanel_ChapterOne(int id,int index=0)
    {
        StoryData data = GetChapterOneEventDataById(id);
        data.index = index;
        EventStoryPanel.data = data;
        GUIManager.ShowView("EventStoryPanel");
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
            data.spriteName = spriteName.InnerText;

            foreach (XmlNode pair in speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            DataDic.Add(data.id, data);
        }

    }

    #region 绑定第一章事件的方法
    private void InitHander()
    {
        StoryData data = GetChapterOneEventDataById(0);
        data.StoryHanderDic.Add(0, InShenSheOfMarisa0);
        data.StoryHanderDic.Add(1, InShenSheOfMarisa0);
    }

    #endregion


    #region   第一章事件触发的方法

    private void InShenSheOfMarisa0()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
        ChoseManager.Instance.ShowChosePanel(0);
    }

 


    #endregion
}
