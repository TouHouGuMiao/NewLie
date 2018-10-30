using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : CharacterPropBase
{
    private GameObject player;
    private Animator m_Animator;
    private bool isMove=false;
    private float move_Dir=0;
    private GameObject bulletPrefab;

    private float dealTime = 0;
    private float thinkTime = 0;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        m_Animator = transform.GetComponent<Animator>();
        bulletPrefab = ResourcesManager.Instance.LoadBullet("carrotBullet");
    }

    private void Start()
    {
        StartCoroutine(RabbitThink());
    }


    private void Update()
    {
        if (isMove == true)
        {
            StartMove();
        }
    }

    /// <summary>
    /// AI的思考，基本功能：0.5秒思考,1s执行。
    /// 
    /// 
    ///                     附：若动画可以增加，可以添加 根据自身血量，玩家血量而做出不同反应
    /// </summary>
    /// <returns></returns>
    IEnumerator RabbitThink()
    {
        

        isMove = false;
        m_Animator.SetBool("Move", false);
        m_Animator.SetBool("PuGong", false);

        if (m_Animator.GetBool("happy"))
        {
            StopAllCoroutines();
        }
        yield return new WaitForSeconds(thinkTime);
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance >= 4)
        {
            m_Animator.SetBool("PuGong", true);
            thinkTime = 2;
        }

        else
        {
            move_Dir = transform.position.x - player.transform.position.x;
            isMove = true;
            m_Animator.SetBool("Move", true);
            dealTime = 1.5f;
            thinkTime = 0.5f;
        }


        yield return new WaitForSeconds(dealTime);
        StartCoroutine(RabbitThink());
    }

    void StartMove()
    {
        if (move_Dir > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }

        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        }
        AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Base Layer.move"))
        {
            transform.Translate(1.0f * Time.deltaTime * 5, 0, 0, Space.Self);
        }
    
    }



    private void UseAttackWithCarrot()
    {
        GameObject go = Instantiate(bulletPrefab);
        ParaCurveBullet script = go.GetComponent<ParaCurveBullet>();
        script.isSpecialBullet = true;
        script.targetPos = player.transform.position;
        go.transform.position = transform.position;
        go.name = "4";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("carrotBullet_Player"))
        {
            m_Animator.SetBool("Move", false);
            m_Animator.SetBool("Pugong", false);
            m_Animator.SetBool("happy", true);
            Destroy(other.gameObject);
        }
    }


    public void RabbitHappyEscape()
    {
        m_Animator.SetBool("happy", false);
        Destroy(this.gameObject,0.5f);
        
    }




}
