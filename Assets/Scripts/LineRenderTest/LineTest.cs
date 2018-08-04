using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour {

    private LineRenderer line;
    public Material testMaterial;
    public Transform testPos;
	// Use this for initialization
	void Start () {
        line = this.gameObject.GetComponent<LineRenderer>();
        line.material = testMaterial;
        line.numPositions = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, testPos.position);
        
	}
	
	// Update is called once per frame
	void Update () {
    
    }
}
