using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SubtitlesManager
{
    private static SubtitlesManager _instance;
    public static SubtitlesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SubtitlesManager();
                _instance.Init();
            }
            return _instance;
        }
    }
    private Dictionary<int, SubtitlesData> SubtitlesDic = new Dictionary<int, SubtitlesData>();


    private void Init()
    {
        LoadSubtitlesConfig("SubtitlesConfig", SubtitlesDic);
    }

    public void ShowSubtitle(int id,int index,int time)
    {
        SubtitlesData data = GetDataById(id);
        data.Index = index;
        if (data.SpeakList[index].Length==0)
        {
            Debug.LogError("subTitle has error");
            return;
        }

        if (time == 0)
        {
            Debug.LogError("time has error");
            return;
        }
        int perChar = data.SpeakList[index].Length / time;
        SubtitlesPanel.perChar = perChar;
        SubtitlesPanel.data = data;
        GUIManager.ShowView("SubtitlesPanel");
    }

    public void ShowSubtitle(int id, int index, int time,string effectAudioName)
    {
        SubtitlesData data = GetDataById(id);
        data.Index = index;
        if (data.SpeakList[index].Length == 0)
        {
            Debug.LogError("subTitle has error");
            return;
        }

        if (time == 0)
        {
            Debug.LogError("time has error");
            return;
        }
        int perChar = data.SpeakList[index].Length / time;
        SubtitlesPanel.perChar = perChar;
        SubtitlesPanel.data = data;
        SubtitlesPanel.effectAudioName = effectAudioName;
        GUIManager.ShowView("SubtitlesPanel");
    }

    private SubtitlesData GetDataById(int id)
    {
        SubtitlesData data = null;
        if (!SubtitlesDic.TryGetValue(id, out data))
        {
            Debug.LogError("subTitleData id has error!" + id);
        }
        return data;
    }

    void LoadSubtitlesConfig(string pathName, Dictionary<int, SubtitlesData> DataDic)
    {
        string path = "Config";

        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode node = xmlDoc.SelectSingleNode("Subtitles");
        XmlNodeList nodeList = node.ChildNodes;

        foreach (XmlNode item in nodeList)
        {
            XmlNode id = item.SelectSingleNode("id");
            XmlNode index = item.SelectSingleNode("index");
            XmlNode name = item.SelectSingleNode("name");
            XmlNode cout = item.SelectSingleNode("cout");
            XmlNode speak = item.SelectSingleNode("Speak");

            SubtitlesData data = new  SubtitlesData();
            data.Id = CommonHelper.Str2Int(id.InnerText);
            data.Name = name.InnerText;
            data.Index = CommonHelper.Str2Int(index.InnerText);
            data.Cout = CommonHelper.Str2Int(cout.InnerText);

            foreach (XmlNode pair in speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            DataDic.Add(data.Id, data);
        }
    }
}

public class SubtitlesData
{
    public int Id
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }

    public List<string> SpeakList = new List<string>();

    public int Index
    {
        get;
        set;
    }
    public int Cout
    {
        get;
        set;
    }
}
