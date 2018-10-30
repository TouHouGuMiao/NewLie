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
    

    public void ShowStoryList(List<StoryData> dataList)
    {
        if (StoryDic == null)
        {
            StoryDic = new Dictionary<int, StoryData>();
            LoadStoryXML("StoryConfig",StoryDic);
        }

        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, global::StoryData>();
            LoadStoryXML("Stage0Config",Stage0Dic);
        }
      
        StoryPanel.dataList = dataList;
        
        GUIManager.ShowView("StoryPanel");
        if (storyPanel == null)
        {
            storyPanel = GUIManager.FindPanel("StoryPanel");
        }
    }

    public void ShowStoryPanel(StoryData data)
    {




        StoryPanel.data = data;
        GUIManager.ShowView("StoryPanel");
        if (storyPanel == null)
        {
            storyPanel = GUIManager.FindPanel("StoryPanel");
        }

    }
    #region  获得对话数据
    public StoryData GetStoryDataByID(int id)
    {
        if (StoryDic == null)
        {
            StoryDic = new Dictionary<int, StoryData>();
            LoadStoryXML("StoryConfig",StoryDic);
        }
        StoryData data = null;
        if (!StoryDic.TryGetValue(id, out data))
        {
            Debug.LogError("not data in storyDic");
            return null;
        }
        return data;
    }


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
                DataDic.Add(data.id,data);
            }
        }
    }




    #region
    public void ShowTestChose()
    {
        ChoseData data = new global::ChoseData();
        data.ChoseDesList.Add("冲了");
        data.ChoseDesList.Add("爽了");
        data.ChoseDesList.Add("废了");
        data.HanderList.Add(ChoseManager.Instance.TestHander1);
        data.HanderList.Add(ChoseManager.Instance.TestHander2);
        data.HanderList.Add(ChoseManager.Instance.TestHander3);
        ChosePanel.data = data;
        GUIManager.ShowView("ChosePanel");
    }


    #endregion



    #region 获得每个单独的对话List，并且绑定了需要触发的方法
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
        return dataList;
    }

    public List<StoryData> GetStage0State1List()
    {
        if (Stage0Dic == null)
        {
            Stage0Dic = new Dictionary<int, StoryData>();
            LoadStoryXML("Stage0Config", Stage0Dic);
        }
        List<StoryData> dataList = new List<global::StoryData>();
        foreach (KeyValuePair<int, StoryData> item in Stage0Dic)
        {
            if (item.Value.state == 1)
            {
                dataList.Add(item.Value);
            }
        }
        return dataList;
    }

    #endregion


    #region Stage0对话所需的方法

    


    #endregion
}
