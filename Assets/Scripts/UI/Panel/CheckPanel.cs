using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPanel : IView {   
    public static Skill s_Check;
    public static DiceHander hander;
    private GameObject CheckCardContainer;
    private GameObject panel;
    protected override void OnDestroy()
    {
       
    }
    protected override void OnHide()
    {
        DestoryCard();
        if (hander != null) {
            hander();
        }
    }       
    protected override void OnStart()
    {
        CheckCardContainer = this.GetChild("CheckCardContainer").gameObject;
        panel= GUIManager.FindPanel("DiceCheckPanel");
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
        card_1.transform.localPosition = new Vector3(-11, 1, 20);
        card_2.transform.localPosition = new Vector3(-11, 1, 20);
        card_1.transform.localRotation = Quaternion.Euler(90, 0, 0);
        card_2.transform.localRotation = Quaternion.Euler(90, 0, 0);
        TweenPosition tp_1 = card_1.GetComponent<TweenPosition>();
        tp_1.enabled = true;
        tp_1.onFinished.Clear();
        tp_1.delay = 0.1f;
        tp_1.duration = 0.7f;
        tp_1.from = tp_1.transform.localPosition;
        tp_1.to = new Vector3(-6.0f, 1, 20);
        tp_1.ResetToBeginning();

        TweenPosition tp_2 = card_2.GetComponent<TweenPosition>();
        tp_2.enabled = true;
        tp_2.onFinished.Clear();
        tp_2.delay = 0.2f;
        tp_2.duration = 0.7f;
        tp_2.from = tp_2.transform.localPosition;
        tp_2.to = new Vector3(-3.4f, 1, 20);
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
            for (int i = 0; i < cardWidget.transform.childCount; i++) {
                TweenPosition tp = cardWidget.transform.GetChild(i).GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.onFinished.Clear();               
                tp.delay = 0.3f+0.1f*i;
                tp.duration = 0.7f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(1 + i * 1.7f, 0.85f, 6.5f);
                tp.ResetToBeginning();
            if (i >= cardWidget.transform.childCount - 1)
            {
                tp.onFinished.Add(new EventDelegate(OnDiceCheckCardFinished));
            }
            }
    }
    void OnDiceCheckCardFinished() {
        if (DiceCheckPanel.diceValue >= s_Check.data.SkillPoints) {
            GameObject cardWidget = panel.transform.GetChild(2).gameObject;
            for (int i = 0; i < cardWidget.transform.childCount; i++) {
                TweenPosition tp = cardWidget.transform.GetChild(i).GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.delay = 0.1f + 0.1f * i;
                tp.duration = 1.0f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(20+2*i, 0.85f, 6.5f);
                tp.ResetToBeginning();
            }
            for (int j = 0; j < CheckCardContainer.transform.childCount; j++) {
                TweenPosition tp = CheckCardContainer.transform.GetChild(j).GetComponent<TweenPosition>();
                TweenRotation tr = CheckCardContainer.transform.GetChild(j).GetComponent<TweenRotation>();
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.delay = 0.1f + 0.1f * j;
                tp.duration = 1.2f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(-12-2*j, 1, 20);
                tp.ResetToBeginning();

                tr.enabled = true;
                tr.onFinished.Clear();
                tr.delay=0.1f*0.1f*j;
                tr.duration = 1.2f;
                tr.from = tr.transform.localRotation.eulerAngles;
                tr.to = new Vector3(90, 0, 0);
                tr.ResetToBeginning();
                if (j >= CheckCardContainer.transform.childCount - 1) {
                    tr.onFinished.Add(new EventDelegate(HidePanelWhileFinished));
                }
            }
        }
    }
    void HidePanelWhileFinished() {
        GUIManager.HideView("CheckPanel");
        GUIManager.HideView("DiceCheckPanel");
    }
}
