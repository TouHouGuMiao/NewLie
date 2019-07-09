using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverRotate : MonoBehaviour {
    private GameObject go;
    private UIButton m_Btn;
	// Use this for initialization
	void Start () {
        m_Btn = gameObject.GetComponent<UIButton>();
        if (SystemPanel.ChooseBtnContainer != null)
        {
            go = SystemPanel.ChooseBtnContainer.gameObject;
        }
        else {
             Debug.LogError("have not this gameObject");
            return;
        }
	}
    private bool isFinished;
	// Update is called once per frame
	void Update () {
       
        if (m_Btn.state == UIButton.State.Normal&& isFinished == true) {
            SureUpDateBtnBack();
            isFinished = false;        
        }
        else if (m_Btn.state == UIButton.State.Hover)
        {
            SureUpDateBtn();
            isFinished = true;
        }
    }
    void TweenRoate(GameObject go) {       
         go.transform.Rotate(Vector3.up *250* Time.deltaTime, Space.World);          
    }
    void BackNull(GameObject go) {
        go.transform.localRotation = Quaternion.Euler(0, 0, 0); 
       
    }
    void SureUpDateBtn() {
        if (m_Btn.name.Contains("0"))
        {
            GameObject m_Go = go.transform.GetChild(0).gameObject;
            TweenRoate(m_Go);
        }
        else if (m_Btn.name.Contains("1"))
        {
            GameObject m_Go = go.transform.GetChild(1).gameObject;
            TweenRoate(m_Go);
        }
        else if (m_Btn.name.Contains("2")) {
            GameObject m_Go = go.transform.GetChild(2).gameObject;
            TweenRoate(m_Go);
        }
        else if (m_Btn.name.Contains("3"))
        {
            GameObject m_Go = go.transform.GetChild(3).gameObject;
            TweenRoate(m_Go);
        }
    }
    void SureUpDateBtnBack()
    {
        if (m_Btn.name.Contains("0"))
        {
            GameObject m_Go = go.transform.GetChild(0).gameObject;
            BackNull(m_Go);
        }
        else if (m_Btn.name.Contains("1"))
        {
            GameObject m_Go = go.transform.GetChild(1).gameObject;
            BackNull(m_Go);
        }
        else if (m_Btn.name.Contains("2"))
        {
            GameObject m_Go = go.transform.GetChild(2).gameObject;
            BackNull(m_Go);
        }
        else if (m_Btn.name.Contains("3"))
        {
            GameObject m_Go = go.transform.GetChild(3).gameObject;
            BackNull(m_Go);
        }
    }
}
