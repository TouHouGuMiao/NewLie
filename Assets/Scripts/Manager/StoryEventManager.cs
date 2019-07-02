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
    private int now_Id=0;
    private int now_Index=0;


    private void InitLoad()
    {
        LoadStoryXML("ChapterOneEventConfig", ChapterOneDic);
        InitHander();
        InitHander_EventOne();
        InitHander_KongWu_Chu();
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
        now_Id = id;
        now_Index = index;
    }

    public int GetNowEventDataId()
    {
        return now_Id;
    }

    public int GetNowEventDataIndex()
    {
        return now_Index;
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
            XmlNode modelName = item.SelectSingleNode("spriteName");

            StoryData data = new StoryData();
            data.id = CommonHelper.Str2Int(id.InnerText);
            data.state = CommonHelper.Str2Int(state.InnerText);
            data.name = name.InnerText;
            data.index = CommonHelper.Str2Int(index.InnerText);
            data.cout = CommonHelper.Str2Int(cout.InnerText);
            data.modelName = modelName.InnerText;

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
        data.StoryHanderDic.Add(25, KongWuGuaiTan25);
        data.StoryHanderDic.Add(26, KongWuGuaiTan26);
        data.StoryHanderDic.Add(27, KongWuGuaiTan27);
        data.StoryHanderDic.Add(28, KongWuGuaiTan28);
        data.StoryHanderDic.Add(29, KongWuGuaiTan29);
        data.StoryHanderDic.Add(30, KongWuGuaiTan30);
        data.StoryHanderDic.Add(31, KongWuGuaiTan31);
        data.StoryHanderDic.Add(32, KongWuGuaiTan32);
        data.StoryHanderDic.Add(33, KongWuGuaiTan33);
        data.StoryHanderDic.Add(34, KongWuGuaiTan34);
        data.StoryHanderDic.Add(36, KongWuGuaiTan35);
        data.StoryHanderDic.Add(37, KongWuGuaiTan36);
        data.StoryHanderDic.Add(38, KongWuGuaiTan37);
        data.StoryHanderDic.Add(39, KongWuGuaiTan38);
        data.StoryHanderDic.Add(40, KongWuGuaiTan39);
        data.StoryHanderDic.Add(41, KongWuGuaiTan40);
        data.StoryHanderDic.Add(42, KongWuGuaiTan41);
        data.StoryHanderDic.Add(43, KongWuGuaiTan42);
        data.StoryHanderDic.Add(44, KongWuGuaiTan43);
        data.StoryHanderDic.Add(45, KongWuGuaiTan44);
        data.StoryHanderDic.Add(46, KongWuGuaiTan45);
        data.StoryHanderDic.Add(47, KongWuGuaiTan46);
        data.StoryHanderDic.Add(48, KongWuGuaiTan47);
        data.StoryHanderDic.Add(49, KongWuGuaiTan48);
        data.StoryHanderDic.Add(51, KongWuGuaiTan49);
        data.StoryHanderDic.Add(52, KongWuGuaiTan50);
        data.StoryHanderDic.Add(53, KongWuGuaiTan51);
        data.StoryHanderDic.Add(54, KongWuGuaiTan52);
        data.StoryHanderDic.Add(55, KongWuGuaiTan53);
        data.StoryHanderDic.Add(56, KongWuGuaiTan54);
    }

    private void InitHander_EventOne()
    {
        StoryData data = new StoryData();
        data = GetChapterOneEventDataById(1);
        data.StoryHanderDic.Add(0, new StoryHander(SureProp_QianYan_1));
        data.StoryHanderDic.Add(1, new StoryHander(SureProp_QianYan_2));
        data.StoryHanderDic.Add(2, new StoryHander(SureProp_QianYan_3));
        data.StoryHanderDic.Add(3, new StoryHander(SureStatureProp));
        data.StoryHanderDic.Add(4, new StoryHander(SureStatureProp_FristDice));
        data.StoryHanderDic.Add(5, new StoryHander(SureStatureProp_SecondDice));
        data.StoryHanderDic.Add(6, new StoryHander(SureStatureProp_GetResult));
        data.StoryHanderDic.Add(7, new StoryHander(SurePowerProp_QianYan1));
        data.StoryHanderDic.Add(8, new StoryHander(SurePowerProp_QianYan2));
        data.StoryHanderDic.Add(9, new StoryHander(SurePowerProp_QianYan3));
        data.StoryHanderDic.Add(10, new StoryHander(SurePowerPror_FristDice));
        data.StoryHanderDic.Add(11, new StoryHander(SurePowerProp_SecondDice_QianYan1));
        data.StoryHanderDic.Add(12, new StoryHander(SurePowerProp_SecondDice_QianYan2));
        data.StoryHanderDic.Add(13, new StoryHander(SurePowerProp_SecondDice));
        data.StoryHanderDic.Add(14, new StoryHander(SurePowerProp_ThridDice));
        data.StoryHanderDic.Add(15, new StoryHander(SurePowerProp_Reslut));
    }


    private void InitHander_KongWu_Chu()
    {
        StoryData data = new StoryData();
        data = GetChapterOneEventDataById(2);
        data.StoryHanderDic.Add(0, new StoryHander(ShenSheToSleep0));
        data.StoryHanderDic.Add(1, new StoryHander(ShenSheToSleep1));
        data.StoryHanderDic.Add(2, new StoryHander(ShenSheToSleep2));
        data.StoryHanderDic.Add(3, new StoryHander(ShenSheToSleep3));
        data.StoryHanderDic.Add(4, new StoryHander(ShenSheToSleep4));
        data.StoryHanderDic.Add(5, new StoryHander(ShenSheToSleep5));
        data.StoryHanderDic.Add(6, new StoryHander(ShenSheToSleep6));
        data.StoryHanderDic.Add(7, new StoryHander(ShenSheToSleep7));
        data.StoryHanderDic.Add(8, new StoryHander(ShenSheToSleep8));
        data.StoryHanderDic.Add(9, new StoryHander(ShenSheToSleep9));
        data.StoryHanderDic.Add(10, new StoryHander(ShenSheToSleep10));
        data.StoryHanderDic.Add(11, new StoryHander(ShenSheToSleep11));
        data.StoryHanderDic.Add(12, new StoryHander(ShenSheToSleep12));
        data.StoryHanderDic.Add(13, new StoryHander(ShenSheToSleep13));
        data.StoryHanderDic.Add(14, new StoryHander(ShenSheToSleep14));
        data.StoryHanderDic.Add(15, new StoryHander(ShenSheToSleep15));
        data.StoryHanderDic.Add(16, new StoryHander(ShenSheToSleep16));
        data.StoryHanderDic.Add(17, new StoryHander(ShenSheToSleep17));
        data.StoryHanderDic.Add(18, new StoryHander(ShenSheToSleep18));
        data.StoryHanderDic.Add(19, new StoryHander(ShenSheToSleep19));
        data.StoryHanderDic.Add(20, new StoryHander(ShenSheToSleep20));
        data.StoryHanderDic.Add(21, new StoryHander(ShenSheToSleep21));
        data.StoryHanderDic.Add(22, new StoryHander(ShenSheToSleep22));
        data.StoryHanderDic.Add(23, new StoryHander(ShenSheToSleep23));
        data.StoryHanderDic.Add(24, new StoryHander(ShenSheToSleep24));
        data.StoryHanderDic.Add(25, new StoryHander(ShenSheToSleep25));
        data.StoryHanderDic.Add(26, new StoryHander(ShenSheToSleep26));
        data.StoryHanderDic.Add(27, new StoryHander(ShenSheToSleep27));
        data.StoryHanderDic.Add(28, new StoryHander(ShenSheToSleep28));
        data.StoryHanderDic.Add(29, new StoryHander(ShenSheToSleep29));
        data.StoryHanderDic.Add(30, new StoryHander(ShenSheToSleep30));
        data.StoryHanderDic.Add(31, new StoryHander(ShenSheToSleep31));
        data.StoryHanderDic.Add(32, new StoryHander(ShenSheToSleep32));
        data.StoryHanderDic.Add(33, new StoryHander(ShenSheToSleep33));
        data.StoryHanderDic.Add(34, new StoryHander(ShenSheToSleep34));
        data.StoryHanderDic.Add(35, new StoryHander(ShenSheToSleep35));
        data.StoryHanderDic.Add(36, new StoryHander(ShenSheToSleep36));
        data.StoryHanderDic.Add(37, new StoryHander(ShenSheToSleep37));
        data.StoryHanderDic.Add(38, new StoryHander(ShenSheToSleep38));
        data.StoryHanderDic.Add(39, new StoryHander(ShenSheToSleep39));
        data.StoryHanderDic.Add(40, new StoryHander(ShenSheToSleep40));
        data.StoryHanderDic.Add(41, new StoryHander(ShenSheToSleep41));
        data.StoryHanderDic.Add(42, new StoryHander(ShenSheToSleep42));
        data.StoryHanderDic.Add(43, new StoryHander(ShenSheToSleep43));
        data.StoryHanderDic.Add(44, new StoryHander(ShenSheToSleep44));
        data.StoryHanderDic.Add(45, new StoryHander(ShenSheToSleep45));
        data.StoryHanderDic.Add(46, new StoryHander(ShenSheToSleep46));
        data.StoryHanderDic.Add(47, new StoryHander(ShenSheToSleep47));
        data.StoryHanderDic.Add(48, new StoryHander(ShenSheToSleep48));
        data.StoryHanderDic.Add(49, new StoryHander(ShenSheToSleep49));
        data.StoryHanderDic.Add(50, new StoryHander(ShenSheToSleep50));
        data.StoryHanderDic.Add(51, new StoryHander(ShenSheToSleep51));
        data.StoryHanderDic.Add(52, new StoryHander(ShenSheToSleep52));
        data.StoryHanderDic.Add(53, new StoryHander(ShenSheToSleep53));
        data.StoryHanderDic.Add(54, new StoryHander(ShenSheToSleep54));
        data.StoryHanderDic.Add(55, new StoryHander(ShenSheToSleep55));
        data.StoryHanderDic.Add(56, new StoryHander(ShenSheToSleep56));
        data.StoryHanderDic.Add(57, new StoryHander(ShenSheToSleep57));
        data.StoryHanderDic.Add(58, new StoryHander(ShenSheToSleep58));
        data.StoryHanderDic.Add(59, new StoryHander(ShenSheToSleep59));
        data.StoryHanderDic.Add(60, new StoryHander(ShenSheToSleep60));
        data.StoryHanderDic.Add(61, new StoryHander(ShenSheToSleep61));
        data.StoryHanderDic.Add(62, new StoryHander(ShenSheToSleep62));
        data.StoryHanderDic.Add(63, new StoryHander(ShenSheToSleep63));
        data.StoryHanderDic.Add(64, new StoryHander(ShenSheToSleep64));
        data.StoryHanderDic.Add(65, new StoryHander(ShenSheToSleep65));
        data.StoryHanderDic.Add(66, new StoryHander(ShenSheToSleep66));
        data.StoryHanderDic.Add(67, new StoryHander(ShenSheToSleep67));
        data.StoryHanderDic.Add(68, new StoryHander(ShenSheToSleep68));
        data.StoryHanderDic.Add(69, new StoryHander(ShenSheToSleep69));
        data.StoryHanderDic.Add(70, new StoryHander(ShenSheToSleep70));
        data.StoryHanderDic.Add(71, new StoryHander(ShenSheToSleep71));
        data.StoryHanderDic.Add(72, new StoryHander(ShenSheToSleep72));
        data.StoryHanderDic.Add(73, new StoryHander(ShenSheToSleep73));
        data.StoryHanderDic.Add(74, new StoryHander(ShenSheToSleep74));
        data.StoryHanderDic.Add(75, new StoryHander(ShenSheToSleep75));
        data.StoryHanderDic.Add(76, new StoryHander(ShenSheToSleep76));
        data.StoryHanderDic.Add(77, new StoryHander(ShenSheToSleep77));
        data.StoryHanderDic.Add(78, new StoryHander(ShenSheToSleep78));
        data.StoryHanderDic.Add(79, new StoryHander(ShenSheToSleep79));
        data.StoryHanderDic.Add(80, new StoryHander(ShenSheToSleep80));

        StoryData data1 = new StoryData();
        data1 = GetChapterOneEventDataById(3);
        data1.StoryHanderDic.Add(0, new StoryHander(CunMingLaiFang0));
        data1.StoryHanderDic.Add(1, new StoryHander(CunMingLaiFang1));
        data1.StoryHanderDic.Add(2, new StoryHander(CunMingLaiFang2));
        data1.StoryHanderDic.Add(3, new StoryHander(CunMingLaiFang3));
        data1.StoryHanderDic.Add(4, new StoryHander(CunMingLaiFang4));
        data1.StoryHanderDic.Add(5, new StoryHander(CunMingLaiFang5));
        data1.StoryHanderDic.Add(6, new StoryHander(CunMingLaiFang6));
        data1.StoryHanderDic.Add(7, new StoryHander(CunMingLaiFang7));
        data1.StoryHanderDic.Add(8, new StoryHander(CunMingLaiFang8));
        data1.StoryHanderDic.Add(9, new StoryHander(CunMingLaiFang9));
        data1.StoryHanderDic.Add(10, new StoryHander(CunMingLaiFang10));
        data1.StoryHanderDic.Add(11, new StoryHander(CunMingLaiFang11));
        data1.StoryHanderDic.Add(12, new StoryHander(CunMingLaiFang12));
        data1.StoryHanderDic.Add(13, new StoryHander(CunMingLaiFang13));
        data1.StoryHanderDic.Add(14, new StoryHander(CunMingLaiFang14));
        data1.StoryHanderDic.Add(15, new StoryHander(CunMingLaiFang15));
        data1.StoryHanderDic.Add(16, new StoryHander(CunMingLaiFang16));
        data1.StoryHanderDic.Add(17, new StoryHander(CunMingLaiFang17));
        data1.StoryHanderDic.Add(18, new StoryHander(CunMingLaiFang18));
        data1.StoryHanderDic.Add(19, new StoryHander(CunMingLaiFang19));
        data1.StoryHanderDic.Add(20, new StoryHander(CunMingLaiFang20));
        data1.StoryHanderDic.Add(21, new StoryHander(CunMingLaiFang21));
        data1.StoryHanderDic.Add(22, new StoryHander(CunMingLaiFang22));
        data1.StoryHanderDic.Add(23, new StoryHander(CunMingLaiFang23));
        data1.StoryHanderDic.Add(24, new StoryHander(CunMingLaiFang24));
        data1.StoryHanderDic.Add(25, new StoryHander(CunMingLaiFang25));
        data1.StoryHanderDic.Add(26, new StoryHander(CunMingLaiFang26));
        data1.StoryHanderDic.Add(27, new StoryHander(CunMingLaiFang27));
        data1.StoryHanderDic.Add(28, new StoryHander(CunMingLaiFang28));
        data1.StoryHanderDic.Add(29, new StoryHander(CunMingLaiFang29));
        data1.StoryHanderDic.Add(30, new StoryHander(CunMingLaiFang30));
        data1.StoryHanderDic.Add(31, new StoryHander(CunMingLaiFang31));
        data1.StoryHanderDic.Add(32, new StoryHander(CunMingLaiFang32));
        data1.StoryHanderDic.Add(33, new StoryHander(CunMingLaiFang33));
        data1.StoryHanderDic.Add(34, new StoryHander(CunMingLaiFang34));
        data1.StoryHanderDic.Add(35, new StoryHander(CunMingLaiFang35));
        data1.StoryHanderDic.Add(36, new StoryHander(CunMingLaiFang36));
        data1.StoryHanderDic.Add(37, new StoryHander(CunMingLaiFang37));
        data1.StoryHanderDic.Add(38, new StoryHander(CunMingLaiFang38));
        data1.StoryHanderDic.Add(39, new StoryHander(CunMingLaiFang39));
        data1.StoryHanderDic.Add(40, new StoryHander(CunMingLaiFang40));
        data1.StoryHanderDic.Add(41, new StoryHander(CunMingLaiFang41));
        data1.StoryHanderDic.Add(42, new StoryHander(CunMingLaiFang42));
        data1.StoryHanderDic.Add(43, new StoryHander(CunMingLaiFang43));
        data1.StoryHanderDic.Add(44, new StoryHander(CunMingLaiFang44));
        data1.StoryHanderDic.Add(45, new StoryHander(CunMingLaiFang45));
        data1.StoryHanderDic.Add(46, new StoryHander(CunMingLaiFang46));
        data1.StoryHanderDic.Add(47, new StoryHander(CunMingLaiFang47));
        data1.StoryHanderDic.Add(48, new StoryHander(CunMingLaiFang48));
        data1.StoryHanderDic.Add(49, new StoryHander(CunMingLaiFang49));


        data1.StoryHanderDic.Add(51, new StoryHander(CunMingLaiFang51));



        StoryData data2 = GetChapterOneEventDataById(4);
        data2.StoryHanderDic.Add(0, GoToCunZi0);
        data2.StoryHanderDic.Add(1, GoToCunZi1);
        data2.StoryHanderDic.Add(2, GoToCunZi2);
        data2.StoryHanderDic.Add(3, GoToCunZi3);
        data2.StoryHanderDic.Add(4, GoToCunZi4);
        data2.StoryHanderDic.Add(5, GoToCunZi5);
        data2.StoryHanderDic.Add(6, GoToCunZi6);
        data2.StoryHanderDic.Add(7, GoToCunZi7);
        data2.StoryHanderDic.Add(8, GoToCunZi8);
        data2.StoryHanderDic.Add(9, GoToCunZi9);
        data2.StoryHanderDic.Add(10, GoToCunZi10);
        data2.StoryHanderDic.Add(11, GoToCunZi11);
        data2.StoryHanderDic.Add(12, GoToCunZi12);
        data2.StoryHanderDic.Add(13, GoToCunZi13);
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
        ShowEventPanel_ChapterOne(0, 9);
        GUIManager.ShowView("SkillUsePanel");
        //TalkManager.Instance.ShowTalkPanel(0);
    }

    private void KongWuGuaiTan9()
    {
        GUIManager.HideView("EventStoryPanel");
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
        TalkManager.Instance.ShowTalkPanel(0, 2);
    }

    private void KongWuGuaiTan14()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }
  

    private  void KongWuGuaiTan15()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan16()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan17()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }


    private void KongWuGuaiTan18()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan19()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan20()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan21()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan22()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan23()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan24()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan25()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan26()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan27()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan28()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan29()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan30()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan31()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan32()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(0,0);
    }

    private void KongWuGuaiTan33()
    {
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(1,0);
    }

    private void KongWuGuaiTan34()
    {
        ChoseManager.Instance.ShowChosePanel(1);
    }
    private void KongWuGuaiTan35() {
       
        GUIManager.HideView("InputPanel");
        ChoseManager.Instance.ShowChosePanel(0);
    }
    private void KongWuGuaiTan36() {
        ShowEventPanel_ChapterOne(0, 38);
    }
    private void KongWuGuaiTan37() {
        ShowEventPanel_ChapterOne(0, 39);
    }
    private void KongWuGuaiTan38() {
        ShowEventPanel_ChapterOne(0, 40);
    }
    private void KongWuGuaiTan39() {
        ShowEventPanel_ChapterOne(0, 41);
    }
    private void KongWuGuaiTan40() {
        ShowEventPanel_ChapterOne(0, 42);
    }
    private void KongWuGuaiTan41() {
        ShowEventPanel_ChapterOne(0, 43);
    }
    private void KongWuGuaiTan42() {
        ShowEventPanel_ChapterOne(0, 44);
    }
    private void KongWuGuaiTan43() {
        GUIManager.ShowView("CoverPanel");
        ShowEventPanel_ChapterOne(0, 45);
        CGManager.instance.ShowCGPanel("kongkongwu");
    }
    private void KongWuGuaiTan44() {
        ShowEventPanel_ChapterOne(0, 46);
    }
    private void KongWuGuaiTan45() {
        ShowEventPanel_ChapterOne(0, 47);
    }
    
    private void KongWuGuaiTan46() {
        GUIManager.ShowView("CoverPanel");
        GUIManager.HideView("CGPanel");
        ShowEventPanel_ChapterOne(0, 48);
    }
    private void KongWuGuaiTan47() {
        
        TalkManager.Instance.ShowTalkPanel(0, 5);
    }
    private void KongWuGuaiTan48() {
        ChoseManager.Instance.ShowChosePanel(2);
    }
    private void KongWuGuaiTan49()
    {
        TalkManager.Instance.ShowTalkPanel(0, 6);
    }
    private void KongWuGuaiTan50() {
        ChoseManager.Instance.ShowChosePanel(3);
       // TalkManager.Instance.ShowTalkPanel(0, 7);
    }
    private void KongWuGuaiTan51() {
        TalkManager.Instance.ShowTalkPanel(0, 7);
    }
    private void KongWuGuaiTan52() {
        ChoseManager.Instance.ShowChosePanel(4);
    }
    private void KongWuGuaiTan53() {
        TalkManager.Instance.ShowTalkPanel(0, 8);
    }
    private void KongWuGuaiTan54() {
        string text = "尽量问出妖怪口中一直念叨着的话是什么意思";
        ShowEventPanel_ChapterOne(0, 57);
        NPCSpeakManager.Instance.ShowNPCSpeakPanel(2, 0);
        if (InputPanel.IsInput) {
            InputPanel.ShowTipsContainer(text);
        }
    }
    #endregion

    #region 骰出属性
    void SureProp_QianYan_1()
    {
        ShowEventPanel_ChapterOne(1, 1);
    }

    void SureProp_QianYan_2()
    {
        ShowEventPanel_ChapterOne(1, 2);
    }

    void SureProp_QianYan_3()
    {
        ShowEventPanel_ChapterOne(1, 3);
    }


    void SureStatureProp()
    {
        SurePropertyPanel.SureState = CreatSureState.Stature_State;
        ShowEventPanel_ChapterOne(1, 4);
    }

    void SureStatureProp_FristDice()
    {
        DiceManager.Instance.ShowDicePanel(6, 0.01f);
    }

    void SureStatureProp_SecondDice()
    {
        DiceManager.Instance.ShowDicePanel(6, 0.01f);
    }

    void SureStatureProp_GetResult()
    {
        SurePropertyPanel.SureState = CreatSureState.Stature_State_Reslut;
    }

    void SurePowerProp_QianYan1()
    {
        ShowEventPanel_ChapterOne(1, 8);
    }

    void SurePowerProp_QianYan2()
    {
        ShowEventPanel_ChapterOne(1, 9);
    }

    void SurePowerProp_QianYan3()
    {
        SurePropertyPanel.SureState = CreatSureState.Power_State;
    }

    void SurePowerPror_FristDice()
    {
        DiceManager.Instance.ShowDicePanel(6, 0.01f);
    }

    void SurePowerProp_SecondDice_QianYan1()
    {
        ShowEventPanel_ChapterOne(1, 12);
    }

    void SurePowerProp_SecondDice_QianYan2()
    {
        ShowEventPanel_ChapterOne(1, 13);
    }

    void SurePowerProp_SecondDice()
    {
        DiceManager.Instance.ShowDicePanel(6, 0.01f);
    }

    void SurePowerProp_ThridDice()
    {
        DiceManager.Instance.ShowDicePanel(6, 0.01f);
    }

    void SurePowerProp_Reslut()
    {
        SurePropertyPanel.SureState = CreatSureState.Power_State_Reslut;
    }
    #endregion


    #region 空鹜之章——初
    #region 梦
    void ShenSheToSleep0()
    {
        ShowEventPanel_ChapterOne(2, 1);
        AudioManager.Instance.PlayBg_Source("BGForDaoRu", true);
    }

    void ShenSheToSleep1()
    {
        ShowEventPanel_ChapterOne(2, 2);
    }

    void ShenSheToSleep2()
    {
        ShowEventPanel_ChapterOne(2, 3);
    }

    void ShenSheToSleep3()
    {
        ShowEventPanel_ChapterOne(2, 4);
    }

    void ShenSheToSleep4()
    {
        ShowEventPanel_ChapterOne(2, 5);
    }

    void ShenSheToSleep5()
    {
        ShowEventPanel_ChapterOne(2, 6);
    }

    void ShenSheToSleep6()
    {
        ShowEventPanel_ChapterOne(2, 7);
    }

    void ShenSheToSleep7()
    {
        ShowEventPanel_ChapterOne(2, 8);
    }

    void ShenSheToSleep8()
    {
        ShowEventPanel_ChapterOne(2, 9);
    }

    void ShenSheToSleep9()
    {
        ShowEventPanel_ChapterOne(2, 10);
    }

    void ShenSheToSleep10()
    {
        ShowEventPanel_ChapterOne(2, 11);
    }

    void ShenSheToSleep11()
    {
        ShowEventPanel_ChapterOne(2, 12);
    }

    void ShenSheToSleep12()
    {
        ShowEventPanel_ChapterOne(2, 13);
    }

    void ShenSheToSleep13()
    {
        ShowEventPanel_ChapterOne(2, 14);
    }

    void ShenSheToSleep14()
    {
        ShowEventPanel_ChapterOne(2, 15);
    }

    void ShenSheToSleep15()
    {
        ShowEventPanel_ChapterOne(2, 16);
    }
    void ShenSheToSleep16()
    {
        ShowEventPanel_ChapterOne(2, 17);
    }

    void ShenSheToSleep17()
    {
        ShowEventPanel_ChapterOne(2, 18);
    }

    void ShenSheToSleep18()
    {
        ShowEventPanel_ChapterOne(2, 19);
    }

    void ShenSheToSleep19()
    {
        ShowEventPanel_ChapterOne(2, 20);
    }

    void ShenSheToSleep20()
    {
        ShowEventPanel_ChapterOne(2, 21);
    }
    void ShenSheToSleep21()
    {
        ShowEventPanel_ChapterOne(2, 22);
    }

    void ShenSheToSleep22()
    {
        ShowEventPanel_ChapterOne(2, 23);
    }

    void ShenSheToSleep23()
    {
        ShowEventPanel_ChapterOne(2, 24);
    }

    void ShenSheToSleep24()
    {
        ShowEventPanel_ChapterOne(2, 25);
    }

    void ShenSheToSleep25()
    {
        AudioManager.Instance.PlayEffect_Source("daHuLu", ShowShenSheToSleep_26);
    }

    void ShenSheToSleep26()
    {
        AudioManager.Instance.PlayEffect_Source("BirdSound", OnToDreamBirdSoundFished);
        
        GUIManager.HideView("EventStoryPanel");
    }

    void ShenSheToSleep27()
    {
        ShowEventPanel_ChapterOne(2, 28);
    }

    void ShenSheToSleep28()
    {
        ShowEventPanel_ChapterOne(2, 29);
    }

    void ShenSheToSleep29()
    {
        ShowEventPanel_ChapterOne(2, 30);
    }

    void ShenSheToSleep30()
    {
        ShowEventPanel_ChapterOne(2, 31);
    }

    void ShenSheToSleep31()
    {
        ShowEventPanel_ChapterOne(2, 32);
    }

    void ShenSheToSleep32()
    {
        ShowEventPanel_ChapterOne(2, 33);
    }

    void ShenSheToSleep33()
    {
        ShowEventPanel_ChapterOne(2, 34);
    }

    void ShenSheToSleep34()
    {
        ShowEventPanel_ChapterOne(2, 35);
     
    }

    void ShenSheToSleep35()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();

        Dictionary<string, EventDelegate> targetDic = new Dictionary<string, EventDelegate>();
        targetDic.Add("myself", new EventDelegate (PressCheckInDream));
        skillDic.Add(7, targetDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic,true);
        //SkillManager.Instance.UpdataAndShowSkillUsePanel(list);
    }
    void ShenSheToSleep36()
    {
        ShowEventPanel_ChapterOne(2, 37);
    }
    void ShenSheToSleep37()
    {
        AudioManager.Instance.CloseBg_Source();
        ShowEventPanel_ChapterOne(2, 38);
    }

    void ShenSheToSleep38()
    {
        AudioManager.Instance.PlayBg_Source("Envy", false, 2.0f);
        ShowEventPanel_ChapterOne(2, 39);
    }

    void ShenSheToSleep39()
    {
        ShowEventPanel_ChapterOne(2, 40);
    }

    void ShenSheToSleep40()
    {
        ShowEventPanel_ChapterOne(2, 41);
    }

    void ShenSheToSleep41()
    {
        ShowEventPanel_ChapterOne(2, 42);
        //DiceManager.Instance.ShowDicePanel(10, 0.01f, ShenSheToSleep_IdeaCheck, 2);
    }

    void ShenSheToSleep42()
    {
        ShowEventPanel_ChapterOne(2, 43);
        
    }

    void ShenSheToSleep43()
    {
        ShowEventPanel_ChapterOne(2, 44);
    }

    void ShenSheToSleep44()
    {
        ShowEventPanel_ChapterOne(2, 45);
    }

    void ShenSheToSleep45()
    {
        ShowEventPanel_ChapterOne(2, 46);
    }


    void ShenSheToSleep46()
    {
        ShowEventPanel_ChapterOne(2, 47);
    }


    void ShenSheToSleep47()
    {
        ShowEventPanel_ChapterOne(2, 48);
    }


    void ShenSheToSleep48()
    {
        CGManager.instance.ShowBlackCover(1.0f);
        ShowEventPanel_ChapterOne(2, 49);
    }

    void ShenSheToSleep49()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> targetDic = new Dictionary<string, EventDelegate>();
        targetDic.Add("scene", new EventDelegate(IdeaCheckInDream));

        skillDic.Add(6, targetDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic,true);
    }

    void ShenSheToSleep50()
    {
        ShowEventPanel_ChapterOne(2, 51);
    }

    void ShenSheToSleep51()
    {
        ShowEventPanel_ChapterOne(2, 52);
    }

    void ShenSheToSleep52()
    {
        ShowEventPanel_ChapterOne(2, 53);
    }

    void ShenSheToSleep53()
    {
        ShowEventPanel_ChapterOne(2, 54);
    }

    void ShenSheToSleep54()
    {
        ShowEventPanel_ChapterOne(2, 55);
    }

    void ShenSheToSleep55()
    {
        ShowEventPanel_ChapterOne(2, 56);
    }

    void ShenSheToSleep56()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> targetDic_Investigate = new Dictionary<string, EventDelegate>();
        Dictionary<string, EventDelegate> targetDic_Listen = new Dictionary<string, EventDelegate>();
        targetDic_Listen.Add("scene", new EventDelegate(LisnCheckInDream));
        targetDic_Investigate.Add("scene", new EventDelegate(InvestigateCheckInDream));

        skillDic.Add(4, targetDic_Investigate);
        skillDic.Add(5, targetDic_Listen);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic,true);
    }

    void ShenSheToSleep57()
    {
        ShowEventPanel_ChapterOne(2, 58);
        AudioManager.Instance.PlayEffect_Source("doll_2");
    }

    void ShenSheToSleep58()
    {
        ShowEventPanel_ChapterOne(2, 59);
    }

    void ShenSheToSleep59()
    {
        ShowEventPanel_ChapterOne(2, 60);
    }

    void ShenSheToSleep60()
    {
        ShowEventPanel_ChapterOne(2, 61);
    }

    void ShenSheToSleep61()
    {
        AudioManager.Instance.PlayEffect_Source("doll_3", ShowShenSheToSleep_62);
    }

    void ShenSheToSleep62()
    {
        ShowEventPanel_ChapterOne(2, 63);
    }

    void ShenSheToSleep63()
    {
        AudioManager.Instance.PlayBg_Source("WeAreDoll",false,2.0f);
        ShowEventPanel_ChapterOne(2, 64);
    }


    void ShenSheToSleep64()
    {
        ShowEventPanel_ChapterOne(2, 65);
    }
    void ShenSheToSleep65()
    {
        ShowEventPanel_ChapterOne(2, 66);
    }

    void ShenSheToSleep66()
    {
        ShowEventPanel_ChapterOne(2, 67);
    }

    void ShenSheToSleep67()
    {
        ShowEventPanel_ChapterOne(2, 68);
    }

    void ShenSheToSleep68()
    {
        ShowEventPanel_ChapterOne(2, 69);
    }

    void ShenSheToSleep69()
    {
        ShowEventPanel_ChapterOne(2, 70);
    }

    void ShenSheToSleep70()
    {
        ShowEventPanel_ChapterOne(2, 71);
    }

    void ShenSheToSleep71()
    {
        ShowEventPanel_ChapterOne(2, 72);
    }

    void ShenSheToSleep72()
    {
        ShowEventPanel_ChapterOne(2, 73);
        IEnumeratorManager.Instance.StartCoroutine(PlayDollEffectSource());
    }

    IEnumerator PlayDollEffectSource()
    {
        AudioManager.Instance.PlayEffect_Source("doll_1");
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlayEffect_Source("doll_3");
        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.PlayEffect_Source("doll_1");
        yield return new WaitForSeconds(1.0f);
        AudioManager.Instance.PlayEffect_Source("doll_2");
        yield return new WaitForSeconds(2.0f);
        AudioManager.Instance.PlayEffect_Source("doll_1");
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlayEffect_Source("doll_3");
    }

    void ShenSheToSleep73()
    {
        ShowEventPanel_ChapterOne(2, 74);
    }

    void ShenSheToSleep74()
    {
        ShowEventPanel_ChapterOne(2, 75);
    }

    void ShenSheToSleep75()
    {
        ShowEventPanel_ChapterOne(2, 76);
    }

    void ShenSheToSleep76()
    {
        ShowEventPanel_ChapterOne(2, 77);
    }

    void ShenSheToSleep77()
    {
        ShowEventPanel_ChapterOne(2, 78);
        CGManager.instance.ShowBlackCover(1.0f);
    }

    void ShenSheToSleep78()
    {
        ShowEventPanel_ChapterOne(2, 79);
    }


    void ShenSheToSleep79()
    {
        ShowEventPanel_ChapterOne(2, 80);
        AudioManager.Instance.FadeOutBGM(1.5f);
    }

    void ShenSheToSleep80()
    {
        AudioManager.Instance.PlayEffect_Source("KnockDoor", ShowGoToStoreHouse0);
    }
    #endregion
    #region 村民来访
    void CunMingLaiFang0()
    {
        ShowEventPanel_ChapterOne(3, 1);
    }

    void CunMingLaiFang1()
    {
        ShowEventPanel_ChapterOne(3, 2);
    }

    void CunMingLaiFang2()
    {
        ShowEventPanel_ChapterOne(3, 3);

    }

    void CunMingLaiFang3()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, CunMingLaiFangPressureCheck,2);
    }

    void CunMingLaiFang4 ()
    {
        ShowEventPanel_ChapterOne(3, 9);
    }

    void CunMingLaiFang5()
    {
        ShowEventPanel_ChapterOne(3, 6);
    }

    void CunMingLaiFang6()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> ideaDic = new Dictionary<string, EventDelegate>();
        ideaDic.Add("myself", new EventDelegate (IdeaCheckWhenAwake));
        skillDic.Add(6, ideaDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic, true);
    }

    void CunMingLaiFang7()
    {
        ShowEventPanel_ChapterOne(3, 9);
    }

    void CunMingLaiFang8()
    {
        ShowEventPanel_ChapterOne(3, 9);
    }

    void CunMingLaiFang9()
    {
        ShowEventPanel_ChapterOne(3, 10);
    }
    void CunMingLaiFang10()
    {
        ShowEventPanel_ChapterOne(3, 11);
    }


    void CunMingLaiFang11()
    {
        TalkManager.Instance.ShowTalkPanel(1, 0);
    }

    void CunMingLaiFang12()
    {
        ShowEventPanel_ChapterOne(3, 13);
    }

    void CunMingLaiFang13()
    {
        ShowEventPanel_ChapterOne(3, 14);
    }

    void CunMingLaiFang14()
    {
        ShowEventPanel_ChapterOne(3, 15);
    }

    void CunMingLaiFang15()
    {
        ShowEventPanel_ChapterOne(3, 16);
    }

    void CunMingLaiFang16()
    {
        ShowEventPanel_ChapterOne(3, 17);
    }

    void CunMingLaiFang17()
    {
        ShowEventPanel_ChapterOne(3, 18);
    }

    void CunMingLaiFang18()
    {
        ShowEventPanel_ChapterOne(3, 19);
       
    }
    void CunMingLaiFang19()
    {
        ShowEventPanel_ChapterOne(3, 20);
    }

    void CunMingLaiFang20()
    {
        ShowEventPanel_ChapterOne(3, 21);
    }

    void CunMingLaiFang21()
    {
        ShowEventPanel_ChapterOne(3, 22);
    }

    void CunMingLaiFang22()
    {
        ShowEventPanel_ChapterOne(3, 23);
    }

    void CunMingLaiFang23()
    {
        ShowEventPanel_ChapterOne(3, 24);
    }

    void CunMingLaiFang24()
    {
        ShowEventPanel_ChapterOne(3, 25);
    }

    void CunMingLaiFang25()
    {
        TalkManager.Instance.ShowTalkPanel(1, 0);
        AudioManager.Instance.PlayEffect_Source("KnockDoor");
    }

    void CunMingLaiFang26()
    {
        EventStateManager.Instance.BeforeOpenDoor_XuZhang();
        GUIManager.HideView("EventStoryPanel");
    }

    void CunMingLaiFang27()
    {
        GUIManager.HideView("EventStoryPanel");
    }

    void CunMingLaiFang28()
    {
        ShowEventPanel_ChapterOne(3, 29);
    }

    void CunMingLaiFang29()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> ideaDic = new Dictionary<string, EventDelegate>();
        ideaDic.Add("A", new EventDelegate(IdeaCheckInCunMingLaiFang));

        skillDic.Add(6, ideaDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic, true);
    }

    void CunMingLaiFang30()
    {
        ShowEventPanel_ChapterOne(3, 31);
    }

    void CunMingLaiFang31()
    {
        ShowEventPanel_ChapterOne(3, 32);
    }

    void CunMingLaiFang32()
    {
        ShowEventPanel_ChapterOne(3, 33);
    }

    void CunMingLaiFang33()
    {
        ShowEventPanel_ChapterOne(3, 34);
    }

    void CunMingLaiFang34()
    {
        ShowEventPanel_ChapterOne(3, 35);
    }

    void CunMingLaiFang35()
    {
        ChoseManager.Instance.ShowChosePanel(5);

    }
    void CunMingLaiFang36()
    {
        ShowEventPanel_ChapterOne(3, 37);
    }

    void CunMingLaiFang37()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> listenDic = new Dictionary<string, EventDelegate>();
        listenDic.Add("scene", new EventDelegate(CunMingLaiFangListenSence));
        Dictionary<string, EventDelegate> InvestigateDic = new Dictionary<string, EventDelegate>();
        InvestigateDic.Add("A", new EventDelegate(CunMingLaiFangInvestigateToCunMing));
        Dictionary<string, EventDelegate> ThridEyeDic = new Dictionary<string, EventDelegate>();
        ThridEyeDic.Add("A", new EventDelegate(CunMingLaiFangThridEye));


        skillDic.Add(5, listenDic);
        skillDic.Add(4, InvestigateDic);
        skillDic.Add(3, ThridEyeDic);

        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic, true);
        ShowEventPanel_ChapterOne(3, 38);
    }
    void CunMingLaiFang38()
    {
        ChoseManager.Instance.ShowChosePanel(6)
;    }
    void CunMingLaiFang39()
    {
        TalkManager.Instance.ShowTalkPanel(1, 5);
    }

    void CunMingLaiFang40()
    {
        ShowEventPanel_ChapterOne(3, 41);
    }

    void CunMingLaiFang41()
    {
        TalkManager.Instance.ShowTalkPanel(1, 8);
    }

    void CunMingLaiFang42()
    {
        ShowEventPanel_ChapterOne(3, 43);
    }

    void CunMingLaiFang43()
    {
        ShowEventPanel_ChapterOne(3, 44);
    }

    void CunMingLaiFang44()
    {
        ShowEventPanel_ChapterOne(3, 45);
    }

    void CunMingLaiFang45()
    {
        ShowEventPanel_ChapterOne(3, 46);
    }

    void CunMingLaiFang46()
    {
        ShowEventPanel_ChapterOne(3, 47);
    }

    void CunMingLaiFang47()
    {
        ShowEventPanel_ChapterOne(3, 48);
    }

    void CunMingLaiFang48()
    {
        GUIManager.HideView("EventStoryPanel");
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> InvestigateDic = new Dictionary<string, EventDelegate>();
        InvestigateDic.Add("A", new EventDelegate(SeeWithCunMingOver_Investigate_A));
        skillDic.Add(5, InvestigateDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic);
        EventStateManager.Instance.WhenSeeWithCunMingOver();
    }

    void CunMingLaiFang49()
    {
        GameObject player = GameObject.FindWithTag("Player");
        CameraManager.Instance.FeatureOver("Player");
        GUIManager.HideView("EventStoryPanel");
    }

    void CunMingLaiFang51()
    {
        ChoseManager.Instance.ShowChosePanel(7);
    }

    #endregion
    #endregion


    #region 来到人里
    void GoToCunZi0()
    {
        ShowEventPanel_ChapterOne(4, 1);
        SkillManager.Instance.ClearSkillUsePanel();
    }


    void GoToCunZi1()
    {
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> ListenDic = new Dictionary<string, EventDelegate>();
        ListenDic.Add("Scene", new EventDelegate(ListenInCunZiWhenFrist));
        skillDic.Add(5, ListenDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic, false);
        GUIManager.HideView("EventStoryPanel");
    }

    void GoToCunZi2()
    {
        GUIManager.HideView("EventStoryPanel");

    }

    void GoToCunZi3()
    {
        GameObject player = GameObject.FindWithTag("Player");
        CameraManager.Instance.FeatureOver("Player");
        GUIManager.HideView("EventStoryPanel");
    }

    void GoToCunZi4()
    {
        GUIManager.HideView("EventStoryPanel");
    }

    void GoToCunZi5()
    {
        ShowEventPanel_ChapterOne(4, 6);
        NPCAnimatorManager.Instance.PlayCharacterTweenScale(NPCAnimatorManager.BGEnmu.Village, "cunMingA",new EventDelegate (CunMingARotateFished));
    }

    void GoToCunZi6()
    {
        TalkManager.Instance.ShowTalkPanel(2, 0);
    }

    void GoToCunZi7()
    {
        TalkManager.Instance.ShowTalkPanel(2, 1);
    }

    void GoToCunZi8()
    {
        ShowEventPanel_ChapterOne(4, 9);
    }

    void GoToCunZi9()
    {
        ShowEventPanel_ChapterOne(4, 10);
    }

    void GoToCunZi10()
    {
        ShowEventPanel_ChapterOne(4, 11);
    }

    void GoToCunZi11()
    {
        ChoseManager.Instance.ShowChosePanel(8);
    }

    void GoToCunZi12()
    {
        CameraManager.Instance.Feature(  NPCAnimatorManager.BGEnmu.Village,"investigate");
        ShowEventPanel_ChapterOne(4, 13);
    }

    void GoToCunZi13()
    {
        GameObject player = GameObject.FindWithTag("Player");
        CameraManager.Instance.FeatureOver("Player");
    }
    #endregion


    #region 绑定一些杂项的方法

    void CunMingARotateFished()
    {
        CameraManager.Instance.Feature(NPCAnimatorManager.BGEnmu.Village, "cunMingA", "investigate");
        TalkManager.Instance.ShowTalkPanel(2, 0);
    }

    void ShowEventInvestigate_CunMing()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (DiceCheckPanel.diceValue <= data.Investigate)
        {
            StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 49);
        }
        GUIManager.HideView("DiceCheckPanel");
    }


    void CunMingLaiFangListenSence()
    {

    }

    void CunMingLaiFangInvestigateToCunMing()
    {

    }

    void CunMingLaiFangThridEye()
    {

    }


    void CunMingLaiFangPressureCheck()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (DiceCheckPanel.diceValue >= data.preesure)
        {
            ShowEventPanel_ChapterOne(3, 4);
            GUIManager.HideView("DiceCheckPanel");
        }
        else
        {
            DiceManager.Instance.ShowDicePanel(4, 0.01f, CunMingLaiFangPressureAdd);
            GUIManager.HideView("DiceCheckPanel");
        }
    }

    void CunMingLaiFangPressureAdd()
    {
        CharacterPropBase characterPropBase = CharacterPropManager.Instance.GetPlayerCureentProp();
        float value = characterPropBase.preesure + DicePanel.diceValue;
        CharacterPropManager.Instance.ChangePlayerCurrentProp(PropType.preesure, value, ShowCunMingLaiFang5);
    }

    void ShowCunMingLaiFang5()
    {
        ShowEventPanel_ChapterOne(3, 5);
    }

    void ShowShenSheToSleep_26()
    {
        ShowEventPanel_ChapterOne(2, 26);
        AudioManager.Instance.FadeOutBGM(2.0f);
    }

    void OnToDreamBirdSoundFished()
    {
        GameZaXiangManager.Instance.ShowCover();
        CGManager.instance.ShowCGPanel("DreamDoll");
        AudioManager.Instance.PlayBg_Source("People",true,7.0f);
        ShowEventPanel_ChapterOne(2, 27);
    }

    void SleepPressureCheck()
    {
        CharacterPropBase characterPropBase = CharacterPropManager.Instance.GetPlayerCureentProp();
        float value = characterPropBase.preesure - DicePanel.diceValue;
        CharacterPropManager.Instance.ChangePlayerCurrentProp(PropType.preesure, value, ShowShenSheToSleep_36);
    }

    void ShowShenSheToSleep_36()
    {
        ShowEventPanel_ChapterOne(2, 36);
    }

    void ShowShenSheToSleep_57()
    {
        ShowEventPanel_ChapterOne(2, 57);
    }

    void ShowShenSheToSleep_62()
    {
        ShowEventPanel_ChapterOne(2, 62);
    }

    void ShenSheToSleep_IdeaCheck()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (DiceCheckPanel.diceValue <= data.Idea)
        {
            ShowEventPanel_ChapterOne(2, 50);
        }
        AudioManager.Instance.PauseBg_Source(2.0f);
        CGManager.instance.ShowCGPanel("DreamDoll");
        GUIManager.HideView("DiceCheckPanel");
    }

    void ShenSheToSleep_ListenCheck()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (DiceCheckPanel.diceValue <= data.Idea)
        {
            AudioManager.Instance.PlayEffect_Source("doll_1", ShowShenSheToSleep_57);
        }
        //AudioManager.Instance.ContinueBg_Source(1.0f);
        GUIManager.HideView("DiceCheckPanel");
    }

    void ShenSheToSleep_InvestigateCheck()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (DiceCheckPanel.diceValue <= data.Idea)
        {
            //ShowEventPanel_ChapterOne(2, 58);
        }
        AudioManager.Instance.ContinueBg_Source(1.0f);
        GUIManager.HideView("DiceCheckPanel");
    }


    void ShowGoToStoreHouse0()
    {
        GUIManager.HideView("CGPanel");
        GameZaXiangManager.Instance.ShowCover(1.0f, true);
        ShowEventPanel_ChapterOne(3, 0);
    }

    void ShowEvent3_30()
    {
        ShowEventPanel_ChapterOne(3, 30);
        GUIManager.HideView("DiceCheckPanel");
    }

    void IdeaCheckWhenAwake_Reslut()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (DiceCheckPanel.diceValue <= data.Idea)
        {
            ShowEventPanel_ChapterOne(3, 7);
        }
        else
        {
            ShowEventPanel_ChapterOne(3, 8);
        }
        GUIManager.HideView("DiceCheckPanel");
    }

    #endregion
    #region 绑定技能方法



    void ListenInCunZiWhenFrist()
    {
        GameObject player = GameObject.FindWithTag("Player").gameObject;
        ShowEventPanel_ChapterOne(4, 2);
        SkillManager.Instance.MoveSkillInSkillUsePanel(5);
    }

    void SeeWithCunMingOver_Investigate_A()
    {
        GameObject player = GameObject.FindWithTag("Player").gameObject;
        CameraManager.Instance.Feature(  NPCAnimatorManager.BGEnmu.ShenShe,"investigate");
        //DiceManager.Instance.ShowDicePanel(10, 0.01f, ShowEventInvestigate_CunMing, 2);
        ShowEventPanel_ChapterOne(3, 49);
        SkillManager.Instance.ClearSkillUsePanel();
    }


    void IdeaCheckWhenAwake()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, IdeaCheckWhenAwake_Reslut,2);
        SkillManager.Instance.ClearSkillUsePanel();
    }

    void PressCheckInDream()
    {
        DiceManager.Instance.ShowDicePanel(4, 0.02f, SleepPressureCheck);
        SkillManager.Instance.ClearSkillUsePanel();
    }

    void IdeaCheckInDream()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, ShenSheToSleep_IdeaCheck, 2);
        SkillManager.Instance.ClearSkillUsePanel();
    }

    void LisnCheckInDream()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, ShenSheToSleep_ListenCheck,2);
        SkillManager.Instance.ClearSkillUsePanel();
    }

    void InvestigateCheckInDream()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, ShenSheToSleep_InvestigateCheck, 2);
        SkillManager.Instance.ClearSkillUsePanel();
    }

    void IdeaCheckInCunMingLaiFang()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, ShowEvent3_30, 2);
        SkillManager.Instance.ClearSkillUsePanel();
    }

    #endregion
    #endregion
}
