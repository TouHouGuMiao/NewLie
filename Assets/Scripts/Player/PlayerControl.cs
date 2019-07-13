using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    attack=10,
    talk=20,
}


public class PlayerControl : CharacterPropBase {

    public static PlayerState state = PlayerState.talk;

    public static PlayerControl Instance;
    public float speed;

    public GameObject bulletPrefab;
    private GameObject BagPanel;

    private Animator m_Animator;
    private float rate_Z=0.5f;
    private float rate_X = 0.5f;

    private float tempTime_Z;
    private float tempTime_X;



    private Transform bulletCollider;

    #region 提供给外部对Animator的操作
    public void PauseAnimator()
    {
        m_Animator.Stop();
       
    }

    public void PlayPlayerSkill(string name)
    {
        m_Animator.Play(name);

    }

    public void PlayIdle()
    {
        m_Animator.Play("loopIdle", 0, 0);
    }


    #endregion



    //private GameObject systemPanel;//控制SystemPanel的GameObject

    //private TweenPosition m_TP1;
    //private TweenPosition m_TP2;

    private UISlider m_Slider;
    private float m_HP;  //表示血条现有HP,而不是HP属性

    private void Awake() 
    {
        Instance = this;
        //HandWithPlayer.Instance.Init(transform);
    }

    void Start ()
    {
        
       
       // FindBagPanel();
        // systemPanel=this.transform.GetChild("")
        m_Animator = this.GetComponent<Animator>();
        bulletPrefab = ResourcesManager.Instance.LoadBullet("initBullet");
        //GameObject yinYangYu1 = transform.FindRecursively("YinYangYu1").gameObject;
        //GameObject yinYangYu2 = transform.FindRecursively("YinYangYu2").gameObject;
        //m_TP1 = yinYangYu1.GetComponent<TweenPosition>();
        //m_TP2 = yinYangYu2.GetComponent<TweenPosition>();
        m_HP = HP;
        //TPEffectSet();
        //WingmanData data = new WingmanData();
        //data.bulletName = "StarBullet";
        //data.tempTime = 0.5f;
        //WingmanManager.Instance.ShowWingman(data, 6);
        UpDataPlayerPro();
    }
    public static bool isFirstEsc = false;
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&isFirstEsc==false) {

            GUIManager.ShowView("SystemPanel");
            //SystemPanel.SystemPanelIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isFirstEsc == true) {
            GUIManager.HideView("SystemPanel");
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SystemPanel.CardCollectionsIsActive == true) {
            GUIManager.HideView("CardCollectionsPanel");
            SystemPanel.CardCollectionsIsActive = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SystemPanel.Bg_IsActive == true) {
            GUIManager.HideView("BagPanel");
            SystemPanel.Bg_IsActive = false;
        }        
        //YinYangYuControl();
        //UpDataPlayerPro();//测试用
    }
    float deltaTime = 0;
    void CharacterControl()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (state == PlayerState.talk)
            {
                state = PlayerState.attack;
            }

            else
            {
                state = PlayerState.talk;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GUIManager.ShowView("SystemPanel");
        //}
        //if (Input.GetKeyDown(KeyCode.Escape)) {

        //    GUIManager.ShowView("SystemPanel");
        //}

        if (TalkPanel.isSpeak||EventStoryPanel.isEventSpeak||CGPanel.IsCGPlay||BattleCamera.Instance.isUseCamera)
        {
            return;
        }

        //if (BattleCommoUIManager.Instance.IsBlackShade)
        //{
        //    return;
        //}
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return;
            }
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 0, this.transform.rotation.eulerAngles.z);
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.loopIdle") || stateInfo.IsName("Base Layer.startIdle") || stateInfo.IsName("Base Layer.idle"))
            {
                m_Animator.SetBool("isWalk", true);
            }

            if (stateInfo.IsName("Base Layer.move"))
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.Self);
            }
        }




        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 180, this.transform.rotation.eulerAngles.z);
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Base Layer.loopIdle") || stateInfo.IsName("Base Layer.startIdle") || stateInfo.IsName("Base Layer.idle"))
            {
                m_Animator.SetBool("isWalk", true);
            }

            if (stateInfo.IsName("Base Layer.move"))
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed, Space.Self);
            }


        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            m_Animator.SetBool("isWalk", false);
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            m_Animator.SetBool("isWalk", false);
        }
    

        if (tempTime_Z < Time.time)
        {
            tempTime_Z = Time.time;
            tempTime_X = Time.time;
        }
       
        if (Input.GetKey(KeyCode.X))
        {
      

            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("Base Layer.Move")||stateInfo.IsName("Base Layer.idle"))
            {    
                m_Animator.SetBool("Move", false);
                m_Animator.SetBool("move", false);
                m_Animator.SetBool("Attack", true);
            }
        }

     


        if (Input.GetKey(KeyCode.Z) && Time.time >= tempTime_Z)
        {
            GameObject go = transform.Find("wingman").gameObject;
            GameObject item = go.transform.GetChild(0).gameObject;
            TweenPosition tp = item.GetComponent<TweenPosition>();
            tp.enabled = false;
            tp.transform.SetParent(null);
            tp.GetComponent<WingmanInfo>().enabled = false;


            item.AddComponent<Rigidbody>();
            item.GetComponent<Rigidbody>().useGravity = false;
            tp.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * 1500);

            tempTime_Z += rate_Z;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            WingmanData data = WingmanManager.Instance.GetDataById(0);
            WingmanManager.Instance.CreatWingmanReturnCout(data, 6);
        }
       
        
        
    }
    


#region 动画帧事件




    #endregion

    private void UpDataPlayerPro()
    {
        //CharacterPropBase baseData = CharacterPropManager.Instance.GetCharcaterDataByName("reimu");
        //if (baseData == null)
        //{
        //    Debug.LogError("baseData is null");
        //    return;
        //}
        //Power = baseData.Power;
        //VIT = baseData.VIT;
        //Lucky = baseData.Lucky;

    }
   
    //void YinYangYuControl()
    //{
    //    m_TP1.transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime, 10);
    //    m_TP2.transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime, 10);
    //}

    //#region   阴阳玉循环特效;
    //private void TP1PingPangOneStop()
    //{
    //    SpriteRenderer m_Render = m_TP1.GetComponent<SpriteRenderer>();
    //    m_Render.sortingLayerName = "Bounds";
    //}

    //private void TP1PingPangTwoStop()
    //{
    //    SpriteRenderer m_Render = m_TP1.GetComponent<SpriteRenderer>();
    //    m_Render.sortingLayerName = "warriorCenter";
    //}

    //private void TP2PingPangOneStop()
    //{
    //    SpriteRenderer m_Render = m_TP2.GetComponent<SpriteRenderer>();
    //    m_Render.sortingLayerName = "warriorCenter";
    //}

    //private void TP2PingPangTwoStop()
    //{
    //    SpriteRenderer m_Render = m_TP2.GetComponent<SpriteRenderer>();
    //    m_Render.sortingLayerName = "Bounds";
    //}


    //private void TPEffectSet()
    //{
    //    m_TP1.PingPangOneStop += TP1PingPangOneStop;
    //    m_TP1.PingPangTwoStop += TP1PingPangTwoStop;

    //    m_TP2.PingPangOneStop += TP2PingPangOneStop;
    //    m_TP2.PingPangTwoStop += TP2PingPangTwoStop;
    //}

    //#endregion



    private void OnTriggerEnter(Collider other)
    {
        //BulletBase m_Base = other.GetComponent<BulletBase>();
        //if (m_Base != null)
        //{
        //    if (m_Base.m_Type == BulletBase.BulletTpye.emptyBullet)
        //    {
        //        float injured = m_Base.injured;
        //        injured = injured * defenseLV;
        //        m_HP = BattleCommoUIManager.Instance.UpdataHP_Player(m_HP, HP, injured, -1);
        //        GameObject.Destroy(m_Base.gameObject);
        //    }
        //}

     

       

      

     
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Event"))
        {
            string name = other.gameObject.name;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EventStoryPanel.isEventSpeak || TalkPanel.isSpeak)
                {
                    return;
                }
                EventStateManager.Instance.GameEventSet(name);
            }
        }

        if (other.gameObject.CompareTag("EventAtOnce"))
        {
            string name = other.gameObject.name;
            EventStateManager.Instance.GameEventSet(name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
   
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (TalkPanel.isSpeak||EventStoryPanel.isEventSpeak)
                {
                    return;
                }
                string name = other.gameObject.name;
                int id = CommonHelper.Str2Int(name);
                StoryEventManager.Instance.ShowEventPanel_ChapterOne(id);
            }
        }

        if (other.CompareTag("Event"))
        {
            string name = other.gameObject.name;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EventStoryPanel.isEventSpeak|| TalkPanel.isSpeak)
                {
                    return;
                }
                EventStateManager.Instance.GameEventSet(name);            
            }
        }
        if (other.CompareTag("EventAtOnce"))
        {
            string name = other.gameObject.name;
            EventStateManager.Instance.GameEventSet(name);
        }


      
    }





   


    private void OnParticleCollision(GameObject other)
    {

    }
    void FindBagPanel() {
        GameObject[] arry;
        arry= Resources.FindObjectsOfTypeAll<GameObject>();
        for (int i = 0; i < arry.Length; i++) {
            if (arry[i].name == "BagPanel") {
                BagPanel = arry[i];
            }
        }
        
    }
    
}
