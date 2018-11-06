using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStoryPanel : IView
{
    public EventStoryPanel()
    {
        m_Layer = Layer.SpeicalUI;
    }

    public static StoryData data;
    public static List<StoryData> dataList = new List<StoryData>();
    private UILabel nameLabel;
    private UILabel speakLabel;
    private TweenPosition tp;



    private TypewriterEffect writer;
    /// <summary>
    /// list中 StoryData 的标志位
    /// </summary>
    private int index = 0;
    /// <summary>
    /// list 中 StoryData中的SpeakList的标志位
    /// </summary>
    private int dataIndex = 0;


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
        writer = speakLabel.GetComponent<TypewriterEffect>();
        tp = this.GetChild("EventSprite").GetComponent<TweenPosition>();
        EventDelegate EventAffterTP = new EventDelegate(ShowTextAfterTp);
        tp.onFinished.Add(EventAffterTP);

        eventStoryPanel = GUIManager.FindPanel("EventStoryPanel");
    }



    protected override void OnShow()
    {
        tp.enabled = true;
        tp.ResetToBeginning();
    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        speakLabel.text = "";
        speakLabel.gameObject.SetActive(false);
       


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
        writer.ResetToBeginning();
        if (dataList.Count > 0)
        {
            //if (dataList[0].spriteName != eventSpriteName)
            //{
            //    eventSpriteName = dataList[0].spriteName;
            //}
            string text = dataList[index].SpeakList[dataIndex];
            if (text.Length > 60)
            {
                
                addText = text.Substring(60, text.Length - 61);
                text = text.Substring(0, 60);
            }
            //nameLabel.text = dataList[index].name;
            speakLabel.text = text;
        }
        else if (data != null)
        {
            string text = data.SpeakList[data.index];

            if (text.Length > 40)
            {

                addText = text.Substring(40, text.Length - 41);
                text = text.Substring(0, 40);
            }
            //nameLabel.text = data.name;
            speakLabel.text = text;

        }
        IEnumeratorManager.Instance.StartCoroutine(SetAcitveSpeakLabel_Delay());
    }




    public override void Update()
    {
        if (tp.isActiveAndEnabled)
        {
            return;
        }

        if (EventStoryPanel.isEventSpeak)
        {
            if (StoryPanel.isSpeak ||ChosePanel.isChose)
            {
                return;
            }
            if (addText == null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (speakLabel.gameObject.activeSelf == false)
                    {
                        speakLabel.gameObject.SetActive(true);
                        return;
                    }
                    if (writer.isActive)
                    {
                        writer.Finish();
                        //for (int i = 0; i < dataList.Count; i++)
                        //{
                        //    if (dataList[i].Hander != null)
                        //    {
                        //         dataList[i].Hander();
                        //    }
                        //}
                    }

                    else if (!writer.isActive)
                    {
                        bool needHide = true;
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            if (dataList[i].Hander != null)
                            {
                                needHide = false;
                                dataList[i].Hander();
                            }
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
                        speakLabel.gameObject.SetActive(true);
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
        yield return new WaitForSeconds(0.2f);
        speakLabel.gameObject.SetActive(true);
    }

    IEnumerator ListOnHideSet()
    {
        yield return new WaitForSeconds(0.5f);
        GUIManager.ShowView("EventStory");
    }
}