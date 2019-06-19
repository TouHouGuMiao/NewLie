using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChoiceMark : MonoBehaviour {
    private UIButton m_Btn;
    private GameObject ChoiceMark;
    private GameObject go;

    private bool isHover;
	// Use this for initialization
	void Start () {
      ChoiceMark = LoginPanel.GameChoice.transform.Find("markChoice").gameObject;
       
      //  ChoiceMark = LoginPanel.GameChoice.transform.GetChild(1).gameObject;
        m_Btn = gameObject.GetComponent<UIButton>();
        //Debug.LogError(11111);
    }
    void Play_EffectMusic() {
        AudioManager.Instance.PlayEffect_Source("hover_2");      
    }
	// Update is called once per frame
	void Update () {
       // Debug.Log(111);
       
            if (m_Btn.state == UIButtonColor.State.Hover)
            {
            if (go == null)
            {
                if (isHover == true)
                {
                    Play_EffectMusic();
                    InitChoiceMark();
                    isHover = false;
                }
            }
            else
            {
                if (isHover == true)
                {
                    Play_EffectMusic();
                    go.transform.position = m_Btn.transform.position;
                    go.SetActive(true);
                    isHover = false;
                }
            }
            }
            else if (m_Btn.state == UIButtonColor.State.Normal){
            isHover = true;
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
