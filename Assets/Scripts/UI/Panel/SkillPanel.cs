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
    
    int currentPoints = 0;


    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnStart()
    {
        points = this.GetChild("points").GetComponent<UILabel>();
        canAddPoints = this.GetChild("canAddPoints").GetComponent<UILabel>();
        addPointsBtn = this.GetChild("addPoints").GetComponent<UIButton>();
        minusPointsBtn = this.GetChild("minusPoints").GetComponent<UIButton>();
        points.text = currentPoints.ToString();
        canAddPoints.text = skillPoints.ToString();
        addPointsBtn.onClick.Add(new EventDelegate(OnClickAddPointsBtn));
        minusPointsBtn.onClick.Add(new EventDelegate(OnClickMinusPointsBtn));
    }
    int pointts = SkillPanel.skillPoints;
    void OnClickAddPointsBtn() {
       
        if (pointts == 0)
        {
            Debug.Log("技能点数不够");
        }
        else {
            currentPoints++;
            points.text = currentPoints.ToString();
            if (pointts > 0)
            {
                pointts--;
                canAddPoints.text = pointts.ToString();
            }
            else {
                return;
            }
        }
    }
    void OnClickMinusPointsBtn() {
        
        if (currentPoints == 0)
        {
            Debug.Log("cant minus points");
        }
        else {
            currentPoints--;
            pointts++;
            points.text = currentPoints.ToString();
            if (pointts <= skillPoints) {
                canAddPoints.text = pointts.ToString();
            }
            else {
                return;
            }
        }
       
    }
}
