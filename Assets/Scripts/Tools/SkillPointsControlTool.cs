using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointsControlTool : MonoBehaviour {
   
    private UIButton btn;
    private UILabel showPoints;
    
    int skillPoints = 10;
    
    int pointts = SkillPanel.skillPoints;
    int currentpoints = 0;
    void Start () {
        btn = gameObject.GetComponent<UIButton>();
        showPoints = gameObject.GetComponent<UILabel>();
        showPoints.text = currentpoints.ToString();
       
	}
    void Update()
    {
        if (btn.button_type == UIButton.button_Class.add_button)
        {
            AddPoints();
        }
        if (btn.button_type == UIButton.button_Class.minus_button)
        {
            MinusPoints();
        }
    }
    void AddPoints()
    {
        
            if (pointts == 0)
            {
                return;
            }
            else {
                currentpoints++;
                showPoints.text = currentpoints.ToString();
               
            }
        
    }
    void MinusPoints() {
       
            if (currentpoints == 0)
            {
                Debug.LogError("cant minus points");
            }
            else
            {
                currentpoints--;               
                showPoints.text = currentpoints.ToString();                
            }

       
    }
    
	
}
