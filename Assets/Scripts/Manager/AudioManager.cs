using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource bg_Source;
    private AudioSource effect_Source;

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


    public void PlayBg_Source(string name)
    {
        AudioClip clip = ResourcesManager.Instance.LoadAudioClip(name);
        if (clip == null)
        {
            Debug.LogError("clip is null");
            return;
        }


        if (clip != null)
        {
            bg_Source.clip = clip;
            bg_Source.Play();
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
}
