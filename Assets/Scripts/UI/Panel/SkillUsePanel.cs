using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsePanel : IView
{
    private enum ArrowState
    {
        moveToUp=1,
        moveToDown=2,
    }

    private ArrowState arrowState = ArrowState.moveToUp;
    private List<Skill> skillList = new List<Skill>();
    private float radius=1.0f;
    private Transform cardWidget;
    private Transform arrow;
    protected override void OnStart()
    {
        arrow = this.GetChild("arrow");
        UIButton arrowBtn = arrow.GetComponent<UIButton>();
        arrowBtn.onClick.Add(new EventDelegate(OnUpArrowBtnClick));
        cardWidget = this.GetChild("CardWidget");
    }

    protected override void OnShow()
    {
        skillList = SkillManager.Instance.CanUseSkillList();
        SetSkillCardPos();
    }

    void SetSkillCardPos()
    {
        int count = skillList.Count;
        Vector2 centerPoint = new Vector2(0, 0);
        if (count == 0)
        {
            Debug.LogError("skill Count has error");
            return;
        }
        float offset_x = 3.0f / (count-1);
        float time_x = 1.0f / (count-1);
        for (int i = 0; i < skillList.Count; i++)
        {
            GameObject go = GameObject.Instantiate(skillList[i].cardPrefab);
            go.transform.SetParent(this.GetChild("CardWidget"), false);
            go.name = skillList[i].data.Name;
            float sphere_x;
            if (i == count - 1)
            {
                sphere_x = 0.5f;
            }
            else
            {
                sphere_x = -0.5f + i * time_x;
            }
         
            float sphere_y = Mathf.Sqrt((1 - (sphere_x * sphere_x)));
            float go_y = -5.0f / sphere_y;
            Vector2 vecPoint = new Vector2(sphere_x,sphere_y);
            Vector2 oppositeVec = vecPoint-centerPoint ;
            float angle = Mathf.Atan2(oppositeVec.x, oppositeVec.y) * Mathf.Rad2Deg;

            go.transform.rotation = Quaternion.Euler(go.transform.rotation.x, 180, angle);
            go.transform.localPosition = new Vector3(-1.5f+ i * offset_x, go_y, 5 + i * 0.04f);
        }
    }

    protected override void OnDestroy()
    {
     
    }

    protected override void OnHide()
    {
       
    }


    void OnUpArrowBtnClick()
    {
        TweenPosition arrow_Tp = arrow.GetComponent<TweenPosition>();
        TweenRotation arrow_RT = arrow.GetComponent<TweenRotation>();
        TweenPosition cardWidget_Tp = cardWidget.GetComponent<TweenPosition>();
        arrow_Tp.enabled = true;
        cardWidget_Tp.enabled = true;
        arrow_RT.enabled = true;
        if (arrowState== ArrowState.moveToUp)
        {
            arrow_Tp.from = arrow_Tp.transform.localPosition;
            arrow_Tp.to = new Vector3(0, -183.9f, 0);
            arrowState = ArrowState.moveToDown;

            cardWidget_Tp.from = cardWidget_Tp.transform.localPosition;
            cardWidget_Tp.to = new Vector3(0, 2, 0);

            arrow_RT.from = arrow_RT.transform.rotation.eulerAngles;
            arrow_RT.to = new Vector3(0, 0, 180);
        }
        else if(arrowState == ArrowState.moveToDown)
        {
            arrow_Tp.from = arrow_Tp.transform.localPosition;
            arrow_Tp.to = new Vector3(0, -518.94f, 0);
            arrowState = ArrowState.moveToUp;


            cardWidget_Tp.from = cardWidget_Tp.transform.localPosition;
            cardWidget_Tp.to = new Vector3(0, -1, 0);


            arrow_RT.from = arrow_RT.transform.rotation.eulerAngles;
            arrow_RT.to = new Vector3(0, 0, 0);
        }
        arrow_Tp.ResetToBeginning();
        cardWidget_Tp.ResetToBeginning();
        arrow_RT.ResetToBeginning();
    }

}
