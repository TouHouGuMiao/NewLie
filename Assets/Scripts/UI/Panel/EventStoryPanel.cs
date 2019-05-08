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
    }



    protected override void OnShow()
    {
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            GameObject go = cardWidget.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }

        if (data != null)
        {
            GameObject card = GameObject.Instantiate(ResourcesManager.Instance.LoadEventCard(data.modelName));
            card.name = data.name;
            card.transform.SetParent(cardWidget, false);
            card.transform.localPosition = new Vector3(-11.09f, 2.27f, 26.2f);
            ShowTextAfterTp();
        }
       
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
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
        writer.ResetToBeginning();
        IEnumeratorManager.Instance.StartCoroutine(SetAcitveSpeakLabel_Delay());
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
             
                        if (needHide)
                        {                            
                            GUIManager.HideView("EventStoryPanel");
                        }
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