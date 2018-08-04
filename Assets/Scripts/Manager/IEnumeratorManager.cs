using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumeratorManager : MonoBehaviour
{
    public static IEnumeratorManager Instance;
    private void Awake()
    {
        Instance = this;   
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
