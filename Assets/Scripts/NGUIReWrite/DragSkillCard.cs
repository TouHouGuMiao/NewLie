using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSkillCard : UIDragObject {
    public static DragSkillCard current;
    private Vector3 startPos;
    public float finshed_y = 0;
    public List<EventDelegate> OnCardDragFished;
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
            if (gameObject.transform.localPosition.y >= finshed_y)
            {
                if (OnCardDragFished != null)
                {
                    current = this;
                    EventDelegate.Execute(OnCardDragFished);
                    current = null;
                }
            }
            transform.localPosition = startPos;
        }
    }



}
