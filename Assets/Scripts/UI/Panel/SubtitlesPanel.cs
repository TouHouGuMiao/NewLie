using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesPanel : IView
{
    private TypewriterEffect typeWriter;
    public static string effectAudioName;
    public static int perChar;
    public static  SubtitlesData data;
    public static SubtitlePositionEnum positionEnum = SubtitlePositionEnum.bottom;
    private bool handerIsActive = false;
    public SubtitlesPanel()
    {
        m_Layer = Layer.SpeicalUI;
    }
    protected override void OnStart()
    {
        typeWriter = this.GetChild("Label").GetComponent<TypewriterEffect>();
        
    }

    protected override void OnShow()
    {
        AudioManager.Instance.PlayEffect_Source("waterAudio");
        AudioClip audioClip= ResourcesManager.Instance.LoadAudioClip(effectAudioName);
        if (audioClip != null)
        {
            typeWriter.printOneCharHader = PlayEffectAduio;
        }
        else
        {
            typeWriter.printOneCharHader = null;
        }
        typeWriter.gameObject.SetActive(false);
        typeWriter.enabled = false;
        typeWriter.ResetToBeginning();
        UILabel label = typeWriter.GetComponent<UILabel>();
        if (positionEnum== SubtitlePositionEnum.bottom)
        {
            label.transform.localPosition = new Vector3 (label.transform.localPosition.x, -375,label.transform.localPosition.z);
        }
        else if(positionEnum== SubtitlePositionEnum.top)
        {
            label.transform.localPosition = new Vector3(label.transform.localPosition.x, 409.78f, label.transform.localPosition.z);
        }

        label.text = data.SpeakList[data.Index];
        typeWriter.charsPerSecond = perChar;
        typeWriter.enabled = true;
        typeWriter.gameObject.SetActive(true);
        handerIsActive = false;
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
       
    }

    void PlayEffectAduio()
    {
        AudioManager.Instance.PlayEffect_Source(effectAudioName);
    }

    public override void Update()
    {
        if (!typeWriter.isActive)
        {
            StoryHander hander = null;
            if (data.SubtitlesDic.TryGetValue(data.Index, out hander))
            {
                if (hander != null && handerIsActive)
                {
                    hander();
                }

                handerIsActive = true;
            }
        }
       
    }


}
