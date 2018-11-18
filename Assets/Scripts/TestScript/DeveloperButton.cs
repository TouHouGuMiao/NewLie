using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DeveloperButton : MonoBehaviour
{
    UIButton bt;
    public Sprite pic;
    public GameObject go;
    private Vector3 v1, v2;
    // Use this for initialization
    void Start()
    {
       bt = this.GetComponent<UIButton>();
        
       
            OnMouseEnter();
      
          // OnMouseExit();
           

    }
   
    private void OnMouseEnter ()
    {
        go.AddComponent<TweenScale>();
        go.GetComponent<TweenScale>();
        // TweenScale s1 = new TweenScale();
        v1 = this.transform.position;
        v2 = new Vector3(3, 2, 1);
        go.GetComponent<TweenScale>().from = v1;
        go.GetComponent<TweenScale>().to = v2;
        go.GetComponent<TweenScale>().duration = 5.0f;
        go.GetComponent<TweenScale>().style = UITweener.Style.Once;
       // go.GetComponent<TweenScale>().delay = 1.0f;
        go.GetComponent<TweenScale>().PlayForward();
       
    }
    //private void OnMouseExit()
    //{
    //    go.GetComponent<TweenScale>().from = v1;
    //    go.GetComponent<TweenScale>().to = v2;
    //    go.GetComponent<TweenScale>().duration = 5.0f;
    //    go.GetComponent<TweenScale>().style = UITweener.Style.Once;
    //    go.GetComponent<TweenScale>().PlayForward();

    //}
    // Update is called once per frame
    void Update()
    {
      
      

    }
}

