using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosePanel : IView
{
    private UIGrid grid;
    private GameObject panel;
    private GameObject item;
    public static ChoseData data;
    private int index=0;
    private static GameObject chosePanel;

    public static bool isChose
    {
        get
        {
            if (chosePanel == null)
            {
                return false;
            }

            if (chosePanel.activeSelf)
            {
                return true;
            }
            return false;
        }
    }
    public ChosePanel()
    {
        m_Layer = Layer.UI;
    }
   
    protected override void OnStart()
    {
        grid = this.GetChild("Grid").GetComponent<UIGrid>();
        panel = GUIManager.FindPanel("ChosePanel");
        item = this.GetChild("item").gameObject;
        chosePanel = GUIManager.FindPanel("ChosePanel");
    }
    protected override void OnShow()
    {

        if (!InputPanel.IsInput)
        {
            panel.transform.localPosition = new Vector3(-324, 47);
        }

        else
        {

            panel.transform.localPosition = new Vector3(-324, -217);


        }

        UpdataItemData();
    }
    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
        index = 0;
        data = null;
    }

    public override void Update()
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            GameObject go = grid.transform.GetChild(i).gameObject;
            UIButton button = go.GetComponent<UIButton>();
            if (i == index)
            {
                button.SetState(UIButtonColor.State.Normal, false);
                button.SetState(UIButtonColor.State.Hover, true);
            }

            else
            {
                button.SetState(UIButtonColor.State.Hover, false);
                button.SetState(UIButtonColor.State.Normal, true);
            }
        }

        if (InputPanel.IsInput)
        {
            if (!InputPanel.isChange)
            {
                index = -1;
                return;

            }
        }
      
        else
        {

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
               
                if (index >=data.HanderDic.Count-1)
                {
                    return;
                }
                index++;

            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                index--;
                if (index < 0)
                {
                    index = -1;
                    InputPanel.isChange = false;
                    return;
                }
            }

            

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (data != null)
                {
                    data.HanderDic[index]();
                }
                GUIManager.HideView("ChosePanel");
            }
        }
    }

    void UpdataItemData()
    {
        if (data == null)
        {
            return;
        }

        if (grid.transform.childCount <= data.ChoseDesList.Count)
        {
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                GameObject go = grid.transform.GetChild(i).gameObject;
              
                UILabel label = go.transform.Find("Label").GetComponent<UILabel>();
                label.text = data.ChoseDesList[i];
                go.name = data.Name;
                go.SetActive(true);
            }

            for (int i = grid.transform.childCount; i < data.ChoseDesList.Count; i++)
            {
                GameObject go = GameObject.Instantiate(item);
       
                UILabel label = go.transform.Find("Label").GetComponent<UILabel>();
                label.text = data.ChoseDesList[i];
                go.name = data.Name;
                go.transform.SetParent(grid.transform, false);
                go.SetActive(true);
            }
        }

        else
        {
            for (int i = grid.transform.childCount-1; i >= data.ChoseDesList.Count; i--) 
            {
                GameObject go = grid.transform.GetChild(i).gameObject;
                go.SetActive(false);
            }

            for (int i = 0; i < data.ChoseDesList.Count; i++)
            {
                GameObject go = grid.transform.GetChild(i).gameObject;

                UILabel label = go.transform.Find("Label").GetComponent<UILabel>();
                label.text = data.ChoseDesList[i];
                go.name = data.Name;
                go.SetActive(true);
            }
        }

        grid.Reposition();
    }
}


