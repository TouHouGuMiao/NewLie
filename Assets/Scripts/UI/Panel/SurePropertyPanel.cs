using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatSureState
{
    Stature_State = 1,
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
    private static UIButton stature_SecDice;
    public SurePropertyPanel()
    {
        m_Layer = Layer.bottom;
    }

    protected override void OnStart()
    {
        Stature = this.GetChild("Stature");
        Container = this.GetChild("Container");
        stature_SecDice = Stature.Find("stature_SecDice").GetComponent<UIButton>();
        BtnAddEvent();
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
    }

    void BtnAddEvent()
    {
        stature_SecDice.onClick.Add(new EventDelegate(OnStatureSecondBtnClick));
    }

    void OnStatureSecondBtnClick()
    {
        GUIManager.HideView("DicePanel");
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(1, 2);
    }

    public static void ShowStatureSecondBtn()
    {
        stature_SecDice.gameObject.SetActive(true);
    }

    
}
