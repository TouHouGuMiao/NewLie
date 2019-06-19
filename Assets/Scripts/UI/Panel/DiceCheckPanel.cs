using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckPanel : IView
{
    public static string modelName;
    private Transform cardWidget;
    public static  DiceHander OnDiceCardMoveFished;
    /// <summary>
    /// 掷多个骰子的骰子取值
    /// </summary>
    public static int diceValue;

    public DiceCheckPanel()
    {
        m_Layer = Layer.DiceCheck;
    }

    protected override void OnStart()
    {
        cardWidget = this.GetChild("CardWidget");
    }

    protected override void OnShow()
    {
        GameObject prefab = ResourcesManager.Instance.LoadDiceCard(modelName);
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.localPosition = new Vector3(-15, 0, 0);
        go.transform.SetParent(cardWidget, false);
        go.name = prefab.name;
        TweenPosition tp = go.AddComponent<TweenPosition>();
        if (cardWidget.childCount == 1)
        {  
            tp.duration = 1.5f;
            tp.delay = 0.2f;
            tp.from = new Vector3(-15, 0, 0);
            tp.to = new Vector3(16.5f, 6, 30);
        }

        else if (cardWidget.childCount == 2)
        {
            tp.duration = 1.5f;
            tp.delay = 0.2f;
            tp.from = new Vector3(-15, 0, 0);
            tp.to = new Vector3(15.0f, 6, 30);
        }

        else
        {
            Debug.LogError("cardChildCount has error" + " __" + cardWidget.childCount);
            return;
        }
        tp.onFinished.Clear();
        tp.onFinished.Add(new EventDelegate (OnDiceTPFished));
        TweenRotation tr = go.AddComponent<TweenRotation>();
        tr.ignoreTimeScale = false;
        tr.from = new Vector3(90, 0, 0);
        tr.to = new Vector3(0, 180, 0);
        tr.delay = 0.2f;
        tr.duration = 1.5f;
    }

 
    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        cardWidget.DestroyChildren();
        OnDiceCardMoveFished = null;
    }

    void OnDiceTPFished()
    {
        if (OnDiceCardMoveFished != null)
        {
            int[] diceArrary=new int[cardWidget.childCount];
            for (int i = 0; i < cardWidget.childCount; i++)
            {
                GameObject go = cardWidget.transform.GetChild(i).gameObject;
                diceArrary[i] = CommonHelper.Str2Int(go.name);
            }
            if (cardWidget.childCount == 2)
            {
                diceValue = diceArrary[1] * 10 + diceArrary[0];
            }
            OnDiceCardMoveFished();
        }
    }
}
