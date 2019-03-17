using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePanel : IView
{
    private GameObject card;
    private float radius=7;
    public static float rate;
    public static List<int> DiceNumberList = new List<int>();
    private Dictionary<int, GameObject> DiceCardDic = new Dictionary<int, GameObject>();
    public DicePanel()
    {
        m_Layer = Layer.Dice;
    }

    protected override void OnStart()
    {

;    }

    protected override void OnShow()
    {
        GameObject panel = GUIManager.FindPanel("DicePanel");
        for (int i = 0; i < DiceNumberList.Count; i++)
        {
            GameObject Container = this.GetChild("Container").gameObject;
            GameObject go = ResourcesManager.Instance.LoadDiceCard(DiceNumberList[i].ToString());
            GameObject diceCard = GameObject.Instantiate(go);
            diceCard.transform.SetParent(Container.transform,false);
            DiceCardDic.Add(DiceNumberList[i], diceCard);
        }
        DiceCardDic[DiceNumberList[0]].AddComponent<SlerpRun>();
    }



    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
        DiceNumberList.Clear();
        DiceCardDic.Clear();
    }


    public override void Update()
    {
        AddSlerpRotate();
    }

    int slerpIndex = 0;
    void AddSlerpRotate()
    {
      
        if (DiceCardDic.Count <= 0)
            return;
        GameObject go = DiceCardDic[DiceNumberList[0]];
        if (slerpIndex >= DiceNumberList.Count-1)
        {
            BoxCollider collider = go.GetComponent<BoxCollider>();
            if (collider.enabled == false)
            {
                Transform parent = go.transform.parent;
                for (int i = 0; i < parent.childCount; i++)
                {
                    GameObject child = parent.GetChild(i).gameObject;
                    BoxCollider boxCollider = child.GetComponent<BoxCollider>();
                    boxCollider.enabled = true;
                }
            }
            return;
        }
       

        Vector3 tempPos = go.transform.localPosition - new Vector3(0, 0, radius);
        float angle = Mathf.Atan2(tempPos.z, tempPos.x) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle = 360 + angle;
        }
        if (DiceNumberList.Count == 10)
        {

            if ((angle <= 234 && angle > 198) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 198 && angle > 162) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 162 && angle > 126) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 126 && angle > 90) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 90 && angle > 54) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 54 && angle > 18) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 18 && angle > 0) && slerpIndex == 6)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 342 && angle > 306) && slerpIndex == 7)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }


            if ((angle <= 306 && angle > 270) && slerpIndex == 8)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
        }
        if (DiceNumberList.Count == 9)
        {

            if ((angle <= 230 && angle > 190) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 190 && angle > 150) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 150 && angle > 110) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 110 && angle > 70) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 70 && angle > 30) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 30 && angle > 0) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 350 && angle > 310) && slerpIndex == 6)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 310 && angle > 270) && slerpIndex == 7)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
        }
        if (DiceNumberList.Count == 8)
        {
            if ((angle <= 225 && angle > 180) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 180 && angle > 135) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 135 && angle > 90) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 90 && angle > 45) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 45 && angle > 0) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 360 && angle > 315) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
            if ((angle <= 315 && angle > 270) && slerpIndex == 6)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

        }
        if (DiceNumberList.Count == 7)
        {

            if ((angle <= 219 && angle > 168) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 168 && angle > 117) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 117 && angle > 66) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 66 && angle > 15) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 15 && angle > 0) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 324 && angle > 273) && slerpIndex == 5)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
        }
        if (DiceNumberList.Count == 6)
        {
            if ((angle <=210 && angle > 150) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <=150 && angle >90) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 90 && angle >30) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <=30 && angle >= 0) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <=330 && angle > 270) && slerpIndex == 4)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
        }
        if (DiceNumberList.Count == 5)
        {
            if ((angle <= 198 && angle > 126) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 126 && angle > 54) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 54 && angle > 0) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 342 && angle > 270) && slerpIndex == 3)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

        }

        if (DiceNumberList.Count == 4)
        {
            if ((angle <= 180 && angle > 90) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 90 && angle > 0) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 360 && angle > 270) && slerpIndex == 2)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

        }

        if (DiceNumberList.Count == 3)
        {
            if ((angle <= 150 && angle >30 ) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }

            if ((angle <= 30 && angle > 0) && slerpIndex == 1)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
        }

        if (DiceNumberList.Count == 2)
        {
            if ((angle <= 90 && angle >0) && slerpIndex == 0)
            {
                slerpIndex++;
                DiceCardDic[DiceNumberList[slerpIndex]].AddComponent<SlerpRun>();
            }
        }
    }
   

}
