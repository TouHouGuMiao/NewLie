using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Transform shotPoint;

    public float radious = 3;
    public Transform target;
    protected Animator m_Aniamtor;
    public float speed;
    public Transform bulletParent;
    protected string animatorName;

    private bool isIdelToUseSkill
    {
        get
        {
            AnimatorStateInfo stateInfo = m_Aniamtor.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Base Layer.Idle") && !m_Aniamtor.IsInTransition(0))
            {
                return true;
            }
            return false;
        }
    }

    protected virtual bool NeedMove
    {
        get
        {
            if (target == null) return false;

            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance > radious)
            {
                return true;
            }
            return false;
        }
    }
    protected virtual void Start()
    {

        shotPoint = transform.Find("point");
        m_Aniamtor = this.gameObject.GetComponent<Animator>();
        CapsuleCollider m_collider = this.gameObject.AddComponent<CapsuleCollider>();
        m_collider.radius = 0.5f;
        m_collider.height = 1.8f;
    }

    protected virtual void Update()
    {

        FightStateUpdata();
    }

    protected virtual void FightStateUpdata()
    {
        if (target == null)
        {
            target = FindTargetInRadius();
        }

        if (target != null)
        {

            TryAttack();
        }
    }


    protected virtual void TryAttack()
    {
    
        if (Vector2.Distance(transform.position, target.transform.position) > radious)
        {
            m_Aniamtor.SetBool(animatorName, false);
            return;
        }
        m_Aniamtor.SetBool(animatorName, true);

    }


    protected virtual Transform FindTargetInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100, 1 << LayerMask.NameToLayer("Warrior"));
        foreach (Collider item in colliders)
        {
            if (item == null) continue;

            if (item.name == this.gameObject.name) continue;

            return item.transform;

        }

        return null;
    }


    protected virtual void OnAnimatorMove()
    {

        if (NeedMove)
        {
            if (!m_Aniamtor.GetBool("Move"))
            {
                m_Aniamtor.SetBool("Move", true);
            }
            AnimatorStateInfo stateInfo = m_Aniamtor.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Base Layer.move") && !m_Aniamtor.IsInTransition(0))
            {
                HeroMove();
            }
        }

        else
        {
            if (m_Aniamtor.GetBool("Move"))
            {
                m_Aniamtor.SetBool("Move", false);
            }
        }
    }

    protected virtual void HeroMove()
    {
        Vector2 lookat = target.transform.position - transform.position;
        lookat = lookat.normalized;
        Vector3 forward = transform.forward;
        float z = forward.z;
        if ((z == 1 && lookat.x < 0) || (z == -1 && lookat.x > 0))
        {

            this.gameObject.transform.Rotate(0, 180, 0);
        }
        Vector2 startPos = this.gameObject.transform.position;
        Vector2 endPos = target.gameObject.transform.position ;

        this.gameObject.transform.position = Vector2.Lerp(startPos, endPos, Time.deltaTime * speed);
    }



    protected virtual void ShowHeroPuGong()
    {
    
    }
    protected virtual void ShowHeroSkillOne()
    {
     
    }
}
