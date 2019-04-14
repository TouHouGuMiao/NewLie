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
    private GameObject cg_6;
    private GameObject cg_7;
    private GameObject cg_8;
    private GameObject cg_9;
    private GameObject cg_10;
    private GameObject cg_11;
    private GameObject cg_12;

    private TweenScale cg_12TS;

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
        cg_6 = this.GetChild("6").gameObject;
        cg_7 = this.GetChild("7").gameObject;
        cg_8 = this.GetChild("8").gameObject;
        cg_9 = this.GetChild("9").gameObject;
        cg_10 = this.GetChild("10").gameObject;
        cg_11= this.GetChild("11").gameObject;
        cg_12 = this.GetChild("12").gameObject;
        cg_12TS = cg_12.GetComponent<TweenScale>();
        TweenPosition tp_cg12 = cg_12.GetComponent<TweenPosition>();
        tp_cg12.onFinished.Add(new EventDelegate(OnLastBGTPFished));
        cg_12TS.onFinished.Add(new EventDelegate(OnLastBGTweenScaleFished));
    }

    protected override void OnShow()
    {
        AudioManager.Instance.PlayBg_Source("BGStoryBGM",false);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryOne), "BGStoryBGM", 1.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryTwo), "BGStoryBGM", 9.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryThrid), "BGStoryBGM", 18.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryFour), "BGStoryBGM", 24.0f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryFive), "BGStoryBGM", 29f);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStorySix), "BGStoryBGM", 34);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStorySeven), "BGStoryBGM", 37);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryEight), "BGStoryBGM", 40);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryNine), "BGStoryBGM", 43);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStoryTen), "BGStoryBGM", 46);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStory_11), "BGStoryBGM", 49);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStory_12), "BGStoryBGM", 52);
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(BGStory_13), "BGStoryBGM", 55);
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
      
    }

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Escape))
        {
            OnLastBGTweenScaleFished();
        }
    }

    void BGStoryOne()
    {
        cg_0.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 0,6, "Knock");
    }

    void BGStoryTwo()
    {
        cg_0.SetActive(false);
        cg_1.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 1, 5, "Knock");
    }

    void BGStoryThrid()
    {
        cg_1.SetActive(false);
        cg_2.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 2, 6, "Knock");
    }

    void BGStoryFour()
    {
        cg_2.SetActive(false);
        cg_3.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 3, 5, "Knock");
    }

    void BGStoryFive()
    {
        cg_3.SetActive(false);
        cg_4.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 4, 5, "Knock");
    }

    void BGStorySix()
    {
        cg_4.SetActive(false);
        cg_5.SetActive(true);
        SubtitlesManager.Instance.ShowSubtitle(0, 5, 4, "Knock");
    }

    void BGStorySeven()
    {
        GUIManager.HideView("SubtitlesPanel");
        cg_5.SetActive(false);
        cg_6.SetActive(true);
    }

    void BGStoryEight()
    {
        cg_6.SetActive(false);
        cg_7.SetActive(true);
    }

    void BGStoryNine()
    {
        cg_7.SetActive(false);
        cg_8.SetActive(true);
    }

    void BGStoryTen()
    {
        cg_8.SetActive(false);
        cg_9.SetActive(true);
    }

    void BGStory_11()
    {
        cg_9.SetActive(false);
        cg_10.SetActive(true);
    }

    void BGStory_12()
    {
        cg_10.SetActive(false);
        cg_11.SetActive(true);
    }

    void BGStory_13()
    {
        cg_11.SetActive(false);
        cg_12.SetActive(true);
    }

    private void OnLastBGTPFished()
    {
        cg_12TS.enabled = true;
    }

    private void OnLastBGTweenScaleFished()
    {
        AudioManager.Instance.PlayEffect_Source("waterAudio");
        AudioManager.Instance.CloseBg_Source();
        GUIManager.ShowView("CoverPanel");
        GUIManager.HideView("BGStoryPanel");
        GUIManager.HideView("SubtitlesPanel");
        GUIManager.ShowView("LoginPanel");
    }
}
