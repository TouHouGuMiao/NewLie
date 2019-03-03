using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanel : IView
{
    public static bool IsInput
    {
        get
        {
            if (inputPanel == null)
            {
                return false;
            }

            if (inputPanel.activeSelf)
            {
               
                    return true;
                                
            }
            return false;
        }
    }
    public static bool isChange = false;

    private static GameObject inputPanel;
    private UIInput input;
    public static int NPCId;
    public static int eventId;
    private Dictionary<int, NPCSpeakData> NPCSpeakDic;
    private NPCSpeakData m_NPCSpeakData=new NPCSpeakData ();
    protected override void OnStart()
    {
        input = this.GetChild("InputBG").GetComponent<UIInput>();
        input.onSubmit.Add(new EventDelegate(OnSumbit));
        inputPanel = GUIManager.FindPanel("InputPanel");
    }

    protected override void OnShow()
    {
        NPCSpeakDic = NPCSpeakManager.Instance.GetNPCDicById(NPCId);
        inputPanel.transform.localPosition = new Vector3(38, 198, 0);
        isChange = false;
        foreach (KeyValuePair<int,NPCSpeakData> item in NPCSpeakDic)
        {
            if (item.Key == eventId)
            {
                m_NPCSpeakData = item.Value;
            }
        }
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        input.value = null;
        isChange = false;
    }

    public override void Update()
    {
        if (TalkPanel.isSpeak)
        {
            input.enabled = false;
        }

        else
        {
            input.enabled = true;
        }
        if (isChange==false)
        {
            input.hideInput = false;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isChange = true;
                input.hideInput = true;
            }
        }

    }


    void OnSumbit()
    {

        if (isChange)
        {
            return;
        }
        string text = input.value;
        TestInputManager.Instance.CreatOrWriteConfig(NPCId.ToString() + text);
        if (text.Length<=0)
        {
            return;
        }
        for (int i = 0; i < m_NPCSpeakData.SpeakCount; i++)
        {
            if (text.Contains(m_NPCSpeakData.MainList[i]))
            {
                m_NPCSpeakData.OnEnterDownDic[i]();
                return;
            }
        }
        if (NPCId == 0)
        {
            if (eventId == 0)
            {
                m_NPCSpeakData.OnEnterDownDic[18]();
            }
        }
    }




}
