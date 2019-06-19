using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStoryPanel : IView
{
    public EventStoryPanel()
    {
        m_Layer = Layer.SpeicalUI;
    }

    //public static StoryData data;
    public static StoryData data;
    private UILabel nameLabel;
    private UILabel speakLabel;
 

    private StoryHander lastHander=null;
    private Transform cardWidget;

    private GameObject bg_GameObject;
    
    private StoryData curData = new StoryData ();

    private TypewriterEffect writer;
    /// <summary>
    /// list中 StoryData 的标志位
    /// </summary>
    private int index = 0;
    /// <summary>
    /// list 中 StoryData中的SpeakList的标志位
    /// </summary>


   
    private string addText = null;

    private static GameObject eventStoryPanel;

    public static bool isEventSpeak
    {
        get
        {
            if (eventStoryPanel == null)
            {
                return false;
            }

            if (eventStoryPanel.activeSelf)
            {
                return true;
            }
            return false;
        }
    }
    protected override void OnStart()
    {
        //nameLabel = this.GetChild("nameLabel").GetComponent<UILabel>();
        speakLabel = this.GetChild("sperakLabel").GetComponent<UILabel>();
        cardWidget = this.GetChild("CardWidget");
        writer = speakLabel.GetComponent<TypewriterEffect>();
        eventStoryPanel = GUIManager.FindPanel("EventStoryPanel");
        bg_GameObject = this.GetChild("BG").gameObject;
    }



    protected override void OnShow()
    {

        if (bg_GameObject.activeSelf && !data.SpeakList[data.index].Contains("*"))
        {

            ShowTextAfterTp();

        }
        else
        {
            for (int i = 0; i < cardWidget.transform.childCount; i++)
            {
                GameObject go = cardWidget.transform.GetChild(i).gameObject;
                GameObject.Destroy(go);
            }

            if (data != null)
            {
                bool isSkillCheck = false;
                string modelName = data.modelName;
                if (data.SpeakList[data.index].Contains("*"))
                {
                    string[] sArray = data.SpeakList[data.index].Split('*');
                    modelName = sArray[1];
                    isSkillCheck = true;
                }
                GameObject card = GameObject.Instantiate(ResourcesManager.Instance.LoadEventCard(modelName));
                card.name = data.name;
                card.transform.SetParent(cardWidget, false);
                TweenPosition tp = card.AddComponent<TweenPosition>();

                if (isSkillCheck)
                {
                    card.transform.localPosition = new Vector3(0, -15f, 10f);
                    tp.from = new Vector3(0, -15f, 10.0f);
                    tp.to = new Vector3(-11.09f, 2.27f, 26.2f);
                    TweenRotation tr = card.GetComponent<TweenRotation>();
                    tr.enabled = true;
                    tr.from = new Vector3(0, 0, 0);
                    tr.to = new Vector3(0, 116, 0);
           
                    tr.ResetToBeginning();
                }

                else
                {
                    card.transform.localPosition = new Vector3(-20.0f, 2.27f, 26.2f);
                    tp.from = new Vector3(-20.0f, 2.27f, 26.2f);
                    tp.to = new Vector3(-11.09f, 2.27f, 26.2f);
                }
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.ignoreTimeScale = false;
                tp.duration = 1.0f;
                tp.onFinished.Add(new EventDelegate(ShowTextAfterTp));
                tp.ResetToBeginning();
            }
        }
     
       
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        bg_GameObject.SetActive(false);
        lastHander = null;
        speakLabel.text = "";
        speakLabel.enabled = false;
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            GameObject go = cardWidget.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }
        //if (data != null)
        //{

        //    if (data.Hander != null)
        //    {
        //        data.Hander();
        //    }
        //    data.index++;
        //    data = null;
        //}

        //if (dataList.Count > 0)
        //{
        //    if (dataList[index].Hander != null)
        //    {
        //        dataList[index].Hander();
        //    }

        //    dataIndex++;
        //    if (dataIndex >= dataList[index].SpeakList.Count)
        //    {
        //        dataIndex = 0;
        //        index++;
        //    }

        //    if (index > dataList.Count - 1)
        //    {
        //        dataList.Clear();
        //        index = 0;
        //        return;
        //    }


        //IEnumeratorManager.Instance.StartCoroutine(ListOnHideSet());
        //}
    }

    private void ShowTextAfterTp()
    {
        if (data.cout > 0)
        {
            //if (dataList[0].spriteName != eventSpriteName)
            //{
            //    eventSpriteName = dataList[0].spriteName;
            //}
            string text = data.SpeakList[data.index];
            if (text.Contains("*"))
            {
                string[] sArrary = text.Split('*');
                text = sArrary[0];
            }
            if (text.Length > 60)
            {
                
                addText = text.Substring(60, text.Length - 61);
                text = text.Substring(0, 60);
            }
            //nameLabel.text = dataList[index].name;
            speakLabel.text = text;
        }
        //else if (data != null)
        //{
        //    string text = data.SpeakList[data.index];

        //    if (text.Length > 40)
        //    {

        //        addText = text.Substring(40, text.Length - 41);
        //        text = text.Substring(0, 40);
        //    }
        //    //nameLabel.text = data.name;
        //    speakLabel.text = text;

        //}
        if(bg_GameObject.activeSelf==false )
        {
            bg_GameObject.SetActive(true);
            TweenAlpha ta = bg_GameObject.GetComponent<TweenAlpha>();
            ta.enabled = true;
            ta.ResetToBeginning();
          
        }
        writer.ResetToBeginning();
        IEnumeratorManager.Instance.StartCoroutine(SetAcitveSpeakLabel_Delay());
        curData = data;

    }




    public override void Update()
    {
        //if (tp.isActiveAndEnabled)
        //{
        //    return;
        //}

        if (EventStoryPanel.isEventSpeak)
        {
            if (TalkPanel.isSpeak ||ChosePanel.isChose)
            {
                return;
            }
            if (addText == null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (speakLabel.gameObject.activeSelf == false)
                    {
                        speakLabel.enabled = true;
                        return;
                    }
                    if (writer.isActive)
                    {
                        writer.Finish();
                
                    }

                    else if (!writer.isActive)
                    {
                        bool needHide = true;
                        StoryHander hander = null;
                        if (data.StoryHanderDic.TryGetValue(data.index, out hander))
                        {
                            if (lastHander != hander)
                            {
                                hander();
                                lastHander = hander;
                                needHide = false;
                            }
                           
                   
                        }

                        if (TalkPanel.isSpeak)
                        {
                            needHide = false;
                        }


                        if (InputPanel.IsInput)
                        {
                            needHide = false;
                        }

                        if (DicePanel.IsDice)
                        {
                            needHide = false;
                        }
             
                        //if (needHide)
                        //{                            
                        //    GUIManager.HideView("EventStoryPanel");
                        //}
                    }
                }
            }

            if (addText != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (speakLabel.gameObject.activeSelf == false)
                    {
                        speakLabel.enabled = true;
                        return;
                    }

                    if (writer.isActive)
                    {
                        writer.Finish();
                    }

                    else if (!writer.isActive)
                    {
                        speakLabel.text = addText;
                        writer.ResetToBeginning();
                        addText = null;
                    }
                }
            }
        }

    }

    IEnumerator SetAcitveSpeakLabel_Delay()
    {
        yield return new WaitForSeconds(0.4f);
        speakLabel.enabled = true;
    }


}