using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryData
{
    public int id { get; set; }

    public int state { get; set; }

    public string name { get; set; }

    public int index { get; set; }

    public int cout { get; set; }

    public List<string> SpeakList = new List<string>();
}
