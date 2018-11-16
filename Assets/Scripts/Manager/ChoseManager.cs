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
      

        if (ChoseDataDic == null)
        {
            Init();
        }
        ChoseData data = null;
        if(!ChoseDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("choseData is null");
            return;
        }
        ChosePanel.data = data;
        GUIManager.ShowView("ChosePanel");
    }





    #region 

    void InitChoseHander()
    {
        ChoseData data = ChoseDataDic[0];
        data.HanderList.Add(ContinueGo);

        ChoseData data1 = ChoseDataDic[1];
        data1.HanderList.Add(ObserveRabit_1);
        data1.HanderList.Add(GiveUpYinYangYu_1);

        ChoseData data2 = ChoseDataDic[2];
        data2.HanderList.Add(ObserveRabit_2);
        data2.HanderList.Add(GiveUpYinYangYu_2);

        ChoseData data3 = ChoseDataDic[3];
        data3.HanderList.Add(ObserveRabit_3);
        data3.HanderList.Add(GiveUpYinYangYu_3);

        ChoseData data4 = ChoseDataDic[4];
        data4.HanderList.Add(ObserveRabit_4);
        data4.HanderList.Add(GiveUpYinYangYu_4);

        ChoseData data5 = ChoseDataDic[5];
        data5.HanderList.Add(ObserveRabit_5);
        data5.HanderList.Add(GiveUpYinYangYu_5);

        ChoseData data6 = ChoseDataDic[6];
        data6.HanderList.Add(ObserveRabit_6);
        data6.HanderList.Add(GiveUpYinYangYu_6);
    }

    #endregion


    #region Stage0中选项方法设置
    void ContinueGo()
    {
        GUIManager.HideView("EventStoryPanel");
    }
    void ObserveRabit_1()
    {
        StoryManager.Instacne.ShowEventStoryList(StoryManager.Instacne.GetStage0TheFunnyRabitEvent_2());
        StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0WhiteRabbitSpeak_1());
    }

    void GiveUpYinYangYu_1()
    {

    }

    void ObserveRabit_2()
    {
        StoryManager.Instacne.ShowEventStoryList(StoryManager.Instacne.GetStage0TheFunnyRabitEvent_3());
        StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0WhiteRabbitSpeak_2());
    }

    void GiveUpYinYangYu_2()
    {

    }

    void ObserveRabit_3()
    {
        StoryManager.Instacne.ShowEventStoryList(StoryManager.Instacne.GetStage0TheFunnyRabitEvent_4());
        StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0WhiteRabbitSpeak_3());
    }

    void GiveUpYinYangYu_3()
    {

    }

    void ObserveRabit_4()
    {
        StoryManager.Instacne.ShowEventStoryList(StoryManager.Instacne.GetStage0TheFunnyRabitEvent_5());
        StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0WhiteRabbitSpeak_4());
    }

    void GiveUpYinYangYu_4()
    {

    }

    #endregion

    void ObserveRabit_5()
    {
        StoryManager.Instacne.ShowEventStoryList(StoryManager.Instacne.GetStage0TheFunnyRabitEvent_6());
    }

    void GiveUpYinYangYu_5()
    {

    }

    void ObserveRabit_6()
    {
        StoryManager.Instacne.ShowEventStoryList(StoryManager.Instacne.GetStage0TheFunnyRabitEvent_7());
    }

    void GiveUpYinYangYu_6()
    {

    }



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
}
