using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class ChoseManager
{
    private static ChoseManager _Instace=null;

    public static ChoseManager Instance
    {
        get
        {
            if (_Instace == null)
            {
                _Instace = new ChoseManager();
                _Instace.Init();
            }

            return _Instace;
        }
    }


  

    private Dictionary<int, ChoseData> ChoseDataDic;

    void Init()
    {
        ChoseDataDic = new Dictionary<int, ChoseData>();
        LoadChoseXML("ChoseConfig", ChoseDataDic);
        InitChoseHander();
    }

    public void ShowChosePanel(int id)
    {
      
        ChoseData data = null;
        if(!ChoseDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("choseData is null");
            return;
        }
        ChosePanel.data = data;
        GUIManager.ShowView("ChosePanel");
    }

    public ChoseData GetChsoeDataByID(int id)
    {
        ChoseData data;
        if(!ChoseDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("not choseData in Dic" + "... " + id);
        }
        return data;
    }



    #region 

    void InitChoseHander()
    {
        ChoseData data = GetChsoeDataByID(0);
        data.HanderDic.Add(0,KongWuGuaiTanChoseNPCSpeak0);
        data.HanderDic.Add(1, KongWuGuaiTanChoseNPCSpeak1);
        data.HanderDic.Add(2, KongWuGuaiTanChoseNPCSpeak2);
        ChoseData data1 = GetChsoeDataByID(1);
        data1.HanderDic.Add(0,KongWuGuaiTanIntoCangKu0);

    }

    #endregion



    #region 与NPC对话相关选项

    #endregion



    void LoadChoseXML(string pathName, Dictionary<int, ChoseData> DataDic)
    {
        string path = "Config";
        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode node = xmlDoc.SelectSingleNode("Chose");
        XmlNodeList nodeList = node.ChildNodes; 

        foreach (XmlNode item in nodeList)
        {
            XmlNode id = item.SelectSingleNode("id");

            XmlNode name = item.SelectSingleNode("name");

            XmlNode choseList = item.SelectSingleNode("choseList");


            ChoseData data = new ChoseData();

            data.Id = CommonHelper.Str2Int(id.InnerText);
            data.Name = name.InnerText;
            foreach (XmlNode pair in choseList)
            {
                data.ChoseDesList.Add(pair.InnerText);
            }
            DataDic.Add(data.Id, data);
        
        }
    }

    #region   事件一中的选项

    void KongWuGuaiTanChoseNPCSpeak0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 13);
    }

    void KongWuGuaiTanChoseNPCSpeak1()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 14);
    }

    void KongWuGuaiTanChoseNPCSpeak2()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 34);
    }


    void KongWuGuaiTanIntoCangKu0()
    {
       GUIManager.ShowView("CoverPanel");
       TalkManager.Instance.ShowTalkPanel(0,4);
       
    }

    void KongWuGuaiTanIntoCangKu1()
    {
       
    }

    #endregion
}
