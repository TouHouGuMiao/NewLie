using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGamStarBunHover :MonoBehaviour{
  
    UIButton button;
    private Vector3 v1 = new Vector3(1, 1, 1);
    private Vector3 v2 = new Vector3(1.5f, 1.6f, 1);
   
    private TweenScale ts;
    
    // Use this for initialization
    void Start () {
       
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
                }
                else  if(ts.to != v2)
                {
                   
                    Big();
                }
               
            }
            else if (button.state == UIButtonColor.State.Normal)
            {
                TweenScale ts = this.GetComponent<TweenScale>();
                if (ts != null&&ts.to!=v1)
                {
                   
                    Back();
                }
                
            }
        }
      
	}

    void Init()
    {
       ts= gameObject.AddComponent<TweenScale>();
   
       ts.from = v1;
       ts.to = v2;
       ts.duration = 0.8f;
       ts.ignoreTimeScale = false;
       ts.delay =0.0f;

    }
    void Big() {
       ts.enabled = true;
       ts.from = this.transform.localScale;
       ts.to = v2;
       ts.duration =0.8f;
       ts.ResetToBeginning();

    }
    void Back() {
       ts.enabled = true;
       ts.from = this.transform.localScale;
       ts.to = v1;
        ts.duration = 0.8f;
       ts.ResetToBeginning();

    }
   
    
}
