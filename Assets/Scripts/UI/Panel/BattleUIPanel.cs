using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIPanel :IView
{
    //private GameObject m_Item;
    //private UIGrid m_Grid;
    ////private List<CardData> m_HandCardsList;
    //private UIButton chouKaBtn;
    //private AnimationCurve m_Curve;
    //private Vector3 initVec = new Vector3(0, 1, 0);
    //private CharacterBase playerProBase;
    //private GameObject player;

    //private UISprite bulletIcon;
    //private UILabel bulletNum;

    private static  UISlider pressureSlider;
    private static  UISlider playerBattlePressureSlider;
    private static GameObject roundStart;
    public BattleUIPanel()
    {
        m_Layer = Layer.city;
    }
   /// <summary>
   /// 
   /// </summary>
    protected override void OnStart()
    {

        //GameObject panel = GUIManager.FindPanel("BattleUIPanel");
        pressureSlider = this.GetChild("PressureSlider").GetComponent<UISlider>();
        playerBattlePressureSlider = this.GetChild("BattlePressureSlider").GetComponent<UISlider>();
        //BattleCommoUIManager.Instance.InitUI(panel);
        roundStart = this.GetChild("roundStart").gameObject;

    }

    protected override void OnShow()
    {

        //BattleCommoUIManager.Instance.UpdataSlider_Player();
    }

    protected override void OnHide()
    {
       


    }

    protected override void OnDestroy()
    {
        
    }

    public override void Update()
    {
        //BattleCommoUIManager.Instance.UpdataSlider_Player();
       
    }


    private void SetPressureValue()
    {
        CharacterPropBase playerData = CharacterPropManager.Instance.GetPlayerProp();
        CharacterPropBase currentData = CharacterPropManager.Instance.GetPlayerCureentProp();
        float value = currentData.Pressure / playerData.Pressure;
        
        pressureSlider.value = value;
    }
    
    public static void SetBattlePressSliderPos()
    {
        if (!playerBattlePressureSlider.gameObject.activeSelf)
        {
            playerBattlePressureSlider.value = 0;
            playerBattlePressureSlider.gameObject.SetActive(true);
        }
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 screenVec = Camera.main.WorldToScreenPoint(player.transform.position);
        screenVec.z = 0;
        Vector3 nguiTargetVec = UICamera.currentCamera.ScreenToWorldPoint(screenVec);
        nguiTargetVec = nguiTargetVec - new Vector3(0, 0.3f, 0);
        playerBattlePressureSlider.transform.position = nguiTargetVec;

    }


    #region 供外部调用的函数
    public static void RoundStart()
    {
        TweenAlpha tweenAlpha = roundStart.GetComponent<TweenAlpha>();
        tweenAlpha.enabled = true;
        tweenAlpha.from = 1;
        tweenAlpha.to = 0;
        tweenAlpha.onFinished.Clear();
        tweenAlpha.ignoreTimeScale = false;
        tweenAlpha.delay = 0;
        tweenAlpha.duration = 1.5f;
        tweenAlpha.ResetToBeginning();

        TweenScale tweenScale = roundStart.GetComponent<TweenScale>();
        tweenScale.enabled = true;
        tweenScale.from = new Vector3 (1,1,1);
        tweenScale.to = new Vector3 (1.5f,1.5f,1.5f);
        tweenScale.onFinished.Clear();
        tweenScale.ignoreTimeScale = false;
        tweenScale.delay = 0;
        tweenScale.duration = 1.5f;
        tweenScale.ResetToBeginning();
    }
   

    public static void ShowChangePlayerBattlePressureSlider(float changeValue, PropEventDelegate hander = null)
    {
        SetBattlePressSliderPos();
        CharacterPropBase propData = CharacterPropManager.Instance.GetPlayerProp();
        float needvalue = changeValue / propData.Pressure;
        if (needvalue != 0)
        {
            IEnumeratorManager.Instance.StartCoroutine(ChangePressureSlider_IEnumerator_PlayerBattle(needvalue, hander));
        }
      
    }

    public static void ShowChangePressureSlider(PropEventDelegate hander=null)
    {
        CharacterPropBase propData = CharacterPropManager.Instance.GetPlayerProp();
        CharacterPropBase currentData = CharacterPropManager.Instance.GetPlayerCureentProp();
        float sliderValue = currentData.Pressure / propData.Pressure;
        float needvalue = sliderValue - pressureSlider.value;
        pressureSlider.gameObject.SetActive(true);
        TweenAlpha ta = pressureSlider.GetComponent<TweenAlpha>();
        ta.onFinished.Clear();
        ta.enabled = true;
        ta.ResetToBeginning();
        IEnumeratorManager.Instance.StartCoroutine(ChangePressureSlider_IEnumerator(needvalue,hander));
    }

    private static IEnumerator ChangePressureSlider_IEnumerator(float needValue,PropEventDelegate hander=null)
    {

        float time = 10.0f * Mathf.Abs(needValue);
        int count = (int)(time / 0.02f);
        float rate = needValue / count;

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.02f);
            pressureSlider.value += rate;
            if (i == count - 1)
            {
                if (hander != null)
                {
               
                    yield return new WaitForSeconds(1.0f);
                    pressureSlider.gameObject.SetActive(false);
                    hander();
                    hander = null;
                }
            }
        }
    }

    private static IEnumerator ChangePressureSlider_IEnumerator_PlayerBattle(float needValue, PropEventDelegate hander = null)
    {
        float time = 10.0f * Mathf.Abs(needValue);
        int count = (int)(time / 0.02f);
        float rate = needValue / count;

        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(0.02f);
            playerBattlePressureSlider.value += rate;
            if (i == count - 1)
            {
                if (hander != null)
                {

                    yield return new WaitForSeconds(1.0f);
                    hander();
                    hander = null;
                }
            }
        }
    }

    #endregion
}
