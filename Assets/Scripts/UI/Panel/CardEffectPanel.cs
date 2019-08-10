using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectPanel : IView
{
    private Transform eventGroundWidget;
    private Transform childWidget;
    private Transform eventWidget;

    private GameObject item;

    private Vector3 eventGroundInfoVec;
    private Vector3 childInfoVec;
    private Vector3 eventInfoVec;

    public static bool isReplaceEventGround=false;
    public static bool isReplaceEventCard = false;
    public static bool isReplaceChildGround = false;
    public static bool isSetHandCard = false;
    public static int eventGroundReplaceCount;
    public static int eventChildGroundReplaceCount;
    public static string cardName;
    public static List<string> addToHandCardNameList;


   
    public CardEffectPanel()
    {
        m_Layer = Layer.Tabel;
    }

    protected override void OnStart()
    {
        eventGroundWidget = this.GetChild("EventGroundWidget");
        childWidget = this.GetChild("ChildtWidget");
        eventWidget = this.GetChild("EventWidget");
        eventGroundInfoVec = new Vector3(-3.67f, -2.3f, 10.65f);
        eventInfoVec = new Vector3(1.67f, -2.3f, 10.65f);
        childInfoVec = new Vector3(3.08f, -2.3f, 10.65f);
        item = this.GetChild("Item").gameObject;
    }

    protected override void OnShow()
    {
        if (isReplaceChildGround)
        {
            RelpaceChildGround_OldGourndGoOut();
        }

        if (isSetHandCard)
        {
            SetHandCard();
        }
    }

    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
     
    }

    #region 替换ChilidGournd相关
    private void RelpaceChildGround_OldGourndGoOut()
    {
        if (childWidget.transform.childCount <= 0)
        {
            RelpaceChildGround_NewGroundInto();
        }
        else
        {

            int index = childWidget.transform.childCount - 1;
            for (int i = 0; i < childWidget.transform.childCount; i++)
            {
                GameObject go = childWidget.GetChild(index).gameObject;
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.OnStarted.Add(new EventDelegate(PlayerCardMoveAudio));
                tp.enabled = true;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(12.22f, -0.3f, 10.5f);
                tp.onFinished.Clear();
                tp.ResetToBeginning();
                tp.duration = 0.75f;
                tp.delay = 0.08f * i;
                if (i >= childWidget.transform.childCount - 1)
                {
                    tp.onFinished.Add(new EventDelegate(RelpaceChildGround_NewGroundInto));
                }

                else
                {
                    tp.onFinished.Add(new EventDelegate(OnCardTPFished_Destory));
                }
                index--;
            }
        }
     
    }

    private void RelpaceChildGround_NewGroundInto()
    {
        for (int i = 0; i < eventChildGroundReplaceCount; i++)
        {
            GameObject go = GameObject.Instantiate(item);
            go.transform.SetParent(childWidget);
            go.transform.localPosition = new Vector3(9.22f, -0.3f, 10.5f);
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.Euler(90, 0, 0);
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = new Vector3(12.22f, -0.3f, 10.5f);
            tp.OnStarted.Add(new EventDelegate(PlayerCardMoveAudio));
            tp.to = childInfoVec+new Vector3 (0,0.05f*i,0);
            tp.duration = 0.75f;
            tp.delay = 0.08f * i;
            go.gameObject.SetActive(true);
            tp.ResetToBeginning();
        }
    }

    private void OnCardTPFished_Destory()
    {
        TweenPosition tp = (TweenPosition)TweenPosition.current;
        GameObject.Destroy(tp.gameObject);
    }

    private void PlayerCardMoveAudio()
    {
        AudioManager.Instance.PlayEffect_Source("cardMove");
    }

    #region 抽牌
    private void SetHandCard()
    {
        for (int i = 0; i < addToHandCardNameList.Count; i++)
        {
            GameObject go = childWidget.transform.GetChild(childWidget.transform.childCount - 1 - i).gameObject;
            GameObject mian = go.transform.Find("面_1").gameObject;
            mian.GetComponent<MeshRenderer>().material = ResourcesManager.Instance.LoadMaterial(addToHandCardNameList[i]);
            UIButton button = go.GetComponent<UIButton>();
            if (i >= addToHandCardNameList.Count - 1)
            {
                button.onClick.Add(new EventDelegate(OnCardClick_Last));
            }
            else
            {
                button.onClick.Add(new EventDelegate(OnCardClick));
            }
          
        }
        SetChouKaTp();
    }

    private void OnCardClick()
    {
        Debug.LogError(1);
        GameObject current = UIButton.current.gameObject;

        current.transform.SetParent(GUIManager.FindPanel("CardEffectPanel").transform,false);
        TweenPosition currentTp = current.GetComponent<TweenPosition>();
        currentTp.enabled = true;
        currentTp.onFinished.Clear();
        currentTp.from = current.gameObject.transform.localPosition;
        currentTp.to = new Vector3(current.gameObject.transform.localPosition.x, current.transform.localPosition.y - 10, current.transform.localPosition.z);
        currentTp.ResetToBeginning();
        SetChouKaTp();
    }

    private void OnCardClick_Last()
    {
        GameObject current = UIButton.current.gameObject;

        current.transform.SetParent(GUIManager.FindPanel("CardEffectPanel").transform, false);
        TweenPosition currentTp = current.GetComponent<TweenPosition>();
        currentTp.enabled = true;
        currentTp.onFinished.Clear();
        currentTp.from = current.gameObject.transform.localPosition;
        currentTp.to = new Vector3(current.gameObject.transform.localPosition.x, current.transform.localPosition.y - 10, current.transform.localPosition.z);
        currentTp.ResetToBeginning();
    }
    private void SetChouKaTp()
    {
        GameObject go = GetChildWidgetFristCard();
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.from = go.transform.localPosition;
        tp.to = new Vector3(0, 0, 8);
        tp.delay = 0;
        tp.duration = 0.75f;
        tp.onFinished.Clear();
        tp.onFinished.Add(new EventDelegate(SetButtonTure));
        tp.ResetToBeginning();

        TweenRotation tr = go.GetComponent<TweenRotation>();
        tr.enabled = true;
        tr.from = go.transform.localRotation.eulerAngles;
        tr.to = new Vector3(0, 180, 0);
        tr.delay = 0.1F;
        tr.duration = 0.5f;
        tr.ResetToBeginning();
    }

    private GameObject GetChildWidgetFristCard()
    {
        GameObject go = childWidget.transform.GetChild(childWidget.transform.childCount - 1).gameObject;
        return go;
    }


    private void SetButtonTure()
    {
        TweenPosition tp = (TweenPosition)TweenPosition.current;
        UIButton button = tp.GetComponent<UIButton>();
        button.enabled = true;
    }



    #endregion

    #endregion


    #region EventCard相关

    /// <summary>
    /// 移走当前Card
    /// </summary>
    //void MoveCurrentCard()
    //{
    //    for (int i = 0; i < eventWidget.childCount; i++)
    //    {
    //        GameObject go = eventWidget.transform.GetChild(i).gameObject;
    //        TweenPosition tp = go.GetComponent<TweenPosition>();
    //        tp.from = go.transform.localPosition;
    //        tp.
    //    }
    //}

    #endregion
}
