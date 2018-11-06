using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void ChoseHandler();
public class ChoseData
{


    public List<ChoseHandler> HanderList=new List<ChoseHandler>();
    public List<string> ChoseDesList = new List<string>();

  
    public int Id
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }



}
