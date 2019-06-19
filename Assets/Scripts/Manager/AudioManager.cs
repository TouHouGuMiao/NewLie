using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AudioEventDelegate();

public class AudioManager:MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bg_Source;
    private AudioSource effect_Source;
    private bool isEffectOnFished = false;
    private int dicIndex = 0;
    private Dictionary<int, KeyValuePair<float, AudioEventDelegate>> audioDelegateDic = new Dictionary<int, KeyValuePair<float, AudioEventDelegate>>();
    public AudioEventDelegate OnEffectAudioPlayFinshed;
    private bool IsHnader
    {
        get
        {
            if (audioDelegateDic.Count<=0)
            {
                index = 100;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public float BgVolume
    {
        get
        {
            return bg_Source.volume;
        }

        set
        {
            bg_Source.volume = value;
        }
    }


    public float EffectVolume
    {
        get
        {
            return effect_Source.volume;
        }

        set
        {
            effect_Source.volume = value;
        }
    }


    private void Awake()
    {
        Instance = this;

        bg_Source = this.gameObject.AddComponent<AudioSource>();
        effect_Source = this.gameObject.AddComponent<AudioSource>();

        bg_Source.loop = true;
        bg_Source.playOnAwake = false;


        effect_Source.loop = false;
        effect_Source.playOnAwake = false;
    }


    public void PlayBg_Source(string name,bool isLoop)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);
        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }


        if (clip != null)
        {
            audioDelegateDic.Clear();
            dicIndex = 0;
            bg_Source.clip = clip;
            bg_Source.loop = isLoop;
            bg_Source.Play();
            bg_Source.volume = BgVolume;
        }
    }


    public void PlayBg_Source(string name, bool isLoop,float fadeTime)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);
        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }


        if (clip != null)
        {
            audioDelegateDic.Clear();
            dicIndex = 0;
            bg_Source.clip = clip;
            bg_Source.loop = isLoop;
            bg_Source.volume = 0;
            bg_Source.Play();
       
            FadeInBMG(fadeTime);
        }
    }

    public void CloseBg_Source()
    {
        bg_Source.clip = null;
    }


    public void CloseEffect_Source()
    {
        effect_Source.clip = null;
    }

    public void PlayEffect_Source_NeedLoop(string name)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);

        if (clip != null)
        {
            effect_Source.clip = clip;
            effect_Source.loop = true;
            effect_Source.Play();
        }
    }

    public void PlayEffect_Source(string name)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);

        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }

        effect_Source.PlayOneShot(clip,effect_Source.volume);
    }

    public void PlayEffect_Source(string name,AudioEventDelegate hander)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);
        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }
        if (clip != null)
        {
            effect_Source.clip = clip;
            effect_Source.loop = false;
            effect_Source.Play();
            OnEffectAudioPlayFinshed = hander;
            isEffectOnFished = true;
        }

    }



    public void PlayEffect_Source(string name,Vector3 position, float volume)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);

        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    /// <summary>
    /// 监视并给BGM绑定事件的方法，第一个参数为需要绑定的委托，第二个参数为BGM的名字(用于核对与当前播放的BGM是否匹配)，第三个参数是绑定的时间
    /// 注意：该方法为临时绑定，不是永久绑定。也就是，每次绑定的方法触发过后，会自动将委托清空。
    /// </summary>
    /// <param name="hander"></param>
    /// <param name="name"></param>
    /// <param name="time"></param>
    public void LockBGMTimeEvent(AudioEventDelegate hander,string name,float time)
    {
        if (bg_Source.clip.name != name)
        {
            Debug.LogError("bgmName has error,playerChenck,NowBGM is " + bg_Source.clip.name + "   InputBGMName is" + name);
            return;
        }
        //if (bg_Source.time > time)
        //{
        //    Debug.LogError("needTime>bgmNowTime,playCheck!");
        //    return;
        //}
        audioDelegateDic.Add(dicIndex,new KeyValuePair<float, AudioEventDelegate> (time,hander));
        dicIndex++;
    }

    public void FadeOutBGM(float fadeTime)
    {
        IEnumeratorManager.Instance.StartCoroutine(FadeOutBGM_IEnumerator(fadeTime));
    }


    public void PauseBg_Source(float fadeTime)
    {
        IEnumeratorManager.Instance.StartCoroutine(FadeOutBGM_IEnumerator_Pause(fadeTime));
    }


    public void ContinueBg_Source(float fadeTime)
    {
        FadeInBMG(fadeTime);
    }

    IEnumerator FadeOutBGM_IEnumerator(float fadeTime)
    {
        int count = (int)(fadeTime / 0.02f);
        float rate = 1.0f / count;
        float soundValue=BgVolume;
        for (int i = 0; i < count; i++)
        {
            soundValue -= rate;
            bg_Source.volume = soundValue;
            yield return new WaitForSeconds(0.02f);
        }
        
    }

    IEnumerator FadeOutBGM_IEnumerator_Pause(float fadeTime)
    {
        int count = (int)(fadeTime / 0.02f);
        float rate = 1.0f / count;
        float soundValue = BgVolume;
        for (int i = 0; i < count; i++)
        {
            soundValue -= rate;
            bg_Source.volume = soundValue;
            yield return new WaitForSeconds(0.02f);
        }
        bg_Source.Pause();

    }

    private void FadeInBMG(float fadeTime)
    {
        IEnumeratorManager.Instance.StartCoroutine(FadeInBGM_IEnumerator(fadeTime));
    }

    IEnumerator FadeInBGM_IEnumerator(float fadeTime)
    {

        int count = (int)(fadeTime / 0.02f);
        float rate = 1.0f / count;
        float soundValue = 0;
        bg_Source.volume = 0;
        bg_Source.Play();
        for (int i = 0; i < count; i++)
        {
     
            soundValue += rate;
            bg_Source.volume = soundValue;
            yield return new WaitForSeconds(0.02f);
        }
        
    }


    private int index=100;
    private void Update()
    {
        if (IsHnader)
        {
            foreach (KeyValuePair<int, KeyValuePair<float, AudioEventDelegate>> item in audioDelegateDic)
            {
                if (bg_Source.time <= (item.Value.Key + 0.02f) && (bg_Source.time >= item.Value.Key - 0.02f))
                {
                    if (index == item.Key)
                    {
                        continue;
                    }

                    item.Value.Value();
                    index = item.Key;
                }
            }
        }

        else
        {
            dicIndex = 0;
            audioDelegateDic.Clear();
        }

        if (effect_Source.clip != null)
        {
            if (isEffectOnFished)
            {
              
                if (OnEffectAudioPlayFinshed != null)
                {
                    if (effect_Source.time >= (effect_Source.clip.length-0.1f))
                    {
                        OnEffectAudioPlayFinshed();
                        isEffectOnFished = false;
                    }      
                }
            }
        }
    }
}
