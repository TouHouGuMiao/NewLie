using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatSureState
{
    Stature_State = 1,
    Stature_State_Dice1=2,
    Stature_State_Dice2=3,
    Stature_State_Reslut=4,
}

public class SurePropertyPanel : IView {

    private Transform Stature;
    private Transform Container;
    /// <summary>
    /// 初始值大于9，以保重不与9张牌冲突
    /// </summary>
    public static int dice1=10;
    public static int dice2 =10;
    public static int dice3 = 10;
    public static CreatSureState SureState;

    public static int stature_Prop;


    private GameObject statureCard;
    private GameObject statureCardExpress;
    private UILabel statureDes;
    private UILabel statureNumber;
    private UILabel stature_FristDice;
    private UILabel stature_SecondDice;

    public SurePropertyPanel()
    {
        m_Layer = Layer.bottom;
    }

    protected override void OnStart()
    {
        Container = this.GetChild("Container");
        Stature = this.GetChild("Stature");
        stature_FristDice = Stature.Find("fristDice").GetComponent<UILabel>();
        stature_SecondDice= Stature.Find("secondDice").GetComponent<UILabel>();

        statureCard = Stature.Find("statureCard").gameObject;
        statureCardExpress = Stature.Find("statureCardExpress").gameObject;
        statureDes = statureCardExpress.transform.Find("Des").GetComponent<UILabel>();
        statureNumber = statureCardExpress.transform.Find("Number").GetComponent<UILabel>();

    }
     
    protected override void OnShow()
    {

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
        if (SureState == CreatSureState.Stature_State)
        {
            if (!Stature.gameObject.activeSelf)
            {
                for (int i = 0; i < Container.childCount; i++)
                {
                    GameObject go = Container.GetChild(i).gameObject;
                    if (go.name != Stature.name)
                    {
                        go.SetActive(false);
                    }
                }
                Stature.gameObject.SetActive(true);
            }
        }
        if (SureState == CreatSureState.Stature_State_Dice1)
        {
            if (stature_FristDice.text != dice1.ToString())
            {
                stature_FristDice.text = dice1.ToString();
                StoryEventManager.Instance.ShowEventPanel_ChapterOne(1, 5);
            }
        }
        if (SureState == CreatSureState.Stature_State_Dice2)
        {
            if (stature_SecondDice.text != dice2.ToString())
            {
                stature_SecondDice.text = dice2.ToString();
                StoryEventManager.Instance.ShowEventPanel_ChapterOne(1, 6);
            }
        }

        if (SureState == CreatSureState.Stature_State_Reslut)
        {
            if (!statureCard.activeSelf)
            {
                stature_Prop = (dice1 + dice2 + 6) * 5;
                TweenPosition tp = statureCard.GetComponent<TweenPosition>();
                tp.onFinished.Add(new EventDelegate(ShowStaturePropExpress));
                statureCard.SetActive(true);
            }
        }
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
}
