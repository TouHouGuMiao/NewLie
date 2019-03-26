using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGamStarBunHover :MonoBehaviour{
  
    private UIButton button;
    private Vector3 v1 = new Vector3(1, 1, 1);
    private Vector3 v2 = new Vector3(1.5f, 1.6f, 1);
   // private GameObject markChoice;
    private TweenScale ts;
   // private TweenScale tsMark;
    
    // Use this for initialization
    void Start () {
        //markChoice = LoginPanel.GameChoice.transform.Find("markChoice").gameObject;
        button = gameObject.GetComponent<UIButton>();
       
     
	}

    // Update is called once per frame
    void Update () {

        if (button != null)
        {
            if (button.state == UIButtonColor.State.Hover)
            {
                
                TweenScale ts = this.GetComponent<TweenScale>();
                if (ts == null)
                {
                    Init();
                    //ShowChoice();
                }
                else  if(ts.to != v2)
                {
                   
                    Big();
                   // ShowChoice();
                }
               
            }
            else if (button.state == UIButtonColor.State.Normal)
            {
               
                TweenScale ts = this.GetComponent<TweenScale>();
                if (ts != null&&ts.to!=v1)
                {
                   
                    Back();
                    //HideChoice();
                }
                
            }
        }
      
	}

    void Init()
    {
        InitBtn();
        //InitMark();
    }
    void Big() {
        BigBtn();
       // BigMark();
    }
    void Back() {
        BackBtn();
        //BackMark();
    }
   //void ShowChoice()
   // {
   //     if (markChoice.activeInHierarchy == false)
   //     {
   //         markChoice.transform.position = button.transform.position;
   //         markChoice.SetActive(true);
   //     }
   //     else
   //     {
   //         markChoice.SetActive(false);
   //     }

   // }
    //void HideChoice()
    //{
    //    if(markChoice.activeInHierarchy == true)
    //    {
    //        markChoice.SetActive(false);
    //    }
    //}
    void BackBtn() {
        ts.enabled = true;
        ts.from = this.transform.localScale;
        ts.to = v1;
        ts.duration = 0.8f;
        ts.ResetToBeginning();
    }
    //void BackMark() {
    //    tsMark.enabled = true;
    //    tsMark.from = this.transform.localScale;
    //    tsMark.to = v1;
    //    tsMark.duration = 0.8f;
    //    tsMark.ResetToBeginning();
    //}
    void InitBtn() {
        ts = gameObject.AddComponent<TweenScale>();
        ts.from = v1;
        ts.to = v2;
        ts.duration = 0.8f;
        ts.ignoreTimeScale = false;
        ts.delay = 0.0f;
    }
    //void InitMark() {
    //    tsMark = markChoice.AddComponent<TweenScale>();
    //    tsMark.from = v1;
    //    tsMark.to = v2;
    //    tsMark.duration = 0.8f;
    //    tsMark.ignoreTimeScale = false;
    //    tsMark.delay = 0.0f;
    //}
    void BigBtn() {
        ts.enabled = true;
        ts.from = this.transform.localScale;
        ts.to = v2;
        ts.duration = 0.8f;
        ts.ResetToBeginning();
    }
    //void BigMark() {
    //    tsMark.enabled = true;
    //    tsMark.from = this.transform.localScale;
    //    tsMark.to = v2;
    //    tsMark.duration = 0.8f;
    //    tsMark.ResetToBeginning();
    //}
    
}
