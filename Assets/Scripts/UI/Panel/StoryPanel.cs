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
    private UILabel nameLabel;
    private UILabel speakLabel;
    private TypewriterEffect writer;

    private string addText=null;
    protected override void OnStart()
    {
        nameLabel = this.GetChild("nameLabel").GetComponent<UILabel>();
        speakLabel=this.GetChild("sperakLabel").GetComponent<UILabel>();
        writer = speakLabel.GetComponent<TypewriterEffect>();
    }

    protected override void OnShow()
    {
       
        string text = data.SpeakList[data.index];
    
        if (text.Length > 40)
        {

            addText = text.Substring(40,text.Length-41 );
            text = text.Substring(0, 40);
        }
        nameLabel.text = data.name;
        speakLabel.text = text;
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        speakLabel.gameObject.SetActive(false);
        writer.ResetToBeginning();
        if (data.index >= data.cout - 1)
        {
            return;
        }
        data.index++;
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
}
