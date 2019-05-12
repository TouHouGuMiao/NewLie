using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSkillCard : UIDragObject {

    private Vector3 startPos;
    private void Awake()
    {
        target = this.transform;
    }

    private void Start()
    {
        startPos = this.transform.localPosition;
    }

    protected override void OnPress(bool pressed)
    {
        base.OnPress(pressed);
        if (GetMPress())
        {
            
        }
        else
        {
            transform.localPosition = startPos;
        }
    }



}
