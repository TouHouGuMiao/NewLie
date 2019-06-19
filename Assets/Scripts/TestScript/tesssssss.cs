using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesssssss : MonoBehaviour {
    public GameObject go;
	// Use this for initialization
	void Start () {
        Debug.LogError(IsHaveUIButtonComponent(go));      
	}
    bool IsHaveUIButtonComponent(GameObject go)
    {
        if (go.GetComponent<UIButton>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
