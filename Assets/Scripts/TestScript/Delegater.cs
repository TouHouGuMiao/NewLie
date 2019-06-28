using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegater : MonoBehaviour {
  
    public delegate void TestDelegater();
    public delegate void BigSucce();
    public TestDelegater test;
	void Start () {
        AddDelegater();
        if (test != null) {
            
            test -= Show2;
            test();
        }
    }
    
    void AddDelegater() {
        test += Show1;
        test += Show2;
        
    }
    void Show1() {
        Debug.LogError("1");
    }
    void Show2() {
        Debug.LogError("2");
    }
    void Show3()
    {
        Debug.LogError("3");
    }
	void Update () {
		
	}
}
