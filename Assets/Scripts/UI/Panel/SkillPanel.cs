using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : IView
{
    private UILabel points;
    private UILabel canAddPoints;
    private UIButton addPointsBtn;
    private UIButton minusPointsBtn;
    public static int skillPoints=10;//技能职业加点
    int pointts;
    int currentPoints = 0;

    
    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        
    }

    protected override void OnShow()
    {
         pointts = SkillPanel.skillPoints;
    }

    protected override void OnStart()
    {
        points = this.GetChild("points").GetComponent<UILabel>();
        canAddPoints = this.GetChild("canAddPoints").GetComponent<UILabel>();
        addPointsBtn = this.GetChild("addPoints").GetComponent<UIButton>();
        addPointsBtn.button_type = UIButton.button_Class.add_button;
        minusPointsBtn = this.GetChild("minusPoints").GetComponent<UIButton>();
        minusPointsBtn.button_type = UIButton.button_Class.minus_button;
        points.text = currentPoints.ToString();
        canAddPoints.text = skillPoints.ToString();
        AddDelegate();
        //addPointsBtn.onClick.Add(new EventDelegate(OnClickAddPointsBtn));
        //minusPointsBtn.onClick.Add(new EventDelegate(OnClickMinusPointsBtn));
    }
    
    void OnClickAddPointsBtn() {


        if (pointts == 0)
        {
            Debug.LogError("技能点数不够");
        }
        else
        {
            currentPoints++;
            points.text = currentPoints.ToString();
            if (pointts > 0)
            {
                pointts--;
                canAddPoints.text = pointts.ToString();
            }

        }
    }
    void OnClickMinusPointsBtn() {



        if (currentPoints == 0)
        {
            Debug.LogError("cant minus points");
        }
        else
        {
            currentPoints--;
            pointts++;
            points.text = currentPoints.ToString();
            if (pointts <= skillPoints)
            {
                canAddPoints.text = pointts.ToString();
            }

        }

    }
     void ChoiceAddOrMinus(UIButton button)
    {
        if (button.button_type == UIButton.button_Class.add_button)
        {
            OnClickAddPointsBtn();
        }
        else if (button.button_type == UIButton.button_Class.minus_button)
        {
            OnClickMinusPointsBtn();
        }
    }
    void AddDelegate() {
      addPointsBtn.skillBtnDelegate += ChoiceAddOrMinus;
      minusPointsBtn.skillBtnDelegate += ChoiceAddOrMinus;
    }
}
