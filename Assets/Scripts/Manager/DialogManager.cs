using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DialogManager
{
    /// <summary>
    /// 说话者所在屏幕位置
    /// </summary>
    public enum DialogDirection
    {
        left,
        right,
    }

    private static DialogManager _instance = null;
    public static DialogManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DialogManager();
                _instance.InitLoad();
            }
            return _instance;
        }
    }

    private Dictionary<int, StoryData> DialogDic = new Dictionary<int, StoryData>();

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


    private StoryData GetDialogDataById(int id)
    {
        StoryData data = null;
        if (!DialogDic.TryGetValue(id, out data))
        {
            Debug.LogError("DialogDic id has error!" + id);
        }
        return data;

    }

    public void ShowDialogPanel(int id,int index,NPCAnimatorManager.BGEnmu bgEnum,string name, DialogDirection dialogDirection)
    {
        GameObject go = null;

        if (name == "player")
        {
            go = GameObject.FindWithTag("Player");
        }
        else
        {
            Transform parentBG=null;
            if (bgEnum == NPCAnimatorManager.BGEnmu.ShenShe)
            {
                parentBG = GameObject.FindWithTag("ShenSheBG").transform;
            }

            else if (bgEnum == NPCAnimatorManager.BGEnmu.Village)
            {
                parentBG = GameObject.FindWithTag("VillageBG").transform;
            }
            go = parentBG.FindRecursively(name).gameObject;
        }
        DialogBoxPanel.data = GetDialogDataById(id);
        DialogBoxPanel.data.index = index;
        DialogBoxPanel.direEnmu = dialogDirection;
        DialogBoxPanel.targetGameObject = go;
        GUIManager.ShowView("DialogBoxPanel");
    }

    private void InitLoad()
    {
        LoadStoryXML("DialogConfig", DialogDic);
        InitDialog();
    }

    private void InitDialog()
    {
        StoryData data = GetDialogDataById(0);
        data.StoryHanderDic.Add(0, DuiShiWithCunMing0);
        data.StoryHanderDic.Add(1, DuiShiWithCunMing1);
        data.StoryHanderDic.Add(2, DuiShiWithCunMing2);
        data.StoryHanderDic.Add(3, DuiShiWithCunMing3);
        data.StoryHanderDic.Add(4, DuiShiWithCunMing4);
        data.StoryHanderDic.Add(5, DuiShiWithCunMing5);
        data.StoryHanderDic.Add(6, DuiShiWithCunMing6);
        data.StoryHanderDic.Add(7, DuiShiWithCunMing7);
        data.StoryHanderDic.Add(8, DuiShiWithCunMing8);
        data.StoryHanderDic.Add(9, DuiShiWithCunMing9);
        data.StoryHanderDic.Add(10, DuiShiWithCunMing10);
        data.StoryHanderDic.Add(11, DuiShiWithCunMing11);
        data.StoryHanderDic.Add(12, DuiShiWithCunMing12);
        data.StoryHanderDic.Add(13, DuiShiWithCunMing13);
        data.StoryHanderDic.Add(14, DuiShiWithCunMing14);
        data.StoryHanderDic.Add(15, DuiShiWithCunMing15);
        data.StoryHanderDic.Add(16, DuiShiWithCunMing16);
        data.StoryHanderDic.Add(17, DuiShiWithCunMing17);
        data.StoryHanderDic.Add(18, DuiShiWithCunMing18);
        data.StoryHanderDic.Add(19, DuiShiWithCunMing19);
        data.StoryHanderDic.Add(20, DuiShiWithCunMing20);
        data.StoryHanderDic.Add(21, DuiShiWithCunMing21);
        data.StoryHanderDic.Add(22, DuiShiWithCunMing22);
        data.StoryHanderDic.Add(23, DuiShiWithCunMing23);
        data.StoryHanderDic.Add(24, DuiShiWithCunMing24);
        data.StoryHanderDic.Add(25, DuiShiWithCunMing25);
    }

    #region 仓库门口的对视
    void DuiShiWithCunMing0()
    {
        ShowDialogPanel(0, 1, NPCAnimatorManager.BGEnmu.Village, "cunMingC", DialogDirection.right);       
    }

    void DuiShiWithCunMing1()
    {
        NPCAnimatorManager.Instance.PlayCharacterTweenScale(NPCAnimatorManager.BGEnmu.Village, CameraManager.FeatureMode.right, "cunMingB", new EventDelegate(CunMingBAngry_0));
        GUIManager.HideView("DialogBoxPanel");
    }

    void DuiShiWithCunMing2()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 19);
    }

    void DuiShiWithCunMing3()
    {
        ShowDialogPanel(0, 4, NPCAnimatorManager.BGEnmu.Village, "cunMingB", DialogDirection.right);
       
    }

    void DuiShiWithCunMing4()
    {
        ShowDialogPanel(0, 5, NPCAnimatorManager.BGEnmu.Village, "cunMingC", DialogDirection.right);
    }
    void DuiShiWithCunMing5()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 20);
        GUIManager.HideView("DialogBoxPanel");
    }

    void DuiShiWithCunMing6()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 22);
    }

    void DuiShiWithCunMing7()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 23);
    }

    void DuiShiWithCunMing8()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 34);
    }

    void DuiShiWithCunMing9()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 35);
    }

    void DuiShiWithCunMing10()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 26);
    }

    void DuiShiWithCunMing11()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 27);
    }

    void DuiShiWithCunMing12()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 29);
    }

    void DuiShiWithCunMing13()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 30);
    }

    void DuiShiWithCunMing14()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 32);
    }

    void DuiShiWithCunMing15()
    {
        ShowDialogPanel(0, 16, NPCAnimatorManager.BGEnmu.Village, "cunMingA", DialogDirection.right);
    }

    void DuiShiWithCunMing16()
    {
        ShowDialogPanel(0, 17, NPCAnimatorManager.BGEnmu.Village, "cunMingB", DialogDirection.right);
    }


    void DuiShiWithCunMing17()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 33);
    }

    void DuiShiWithCunMing18()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 36);
    }

    void DuiShiWithCunMing19()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 37);
    }

    void DuiShiWithCunMing20()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 38);
    }

    void DuiShiWithCunMing21()
    {
        ShowDialogPanel(0, 22, NPCAnimatorManager.BGEnmu.Village, "cunMingC", DialogDirection.right);
    }

    void DuiShiWithCunMing22()
    {
        ShowDialogPanel(0, 23, NPCAnimatorManager.BGEnmu.Village, "cunMingA", DialogDirection.right);
    }

    void DuiShiWithCunMing23()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 39);
    }

    void DuiShiWithCunMing24()
    {
        ShowDialogPanel(0, 25, NPCAnimatorManager.BGEnmu.Village, "cunMingA", DialogDirection.right);
    }

    void DuiShiWithCunMing25()
    {
        ShowDialogPanel(0, 26, NPCAnimatorManager.BGEnmu.Village, "cunMingB", DialogDirection.right);
    }

    void DuiShiWithCunMing26()
    {
        GUIManager.HideView("DialogBoxPanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 40);
    }
    #endregion

    #region 绑定杂项方法
    void CunMingBAngry_0()
    {
        CameraManager.Instance.Feature(CameraManager.FeatureMode.right, NPCAnimatorManager.BGEnmu.Village, "investigate", "cunMingB", "investigate");
        TalkManager.Instance.ShowTalkPanel(2, 1);
    }
    #endregion
}
