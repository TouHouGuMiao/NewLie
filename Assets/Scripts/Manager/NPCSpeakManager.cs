using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class NPCSpeakManager
{
    private static NPCSpeakManager _instance=null;

    public static NPCSpeakManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NPCSpeakManager();
            }
            return _instance;
        }
    }

    private Dictionary<int, NPCSpeakData> MarisaDic=new Dictionary<int, NPCSpeakData> ();

    void Init()
    {
        LoadNPCSpeakData("MarisaConfig",MarisaDic);
    }



    private void LoadNPCSpeakData(string pathName,Dictionary<int,NPCSpeakData> DataDic)
    {
        string path = "Config"+"/"+"NPC";

        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode node = xmlDoc.SelectSingleNode("Marisa");
        XmlNodeList nodeList = node.ChildNodes;

        foreach (XmlNode item in nodeList)
        {
            XmlNode id = item.SelectSingleNode("id");
            XmlNode main = item.SelectSingleNode("main");
            XmlNode SpeakCout = item.SelectSingleNode("SpeakCout");
            XmlNode Speak = item.SelectSingleNode("Speak");
            NPCSpeakData data = new NPCSpeakData();
           

            foreach (XmlNode pair in Speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            data.Id = CommonHelper.Str2Int(id.InnerText);
            data.Main = main.InnerText;
            data.SpeakCount = CommonHelper.Str2Int(SpeakCout.InnerText);
            DataDic.Add(data.Id, data);
        }
    }

}
