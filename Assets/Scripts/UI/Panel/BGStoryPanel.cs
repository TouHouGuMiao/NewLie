using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGStoryPanel : IView
{
    private GameObject cg_0;
    private GameObject cg_1;
    private GameObject cg_2;
    private GameObject cg_3;
    private GameObject cg_4;
    private GameObject cg_5;

    public BGStoryPanel()
    {
        m_Layer = Layer.bottom;
    }

    protected override void OnStart()
    {
        cg_0 = this.GetChild("0").gameObject;
        cg_1 = this.GetChild("1").gameObject;
        cg_2 = this.GetChild("2").gameObject;
        cg_3 = this.GetChild("3").gameObject;
        cg_4 = this.GetChild("4").gameObject;
        cg_5 = this.GetChild("5").gameObject;
    }

    protected override void OnShow()
    {
        AudioManager.Instance.PlayBg_Source("BGStoryBGM");
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryOne), "BGStoryBGM", 1.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryTwo), "BGStoryBGM", 11.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryThrid), "BGStoryBGM", 22.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryFour), "BGStoryBGM", 32.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryFive), "BGStoryBGM", 42.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStorySix), "BGStoryBGM", 48.0f);
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
      
    }

    void BGStoryOne()
    {
        cg_0.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 0,8, "Knock");
    }

    void BGStoryTwo()
    {
        cg_0.SetActive(false);
        cg_1.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 1, 8, "Knock");
    }

    void BGStoryThrid()
    {
        cg_1.SetActive(false);
        cg_2.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 2, 8, "Knock");
    }

    void BGStoryFour()
    {
        cg_2.SetActive(false);
        cg_3.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 3, 8, "Knock");
    }

    void BGStoryFive()
    {
        cg_3.SetActive(false);
        cg_4.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 4, 8, "Knock");
    }

    void BGStorySix()
    {
        cg_4.SetActive(false);
        cg_5.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 5, 8, "Knock");
    }
}
