using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AudioEventDelegate();

public class AudioManager:MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bg_Source;
    private AudioSource effect_Source;
    private int dicIndex = 0;
    private Dictionary<int, KeyValuePair<float, AudioEventDelegate>> audioDelegateDic = new Dictionary<int, KeyValuePair<float, AudioEventDelegate>>();
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
            
        }
    }

    public void CloseBg_Source()
    {
        bg_Source.clip = null;
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

        if (bg_Source.time > time)
        {
            Debug.LogError("needTime>bgmNowTime,playCheck!");
            return;
        }
        audioDelegateDic.Add(dicIndex,new KeyValuePair<float, AudioEventDelegate> (time,hander));
        dicIndex++;
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
    }
}
