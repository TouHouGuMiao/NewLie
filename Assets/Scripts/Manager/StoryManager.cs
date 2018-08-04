using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class StoryManager
{
    private static StoryManager _instance = null;
    private Dictionary<int, StoryData> StoryDic;
    private GameObject storyPanel;

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

    public  bool isSpeak
    {
        get
        {
            if (storyPanel == null)
            {
                return false;
            }

            if (storyPanel.activeSelf)
            {
                return true;
            }
            return false;
        }
    }
    

    public void ShowStoryPanel(int id)
    {
        StoryData data = null;

        if (StoryDic == null)
        {
            StoryDic = new Dictionary<int, StoryData>();
           
            LoadStoryXML();
        }

       

        if(!StoryDic.TryGetValue(id,out data))
        {
            Debug.LogError("not data in storyDic");
            return;
        }

        StoryPanel.data = data;
        GUIManager.ShowView("StoryPanel");
        if (storyPanel == null)
        {
            storyPanel = GUIManager.FindPanel("StoryPanel");
        }

    }

    void LoadStoryXML()
    {
        string filePath = Application.dataPath + @"/Resources/Config/StoryConfig.xml";
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
                StoryDic.Add(data.id,data);
            }
        }
    }
}
