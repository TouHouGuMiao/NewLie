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

        //BattleCommoUIManager.Instance.InitUI(panel);
    }

    protected override void OnShow()
    {
        SetPressureValue();
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
        float value = currentData.preesure / playerData.preesure;
        
        pressureSlider.value = value;
    }



    #region 供外部调用的函数
    public static void ShowChangePressureSlider(PropEventDelegate hander=null)
    {
        CharacterPropBase propData = CharacterPropManager.Instance.GetPlayerProp();
        CharacterPropBase currentData = CharacterPropManager.Instance.GetPlayerCureentProp();
        float sliderValue = currentData.preesure / propData.preesure;
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
    #endregion
}
