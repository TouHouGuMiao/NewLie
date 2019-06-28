using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckManager  {
    private static CheckManager _Instance=null;
    public static CheckManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new CheckManager();
            }
            return _Instance;
        }
    }
    /*id 用来传递要使用的技能*/
    public void ShowCheckPanel(int id,DiceHander hander) {
        Skill s = SkillManager.Instance.GetSkillById(id);
        CheckPanel.s_Check = s;
        CheckPanel.hander = hander;
        GUIManager.ShowView("CheckPanel");
    }
}
