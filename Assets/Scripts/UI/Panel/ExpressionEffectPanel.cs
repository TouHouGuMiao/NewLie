using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionEffectPanel : IView
{
    private static TweenPosition pressEffect;
    protected override void OnStart()
    {
        pressEffect = this.GetChild("press").GetComponent<TweenPosition>();
    }

    protected override void OnShow()
    {

    }

    protected override void OnDestroy()
    {

    }

    protected override void OnHide()
    {
        
    }

    public static void ShowPressEffect(GameObject go,int pressValue)
    {
        

        UILabel label = pressEffect.transform.Find("Label").GetComponent<UILabel>();
        label.text = pressValue.ToString();
        Vector3 screenVec = Camera.main.WorldToScreenPoint(go.transform.position);
        screenVec.z = 0;
        Vector3 targetVec = UICamera.currentCamera.ScreenToWorldPoint(screenVec);
        targetVec = targetVec + new Vector3(-0.05F, 0.3f, 0);
        pressEffect.transform.position = targetVec;
        pressEffect.from = pressEffect.transform.localPosition;
        pressEffect.to = pressEffect.transform.localPosition - new Vector3(0, 100, 0);
        pressEffect.enabled = true;
        pressEffect.ResetToBeginning();

        TweenAlpha ta = pressEffect.gameObject.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.ResetToBeginning();
        AudioManager.Instance.PlayEffect_Source("press");
    }
   
}
