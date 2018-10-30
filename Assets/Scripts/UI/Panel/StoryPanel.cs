using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPanel : IView
{
    
    public StoryPanel()
    {
        m_Layer = Layer.UI;
    }

    public static StoryData data;
    public static List<StoryData> dataList=new List<StoryData> ();
    private UILabel nameLabel;
    private UILabel speakLabel;
    private TypewriterEffect writer;
    /// <summary>
    /// list中 StoryData 的标志位
    /// </summary>
    private int index=0;
    /// <summary>
    /// list 中 StoryData中的SpeakList的标志位
    /// </summary>
    private int dataIndex = 0;


    private string addText=null;
    protected override void OnStart()
    {

        nameLabel = this.GetChild("nameLabel").GetComponent<UILabel>();
        speakLabel=this.GetChild("sperakLabel").GetComponent<UILabel>();
        writer = speakLabel.GetComponent<TypewriterEffect>();     
    }



    protected override void OnShow()
    {
        
        if (dataList.Count > 0)
        {
            string text = dataList[index].SpeakList[dataIndex];
            if (text.Length > 40)
            {

                addText = text.Substring(40, text.Length - 41);
                text = text.Substring(0, 40);
            }
            nameLabel.text = dataList[index].name;
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
            nameLabel.text = data.name;
            speakLabel.text = text;
            
        }
        speakLabel.gameObject.SetActive(true);
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        speakLabel.text = "";
        speakLabel.gameObject.SetActive(false);
        writer.ResetToBeginning();
        
  
        if (data != null)
        {
         
            if (data.Hander != null)
            {
                data.Hander();
            }
            data.index++;
            data = null;
        }

        if (dataList.Count > 0)
        {
            if (dataList[index].Hander != null)
            {
                dataList[index].Hander();
            }

            dataIndex++;
            if (dataIndex >= dataList[index].SpeakList.Count)
            {
                dataIndex = 0;
                index++;
            }

            if (index > dataList.Count-1)
            {
                dataList.Clear();
                index = 0;
                return;
            }


            IEnumeratorManager.Instance.StartCoroutine(ListOnHideSet());
        }
    }

    
   

    public override void Update()
    {

        if (StoryManager.Instacne.isSpeak)
        {

         
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
                    }

                    else if (!writer.isActive)
                    {
                        GUIManager.HideView("StoryPanel");
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


    IEnumerator ListOnHideSet()
    {
        yield return new WaitForSeconds(0.5f);
        GUIManager.ShowView("StoryPanel");
    }
}
