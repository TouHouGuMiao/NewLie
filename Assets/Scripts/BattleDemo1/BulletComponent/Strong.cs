using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StoryRank
{
    No,
    D,
    C,
    B,
    A,
}
public class Strong : MonoBehaviour {

    public StoryRank rank = StoryRank.D;
}
