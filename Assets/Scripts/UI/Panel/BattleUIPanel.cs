using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIPanel :IView
{
    private GameObject m_Item;
    private UIGrid m_Grid;
    //private List<CardData> m_HandCardsList;
    private UIButton chouKaBtn;
    private AnimationCurve m_Curve;
    private Vector3 initVec = new Vector3(0, 1, 0);
    private CharacterBase playerProBase;
    
    public BattleUIPanel()
    {
        m_Layer = Layer.city;
    }

    protected override void OnStart()
    {
        //m_HandCardsList = new List<global::CardData>();
        GameObject panel = GUIManager.FindPanel("BattleUIPanel");
        //m_HandCardsList = CardDataManager.Instance.GetInitCardList();
        m_Item = panel.transform.FindRecursively("Item").gameObject;
        m_Grid = panel.transform.FindRecursively("Grid").GetComponent<UIGrid>();
        //chouKaBtn = panel.transform.FindRecursively("ChouKaBtn").GetComponent<UIButton>();

       

        //EventDelegate ChouKaBtnClick = new global::EventDelegate(OnChouKaBtnClick);
        //chouKaBtn.onClick.Add(ChouKaBtnClick);
        //m_Grid.onReposition += GridHandCard;
        BattleCommoUIManager.Instance.InitUI(panel);
        //InitCurve();
    }

    protected override void OnShow()
    {
        BaGroundCardManager.Instance.Init();
        //m_HandCardsList = BaGroundCardManager.Instance.GetHandList();
        BaGroundCardManager.Instance.UpdataHandCardData();

        BattleCommoUIManager.Instance.UpdataSlider_Player();
    }

    protected override void OnHide()
    {
       
    }

    protected override void OnDestroy()
    {
        
    }

    public override void Update()
    {
        BattleCommoUIManager.Instance.UpdataSlider_Player();
    }

    
  


    private void OnChouKaBtnClick()
    {
        BaGroundCardManager.Instance.ChouCard();
        
        //m_HandCardsList = BaGroundCardManager.Instance.GetHandList();
        BaGroundCardManager.Instance.UpdataHandCardData();
    }
 




    private void GridHandCard()
    {
        int count = m_Grid.transform.childCount;

        int deNum = count % 2;

        if (deNum == 0)
        {
      
            int leftCount = count / 2;

            int euler = 5 * leftCount;
            for (int i = 0; i < leftCount; i++)
            {
                GameObject go = m_Grid.transform.GetChild(i).gameObject;
                go.transform.localEulerAngles = new Vector3(0, 0, euler);
                euler -= 5;
            }
            euler = 0;
            for (int i = count-leftCount; i < count; i++)
            {
                euler -= 5;
                GameObject go = m_Grid.transform.GetChild(i).gameObject;
                go.transform.localEulerAngles = new Vector3(0, 0, euler);
            }
        }

        else if (deNum == 1)
        {
            int leftCount = count / 2;

            int euler = 5 * leftCount;
            for (int i = 0; i < leftCount; i++)
            {
                GameObject go = m_Grid.transform.GetChild(i).gameObject;
                go.transform.localEulerAngles = new Vector3(0, 0, euler);
                euler -= 5;
            }

            for (int i = leftCount; i < count; i++)
            {
                GameObject go = m_Grid.transform.GetChild(i).gameObject;
                go.transform.localEulerAngles = new Vector3(0, 0, euler);
                euler -= 5;
            }
        }

       
        MovePosByCureve();

    }

    private void MovePosByCureve()
    {
        int changeNum = 120;

        int count = m_Grid.transform.childCount;
        float m_Num = 2.0f / (count + 1);
        float initPos = m_Num;
        for (int i = 0; i < count; i++)
        {
            GameObject go = m_Grid.transform.GetChild(i).gameObject;
            float angle = go.transform.localRotation.z;
            Vector3 goCenterPoint = go.transform.localPosition;

            UISprite sprite = go.GetComponent<UISprite>();
            float length = sprite.height * sprite.aspectRatio*go.transform.localScale.x*0.5f;
            float x = (float)Math.Sin(Math.Abs(angle)) * length;
            if (angle < 0)
            {
                x = -x;
            }
            Vector3 posX = new Vector3(1, 0, 0) * x;

            float y = (float)Math.Cos(Math.Abs(angle)) * length;
            Vector3 posY = new Vector3(0, 1, 0) * y;
            goCenterPoint += (posX + posY);

        

            goCenterPoint += initVec * m_Curve.Evaluate(initPos) * changeNum;
            goCenterPoint-= m_Curve.Evaluate(1.0f) * changeNum * initVec;
            go.transform.localPosition = Vector2.Lerp(go.transform.localPosition, goCenterPoint, Time.time);

            initPos += m_Num;
   
        }
    }

    private void InitCurve()
    {
        HandUseCurve curve = m_Grid.GetComponent<HandUseCurve>();
        m_Curve = curve.cureve;
    }

    
}
