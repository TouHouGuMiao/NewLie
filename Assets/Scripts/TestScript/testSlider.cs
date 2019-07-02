using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSlider : MonoBehaviour {
    private UISlider sl;
    private UILabel lb;
	// Use this for initialization
	void Start () {
        sl = GameObject.Find("Slider").gameObject.GetComponent<UISlider>();
        lb = GameObject.Find("Label").gameObject.GetComponent<UILabel>();
        sl.onChange.Add(new EventDelegate(OnChange));
	}
    private void OnChange() {
        Debug.LogError(lb.text);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
