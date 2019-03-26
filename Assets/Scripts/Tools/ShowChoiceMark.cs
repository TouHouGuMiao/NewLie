using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChoiceMark : MonoBehaviour {
    private UIButton m_Btn;
    private GameObject ChoiceMark;
    private GameObject go;
	// Use this for initialization
	void Start () {
      ChoiceMark = LoginPanel.GameChoice.transform.Find("markChoice").gameObject;
       
      //  ChoiceMark = LoginPanel.GameChoice.transform.GetChild(1).gameObject;
        m_Btn = gameObject.GetComponent<UIButton>();
        //Debug.LogError(11111);
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(111);
       
            if (m_Btn.state == UIButtonColor.State.Hover)
            {
            if (go == null)
            {
                InitChoiceMark();
            }
            else {
                go.transform.position = m_Btn.transform.position;
                go.SetActive(true);
            }
            }
            else if (m_Btn.state == UIButtonColor.State.Normal)
        {
            if (go == null)
            {
                InitHideChoiceMark();
                go.SetActive(false);
            }
            else {
                if (go.activeInHierarchy == true) {
                    go.SetActive(false);
                }
            }
                          
            }
        }
    void InitHideChoiceMark() {
        go = GameObject.Instantiate(ChoiceMark);
        go.transform.SetParent(m_Btn.transform);
        go.transform.position = m_Btn.transform.position;
        go.transform.rotation = m_Btn.transform.rotation;
        go.transform.localScale = m_Btn.transform.localScale;
        go.SetActive(false);
    }

   
   
    void InitChoiceMark() {
         go = GameObject.Instantiate(ChoiceMark);
        go.transform.SetParent(m_Btn.transform);
        go.transform.position = m_Btn.transform.position;
        go.transform.rotation = m_Btn.transform.rotation;
        go.transform.localScale = m_Btn.transform.localScale;               
        go.SetActive(true);
    }
}
