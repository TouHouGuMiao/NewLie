using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void ChoseHandler();
public class ChoseData
{


    public List<ChoseHandler> HanderList=new List<ChoseHandler>();
    public List<string> ChoseDesList = new List<string>();

  

    public string name
    {
        get;
        set;
    }



}
