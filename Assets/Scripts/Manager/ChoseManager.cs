using System.Collections;
using System.Collections.Generic;
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

    private Dictionary<string, ChoseData> ChoseDataDic = new Dictionary<string, ChoseData>();

    public void Init()
    {

        
    }

    public void ShowChosePanel(string name)
    {
        ChoseData data = null;
        if(!ChoseDataDic.TryGetValue(name,out data))
        {
            Debug.LogError("not data in choseDic!");
            return;
        }
        ChosePanel.data = data;
        GUIManager.ShowView("ChosePanel");
    }


    #region
    public void TestHander1()
    {
        StoryData data = new StoryData();
        data.id = 0;
        data.name = "";
        data.state = 0;
        data.cout = 1;
        data.index = 0;
        data.SpeakList.Add("你冲了");
        StoryPanel.data = data;
        GUIManager.ShowView("StoryPanel");
    }

    public void TestHander2()
    {

        StoryData data = new StoryData();
        data.id = 0;
        data.name = "";
        data.state = 0;
        data.cout = 1;
        data.index = 0;
        data.SpeakList.Add("你爽了");
        StoryPanel.data = data;
        GUIManager.ShowView("ItemCreatPanel");
    }

    public void TestHander3()
    {

        StoryData data = new StoryData();
        data.id = 0;
        data.name = "";
        data.state = 0;
        data.cout = 1;
        data.index = 0;
        data.SpeakList.Add("你废了");
        StoryPanel.data = data;
        GUIManager.ShowView("StoryPanel");

        GUIManager.ShowView("ItemCreatPanel");
    }



    #endregion

}
