using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSkillCard : UIDragObject {
    public static DragSkillCard current;
    public Vector3 startPos;
    public float finshed_y = 0;
    public List<EventDelegate> OnCardDragFished;
    public List<EventDelegate> OnDragStart;
    public DragDenpent dragDenpent = DragDenpent.byY;
    public string keyCodeName;
    public enum DragDenpent
    {
        byY,
        byLockKeyCode,
    }
    private void Awake()
    {

        target = this.transform;
    }

    private void Start()
    {
        
    }

    protected override void OnPress(bool pressed)
    {
        base.OnPress(pressed);
        if (GetMPress())
        {
            startPos = this.transform.localPosition;
            if (OnDragStart != null)
            {
                current = this;
                EventDelegate.Execute(OnDragStart);
                current = null;
            }
        }
        else
        {
            if(dragDenpent == DragDenpent.byY)
            {
                if (gameObject.transform.localPosition.y >= finshed_y)
                {
                    if (OnCardDragFished != null)
                    {
                        current = this;
                        EventDelegate.Execute(OnCardDragFished);
                        current = null;
                    }
                    else
                    {
                        transform.localPosition = startPos;
                    }
                }
            }
            else if(dragDenpent == DragDenpent.byLockKeyCode)
            {
                if (OnCardDragFished != null)
                {
                    current = this;
                    EventDelegate.Execute(OnCardDragFished);
                    current = null;
                }
                else
                {
                    transform.localPosition = startPos;
                }
            }
        }
    }

    public void ReturnStartVec()
    {
        transform.localPosition = startPos;
    }

    void OnTriggerStay(Collider collider) 
    {
        if (collider.gameObject.tag == "BattleCard")
        {
            return;
        }
        keyCodeName = collider.name;

    }


}
