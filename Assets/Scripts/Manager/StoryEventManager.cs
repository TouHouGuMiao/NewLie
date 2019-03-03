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

    /// <summary>
    /// 第一个参数代表事件编号，第二个参数代表事件数据的index
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
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
        data.StoryHanderDic.Add(0, KongWuGuaiTan0);
        data.StoryHanderDic.Add(1, KongWuGuaiTan1);
        data.StoryHanderDic.Add(2, KongWuGuaiTan2);
        data.StoryHanderDic.Add(3, KongWuGuaiTan3);
        data.StoryHanderDic.Add(4, KongWuGuaiTan4);
        data.StoryHanderDic.Add(5, KongWuGuaiTan5);
        data.StoryHanderDic.Add(6, KongWuGuaiTan6);
        data.StoryHanderDic.Add(7, KongWuGuaiTan7);
        data.StoryHanderDic.Add(8, KongWuGuaiTan8);
        data.StoryHanderDic.Add(9, KongWuGuaiTan9);
        data.StoryHanderDic.Add(10, KongWuGuaiTan10);
        data.StoryHanderDic.Add(11, KongWuGuaiTan11);
        data.StoryHanderDic.Add(12, KongWuGuaiTan12);
        data.StoryHanderDic.Add(13, KongWuGuaiTan13);
        data.StoryHanderDic.Add(14, KongWuGuaiTan14);
        data.StoryHanderDic.Add(15, KongWuGuaiTan15);
        data.StoryHanderDic.Add(16, KongWuGuaiTan16);
        data.StoryHanderDic.Add(17, KongWuGuaiTan17);
        data.StoryHanderDic.Add(18, KongWuGuaiTan18);
        data.StoryHanderDic.Add(19, KongWuGuaiTan19);
        data.StoryHanderDic.Add(20, KongWuGuaiTan20);
        data.StoryHanderDic.Add(21, KongWuGuaiTan21);
        data.StoryHanderDic.Add(22, KongWuGuaiTan22);
        data.StoryHanderDic.Add(23, KongWuGuaiTan23);
        data.StoryHanderDic.Add(24, KongWuGuaiTan24);
    }

    #endregion


    #region   第一章事件触发的方法

    #region 空鹜怪谈
    private void KongWuGuaiTan0()
    {
       ShowEventPanel_ChapterOne(0, 1);
    }
    private void KongWuGuaiTan1()
    {
        ShowEventPanel_ChapterOne(0, 2);
        CGManager.instance.ChangeCG("CG2");
    }

    private void KongWuGuaiTan2()
    {
        ShowEventPanel_ChapterOne(0, 3);
    }

    private void KongWuGuaiTan3()
    {
        ShowEventPanel_ChapterOne(0, 4);
    }
    private void KongWuGuaiTan4()
    {
        ShowEventPanel_ChapterOne(0, 5);
    }

    private void KongWuGuaiTan5()
    {
        ShowEventPanel_ChapterOne(0, 6);
    }
    private void KongWuGuaiTan6()
    {
        ShowEventPanel_ChapterOne(0, 7);
    }

    private void KongWuGuaiTan7()
    {
        ShowEventPanel_ChapterOne(0, 8);
    }

    private void KongWuGuaiTan8()
    {
        GameZaXiangManager.Instance.ShowCover();
        GUIManager.HideView("CGPanel");
        TalkManager.Instance.ShowTalkPanel(0);
    }

    private void KongWuGuaiTan9()
    {
        ShowEventPanel_ChapterOne(0, 10);
    }

    private void KongWuGuaiTan10()
    {
        TalkManager.Instance.ShowTalkPanel(0,1);
    }

    private void KongWuGuaiTan11()
    {
        ShowEventPanel_ChapterOne(0, 12);
    }

    private void KongWuGuaiTan12()
    {
        ChoseManager.Instance.ShowChosePanel(0);
    }

    private void KongWuGuaiTan13()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(1);
    }

    private void KongWuGuaiTan14()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }
    #endregion

    private  void KongWuGuaiTan15()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan16()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan17()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }


    private void KongWuGuaiTan18()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan19()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan20()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan21()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan22()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan23()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }

    private void KongWuGuaiTan24()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0);
    }
    #endregion
}
