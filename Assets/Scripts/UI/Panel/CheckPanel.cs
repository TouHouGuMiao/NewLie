using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPanel : IView {   
    public static Skill s_Check;
    public static DiceHander hander;
    public static CheckLevel level_CheckPanel;
    private GameObject CheckCardContainer;
    private GameObject panel;
    private GameObject failedCover;
    protected override void OnDestroy()
    {
       
    }
    protected override void OnHide()
    {
        DestoryCard();
        active = false;
        active_2 = false;
        active_3 = false;
        if (hander != null) {
            hander();
        }
    }       
    protected override void OnStart()
    {
        CheckCardContainer = this.GetChild("CheckCardContainer").gameObject;
        panel= GUIManager.FindPanel("DiceCheckPanel");
        failedCover = this.GetChild("cover").gameObject;
        //Color tempColor = CheckCardContainer.transform.GetChild(2).GetChild(31).GetComponent<SpriteRenderer>().color;
        //CheckCardContainer.transform.GetChild(2).GetChild(31).GetComponent<SpriteRenderer>().color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);       
    }
    protected override void OnShow()
    {
        InitCheckCard();
    }
    public override void Update()
    {
       
    }
    private void DestoryCard() {
        for (int i = 0; i < CheckCardContainer.transform.childCount; i++) {
            GameObject go = CheckCardContainer.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }
    }
    private void InitCheckCard()
    {
        GameObject go_1 = ResourcesManager.Instance.LoadCheckCard((s_Check.data.SkillPoints / 10).ToString());
        GameObject go_2= ResourcesManager.Instance.LoadCheckCard((s_Check.data.SkillPoints % 10).ToString());
        GameObject card_1 = GameObject.Instantiate(go_1);
        GameObject card_2 = GameObject.Instantiate(go_2);      
        card_1.transform.SetParent(CheckCardContainer.transform, false);
        card_2.transform.SetParent(CheckCardContainer.transform, false);       
        card_1.transform.localPosition = new Vector3(-18, 2.14f, 20);
        card_2.transform.localPosition = new Vector3(-18, 2.14f, 20);
        card_1.transform.localRotation = Quaternion.Euler(90, 0, 0);
        card_2.transform.localRotation = Quaternion.Euler(90, 0, 0);
        TweenPosition tp_1 = card_1.GetComponent<TweenPosition>();
        tp_1.enabled = true;
        tp_1.onFinished.Clear();
        tp_1.delay = 0.1f;
        tp_1.duration = 0.7f;
        tp_1.from = tp_1.transform.localPosition;
        tp_1.to = new Vector3(-3.5f, 2.14f, -1.5f);
        tp_1.ResetToBeginning();

        TweenPosition tp_2 = card_2.GetComponent<TweenPosition>();
        tp_2.enabled = true;
        tp_2.onFinished.Clear();
        tp_2.delay = 0.2f;
        tp_2.duration = 0.7f;
        tp_2.from = tp_2.transform.localPosition;
        tp_2.to = new Vector3(-2.2f, 2.14f, -1.5f);
        tp_2.ResetToBeginning();

        TweenRotation tr_1 = card_1.GetComponent<TweenRotation>();
        tr_1.enabled = true;
        tr_1.onFinished.Clear();
        tr_1.delay = 0.1f;
        tr_1.duration = 0.7f;
        tr_1.from = tr_1.transform.localRotation.eulerAngles;
        tr_1.to=new Vector3(0,180,0);
        tr_1.ResetToBeginning();

        TweenRotation tr_2 = card_2.GetComponent<TweenRotation>();
        tr_2.enabled = true;
        tr_2.onFinished.Clear();
        tr_2.delay = 0.2f;
        tr_2.duration = 0.7f;
        tr_2.from = tr_2.transform.localRotation.eulerAngles;
        tr_2.to = new Vector3(0, 180, 0);
        tr_2.ResetToBeginning();
        tr_2.onFinished.Add(new EventDelegate(OnSkillCardFinshed));
    }
    void OnSkillCardFinshed() {  
            GameObject cardWidget = panel.transform.GetChild(2).gameObject;
        Debug.LogError(DiceCheckPanel.diceValue);
        for (int i = cardWidget.transform.childCount-1; i >= 0; i--) {
                TweenPosition tp = cardWidget.transform.GetChild(i).GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.onFinished.Clear();               
                tp.delay = 0.3f-0.1f*i;
                tp.duration = 0.7f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(2.4f - i * 1.4f, 2.14f, -1.5f);
                tp.ResetToBeginning();
            if (i <= 0)
            {
                tp.onFinished.Add(new EventDelegate(OnDiceCheckCardFinished));//OnDiceCheckCardFinished
            }
            }
    }
    void PlaySuccessedEffect() {
        AudioManager.Instance.PlayEffect_Source("success(1)");
        Debug.LogError(111);
    }
    void PlayFailedEffect() {
        AudioManager.Instance.PlayEffect_Source("gameOver");
    }
    void PlayBigSuccessedEffect() {
        AudioManager.Instance.PlayEffect_Source("");
    }
    void PlayGreatFailedEffect() {
        AudioManager.Instance.PlayEffect_Source("");
    }
    void WhileFailed() {
        if (failedCover.activeInHierarchy == false)
        {
            failedCover.SetActive(true);
        }
    }
    bool active_3 = false;
    void OnDiceCheckCardFinished() {//以后再加判断是否成功的语句
        if (level_CheckPanel == CheckLevel.normal)
        {
            if (DiceCheckPanel.diceValue <= s_Check.data.SkillPoints)
            {
                if (DiceCheckPanel.diceValue <= 5)
                {
                    BigSuccessedWhileCheck();
                }
                else
                {
                    SuccessdWhileCheck();
                }
            }
            else
            {
                if (DiceCheckPanel.diceValue >= 95)
                {
                    WhileFailed();
                    PlayGreatFailedEffect();                   
                    MakeCardFlyAway();
                }
                else
                {
                    WhileFailed();
                    PlayFailedEffect();                    
                    MakeCardFlyAway();
                }
            }
        }
        else if (level_CheckPanel == CheckLevel.difficult)
        {
            if (DiceCheckPanel.diceValue <= s_Check.data.SkillPoints / 2)
            {
                if (DiceCheckPanel.diceValue <= 5)
                {
                    BigSuccessedWhileCheck();
                }
                else
                {
                    SuccessdWhileCheck();
                }
            }
            else
            {
                if (DiceCheckPanel.diceValue >= 95)
                {
                    WhileFailed();
                    PlayGreatFailedEffect();
                    MakeCardFlyAway();
                }
                else
                {
                    WhileFailed();
                    PlayFailedEffect();
                    MakeCardFlyAway();
                }
            }
        }
        else if (level_CheckPanel == CheckLevel.SoDifficult) {
            if (DiceCheckPanel.diceValue <= s_Check.data.SkillPoints / 3) {
                if (DiceCheckPanel.diceValue <= 5)
                {
                    BigSuccessedWhileCheck();
                }
                else {
                    SuccessdWhileCheck();
                }
            }
            else
            {
                if (DiceCheckPanel.diceValue >= 95)
                {
                    WhileFailed();
                    PlayGreatFailedEffect();
                    MakeCardFlyAway();
                }
                else {
                    WhileFailed();
                    PlayFailedEffect();
                    MakeCardFlyAway();
                }
            }
        }
    }
    void SuccessdWhileCheck() {
        GameObject cardWidget = panel.transform.GetChild(2).gameObject;
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            TweenScale ts = cardWidget.transform.GetChild(i).gameObject.AddComponent<TweenScale>();
            ts.onFinished.Clear();
            ts.delay = 0.1f;
            ts.duration = 0.2f;
            ts.from = ts.transform.localScale;
            ts.to = new Vector3(1.1f, 1.1f, 1);
            if (i >= cardWidget.transform.childCount - 1)
            {
                PlaySuccessedEffect();
                ts.onFinished.Add(new EventDelegate(BackScale));//BackScale                          
            }
        }
    }
    void BigSuccessedWhileCheck() {
        GameObject cardWidget = panel.transform.GetChild(2).gameObject;
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            TweenScale ts = cardWidget.transform.GetChild(i).gameObject.AddComponent<TweenScale>();
            ts.onFinished.Clear();
            ts.delay = 0.1f;
            ts.duration = 0.2f;
            ts.from = ts.transform.localScale;
            ts.to = new Vector3(1.1f, 1.1f, 1);
            if (i >= cardWidget.transform.childCount - 1)
            {
                PlayBigSuccessedEffect();//different sounds
                ts.onFinished.Add(new EventDelegate(BackScale));                       
            }
        }
    }
    void BackScale() {//Successed
        GameObject cardWidget = panel.transform.GetChild(2).gameObject;
        GameObject go = cardWidget.transform.GetChild(0).gameObject;
        GameObject go_1 = cardWidget.transform.GetChild(1).gameObject;
        go.transform.GetChild(6).gameObject.SetActive(true);
        go_1.transform.GetChild(6).gameObject.SetActive(true);       
        Debug.LogError("111111");
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            if (active_3 == false)
            {
                TweenScale ts = cardWidget.transform.GetChild(i).GetComponent<TweenScale>();
                ts.onFinished.Clear();
                ts.enabled = true;
                ts.delay = 0.1f;
                ts.duration = 0.5f;
                ts.from = ts.transform.localScale;
                ts.to = new Vector3(1, 1, 1);
                ts.ResetToBeginning();
                if (i >= cardWidget.transform.childCount - 1)
                {
                    ts.onFinished.Add(new EventDelegate(MakeCardFlyAway));
                    active_3 = true;
                }
            }
        }      
    }
    bool active = false;
    bool active_2 = false;
    IEnumerator Pause() {
        yield return new WaitForSeconds(1);
        if (failedCover.activeInHierarchy == true) {
            failedCover.SetActive(false);
        }
        GameObject cardWidget = panel.transform.GetChild(2).gameObject;       
        for (int j = 0; j < CheckCardContainer.transform.childCount; j++)
        {
            if (active_2 == false)
            {
                TweenPosition tp = CheckCardContainer.transform.GetChild(j).GetComponent<TweenPosition>();
                TweenRotation tr = CheckCardContainer.transform.GetChild(j).GetComponent<TweenRotation>();
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.delay = 0.1f + 0.1f * j;
                tp.duration = 1.0f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(-18 - 2 * j, 2.14f, 20);
                tp.ResetToBeginning();

                tr.enabled = true;
                tr.onFinished.Clear();
                tr.delay = 0.1f + 0.1f * j;
                tr.duration = 1.0f;
                tr.from = tr.transform.localRotation.eulerAngles;
                tr.to = new Vector3(90, 0, 0);
                tr.ResetToBeginning();
                if (j >= CheckCardContainer.transform.childCount - 1)
                {
                    active_2 = true;
                }
            }
        }
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            TweenPosition tp = cardWidget.transform.GetChild(i).GetComponent<TweenPosition>();
            if (active == false)
            {                
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.delay = 0.1f + 0.1f * i;
                tp.duration = 1.0f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(20 + 2 * i, 0.85f, 6.5f);
                tp.ResetToBeginning();
                if (i >= cardWidget.transform.childCount - 1)
                {                   
                    tp.onFinished.Add(new EventDelegate(HidePanelWhileFinished));
                    active = true;
                }
            }
            
        }

    }
    void HidePanelWhileFinished() {
        GUIManager.HideView("CheckPanel");
        GUIManager.HideView("DiceCheckPanel");
    }
    void MakeCardFlyAway() {
        IEnumeratorManager.Instance.StartCoroutine(Pause());
    }
}
