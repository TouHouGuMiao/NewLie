using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckManager  {
    private static CheckManager _Instance=null;
    public static CheckManager Instacne {
        get {
            if (_Instance == null) {
                _Instance = new CheckManager();               
            }
            return _Instance;
        }
    } 
    
}
