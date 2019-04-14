using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesPanel : IView
{
    private TypewriterEffect typeWriter;
    public static string effectAudioName;
    public static int perChar;
    public static  SubtitlesData data;

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
        label.text = data.SpeakList[data.Index];
        typeWriter.charsPerSecond = perChar;
        typeWriter.enabled = true;
        typeWriter.gameObject.SetActive(true);
    
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

 
}
