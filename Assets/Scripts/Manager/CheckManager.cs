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
    public static bool isSucceced = false;//记录Check是否成功
    /*id 用来传递要使用的技能*/
    public void ShowCheckPanel(int id,DiceHander hander,CheckLevel level) {
        Skill s = SkillManager.Instance.GetSkillDataById(id);
        CheckPanel.s_Check = s;
        CheckPanel.hander = hander;
        CheckPanel.level_CheckPanel = level;
        GUIManager.ShowView("CheckPanel");       
        
    }
}
