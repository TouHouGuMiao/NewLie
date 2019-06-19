using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpRun : MonoBehaviour {
    private bool IsLerp_Fu0=false;
    private bool IsLerp_ZhengRadius=false;
    private bool IsLerp_FuRadius=false;
    private bool IsLerp_Zheng0=true;

    public bool stopSlerp=false;
    public bool isDrift=false;
    private float time = 0;

    public float rate = 0.01f;
    [HideInInspector]
    public float radius =7;
    private bool stopRotate = false;
    [HideInInspector]
    public bool isClickCard=false;
    

    // Use this for initialization
    void Start () {
       IsLerp_Zheng0 = true;
        UIButton uIButton = gameObject.GetComponent<UIButton>();
        uIButton.onClick.Add(new EventDelegate(OnButtonClick));
}
	
	// Update is called once per frame
	void Update () {
        RotategameObject();
        CardDrifting();
    }

    void RotategameObject()
    {
     
        Vector3 tempPos = new Vector3(0, 0, radius)-gameObject.transform.localPosition ;    
        float angle = Mathf.Atan2(tempPos.z, tempPos.x) * Mathf.Rad2Deg;
        float rotateAngle = -1 * angle + 90;
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        if (!stopRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotateAngle, transform.rotation.eulerAngles.z);
        }
     
        if (stopSlerp)
        {
            return;
        }
        if (time >= 1)
        {
            time = rate;
            if (IsLerp_Zheng0)
            {
                IsLerp_Zheng0 = false;
                IsLerp_FuRadius = true;
                IsLerp_Fu0 = false;
                IsLerp_ZhengRadius = false;
            }

            else if (IsLerp_FuRadius)
            {
                IsLerp_Zheng0 = false;
                IsLerp_FuRadius = false;
                IsLerp_Fu0 = true;
                IsLerp_ZhengRadius = false;
            }

            else if (IsLerp_Fu0)
            {
                IsLerp_Zheng0 = false;
                IsLerp_FuRadius = false;
                IsLerp_Fu0 = false;
                IsLerp_ZhengRadius = true;
            }

            else if (IsLerp_ZhengRadius)
            {
                IsLerp_Zheng0 = true;
                IsLerp_FuRadius = false;
                IsLerp_Fu0 = false;
                IsLerp_ZhengRadius = false;
            }
        }
        time += rate;

        if (IsLerp_Zheng0)
        {
            Vector3 center = new Vector3(0, 0, radius);
            Vector3 start = new Vector3(0, 0, 0) - center;
            Vector3 end = new Vector3(-radius, 0, radius) - center;
            gameObject.transform.localPosition = Vector3.Slerp(start, end, time);
            gameObject.transform.localPosition += center;
        }

        else if (IsLerp_FuRadius)
        {
            Vector3 center = new Vector3(0, 0, radius);
            Vector3 start = new Vector3(-radius, 0, radius) - center;
            Vector3 end = new Vector3(0, 0, 2 * radius) - center;
            gameObject.transform.localPosition = (Vector3.Slerp(start, end, time));
            gameObject.transform.localPosition += center;
        }

        else if (IsLerp_Fu0)
        {
            Vector3 center = new Vector3(0, 0, radius);
            Vector3 start = new Vector3(0, 0, 2 * radius) - center;
            Vector3 end = new Vector3(radius, 0, radius) - center;
            gameObject.transform.localPosition = (Vector3.Slerp(start, end, time));
            gameObject.transform.localPosition += center;
        }

        else if (IsLerp_ZhengRadius)
        {
            Vector3 center = new Vector3(0, 0, radius);
            Vector3 start = new Vector3(radius, 0, radius) - center;
            Vector3 end = new Vector3(0, 0, 0) - center;
            gameObject.transform.localPosition = Vector3.Slerp(start, end, time);
            gameObject.transform.localPosition += center;
        }
        time += rate;

    }
    float drifTime = 0;
    float drifRata = 0.01f;
    Vector3 drifStart;
    Vector3 drifCenter;
    Vector3 drifEnd;
    public void CardDrifting()
    {
        if (!isDrift)
        {
            return;
        }
        Vector3 runCenter = new Vector3(0, 0, radius);
        if (drifTime == 0)
        {
            drifCenter = (transform.localPosition - runCenter) + transform.localPosition;
            drifEnd = (drifCenter+ new Vector3 (0, radius,0)) - drifCenter;
            drifStart = transform.localPosition - drifCenter;
        }
      
        
        Vector3 tempPos = Vector3.Slerp(drifStart, drifEnd, drifTime);
        transform.localPosition = tempPos + drifCenter;
        drifTime += drifRata;
    }

    void ShowNumberDicCard()
    {
        stopSlerp = true;
        TweenPosition tweenPosition = gameObject.GetComponent<TweenPosition>();
        tweenPosition.enabled = true;
        tweenPosition.onFinished.Clear();
        Vector3 targetVec = new Vector3(0, 0, 0);
        tweenPosition.from = transform.localPosition;
        tweenPosition.to = targetVec;
        tweenPosition.delay = 0;
        tweenPosition.duration = 1.5f;
        tweenPosition.onFinished.Add(new EventDelegate (RotateNumberNDicCard));
        tweenPosition.ResetToBeginning();
    }

    void RotateNumberNDicCard()
    {
        TweenRotation tweenRotation = gameObject.AddComponent<TweenRotation>();
        tweenRotation.from = transform.rotation.eulerAngles;
        tweenRotation.to = new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        tweenRotation.onFinished.Add(new EventDelegate(RotateNumberDicCard_Sec));
    }

    void RotateNumberDicCard_Sec()
    {
        stopRotate = true;
        TweenRotation tweenRotation = gameObject.GetComponent<TweenRotation>();
        tweenRotation.enabled = true;

        tweenRotation.from = transform.rotation.eulerAngles;
        tweenRotation.to = new Vector3(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        tweenRotation.ResetToBeginning();
        tweenRotation.onFinished.Clear();
        tweenRotation.onFinished.Add(new EventDelegate(OnRotateNumberDiceCard_SecFished));
       
    }
    
    void OnRotateNumberDiceCard_SecFished()
    {
        UIButton btn = this.GetComponent<UIButton>();
        btn.onClick.Clear();
        btn.enabled = true;
        btn.onClick.Add(new EventDelegate(OnBtnLastClick));
    }

    void OnButtonClick()
    {
        isClickCard = true;
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject go = parent.GetChild(i).gameObject;
            UIButton btn = go.GetComponent<UIButton>();
            btn.onClick.Clear();
            btn.enabled = false;
            if (go.name == gameObject.name)
            {
                continue;
            }
            SlerpRun slerpRun = go.transform.GetComponent<SlerpRun>();
            slerpRun.stopSlerp = true;
     
            slerpRun.isDrift=true;
        }
        int number= CommonHelper.Str2Int(this.name);
        DicePanel.diceValue = number;
      
        ShowNumberDicCard();
    }

    void OnBtnLastClick()
    {
        UIButton btn = UIButton.current;
        btn.onClick.Clear();
        btn.enabled = false;
   

        HideDiceCards();

    }

    void PlayCardMoveAudio()
    {
        AudioManager.Instance.PlayEffect_Source("cardMove");
    }

    void HideDiceCards()
    {
        TweenPosition tp;
        Transform parent = transform.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject go = parent.GetChild(i).gameObject;
            tp = go.GetComponent<TweenPosition>();
            tp.onFinished.Clear();
            SlerpRun slerpRun = go.GetComponent<SlerpRun>();
            slerpRun.isDrift = false;

            if (!slerpRun.isClickCard)
            {
                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(0, i * 0.1f, 0);
                tp.duration = 2.0f;
                tp.delay = 0.15f * i;
                tp.ignoreTimeScale = true;
                tp.ResetToBeginning();
            }


            else
            {
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(0, i * 0.1f, 0);
                tp.duration = 2.0f;
                tp.delay = 0.2f * i;
                tp.ignoreTimeScale = true;
                ReturnRotateFrist();
                tp.ResetToBeginning();
            }
            if (i == parent.childCount - 1)
            {
                tp.onFinished.Add(new EventDelegate(ReturnTweenPositionToScan));
            }
            else
            {
                tp.onFinished.Add(new EventDelegate(PlayCardMoveAudio));
            }
           

        
        }
    }

    #region 卡牌返回时的方法
    void ReturnRotateFrist()
    {
        stopRotate = true;
        TweenRotation tweenRotation = gameObject.GetComponent<TweenRotation>();
        tweenRotation.enabled = true;
        tweenRotation.onFinished.Clear();
        tweenRotation.from = transform.rotation.eulerAngles;
        tweenRotation.to = new Vector3(0, 0, 0);
        tweenRotation.ResetToBeginning();
        tweenRotation.ignoreTimeScale = false;
        tweenRotation.duration = 0.75f;
        tweenRotation.onFinished.Add(new EventDelegate(ReturnRotateSecond));
    }

    void ReturnRotateSecond()
    {
        TweenRotation tweenRotation = gameObject.GetComponent<TweenRotation>();
        tweenRotation.enabled = true;
        tweenRotation.onFinished.Clear();
        tweenRotation.ignoreTimeScale = false;
        tweenRotation.duration = 0.75f;
        tweenRotation.from = transform.rotation.eulerAngles;
        tweenRotation.to = new Vector3(80, 0, 0);
        tweenRotation.ResetToBeginning();
  
    }

  

    void ReturnTweenPositionToScan()
    {
        if (ReturnTweenPositionToScanIsOver)
        {
            return;
        }
        Transform parent = transform.parent;
        int temp_j=0;
        for (int i = parent.childCount-1; i >=0; i--)
        {
            GameObject go = parent.GetChild(i).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.from = go.transform.localPosition;
            tp.to = new Vector3(-30, go.transform.localPosition.y, go.transform.localPosition.z);
            tp.duration = 0.7f;
            tp.delay = temp_j * 0.2f+0.2f;
            SlerpRun slerp = go.GetComponent<SlerpRun>();
            slerp.stopRotate = false;
            tp.ignoreTimeScale = false;
            tp.onFinished.Clear();
            if (i == 0)
            {
              
                tp.onFinished.Add(new EventDelegate(LastTPHidePanel));
                tp.onFinished.Add(new EventDelegate(PlayCardMoveAudio));
                
            }
            else
            {
                tp.onFinished.Add(new EventDelegate(PlayCardMoveAudio));
            }
            tp.ResetToBeginning();
            temp_j++;
        }
        ReturnTweenPositionToScanIsOver = true;
    }
    private bool ReturnTweenPositionToScanIsOver = false;

    void LastTPHidePanel()
    {
        int number = CommonHelper.Str2Int(this.name);
        DiceManager.Instance.DepentClickSource(number);
        GUIManager.HideView("DicePanel");
    }


    #endregion
}
