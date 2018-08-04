using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KoishiControl : CharacterBase
{
    private ParticleSystem m_pariSystem;
    private ParticleSystem puGongEffect;
    private float skillAngle = 5;
    private KoishiPuGong puGong;
    private bool isIdelToUseSkill
    {
        get
        {
            AnimatorStateInfo stateInfo = m_Aniamtor.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("Base Layer.Idle") && !m_Aniamtor.IsInTransition(0))
            {
                return true;
            }
            return false;
        }
    }

  

    protected override bool NeedMove
    {
        get
        {
            if (target == null) return false;

            float distance = Vector2.Distance(transform.position,target.transform.position );
            Vector2 InitVec = transform.InverseTransformPoint(target.position);
            float angle = Mathf.Atan2(InitVec.y, InitVec.x) * Mathf.Rad2Deg;
            angle = Mathf.Abs(angle);
            if (distance>radious/*||angle>skillAngle*/)
            {
                return true;
            }
            return false;
        }
    }
    protected override void Start()
    {
        radious = 10; 
        KoishiSkillManager.Instance.Init();
        m_pariSystem = this.transform.FindRecursively("effect").GetComponent<ParticleSystem>();
        puGongEffect = this.transform.FindRecursively("PuGongEffect").GetComponent<ParticleSystem>();
        puGong = new KoishiPuGong();
        puGong.Init();
        shotPoint = transform.Find("point");
    
        m_Aniamtor = this.gameObject.GetComponent<Animator>();
        CapsuleCollider m_collider = this.gameObject.AddComponent<CapsuleCollider>();
        m_collider.radius = 0.5f;
        m_collider.height = 1.8f;
      
    }

    protected override void Update()
    {

        base.Update();
    
    }

    protected override void FightStateUpdata()
    {
        base.FightStateUpdata();
    }


    protected override void TryAttack()
    {
        animatorName = "PuGong";
        base.TryAttack();
    }


    protected override Transform FindTargetInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 100, 1 << LayerMask.NameToLayer("empty"));
        List<Collider> distanceList = new List<Collider>();
        foreach (Collider item in colliders)
        {
            if (item == null) continue;

            if (item.gameObject == this.gameObject) continue;

            distanceList.Add(item);
        }
        if (distanceList.Count == 0)
        {
            return null;
        }

        for (int i = 0; i < distanceList.Count; i++)
        {

            for (int j = i + 1; j < distanceList.Count; j++)
            {
                float distance_i = Vector2.Distance(transform.position, distanceList[i].transform.position);
                float distance_j = Vector2.Distance(transform.position, distanceList[j].transform.position);
                if (distance_i > distance_j)
                {
                    Collider tempCollider = new Collider();
                    tempCollider = distanceList[i];
                    distanceList[i] = distanceList[j];
                    distanceList[j] = tempCollider;
                }
            }
        }
        return distanceList[0].transform;
    }


    protected override void OnAnimatorMove()
    {

        base.OnAnimatorMove();
    }

    protected override void HeroMove()
    {
        base.HeroMove();
    }



    protected override void ShowHeroPuGong()
    {
        //GameObject effect = Instantiate(puGongEffect.gameObject) as GameObject;
        //effect.transform.position = m_pariSystem.transform.position;
        //effect.SetActive(true);
        //GameObject.Destroy(effect, 8);
        int sign = 0;
        if (target == null)
        {
            return;
        }
        Vector2 targetVec = target.transform.position - transform.position;
        if (targetVec.x > 0)
        {
            sign = 1;
        }
        else sign = -1;

        puGong.Show(shotPoint, bulletParent, target.transform, sign);
        m_Aniamtor.SetBool("PuGong", false);
    }
    protected override void ShowHeroSkillOne()
    {
        GameObject effect = Instantiate(m_pariSystem.gameObject) as GameObject;
        effect.transform.position = m_pariSystem.transform.position;
        effect.SetActive(true);
        GameObject.Destroy(effect, 8);
        if (target == null)
        {
            return;
        }
        int sign = 0;
        Vector2 targetVec = target.transform.position - transform.position;
        if (targetVec.x > 0)
        {
            sign = 1;
        }
        else sign = -1;
 
        KoishiSkillManager.Instance.ShowSkillOne(shotPoint, bulletParent, name,sign,target.transform);
        m_Aniamtor.SetBool("skill1", false);
    }
}


