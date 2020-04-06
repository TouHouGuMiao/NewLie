using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeBase : MonoBehaviour {
    public float strength;
    public float quick;
    public float life;
    public float luck;
    public float magic;
    public int level;

    public float MaxHP 
    {
        get
        {
            return ((level *20)*strength+200);
        }
    }

    public float MaxMP 
    {
        get
        {
            return ((level * 10) * magic + 100);
        }
    }

}
