using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatSureState
{
    Idle_State=0,
    Stature_State = 1,
    Stature_State_Dice1=2,
    Stature_State_Dice2=3,
    Stature_State_Reslut=4,
    Power_State=5,
    Power_State_Dice1=6,
    Power_State_Dice2=7,
    Power_State_Dice3=8,
    Power_State_Reslut = 9,
    IQ_State = 10,
    IQ_State_Dice1 = 11,
    IQ_State_Dice2 = 12,
    IQ_State_Dice3 = 13,
    IQ_State_Reslut = 14,
    WeiYan_State = 15,
    WeiYan_State_Dice1 = 16,
    WeiYan_State_Dice2 = 17,
    WeiYan_State_Dice3 = 29,
    WeiYan_State_Reslut = 18,
    Lucky_State = 19,
    Lucky_State_Dice1 = 20,
    Lucky_State_Dice2 = 21,
    Lucky_State_Dice3 =22,
    Lucky_State_Reslut = 23,
    VIT_State = 24,
    VIT_State_Dice1 = 25,
    VIT_State_Dice2 = 26,
    VIT_State_Dice3 = 27,
    VIT_State_Reslut = 28,
}

public class SurePropertyPanel : IView {
    private UIButton startBtn;
    private Transform Stature;
    private Transform power;
    private Transform Container;
    /// <summary>
    /// 初始值大于9，以保重不与9张牌冲突
    /// </summary>
    public static int dice1=10;
    public static int dice2 =10;
    public static int dice3 = 10;
    public static CreatSureState SureState;

    public static int stature_Prop;
    public static int power_Prop;
    public static int VIT_Prop;
    public static int IQ_Prop;
    public static int lucky_Prop;
    public static int weiYan_Prop;

    private GameObject statureCardExpress;
    private UILabel statureDes;
    private UILabel statureNumber;
    private static GameObject cardWidget;
    private Transform expressWidget;
    private Transform playerPropResultWidget;

    private static UILabel fristDice;
    private static UILabel secondDice;
    private static  UILabel thridDice;

    private GameObject labelWidget;
    private Transform statureLabel;
    private Transform powerLabel;
    private Transform VITLabel;
    private Transform IQLabel;
    private Transform luckyLabel;
    private Transform weiYanLabel;
    private static GameObject statureCard;
    private Transform propNameWidget;

    private static  int statureIndex=0;

    public SurePropertyPanel()
    {
        m_Layer = Layer.bottom;
    }

    protected override void OnStart()
    {
        //Container = this.GetChild("Container");
        //Stature = this.GetChild("Stature");
        //stature_FristDice = Stature.Find("fristDice").GetComponent<UILabel>();
        //stature_SecondDice= Stature.Find("secondDice").GetComponent<UILabel>();

        //statureCard = Stature.Find("statureCard").gameObject;
        //statureCardExpress = Stature.Find("statureCardExpress").gameObject;
        //statureDes = statureCardExpress.transform.Find("Des").GetComponent<UILabel>();
        //statureNumber = statureCardExpress.transform.Find("Number").GetComponent<UILabel>();
        //stature_NextBtn = Stature.Find("nextBtn").GetComponent<UIButton>();
        //stature_NextBtn.onClick.Add(new EventDelegate(OnStatureNextBtnClick));

        //power = this.GetChild("Power");
        //power_FristDice = power.Find("fristDice").GetComponent<UILabel>();
        //power_SecondDice = power.Find("secondDice").GetComponent<UILabel>();
        //power_ThridDice = power.Find("thDice").GetComponent<UILabel>();
        //powerCardExpress = power.Find("powerCardExpress").gameObject;
        //powerCard = power.Find("powerCard").gameObject;
        //power_NextBtn = power.Find("nextBtn").GetComponent<UIButton>();
        //powerNumber = powerCardExpress.transform.Find("Number").GetComponent<UILabel>();

        expressWidget = this.GetChild("ExpressWidget");
        UIButton returnBtn = expressWidget.transform.FindRecursively("returnBtn").GetComponent<UIButton>();
        returnBtn.onClick.Add(new EventDelegate(ExpressReturnBtnClick));
        cardWidget = this.GetChild("CardWidget").gameObject;
        startBtn = this.GetChild("StartBtn").GetComponent<UIButton>();
        startBtn.onClick.Add(new EventDelegate(CardTweenPositionInit));

        playerPropResultWidget = this.GetChild("PlayerPropResultWidget");

        labelWidget = this.GetChild("LabelWidget").gameObject;
        statureLabel = labelWidget.transform.Find("StatureLabel");
        UIButton statureReturnBtn = statureLabel.transform.Find("returnBtn").GetComponent<UIButton>();
        statureReturnBtn.onClick.Add(new EventDelegate(LabelWidget_ReturnBtn));
        powerLabel = labelWidget.transform.Find("PowerLabel");
        UIButton powerReturnBtn = powerLabel.transform.Find("returnBtn").GetComponent<UIButton>();
        powerReturnBtn.onClick.Add(new EventDelegate(LabelWidget_ReturnBtn));
        VITLabel = labelWidget.transform.Find("VITLabel");
        UIButton VITReturnBtn = VITLabel.transform.Find("returnBtn").GetComponent<UIButton>();
        VITReturnBtn.onClick.Add(new EventDelegate(LabelWidget_ReturnBtn));
        IQLabel = labelWidget.transform.Find("IQLabel");
        UIButton IQReturnBtn = IQLabel.transform.Find("returnBtn").GetComponent<UIButton>();
        IQReturnBtn.onClick.Add(new EventDelegate(LabelWidget_ReturnBtn));
        luckyLabel = labelWidget.transform.transform.Find("LuckyLabel");
        UIButton luckyReturnBtn = luckyLabel.transform.Find("returnBtn").GetComponent<UIButton>();
        luckyReturnBtn.onClick.Add(new EventDelegate(LabelWidget_ReturnBtn));
        weiYanLabel = labelWidget.transform.Find("WeiYanLabel");
        UIButton weiYanReturnBtn = weiYanLabel.transform.Find("returnBtn").GetComponent<UIButton>();
        weiYanReturnBtn.onClick.Add(new EventDelegate(LabelWidget_ReturnBtn));
        propNameWidget = this.GetChild("PropNameWidget");

        UIButton statureBtn = statureLabel.transform.Find("statureButton").GetComponent<UIButton>();
        UIButton powerBtn = powerLabel.transform.Find("powerButton").GetComponent<UIButton>();
        UIButton VITBtn =  VITLabel.transform.Find("VITButton").GetComponent<UIButton>();
        UIButton IQBtn = IQLabel.transform.Find("IQButton").GetComponent<UIButton>();
        UIButton luckyBtn = luckyLabel.transform.Find("luckyButton").GetComponent<UIButton>();
        UIButton weiYanBtn = weiYanLabel.transform.Find("weiYanButton").GetComponent<UIButton>();
        fristDice = this.GetChild("fristDice").gameObject.GetComponent<UILabel>();
        secondDice = this.GetChild("secondDice").gameObject.GetComponent<UILabel>();
        thridDice = this.GetChild("thridDice").gameObject.GetComponent<UILabel>();
        Container = this.GetChild("Container");
        statureBtn.onClick.Add(new EventDelegate(OnPropLabelWidgetBtnClick));
        powerBtn.onClick.Add(new EventDelegate(OnPropLabelWidgetBtnClick));
        VITBtn.onClick.Add(new EventDelegate(OnPropLabelWidgetBtnClick));
        IQBtn.onClick.Add(new EventDelegate(OnPropLabelWidgetBtnClick));
        luckyBtn.onClick.Add(new EventDelegate(OnPropLabelWidgetBtnClick));
        weiYanBtn.onClick.Add(new EventDelegate(OnPropLabelWidgetBtnClick));
        statureCard = cardWidget.transform.Find("StatureCard").gameObject;
    }
     
    protected override void OnShow()
    {
        SureState = CreatSureState.Idle_State;
    }

    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
        dice1 = 10;
        dice2 = 10;
        dice3 = 10;
    }

    public override void Update()
    {
        if(SureState== CreatSureState.Stature_State_Reslut||SureState==CreatSureState.Power_State_Reslut||SureState== CreatSureState.VIT_State_Reslut|| SureState == CreatSureState.IQ_State_Reslut|| SureState == CreatSureState.Lucky_State_Reslut||SureState== CreatSureState.WeiYan_State_Reslut)
        {
            if (!expressWidget.gameObject.activeSelf)
            {
                ShowPropCheckResult();
            }
           
        }
    }

    void LabelWidget_ReturnBtn()
    {
        ExpressReturnBtnClick();
        labelWidget.SetActive(false);
        for (int i = 0; i < labelWidget.transform.childCount; i++)
        {
            GameObject go = labelWidget.transform.GetChild(i).gameObject;
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }
    }


    void ShowPropCheckResult()
    {
        Transform expressWidget = this.GetChild("ExpressWidget");
        UISprite sprite = expressWidget.Find("Sprite").GetComponent<UISprite>();
        UILabel label = sprite.transform.Find("Label").GetComponent<UILabel>();
        CameraManager.Instance.DrawUICameraLens(Vector3.zero, 0.7f, 0.08f);
   
  
        if (SureState == CreatSureState.Stature_State_Reslut)
        {
            int propResult = (dice1 + dice2 + 6) * 5;
            stature_Prop = propResult;
            if (propResult <= 50)
            {
                sprite.spriteName = "Small";
                label.text = "瘦小";
            }

            else if (propResult <= 80 && propResult > 50)
            {
                sprite.spriteName = "Normal";
                label.text = "正常";
            }

            else if (propResult > 80)
            {
                sprite.spriteName = "Strong";
                label.text = "强壮";
            }
 
        }

        else if(SureState == CreatSureState.Power_State_Reslut)
        {
            int propResult = (dice1 + dice2 + dice3) * 5;
            power_Prop = propResult;
            if (propResult <= 50)
            {
                sprite.spriteName = "Small";
                label.text = "微弱";
            }

            else if (propResult <= 80 && propResult > 50)
            {
                sprite.spriteName = "Normal";
                label.text = "正常";
            }

            else if (propResult > 80)
            {
                sprite.spriteName = "Strong";
                label.text = "灵压";
            }
        }

        else if (SureState == CreatSureState.VIT_State_Reslut)
        {
            int propResult = (dice1 + dice2 + dice3) * 5;
            VIT_Prop = propResult;
            if (propResult <= 50)
            {
                sprite.spriteName = "Small";
                label.text = "病弱";
            }

            else if (propResult <= 80 && propResult > 50)
            {
                sprite.spriteName = "Normal";
                label.text = "正常";
            }

            else if (propResult > 80)
            {
                sprite.spriteName = "Strong";
                label.text = "强健体魄";
            }
        }

        else if (SureState == CreatSureState.IQ_State_Reslut)
        {
            int propResult = (dice1 + dice2 + dice3) * 5;
            IQ_Prop = propResult;
            if (propResult <= 50)
            {
                sprite.spriteName = "Small";
                label.text = "愚者";
            }

            else if (propResult <= 80 && propResult > 50)
            {
                sprite.spriteName = "Normal";
                label.text = "正常";
            }

            else if (propResult > 80)
            {
                sprite.spriteName = "Strong";
                label.text = "智者";
            }
        }

        else if (SureState == CreatSureState.Lucky_State_Reslut)
        {
            int propResult = (dice1 + dice2 + dice3) * 5;
            lucky_Prop = propResult;
            if (propResult <= 50)
            {
                sprite.spriteName = "Small";
                label.text = "倒霉";
            }

            else if (propResult <= 80 && propResult > 50)
            {
                sprite.spriteName = "Normal";
                label.text = "正常";
            }

            else if (propResult > 80)
            {
                sprite.spriteName = "Strong";
                label.text = "幸运";
            }
        }

        else if (SureState == CreatSureState.WeiYan_State_Reslut)
        {
            int propResult = (dice1 + dice2 + dice3) * 5;
            weiYan_Prop = propResult;
            if (propResult <= 50)
            {
                sprite.spriteName = "Small";
                label.text = "贫弱";
            }

            else if (propResult <= 80 && propResult > 50)
            {
                sprite.spriteName = "Normal";
                label.text = "正常";
            }

            else if (propResult > 80)
            {
                sprite.spriteName = "Strong";
                label.text = "威严满满";
            }
        }
        expressWidget.gameObject.SetActive(true);
    }

    void ExpressReturnBtnClick()
    {
        SureState = CreatSureState.Idle_State;
      
        expressWidget.gameObject.SetActive(false);

        CameraManager.Instance.DrawUICameraLens(Vector3.zero, 1, 0.08f);
        Container.gameObject.SetActive(false);
        CardTweenPositionInit();
    }

    void ShowStaturePropExpress()
    {
        statureNumber.text = stature_Prop.ToString();

        int height;
        int weight;
        string des="";
        if (stature_Prop <= 50)
        {
            height =(int)(0.8f * stature_Prop + 90);
            weight = (int)(1.6f * stature_Prop - 40);
            des = "不得不承认，你的体型已经完全与你的年龄非常的不符," + height.ToString()+"的身高，放眼整个幻想乡，大概只有红魔馆的那对吸血鬼姐妹与你相似(或许她们比你还要高出一些)。" +
                weight.ToString()+"斤的体重，使得你看起来如此的弱不禁风。你会很轻易的被身边的人当做“小孩子”来看待，这对你树立巫女的“威严”可不是一件好事，" +
                "但小巧的体型也使得你可以快速的融入人群中，当你想追查某些事件的时候，说不定会起到意想不到的效果。";
        }

        else if (stature_Prop > 50 && stature_Prop <= 80)
        {
            height = 1 * stature_Prop + 90;
            weight = (int)(2.0f * stature_Prop - 40);
            des = "你拥有着正常人的体型," + height.ToString() + "的身高，在幻想乡中不高不矮。" + weight.ToString() + "斤的体重，标标准准。你可能会为自己如此“平凡”的" +
                "“体型”而苦恼，其实这并没有什么好苦恼的，极端只能带来极端，而有时平平淡淡才是真。";
        }

        else if(stature_Prop > 80 && stature_Prop <= 90)
        {
            height = (int)(1.1f * stature_Prop + 95);
            weight = (int)(2.5f * stature_Prop - 30);
            des = ".......我也不是很清楚你究竟如何才能长成这幅模样....." + height.ToString() + "的身高,幻想乡中鲜有人类甚至妖怪能达到这样的身高，人里大部分的门你可能需要弯着腰才能通过。" +
                weight.ToString()+"斤的体重........这实在不应该是一个巫女应该拥有的体格，真要用一个词来形容你的话，“熊”应该是最适合的。与此同时，你的体格使得人们在与你交谈的时候" +
                "会更加小心翼翼，生怕惹你生气，毕竟谁也不想和一头熊对打。 但庞大的身体同时使得你是那么的显眼，你若想融入人群，恐怕会很是困难...";
        }
        statureDes.text= des;
        statureCardExpress.SetActive(true);
    }

    void OnStatureNextBtnClick()
    {
        Stature.gameObject.SetActive(false);
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(1, 7);
        dice1 = 10;
        dice2 = 10;
        dice3 = 10;
    }

    void CardTweenPositionInit()
    {
        startBtn.gameObject.SetActive(false);
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            GameObject go = cardWidget.transform.GetChild(i).gameObject;
            UIButton btn = go.GetComponent<UIButton>();
            btn.enabled = false;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.onFinished.Clear();
            tp.delay = i * 0.3f;
            tp.duration = 0.5f;
            tp.from = go.transform.localPosition;
            tp.to = new Vector3(10.92f, i * 0.1f + 12.37f, 80.1f);
            tp.ResetToBeginning();
            TweenRotation tr = go.GetComponent<TweenRotation>();
            tr.enabled = true;
            tr.delay = i * 0.3f;
            tr.duration = 0.5f;
            tr.onFinished.Clear();
            tr.from = go.transform.rotation.eulerAngles;
            tr.to = new Vector3(80, 0, 0);
            tr.ResetToBeginning();   
            if (i >= cardWidget.transform.childCount-1)
            {
                tp.onFinished.Add(new EventDelegate(CardTweenPositionToItsPosition));
                isCardTweenToPos = false;
            }
            else
            {
                tp.onFinished.Add(new EventDelegate(PlayBoomAudio));
            }
            tp.ResetToBeginning();
        }
        cardWidget.SetActive(true);
    }

    void PlayBoomAudio()
    {
        AudioManager.Instance.PlayEffect_Source("cardMove");
    }
    bool isCardTweenToPos=false;
    void CardTweenPositionToItsPosition()
    {
        int cout = cardWidget.transform.childCount-1;
        AudioManager.Instance.PlayEffect_Source("cardMove");
        if (IQ_Prop != 0 && stature_Prop != 0 && power_Prop != 0 && VIT_Prop != 0 && weiYan_Prop != 0 && lucky_Prop != 0)
        {
            cardWidget.gameObject.SetActive(false);
            playerPropResultWidget.gameObject.SetActive(true);
        }
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            if (isCardTweenToPos)
            {
                return;
            }
            GameObject go = cardWidget.transform.GetChild(cout).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.onFinished.Clear();
            tp.from = go.transform.localPosition;
            tp.to = new Vector3(i * 1.6f, 0, 0.1f);
            tp.delay = 0.3f * i+1f;
            tp.duration = 1.0f;
            if (i == cardWidget.transform.childCount - 1)
            {
                tp.onFinished.Add(new EventDelegate(OnCardMoveToScreenFished));
            }

            tp.ResetToBeginning();
            TweenRotation tr = go.GetComponent<TweenRotation>();
            tr.enabled = true;
            tr.from = go.transform.rotation.eulerAngles;
            tr.to = new Vector3(0, 180, 0);
            tr.delay = i * 0.3f+1;
            tr.duration = 1.0f;
            tr.ResetToBeginning();
            cout--;
        }

        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            GameObject go = cardWidget.transform.GetChild(i).gameObject;
            UIButton button = go.GetComponent<UIButton>();
            button.onClick.Add(new EventDelegate(OnCardBtnClick));
        }
        isCardTweenToPos = true;
    }

    void OnCardMoveToScreenFished()
    {
        for (int i = 0; i < cardWidget.transform.childCount; i++)
        {
            GameObject go = cardWidget.transform.GetChild(i).gameObject;
            if (go.name.Contains("Stature"))
            {
                if (stature_Prop != 0)
                {
                    UIWidget widget = propNameWidget.Find("statureLabel").GetComponent<UIWidget>();
                    widget.alpha = 0.4f;
                    continue;
                }
            }

            else if (go.name.Contains("Power"))
            {
                if (power_Prop != 0)
                {
                    UIWidget widget = propNameWidget.Find("powerLabel").GetComponent<UIWidget>();
                    widget.alpha = 0.4f;
                    continue;
                }
            }

            else if (go.name.Contains("VIT"))
            {
                if (VIT_Prop != 0)
                {
                    UIWidget widget = propNameWidget.Find("VITLabel").GetComponent<UIWidget>();
                    widget.alpha = 0.4f;
                    continue;
                }
            }

            else if (go.name.Contains("Lucky"))
            {
                if (lucky_Prop != 0)
                {
                    UIWidget widget = propNameWidget.Find("luckyLabel").GetComponent<UIWidget>();
                    widget.alpha = 0.4f;
                    continue;
                }
            }

            else if (go.name.Contains("IQ"))
            {
                if (IQ_Prop != 0)
                {
                    UIWidget widget = propNameWidget.Find("IQLabel").GetComponent<UIWidget>();
                    widget.alpha = 0.4f;
                    continue;
                }
            }

            else if (go.name.Contains("WeiYan"))
            {
                if (weiYan_Prop != 0)
                {
                    UIWidget widget = propNameWidget.Find("weiYanLabel").GetComponent<UIWidget>();
                    widget.alpha = 0.4f;
                    continue;
                }
            }
            UIButton btn = go.GetComponent<UIButton>();
            btn.enabled = true;
        }
        TweenAlpha ta = propNameWidget.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.ResetToBeginning();
        propNameWidget.gameObject.SetActive(true);
    }

    void OnCardBtnClick()
    {
        if(SureState== CreatSureState.Idle_State)
        {
            UIButton button = UIButton.current;
            int temp_j = 0;
            for (int i = 0; i < cardWidget.transform.childCount; i++)
            {
                GameObject go = cardWidget.transform.GetChild(i).gameObject;
                UIButton btn = go.GetComponent<UIButton>();
                btn.enabled = false;
                if (go.name == button.gameObject.name)
                {
                    TweenPosition tp = go.GetComponent<TweenPosition>();
                    tp.onFinished.Clear();
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(1.86f, 0, -1.11f);
                    tp.duration = 1f;
                    tp.delay = 0;
                    tp.ResetToBeginning();
                    tp.onFinished.Add(new EventDelegate(OnCardTweenPosisitonFished));
                    TweenRotation ts = go.GetComponent<TweenRotation>();
                    ts.enabled = true;
                    ts.from = go.transform.rotation.eulerAngles;
                    ts.to = new Vector3(0, 130, 0);
                    ts.onFinished.Clear();
                    ts.delay = 0;
                    ts.duration = 1f;
                    ts.ResetToBeginning();
                    continue;
                }
                else
                {
                    TweenPosition tp = go.GetComponent<TweenPosition>();
                    tp.enabled = true;
                    tp.onFinished.Clear();
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y + 20, go.transform.localPosition.z);
                    tp.duration = 0.5f;
                    //tp.onFinished.Add(new EventDelegate(OnCardTweenPosisitonFished));
                    tp.delay = 0.15f * temp_j + 0.15f;
                    tp.ResetToBeginning();
                }
                temp_j++;
            }
        }
        propNameWidget.gameObject.SetActive(false);
        //if (button.gameObject.name.Contains("Stature"))
        //{
        //    TweenScale ts = statureLabel.GetComponent<TweenScale>();
        //    ts.enabled = true;
        //    statureLabel.gameObject.SetActive(true);
        //    ts.ResetToBeginning();
        //}
    }
    
    void OnCardTweenPosisitonFished()
    {
        TweenPosition tp = (TweenPosition)TweenPosition.current;
        labelWidget.SetActive(true);
        if (tp.gameObject.name.Contains("Stature"))
        {
            TweenScale ts = statureLabel.GetComponent<TweenScale>();
            ts.enabled = true;
            statureLabel.gameObject.SetActive(true);
            ts.ResetToBeginning();
        }

        else if (tp.gameObject.name.Contains("Power"))
        {
            TweenScale ts = powerLabel.GetComponent<TweenScale>();
            ts.enabled = true;
            powerLabel.gameObject.SetActive(true);
            ts.ResetToBeginning();
        }

        else if (tp.gameObject.name.Contains("VIT"))
        {
            TweenScale ts = VITLabel.GetComponent<TweenScale>();
            ts.enabled = true;
            VITLabel.gameObject.SetActive(true);
            ts.ResetToBeginning();
        }
        else if (tp.gameObject.name.Contains("IQ"))
        {
            TweenScale ts = IQLabel.GetComponent<TweenScale>();
            ts.enabled = true;
            IQLabel.gameObject.SetActive(true);
            ts.ResetToBeginning();
        }

        else if (tp.gameObject.name.Contains("Lucky"))
        {
            TweenScale ts = luckyLabel.GetComponent<TweenScale>();
            ts.enabled = true;
            luckyLabel.gameObject.SetActive(true);
            ts.ResetToBeginning();
        }


        else if (tp.gameObject.name.Contains("WeiYan"))
        {
            TweenScale ts = weiYanLabel.GetComponent<TweenScale>();
            ts.enabled = true;
            weiYanLabel.gameObject.SetActive(true);
            ts.ResetToBeginning();
        }
    }



    void OnPropLabelWidgetBtnClick()
    {
        UIButton button = UIButton.current;
        fristDice.text = "?";
        secondDice.text = "?";
        if (button.name.Contains("stature"))
        {
            SureState = CreatSureState.Stature_State;
            GameObject statureCard = cardWidget.transform.Find("StatureCard").gameObject;
            UIButton btn = statureCard.GetComponent<UIButton>();
            btn.enabled = true;
            TweenPosition tp = statureCard.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = tp.gameObject.transform.localPosition;
            tp.to = new Vector3(-1.69f, 1.42f, 3.76f);
            tp.delay = 0;
            tp.onFinished.Clear();
            tp.duration = 1.0f;
            tp.ResetToBeginning();
            TweenRotation ts = statureCard.GetComponent<TweenRotation>();
            ts.enabled = true;
            ts.from = ts.transform.rotation.eulerAngles;
            ts.to = new Vector3(0, 140, 0);
            ts.delay = 0;
            ts.duration = 1.0f;
            ts.ResetToBeginning();
            Container.gameObject.SetActive(true);
            SubtitlesManager.Instance.ShowSubtitle(1, 0, 2);
            statureLabel.gameObject.SetActive(false);
            thridDice.text ="6";
            dice3 = 6;
        }

        else if (button.name.Contains("power"))
        {
            SureState = CreatSureState.Power_State;
            GameObject powerCard = cardWidget.transform.Find("PowerCard").gameObject;
            TweenPosition tp = powerCard.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = tp.gameObject.transform.localPosition;
            tp.to = new Vector3(-1.69f, 1.42f, 3.76f);
            tp.delay = 0;
            tp.onFinished.Clear();
            tp.duration = 1.0f;
            tp.ResetToBeginning();
            TweenRotation ts = powerCard.GetComponent<TweenRotation>();
            ts.enabled = true;
            ts.from = ts.transform.rotation.eulerAngles;
            ts.to = new Vector3(0, 140, 0);
            ts.delay = 0;
            ts.duration = 1.0f;
            ts.ResetToBeginning();
            Container.gameObject.SetActive(true);
            powerLabel.gameObject.SetActive(false);
            UIButton btn = powerCard.GetComponent<UIButton>();
            btn.onClick.Clear();
            btn.onClick.Add(new EventDelegate(OnPowerCardBtnClick));
            btn.enabled = true;
            thridDice.text = "?";
            dice3 = 10;
        }

        else if (button.name.Contains("VIT"))
        {
            SureState = CreatSureState.VIT_State;
            GameObject VITCard = cardWidget.transform.Find("VITCard").gameObject;
            TweenPosition tp = VITCard.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = tp.gameObject.transform.localPosition;
            tp.to = new Vector3(-1.69f, 1.42f, 3.76f);
            tp.delay = 0;
            tp.onFinished.Clear();
            tp.duration = 1.0f;
            tp.ResetToBeginning();
            TweenRotation ts = VITCard.GetComponent<TweenRotation>();
            ts.enabled = true;
            ts.from = ts.transform.rotation.eulerAngles;
            ts.to = new Vector3(0, 140, 0);
            ts.delay = 0;
            ts.duration = 1.0f;
            ts.ResetToBeginning();
            Container.gameObject.SetActive(true);
            VITLabel.gameObject.SetActive(false);
            UIButton btn = VITCard.GetComponent<UIButton>();
            btn.onClick.Clear();
            btn.onClick.Add(new EventDelegate(OnVITCardBtnClick));
            btn.enabled = true;
            thridDice.text = "?";
            dice3 = 10;
        }


        else if (button.name.Contains("IQ"))
        {
            SureState = CreatSureState.IQ_State;
            GameObject IQCard = cardWidget.transform.Find("IQCard").gameObject;
            TweenPosition tp = IQCard.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = tp.gameObject.transform.localPosition;
            tp.to = new Vector3(-1.69f, 1.42f, 3.76f);
            tp.delay = 0;
            tp.onFinished.Clear();
            tp.duration = 1.0f;
            tp.ResetToBeginning();
            TweenRotation ts = IQCard.GetComponent<TweenRotation>();
            ts.enabled = true;
            ts.from = ts.transform.rotation.eulerAngles;
            ts.to = new Vector3(0, 140, 0);
            ts.delay = 0;
            ts.duration = 1.0f;
            ts.ResetToBeginning();
            Container.gameObject.SetActive(true);
            IQLabel.gameObject.SetActive(false);
            UIButton btn = IQCard.GetComponent<UIButton>();
            btn.onClick.Clear();
            btn.onClick.Add(new EventDelegate(OnIQCardBtnClick));
            btn.enabled = true;
            thridDice.text = "?";
            dice3 = 10;
        }

        else if (button.name.Contains("lucky"))
        {
            SureState = CreatSureState.Lucky_State;
            GameObject luckyCard = cardWidget.transform.Find("LuckyCard").gameObject;
            TweenPosition tp = luckyCard.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = tp.gameObject.transform.localPosition;
            tp.to = new Vector3(-1.69f, 1.42f, 3.76f);
            tp.delay = 0;
            tp.onFinished.Clear();
            tp.duration = 1.0f;
            tp.ResetToBeginning();
            TweenRotation ts = luckyCard.GetComponent<TweenRotation>();
            ts.enabled = true;
            ts.from = ts.transform.rotation.eulerAngles;
            ts.to = new Vector3(0, 140, 0);
            ts.delay = 0;
            ts.duration = 1.0f;
            ts.ResetToBeginning();
            Container.gameObject.SetActive(true);
            luckyLabel.gameObject.SetActive(false);
            UIButton btn = luckyCard.GetComponent<UIButton>();
            btn.onClick.Clear();
            btn.onClick.Add(new EventDelegate(OnLuckyCardBtnClick));
            btn.enabled = true;
            thridDice.text = "?";
            dice3 = 10;
        }

        else if (button.name.Contains("weiYan"))
        {
            SureState = CreatSureState.WeiYan_State;
            GameObject weiYanCard = cardWidget.transform.Find("WeiYanCard").gameObject;
            TweenPosition tp = weiYanCard.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = tp.gameObject.transform.localPosition;
            tp.to = new Vector3(-1.69f, 1.42f, 3.76f);
            tp.delay = 0;
            tp.onFinished.Clear();
            tp.duration = 1.0f;
            tp.ResetToBeginning();
            TweenRotation ts = weiYanCard.GetComponent<TweenRotation>();
            ts.enabled = true;
            ts.from = ts.transform.rotation.eulerAngles;
            ts.to = new Vector3(0, 140, 0);
            ts.delay = 0;
            ts.duration = 1.0f;
            ts.ResetToBeginning();
            Container.gameObject.SetActive(true);
            weiYanLabel.gameObject.SetActive(false);
            UIButton btn = weiYanCard.GetComponent<UIButton>();
            btn.onClick.Clear();
            btn.onClick.Add(new EventDelegate(OnWeiYanCardBtnClick));
            btn.enabled = true;
            thridDice.text = "?";
            dice3 = 10;
        }
    }

    public static void StatureCardBtnClick_ToSubtitles_0()
    {
        SureState = CreatSureState.Stature_State_Dice1;
        UIButton btn = statureCard.GetComponent<UIButton>();
        btn.onClick.Clear();
        btn.onClick.Add(new EventDelegate(OnStatureCardBtnClick));
    }
    #region 卡牌按钮与字幕相关方法
    private int powerIndex = 0;
    void OnPowerCardBtnClick()
    {
        UIButton btn = cardWidget.transform.Find("PowerCard").GetComponent<UIButton>();
        powerIndex++;
        if (powerIndex == 1)
        {
            SureState = CreatSureState.Power_State_Dice1;
            SubtitlesManager.Instance.ShowSubtitle(1, 4, 2, "Knock",SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (powerIndex == 2)
        {
            SureState = CreatSureState.Power_State_Dice2;
            SubtitlesManager.Instance.ShowSubtitle(1, 7, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (powerIndex == 3)
        {
            SureState = CreatSureState.Power_State_Dice3;
            SubtitlesManager.Instance.ShowSubtitle(1, 10, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else
        {
            return;
        }
        DiceManager.Instance.ShowDicePanel(6, 0.01f, new DiceHander(SurePowerDice));
    }
    private int VITIndex = 0;
    void OnVITCardBtnClick()
    {
        UIButton btn = cardWidget.transform.Find("VITCard").GetComponent<UIButton>();
        VITIndex++;
        if (VITIndex == 1)
        {
            SureState = CreatSureState.VIT_State_Dice1;
            SubtitlesManager.Instance.ShowSubtitle(1, 14, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (VITIndex == 2)
        {
            SureState = CreatSureState.VIT_State_Dice2;
            SubtitlesManager.Instance.ShowSubtitle(1, 16, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (VITIndex == 3)
        {
            SureState = CreatSureState.VIT_State_Dice3;
            SubtitlesManager.Instance.ShowSubtitle(1, 18, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else
        {
            return;
        }
        DiceManager.Instance.ShowDicePanel(6, 0.01f, new DiceHander(SureVITDice));
    }

    private int IQIndex = 0;
    void OnIQCardBtnClick()
    {
        UIButton btn = cardWidget.transform.Find("IQCard").GetComponent<UIButton>();
        IQIndex++;
        if (IQIndex == 1)
        {
            SureState = CreatSureState.IQ_State_Dice1;
            SubtitlesManager.Instance.ShowSubtitle(1, 20, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (IQIndex == 2)
        {
            SureState = CreatSureState.IQ_State_Dice2;
            SubtitlesManager.Instance.ShowSubtitle(1, 23, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (IQIndex == 3)
        {
            SureState = CreatSureState.IQ_State_Dice3;
            SubtitlesManager.Instance.ShowSubtitle(1, 25, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else
        {
            return;
        }
        DiceManager.Instance.ShowDicePanel(6, 0.01f, new DiceHander(SureIQDice));
    }

    private int luckyIndex = 0;
    void OnLuckyCardBtnClick()
    {
        luckyIndex++;
        UIButton btn = cardWidget.transform.Find("LuckyCard").GetComponent<UIButton>();
        if (luckyIndex == 1)
        {
            SureState = CreatSureState.Lucky_State_Dice1;
            SubtitlesManager.Instance.ShowSubtitle(1, 28, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (luckyIndex == 2)
        {
            SureState = CreatSureState.Lucky_State_Dice2;
            btn.enabled = false;

        }

        else if (luckyIndex == 3)
        {
            SureState = CreatSureState.Lucky_State_Dice3;
            btn.enabled = false;

        }

        else
        {
            return;
        }
        DiceManager.Instance.ShowDicePanel(6, 0.01f, new DiceHander(SureLuckyDice));
    }

    private int weiYanIndex = 0;
    void OnWeiYanCardBtnClick()
    {
        UIButton btn = cardWidget.transform.Find("WeiYanCard").GetComponent<UIButton>();
        weiYanIndex++;
        if (weiYanIndex == 1)
        {
            SureState = CreatSureState.WeiYan_State_Dice1;
            SubtitlesManager.Instance.ShowSubtitle(1, 45, 2, "Knock", SubtitlePositionEnum.top);
            btn.enabled = false;
        }

        else if (weiYanIndex == 2)
        {
            SureState = CreatSureState.WeiYan_State_Dice2;
            btn.enabled = false;

        }

        else if (weiYanIndex == 3)
        {
            SureState = CreatSureState.WeiYan_State_Dice3;
            btn.enabled = false;

        }

        else
        {
            return;
        }
        DiceManager.Instance.ShowDicePanel(6, 0.01f, new DiceHander(SureWeiYanDice));
    }

    static void OnStatureCardBtnClick()
    {
        UIButton btn = cardWidget.transform.Find("StatureCard").GetComponent<UIButton>();
        statureIndex++;
        if (statureIndex == 1)
        {
            SureState = CreatSureState.Stature_State_Dice1;
            btn.enabled = false;
        }

        else if (statureIndex == 2)
        {
            SureState = CreatSureState.Stature_State_Dice2;
            btn.enabled = false;
        }

        else
        {
            return;
        }
        DiceManager.Instance.ShowDicePanel(6, 0.01f, new DiceHander(SureStatureDice));

    }
    #endregion
    #region 有关骰子的方法
    static void SureStatureDice()
    {
        UIButton btn = cardWidget.transform.Find("StatureCard").GetComponent<UIButton>();
        btn.enabled = true;
        if (SureState== CreatSureState.Stature_State_Dice1)
        {
            dice1 = DicePanel.diceValue;
            fristDice.text = dice1.ToString();
        }

        else if (SureState == CreatSureState.Stature_State_Dice2)
        {
            dice2 = DicePanel.diceValue;
            secondDice.text = dice2.ToString();
            SubtitlesManager.Instance.ShowSubtitle(1, 3, 4, "Knock", SubtitlePositionEnum.top);
        }
    }

    void SureVITDice()
    {
        UIButton btn = cardWidget.transform.Find("VITCard").GetComponent<UIButton>();
        btn.enabled = true;
        if (SureState == CreatSureState.VIT_State_Dice1)
        {
            dice1 = DicePanel.diceValue;
            fristDice.text = dice1.ToString();
        }

        else if (SureState == CreatSureState.VIT_State_Dice2)
        {
            dice2 = DicePanel.diceValue;
            secondDice.text = dice2.ToString();
        }

        else if (SureState == CreatSureState.VIT_State_Dice3)
        {
            dice3 = DicePanel.diceValue;
            thridDice.text = dice3.ToString();
            SubtitlesManager.Instance.ShowSubtitle(1, 19, 4, "Knock", SubtitlePositionEnum.top);
        }
    }


    void SurePowerDice()
    {
        UIButton btn = cardWidget.transform.Find("PowerCard").GetComponent<UIButton>();
        btn.enabled = true;
        if (SureState == CreatSureState.Power_State_Dice1)
        {
            dice1 = DicePanel.diceValue;
            fristDice.text = dice1.ToString();
        }

        else if (SureState == CreatSureState.Power_State_Dice2)
        {
            dice2 = DicePanel.diceValue;
            secondDice.text = dice2.ToString();
        }

        else if (SureState == CreatSureState.Power_State_Dice3)
        {
            dice3 = DicePanel.diceValue;
            thridDice.text = dice3.ToString();
            SubtitlesManager.Instance.ShowSubtitle(1, 13, 4, "Knock", SubtitlePositionEnum.top);
        }
    }

    void SureIQDice()
    {
        UIButton btn = cardWidget.transform.Find("IQCard").GetComponent<UIButton>();
        btn.enabled = true;
        if (SureState == CreatSureState.IQ_State_Dice1)
        {
            dice1 = DicePanel.diceValue;
            fristDice.text = dice1.ToString();
        }

        else if (SureState == CreatSureState.IQ_State_Dice2)
        {
            dice2 = DicePanel.diceValue;
            secondDice.text = dice2.ToString();
        }

        else if (SureState == CreatSureState.IQ_State_Dice3)
        {
            dice3 = DicePanel.diceValue;
            thridDice.text = dice3.ToString();
            SubtitlesManager.Instance.ShowSubtitle(1, 27, 4, "Knock", SubtitlePositionEnum.top);
        }
    }

    void SureLuckyDice()
    {
        UIButton btn = cardWidget.transform.Find("LuckyCard").GetComponent<UIButton>();
        btn.enabled = true;
        if (SureState == CreatSureState.Lucky_State_Dice1)
        {
            dice1 = DicePanel.diceValue;
            fristDice.text = dice1.ToString();
        }

        else if (SureState == CreatSureState.Lucky_State_Dice2)
        {
            dice2 = DicePanel.diceValue;
            secondDice.text = dice2.ToString();
        }

        else if (SureState == CreatSureState.Lucky_State_Dice3)
        {
            dice3 = DicePanel.diceValue;
            thridDice.text = dice3.ToString();
            SubtitlesManager.Instance.ShowSubtitle(1, 44, 4, "Knock", SubtitlePositionEnum.top);
        }
        
    }

    void SureWeiYanDice()
    {
        UIButton btn = cardWidget.transform.Find("WeiYanCard").GetComponent<UIButton>();
        btn.enabled = true;
        if (SureState == CreatSureState.WeiYan_State_Dice1)
        {
            dice1 = DicePanel.diceValue;
            fristDice.text = dice1.ToString();
        }

        else if (SureState == CreatSureState.WeiYan_State_Dice2)
        {
            dice2 = DicePanel.diceValue;
            secondDice.text = dice2.ToString();
        }

        else if (SureState == CreatSureState.WeiYan_State_Dice3)
        {
            dice3 = DicePanel.diceValue;
            thridDice.text = dice3.ToString();
            SubtitlesManager.Instance.ShowSubtitle(1, 47, 4, "Knock", SubtitlePositionEnum.top);
        }
    }
    #endregion


 

}
