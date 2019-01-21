using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager {

	// Use this for initialization
	private static ButtonManager _Instance=null;
    private List<UIButton> ButtonList = new List<UIButton>();
    private Dictionary<int, List<UIButton>> M_ButtonDic = new Dictionary<int, List<UIButton>>(); 

    public static ButtonManager Instance
    {
        get {
            if (_Instance == null) {
                _Instance = new ButtonManager();
            }
            return _Instance;
        }

    }
    List<UIButton> buttonlist = new List<UIButton>();
    public List<UIButton> Add_Btn(UIButton button) {
       
        
        buttonlist.Add(button);

        return buttonlist;
    }

    public List<UIButton> Remove_Btn(UIButton button) {
       
        buttonlist = ButtonList;
        buttonlist.Remove(button);
        return ButtonList;
    }

}
