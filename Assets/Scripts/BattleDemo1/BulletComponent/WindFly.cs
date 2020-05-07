using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindRank
{
    No,
    D,
    C,
    B,
    A,
}
public class WindFly : MonoBehaviour {
   
    public WindRank rank = WindRank.D;
}
