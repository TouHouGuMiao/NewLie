using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InterludeEnum
{
    wuNv,
    kongWu,
    yukari,
}
public class OrderInterludePanel : IView
{
    private UILabel wuNvLabel;
    private TweenAlpha bg;
    private TweenAlpha xuLabelTA;
    public static InterludeEnum m_Enum= InterludeEnum.wuNv;
    protected override void OnStart()
    {
        wuNvLabel = this.GetChild("wuNvLabel").GetComponent<UILabel>();
        bg = this.GetChild("bg").GetComponent<TweenAlpha>();
        xuLabelTA = this.GetChild("XuLabel").GetComponent<TweenAlpha>();


        TweenAlpha wuNV_Ta = wuNvLabel.GetComponent<TweenAlpha>();
        wuNV_Ta.onFinished.Add(new EventDelegate(PlayCrowAudio));
    }

    protected override void OnShow()
    {
        bg.enabled = true;
        bg.from = 0;
        bg.to = 1;
        bg.onFinished.Clear();
        bg.onFinished.Add(new EventDelegate(SetXuLabelTrue));
        bg.ResetToBeginning();
        bg.gameObject.SetActive(true);
     
        if(m_Enum== InterludeEnum.wuNv)
        {

            xuLabelTA.onFinished.Clear();
            xuLabelTA.onFinished.Add(new EventDelegate(SetWuNvLabelTrue));
        }
        
     
    }

    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
        xuLabelTA.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }

    private void SetXuLabelTrue()
    {
        if(m_Enum == InterludeEnum.wuNv)
        {
            EventStateManager.Instance.WhenGoToCunZi();

        }
        AudioManager.Instance.PlayEffect_Source("start");
        xuLabelTA.enabled = true;
        xuLabelTA.ResetToBeginning();
        xuLabelTA.gameObject.SetActive(true);
    }

    private void SetWuNvLabelTrue()
    {
 
        wuNvLabel.gameObject.SetActive(true); ;

    }

    private void PlayCrowAudio()
    {
        AudioManager.Instance.PlayEffect_Source("crow");
        IEnumeratorManager.Instance.StartCoroutine(HideThisPanel());
    }


    IEnumerator HideThisPanel()
    {
        yield return new WaitForSeconds(4.5f);
        bg.enabled = true;
        bg.from = 1;
        bg.to = 0;
        bg.onFinished.Clear();
        if(m_Enum == InterludeEnum.wuNv)
        {
            bg.onFinished.Add(new EventDelegate(ShowEventWhenComeCunZi));
        }
        bg.ResetToBeginning();
    }


    void ShowEventWhenComeCunZi()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 0);
    }
}
