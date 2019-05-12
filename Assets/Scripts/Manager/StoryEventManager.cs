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
        DiceManager.Instance.ShowDicePanel(4, 0.02f, SleepPressureCheck);
    }
    void ShenSheToSleep36()
    {
        ShowEventPanel_ChapterOne(2, 37);
    }
    void ShenSheToSleep37()
    {
        ShowEventPanel_ChapterOne(2, 38);
    }
    #endregion
    #endregion


    #region 绑定一些杂项的方法
    void ShowShenSheToSleep_26()
    {
        ShowEventPanel_ChapterOne(2, 26);
        AudioManager.Instance.FadeOutBGM(2.0f);
    }

    void OnToDreamBirdSoundFished()
    {
        GameZaXiangManager.Instance.ShowCover();
        CGManager.instance.ShowCGPanel("CG2");
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
    #endregion 
}
