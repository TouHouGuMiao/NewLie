using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
public enum SubtitlePositionEnum
{
    top=1,
    bottom=2,
}

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
        InitSubtitlesHander();
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


    public void ShowSubtitle(int id, int index, int time, string effectAudioName,SubtitlePositionEnum subtilePos= SubtitlePositionEnum.bottom)
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
        SubtitlesPanel.positionEnum = subtilePos;
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


    void InitSubtitlesHander()
    {
        SubtitlesData data1 = GetDataById(1);
        data1.SubtitlesDic.Add(0, CreatCharacter_Stature0);
        data1.SubtitlesDic.Add(1, CreatCharacter_Stature1);
        data1.SubtitlesDic.Add(2, CreatCharacter_Stature2);
        data1.SubtitlesDic.Add(3, CreatCharacter_Stature3);
        data1.SubtitlesDic.Add(4, CreatCharacter_Power0);
        data1.SubtitlesDic.Add(5, CreatCharacter_Power1);
        data1.SubtitlesDic.Add(6, CreatCharacter_Power2);
        data1.SubtitlesDic.Add(7, CreatCharacter_Power3);
        data1.SubtitlesDic.Add(8, CreatCharacter_Power4);
        data1.SubtitlesDic.Add(9, CreatCharacter_Power5);
        data1.SubtitlesDic.Add(10, CreatCharacter_Power6);
        data1.SubtitlesDic.Add(11, CreatCharacter_Power7);
        data1.SubtitlesDic.Add(12, CreatCharacter_Power8);
        data1.SubtitlesDic.Add(13, CreatCharacter_Power9);
        data1.SubtitlesDic.Add(14, CreatCharacter_VIT0);
        data1.SubtitlesDic.Add(15, CreatCharacter_VIT1);
        data1.SubtitlesDic.Add(16, CreatCharacter_VIT2);
        data1.SubtitlesDic.Add(17, CreatCharacter_VIT3);
        data1.SubtitlesDic.Add(18, CreatCharacter_VIT4);
        data1.SubtitlesDic.Add(19, CreatCharacter_VIT5);
        data1.SubtitlesDic.Add(20, CreatCharacter_IQ0);
        data1.SubtitlesDic.Add(21, CreatCharacter_IQ1);
        data1.SubtitlesDic.Add(22, CreatCharacter_IQ2);
        data1.SubtitlesDic.Add(23, CreatCharacter_IQ3);
        data1.SubtitlesDic.Add(24, CreatCharacter_IQ4);
        data1.SubtitlesDic.Add(25, CreatCharacter_IQ5);
        data1.SubtitlesDic.Add(26, CreatCharacter_IQ6);
        data1.SubtitlesDic.Add(27, CreatCharacter_IQ7);
        data1.SubtitlesDic.Add(28, CreatCharacter_Lucky0);
        data1.SubtitlesDic.Add(29, CreatCharacter_Lucky1);
        data1.SubtitlesDic.Add(30, CreatCharacter_Lucky2);
        data1.SubtitlesDic.Add(31, CreatCharacter_Lucky3);
        data1.SubtitlesDic.Add(32, CreatCharacter_Lucky4);
        data1.SubtitlesDic.Add(33, CreatCharacter_Lucky5);
        data1.SubtitlesDic.Add(34, CreatCharacter_Lucky6);
        data1.SubtitlesDic.Add(35, CreatCharacter_Lucky7);
        data1.SubtitlesDic.Add(36, CreatCharacter_Lucky8);
        data1.SubtitlesDic.Add(37, CreatCharacter_Lucky9);
        data1.SubtitlesDic.Add(38, CreatCharacter_Lucky10);
        data1.SubtitlesDic.Add(40, CreatCharacter_Lucky11);
        data1.SubtitlesDic.Add(41, CreatCharacter_Lucky12);
        data1.SubtitlesDic.Add(42, CreatCharacter_Lucky13);
        data1.SubtitlesDic.Add(43, CreatCharacter_Lucky14);
        data1.SubtitlesDic.Add(44, CreatCharacter_Lucky15);
        data1.SubtitlesDic.Add(45, CreatCharacter_WeiYan0);

        data1.SubtitlesDic.Add(47, CreatCharacter_WeiYanReslut);

        
     
    }
    #region 车技能
   
  #endregion

    #region 车角色卡
    void CreatCharacter_Stature0()
    {
        ShowSubtitle(1, 1, 2);
    }

    void CreatCharacter_Stature1()
    {
        ShowSubtitle(1, 2, 2);
    }

    void CreatCharacter_Stature2()
    {
        SurePropertyPanel.StatureCardBtnClick_ToSubtitles_0();
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_Stature3()
    {
        SurePropertyPanel.SureState = CreatSureState.Stature_State_Reslut;
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_Power0()
    {
        ShowSubtitle(1, 5,2);
    }

    void CreatCharacter_Power1()
    {
        ShowSubtitle(1, 6, 2);
    }

    void CreatCharacter_Power2()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_Power3()
    {
        ShowSubtitle(1, 8, 2);
    }

    void CreatCharacter_Power4()
    {
        ShowSubtitle(1, 9, 2);
    }

    void CreatCharacter_Power5()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_Power6()
    {
        ShowSubtitle(1, 11, 2);
    }
    void CreatCharacter_Power7()
    {
        ShowSubtitle(1, 12, 2);
    }
    void CreatCharacter_Power8()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_Power9()
    {
        SurePropertyPanel.SureState = CreatSureState.Power_State_Reslut;
        GUIManager.HideView("SubtitlesPanel");
    }


    void CreatCharacter_VIT0()
    {
        ShowSubtitle(1, 15, 2);
    }

    void CreatCharacter_VIT1()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_VIT2()
    {
        ShowSubtitle(1, 17, 2);
    }
    void CreatCharacter_VIT3()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_VIT4()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_VIT5()
    {
        SurePropertyPanel.SureState = CreatSureState.VIT_State_Reslut;
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_IQ0()
    {
        ShowSubtitle(1, 21, 2);
    }

    void CreatCharacter_IQ1()
    {
        ShowSubtitle(1, 22, 2);
    }
    void CreatCharacter_IQ2()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_IQ3()
    {
        ShowSubtitle(1, 24, 2);
    }

    void CreatCharacter_IQ4()
    {
    
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_IQ5()
    {
        ShowSubtitle(1, 26, 2);
    }

    void CreatCharacter_IQ6()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_IQ7()
    {
        SurePropertyPanel.SureState = CreatSureState.IQ_State_Reslut;
        GUIManager.HideView("SubtitlesPanel");
    }
    void CreatCharacter_Lucky0()
    {
        ShowSubtitle(1, 29, 2);
    }

    void CreatCharacter_Lucky1()
    {
        ShowSubtitle(1, 30, 2);
    }

    void CreatCharacter_Lucky2()
    {
        ShowSubtitle(1, 31, 2);
    }
    void CreatCharacter_Lucky3()
    {
        ShowSubtitle(1, 32, 2);
    }

    void CreatCharacter_Lucky4()
    {
        ShowSubtitle(1, 33, 2);
    }

    void CreatCharacter_Lucky5()
    {
        ShowSubtitle(1, 34, 2);
    }
    void CreatCharacter_Lucky6()
    {
        ShowSubtitle(1, 35, 2);
    }

    void CreatCharacter_Lucky7()
    {
        ShowSubtitle(1, 36, 2);
    }

    void CreatCharacter_Lucky8()
    {
        ShowSubtitle(1, 37, 2);
    }
    void CreatCharacter_Lucky9()
    {
        ShowSubtitle(1, 38, 2);
    }

    void CreatCharacter_Lucky10()
    {
        ShowSubtitle(1, 39, 2);
    }

    void CreatCharacter_Lucky11()
    {
        ShowSubtitle(1, 40, 2);
    }

    void CreatCharacter_Lucky12()
    {
        ShowSubtitle(1, 41, 2);
    }
    void CreatCharacter_Lucky13()
    {
        ShowSubtitle(1, 42, 2);
    }

    void CreatCharacter_Lucky14()
    {
        ShowSubtitle(1, 43, 2);
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_Lucky15()
    {
        SurePropertyPanel.SureState = CreatSureState.Lucky_State_Reslut;
        GUIManager.HideView("SubtitlesPanel");
    }
    void CreatCharacter_WeiYan0()
    {
        GUIManager.HideView("SubtitlesPanel");
    }

    void CreatCharacter_WeiYanReslut()
    {
        SurePropertyPanel.SureState = CreatSureState.WeiYan_State_Reslut;
        GUIManager.HideView("SubtitlesPanel");
    }

    #endregion
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

    public Dictionary<int, StoryHander> SubtitlesDic = new Dictionary<int, StoryHander>();
}
