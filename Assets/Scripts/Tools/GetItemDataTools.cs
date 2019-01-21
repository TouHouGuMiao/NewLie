using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemDataTools : MonoBehaviour {
    
   // public  GameObject go;
    public static int ItemID;//用于记录当前脚本所挂的物体的id
                             // Use this for initialization
    void Start()
    {
        
    }
	// Update is called once per frame
	void Update () {
        
    }
    public GameObject getCurrentGo() {
        GameObject go = this.gameObject;
        return go;
    }
    public void printName() {
        GameObject go = this.gameObject;

        Debug.Log(go.name);
    }
    public int getMyGameObjectID() {
        GameObject go = this.gameObject;
        ItemID = System.Int32.Parse(go.name);
        return ItemID;
    }
    //public GameObject getMyGameObject() {
    //   GameObject go = this.gameObject;
    //    return go;
    //}
   
}
