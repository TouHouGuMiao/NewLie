using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TalkManager
{
    private static TalkManager _Instance = null;
    public static TalkManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new TalkManager();
                _Instance.InitLoad();
            }
            return _Instance;
        }
    }

    private Dictionary<int, StoryData> StoryDataDic=new Dictionary<int, StoryData> ();
  
    public void ShowTalkPanel(int id,int index=0)
    {
        StoryData data = GetStoryDataById(id);
        data.index = index;
        TalkPanel.data = data;
        GUIManager.ShowView("TalkPanel");
    }

    public void ShowTalkPanel(StoryData data,int index=0)
    {
        data.index = index;
        TalkPanel.data = data;
        GUIManager.ShowView("TalkPanel");
    }

    void InitLoad()
    {
       LoadStoryXML("StoryConfig",StoryDataDic);
        InitHander();
    }
  
    public StoryData GetStoryDataById(int id)
    {
        StoryData data;
        if(!StoryDataDic.TryGetValue(id, out data))
        {
            Debug.LogError("id has error" + data.id + "      " + "TalkManager");
        }
        return data;
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


            foreach (XmlNode pair in speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            DataDic.Add(data.id, data);
        }
    }


    #region 第一章事件绑定
    void InitHander()
    {
        StoryData data = GetStoryDataById(0);
        data.StoryHanderDic.Add(0, KongWuGuaiTan0);
        data.StoryHanderDic.Add(1, KongWuGuaiTan1);
        data.StoryHanderDic.Add(2, KongWuGuaiTan_MengNanGuMiao0);
        data.StoryHanderDic.Add(4, KongWuGuaiTan_InToCangKu);
        data.StoryHanderDic.Add(5, KongWuGuaiTan_InToCangKu1);
        data.StoryHanderDic.Add(6, KongWuGuaiTan_InToCangKu2);
        data.StoryHanderDic.Add(7, KongWuGuaiTan_InToCangKu3);
        data.StoryHanderDic.Add(8, KongWuGuaiTan_InToCangKu4);

        StoryData data1 = GetStoryDataById(1);
        data1.StoryHanderDic.Add(0, CunMingLaiFang0);
        data1.StoryHanderDic.Add(1, CunMingLaiFang1);
        data1.StoryHanderDic.Add(2, CunMingLaiFang2);
        data1.StoryHanderDic.Add(4, CunMingLaiFang4);
        data1.StoryHanderDic.Add(5, CunMingLaiFang5);
        data1.StoryHanderDic.Add(6, CunMingLaiFang6);
        data1.StoryHanderDic.Add(7, CunMingLaiFang7);
        data1.StoryHanderDic.Add(8, CunMingLaiFang8);
        data1.StoryHanderDic.Add(9, CunMingLaiFang9);
        data1.StoryHanderDic.Add(10, CunMingLaiFang10);
        data1.StoryHanderDic.Add(11, CunMingLaiFang11);

        StoryData data2 = GetStoryDataById(2);
        data2.StoryHanderDic.Add(0, GoToCunZi0);
        data2.StoryHanderDic.Add(1, GoToCunZi1);
        data2.StoryHanderDic.Add(2, GoToCunZi2);
    }


    #endregion


    #region 村民来访
    void CunMingLaiFang0()
    {
        ShowTalkPanel(1, 1);
    }

    void CunMingLaiFang1()
    {
        ShowTalkPanel(1, 2);
    }

    void CunMingLaiFang2()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 26);
    }

    void CunMingLaiFang4()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 28);
    }

    void CunMingLaiFang5()
    {
        ShowTalkPanel(1, 6);
    }

    void CunMingLaiFang6()
    {
        ShowTalkPanel(1, 7);
    }

    void CunMingLaiFang7()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 40);
    }

    void CunMingLaiFang8()
    {
        ShowTalkPanel(1, 9);
    }

    void CunMingLaiFang9()
    {
        ShowTalkPanel(1, 10);
    }

    void CunMingLaiFang10()
    {
        ShowTalkPanel(1, 11);
    }

    void CunMingLaiFang11()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 42);
    }


    #endregion
    #region 空鹜怪谈
    void KongWuGuaiTan0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 9);
    }

    void KongWuGuaiTan1()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 11);
    }
   

    void KongWuGuaiTan_MengNanGuMiao0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 33);
    }
    void KongWuGuaiTan_InToCangKu() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 37);
    }
    void KongWuGuaiTan_InToCangKu1() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 49);
       
    }
    void KongWuGuaiTan_InToCangKu2() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 52);
       // ChoseManager.Instance.ShowChosePanel(3);
    }
    void KongWuGuaiTan_InToCangKu3() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 54);
        //ChoseManager.Instance.ShowChosePanel(4);
    }
    void KongWuGuaiTan_InToCangKu4() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 56);
    }
    #endregion
    #region 来到人里
    void GoToCunZi0()
    {
        CameraManager.Instance.FeatureOver(new EventDelegate (OnCunMingShotFinshed_ArrangeNPCAndPlayer),false);
        TableCardManager.Instance.ReplaceTable_ChildGroundCard(10);

    }

    void GoToCunZi1()
    {
        CameraManager.Instance.FeatureOver(new EventDelegate (OnCunMingBAngry_0ChangePlayerPress),false);
      
    }

    void GoToCunZi2()
    {
        CameraManager.Instance.FeatureOver(new EventDelegate (OnCunMingC_Cry0ChangePlayerPress), false);
      
    }


    #endregion

    #region 杂项方法 

    void OnCunMingBAngry_0ChangePlayerPress()
    {
        BattleManager.Instance.ShowPlayerBattleSlider(3, OnCunMingBAngry_0Fished);
    }

    void OnCunMingC_Cry0ChangePlayerPress()
    {
        BattleManager.Instance.ShowPlayerBattleSlider(4, OnCunMingC_Cry_1Fished);
    } 



    void OnCunMingBAngry_0Fished()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 7);
    }

    void OnCunMingC_Cry_1Fished()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 8);
    }

    void OnCunMingShotFinshed_ArrangeNPCAndPlayer()
    {
        BattleCamera.Instance.needMoveWithPlayer = false;
        Transform curBg = CameraManager.Instance.GetCurBG();
        GameObject prefab = ResourcesManager.Instance.LoadCharacterPrefab("cunMing");
        GameObject cunMingB = GameObject.Instantiate(prefab);
        GameObject cunMingC = GameObject.Instantiate(prefab);
        GameObject cunMingA = curBg.FindRecursively("cunMingA").gameObject;
        GameObject player = GameObject.FindWithTag("Player");
        cunMingB.name = "cunMingB";
        cunMingB.transform.SetParent(curBg, false);
        cunMingB.transform.rotation = Quaternion.Euler(0, 180, 0);
        cunMingB.transform.localPosition = cunMingA.transform.localPosition + new Vector3(10, 0, 0);
        cunMingC.name = "cunMingC";
        cunMingC.transform.rotation = Quaternion.Euler(0, 180, 0);
        cunMingC.transform.SetParent(curBg, false);
        cunMingC.transform.localPosition = cunMingA.transform.localPosition + new Vector3(10, 0, 0);
        GameObject mainCamera = GameObject.FindWithTag("MainCamera").gameObject;
        Vector3 screenVec_Player = new Vector3((Screen.width / 5)*1.5F, 80, Mathf.Abs(mainCamera.transform.position.z));
        Vector3 screenVec_CunMingA = new Vector3((Screen.width / 5) * 3.5F, 80, Mathf.Abs(mainCamera.transform.position.z));

        Vector3 targetVec_Player = Camera.main.ScreenToWorldPoint(screenVec_Player);
        targetVec_Player = new Vector3(targetVec_Player.x, player.transform.position.y, 0);
        Vector3 targetVec_CunMingA = Camera.main.ScreenToWorldPoint(screenVec_CunMingA);
        targetVec_CunMingA = targetVec_CunMingA - curBg.transform.position;
        targetVec_CunMingA = new Vector3(targetVec_CunMingA.x, cunMingA.transform.localPosition.y, 0);
        Vector3 targetVec_CunMingB = targetVec_CunMingA + new Vector3(1.2F, 0, 0);
        Vector3 targetVec_CunMingC = targetVec_CunMingB + new Vector3(1.2F, 0, 0);


        TweenPosition playerTP = player.GetComponent<TweenPosition>();
        TweenPosition cunMingATP = cunMingA.GetComponent<TweenPosition>();
        TweenPosition cunMingBTP = cunMingB.GetComponent<TweenPosition>();
        TweenPosition cunMingCTP = cunMingC.GetComponent<TweenPosition>();
        playerTP.enabled = true;
        playerTP.from = player.transform.position;
        playerTP.to = targetVec_Player;
        playerTP.onFinished.Clear();
        playerTP.ResetToBeginning();

        cunMingATP.enabled = true;
        cunMingATP.from = cunMingA.transform.localPosition;
        cunMingATP.to = targetVec_CunMingA;
        cunMingATP.onFinished.Clear();
        cunMingATP.onFinished.Add(new EventDelegate(PlayAudioSou));
        cunMingATP.ResetToBeginning();

        cunMingBTP.enabled = true;
        cunMingBTP.from = cunMingB.transform.localPosition;
        cunMingBTP.to = targetVec_CunMingB;
        cunMingBTP.delay = 0.4f;
        cunMingBTP.onFinished.Clear();
        cunMingBTP.onFinished.Add(new EventDelegate(PlayAudioSou));
        cunMingBTP.ResetToBeginning();

        cunMingCTP.enabled = true;
        cunMingCTP.from = cunMingC.transform.localPosition;
        cunMingCTP.to = targetVec_CunMingC;
        cunMingCTP.delay = 0.8f;
        cunMingCTP.onFinished.Clear();
        cunMingCTP.onFinished.Add(new EventDelegate(PlayAudioSou));
        cunMingCTP.onFinished.Add(new EventDelegate(PlayBattleBGMAndNextState));
        cunMingCTP.ResetToBeginning();
        BattleManager.Instance.ShowRoundStart();
        AudioManager.Instance.PlayEffect_Source("startBattle");
    }

    void PlayAudioSou()
    {
        AudioManager.Instance.PlayEffect_Source("sou");
    }

    void PlayBattleBGMAndNextState()
    {
        AudioManager.Instance.PlayBg_Source("BattleNormal", true, 3.0f);
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 6);
        BattleManager.Instance.ShowPlayerBattleSlider(0);
    }



    #endregion
}
