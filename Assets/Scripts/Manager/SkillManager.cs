using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager {
    private static SkillManager _Instance = null;
    public static SkillManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new SkillManager();
               
            }
            return _Instance;
        }
    }

}
