using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePanel : IView
{
    public static int diceValue=10;
    public static DiceHander OnDiceRotateFished;
    private GameObject card;
    private GameObject Container;
    private float radius=7;
    public static float rate;
    public static int[] DiceNumerArray;
    private static  GameObject dicePanel;
    private Dictionary<int, GameObject> DiceCardDic = new Dictionary<int, GameObject>();
    public DicePanel()
    {
        m_Layer = Layer.Dice;
    }

    public static bool IsDice
    {
        get
        {
            if (dicePanel == null)
            {
                return false;
            }

            if (dicePanel.activeSelf)
            {
                return true;
            }
            return false;
        }
    }


    protected override void OnStart()
    {
        Container = this.GetChild("Container").gameObject;
        dicePanel = GUIManager.FindPanel("DicePanel");
    }

    protected override void OnShow()
    {
        diceValue = 10;
        GameObject panel = GUIManager.FindPanel("DicePanel");
        for (int i = 0; i < DiceNumerArray.Length; i++)
        {          
            GameObject go = ResourcesManager.Instance.LoadDiceCard(DiceNumerArray[i].ToString());
            GameObject diceCard = GameObject.Instantiate(go);
            UIButton button = diceCard.GetComponent<UIButton>();
            button.tweenTarget = diceCard;
            diceCard.transform.SetParent(Container.transform,false);
            diceCard.transform.localPosition = new Vector3(30, 0, 0);
            TweenPosition tp = diceCard.AddComponent<TweenPosition>();
            tp.from = diceCard.transform.localPosition;
            tp.to = new Vector3(0, i * 0.1f, 0);
            tp.delay = 0.2f * i + 0.2f;
            diceCard.name = go.name;
            diceCard.AddComponent<SlerpRun>().stopSlerp = true;
            DiceCardDic.Add(DiceNumerArray[i], diceCard);
            if (i == DiceNumerArray.Length - 1)
            {
                tp.onFinished.Add(new EventDelegate(StartSlerp));
            }
            else
            {
                tp.onFinished.Add(new EventDelegate(PlayerCardMoveAudio));
            }
        }
    }


    IEnumerator StartSlerp_IEnumerator()
    {
        yield return new WaitForSeconds(1);
        SlerpRun slerpRun = DiceCardDic[DiceNumerArray[0]].gameObject.GetComponent<SlerpRun>();
        slerpRun.stopSlerp = false;
    }

    void StartSlerp()
    {
        IEnumeratorManager.Instance.StartCoroutine(StartSlerp_IEnumerator());
    }



    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
        DestoryDiceCard();
        if (OnDiceRotateFished != null)
        {
            OnDiceRotateFished();
        }
        Array.Clear(DiceNumerArray, 0, DiceNumerArray.Length);
        slerpIndex = 0;
        DiceCardDic.Clear();
        OnDiceRotateFished = null;
        
    }

    void DestoryDiceCard()
    {
        GameObject Container = this.GetChild("Container").gameObject;
        for (int i = 0; i < Container.transform.childCount; i++)
        {
            GameObject go = Container.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }
    }

   

    public override void Update()
    {
        AddSlerpRotate();
    }

    void DiceCardDeep()
    {
        for (int i = 0; i < Container.transform.childCount; i++)
        {
            GameObject go = Container.transform.GetChild(i).gameObject;
            SlerpRun slerpRun = go.GetComponent<SlerpRun>();
            if (slerpRun.stopSlerp == true)
            {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.from = go.transform.localPosition;
                tp.delay = 0;
                tp.duration = 0.1f;
                tp.to = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y - 0.1f, go.transform.localPosition.z);
                tp.ResetToBeginning();
            }
        }
        PlayerCardMoveAudio();
    }

    int slerpIndex = 0;
    void AddSlerpRotate()
    {
      
        if (DiceCardDic.Count <= 0)
            return;
        GameObject go = DiceCardDic[DiceNumerArray[0]];
        if (slerpIndex == DiceNumerArray.Length-1)
        {
            UIButton button = go.GetComponent<UIButton>();
            if (button.enabled == false)
            {
                Transform parent = go.transform.parent;
                for (int i = 0; i < parent.childCount; i++)
                {
                    GameObject child = parent.GetChild(i).gameObject;
                    UIButton btn = child.GetComponent<UIButton>();
                    btn.enabled = true;
                }
            }
            slerpIndex++;
            return;
        }

        if (slerpIndex> DiceNumerArray.Length - 1)
        {
            return;
        }



        Vector3 tempPos = go.transform.localPosition - new Vector3(0, 0, radius);
        float angle = Mathf.Atan2(tempPos.z, tempPos.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        if (DiceNumerArray.Length == 10)
        {

            if ((angle <= 234 && angle > 198) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 198 && angle > 162) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 162 && angle > 126) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 126 && angle > 90) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 90 && angle > 54) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 54 && angle > 18) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 18 && angle > 0) && slerpIndex == 6)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 342 && angle > 306) && slerpIndex == 7)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }


            if ((angle <= 306 && angle > 270) && slerpIndex == 8)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
        }
        if (DiceNumerArray.Length == 9)
        {

            if ((angle <= 230 && angle > 190) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 190 && angle > 150) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 150 && angle > 110) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 110 && angle > 70) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 70 && angle > 30) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 30 && angle > 0) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 350 && angle > 310) && slerpIndex == 6)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 310 && angle > 270) && slerpIndex == 7)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
        }
        if (DiceNumerArray.Length == 8)
        {
            if ((angle <= 225 && angle > 180) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
            }

            if ((angle <= 180 && angle > 135) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 135 && angle > 90) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 90 && angle > 45) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 45 && angle > 0) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 360 && angle > 315) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
            if ((angle <= 315 && angle > 270) && slerpIndex == 6)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

        }
        if (DiceNumerArray.Length == 7)
        {

            if ((angle <= 219 && angle > 168) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 168 && angle > 117) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 117 && angle > 66) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 66 && angle > 15) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 15 && angle > 0) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 324 && angle > 273) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
        }
        if (DiceNumerArray.Length == 6)
        {
            if ((angle <=210 && angle > 150) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <=150 && angle >90) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 90 && angle >30) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <=30 && angle >= 0) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <=330 && angle > 270) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
        }
        if (DiceNumerArray.Length == 5)
        {
            if ((angle <= 198 && angle > 126) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 126 && angle > 54) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 54 && angle > 0) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 342 && angle > 270) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

        }

        if (DiceNumerArray.Length == 4)
        {
            if ((angle <= 180 && angle > 90) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 90 && angle > 0) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 360 && angle > 270) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

        }

        if (DiceNumerArray.Length == 3)
        {
            if ((angle <= 150 && angle >30 ) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }

            if ((angle <= 30 && angle > 0) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
        }

        if (DiceNumerArray.Length == 2)
        {
            if ((angle <= 90 && angle >0) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumerArray[slerpIndex]].GetComponent<SlerpRun>().stopSlerp=false;
                DiceCardDeep();
            }
        }
    }  
    

    void PlayerCardMoveAudio()
    {
        AudioManager.Instance.PlayEffect_Source("cardMove");
    }

}
