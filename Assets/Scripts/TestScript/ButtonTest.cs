using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.Integration;
using MathNet.Numerics;

public class ButtonTest : MonoBehaviour {

    public float a = 2;
    public float b = 1;

    public float up = 0;
    public float down = 0;
    private GameObject prefab;
	// Use this for initialization
	void Start () {
        prefab = ResourcesManager.Instance.LoadBullet("initBullet");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    float TestReslut()
    {
        float e_pow = (Mathf.Pow(a, 2) - Mathf.Pow(b, 2)) / Mathf.Pow(a, 2);
        float resulte =a*(float)Integrate.OnClosedInterval(x => Mathf.Sqrt(1 - e_pow * Mathf.Pow(Mathf.Cos((float)x), 2)),down * Mathf.Deg2Rad, up * Mathf.Deg2Rad);
        return Mathf.Abs(resulte);
    }

    public void TestIns()
    {
        GameObject go = Instantiate(prefab);
        go.transform.SetParent(GameObject.FindWithTag("Bullet").transform, true);
     
    }


}
