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
        data1.HanderDic.Add(1, KongWuGuaiTanIntoCangKu1);
        ChoseData data2 = GetChsoeDataByID(2);
        data2.HanderDic.Add(0, KongWuGuaiTanTalkWithKongWu1_0);
        data2.HanderDic.Add(1, KongWuGuaiTanTalkWithKongWu1_1);
        ChoseData data3 = GetChsoeDataByID(3);
        data3.HanderDic.Add(0, KongWuGuaiTanTalkWithKongWu2_0);
        data3.HanderDic.Add(1, KongWuGuaiTanTalkWithKongWu2_1);
        ChoseData data4 = GetChsoeDataByID(4);
        data4.HanderDic.Add(0, KongWuGuaiTanTalkWithKongWu3_0);
        data4.HanderDic.Add(1, KongWuGuaiTanTalkWithKongWu3_1);

        ChoseData data5 = GetChsoeDataByID(5);
        data5.HanderDic.Add(0, SeeTheCumMing_0);
        data5.HanderDic.Add(1, SeeTheCunMing_1);
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
       StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 50);
       
    }

    void KongWuGuaiTanIntoCangKu1()
    {
       
    }
    /*
      KongWuGuaiTanTalkWithKongWu1_0 前面的1表示的是这是第几次询问
     */
    void KongWuGuaiTanTalkWithKongWu1_0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 51);
    }


    void KongWuGuaiTanTalkWithKongWu1_1()
    {

    }
   
    void KongWuGuaiTanTalkWithKongWu2_0() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 53);
    }
    void KongWuGuaiTanTalkWithKongWu2_1() {
       
    }
    void KongWuGuaiTanTalkWithKongWu3_0() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 55);
    }
    void KongWuGuaiTanTalkWithKongWu3_1() {

    }




    #endregion

    #region 村民来访
    void SeeTheCumMing_0()
    {

    }

    void SeeTheCunMing_1()
    {

    }


    #endregion
}
