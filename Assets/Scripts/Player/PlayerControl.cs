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

    public static PlayerControl Instace;
    

    public GameObject bulletPrefab;
    private GameObject BagPanel;

    private Animator m_Animator;
    private float rate_Z=0.5f;
    private float rate_X = 0.5f;

    private float tempTime_Z;
    private float tempTime_X;



    private Transform bulletCollider;
 


    

    //private GameObject systemPanel;//控制SystemPanel的GameObject

    //private TweenPosition m_TP1;
    //private TweenPosition m_TP2;

    private UISlider m_Slider;
    private float m_HP;  //表示血条现有HP,而不是HP属性

    private void Awake() 
    {
        Instace = this;
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
        PlayerSkillManager.Instance.InitPlayerSkillManager();
        WingmanData data = new WingmanData();
        data.bulletName = "StarBullet";
        data.tempTime = 0.5f;
        WingmanManager.Instance.ShowWingman(data, 6);
        UpDataPlayerPro();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&SystemPanel.Bg_IsActive==false) {

            GUIManager.ShowView("SystemPanel");
        }
        if (SystemPanel.Bg_IsActive==false)
        {
            CharacterControl();
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

        if (TalkPanel.isSpeak||EventStoryPanel.isEventSpeak||CGPanel.IsCGPlay)
        {
            return;
        }

        if (BattleCommoUIManager.Instance.IsBlackShade)
        {
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                return;
            }
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 0, this.transform.rotation.eulerAngles.z);
            deltaTime += 1 * Time.deltaTime;
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            if (deltaTime >= 0.3f)
            {
                if (stateInfo.IsName("Base Layer.idle"))
                {
                    m_Animator.SetBool("move", true);
                    m_Animator.SetBool("Move", true);

                }
            }
            if (stateInfo.IsName("Base Layer.idle") || stateInfo.IsName("Base Layer.Move.MoveLoop") || stateInfo.IsName("Base Layer.Move.MoveBegin"))
            {
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed,Space.Self);
            }



        }

        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_Animator.SetBool("Move", false);
            m_Animator.SetBool("move", false);
            deltaTime = 0; 
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                return;
            }
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, 180, this.transform.rotation.eulerAngles.z);
            AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
            deltaTime += 1 * Time.deltaTime;
            if (deltaTime >= 0.3f)
            {
                if (stateInfo.IsName("Base Layer.idle"))
                {
                    m_Animator.SetBool("move", true);
                    m_Animator.SetBool("Move", true);
                }
            }
         
       
                transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed,Space.Self);
            
         
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            m_Animator.SetBool("Move", false);
            m_Animator.SetBool("move", false);
            deltaTime = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        { 
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed ); ;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
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


public void UseAttack()
    {
        string name = bulletPrefab.name;
        ItemData data = ItemDataManager.Instance.GetItemDataByBulletName(name);
        if (name != "initBullet")
        {
           
            if (data.num <= 0)
            {
                m_Animator.SetBool("Attack", false);
                return;
            }
        }
 
        if (bulletPrefab == null)
        {
            Debug.LogError("bulletPrefab is null");
            return;
        }
 
        if (name != "initBullet")
        {  
            ItemDataManager.Instance.CutMaterialToHasMaterialList(data);
            BattleCommoUIManager.Instance.ChangeBulletType(data.id,data.num.ToString());
        }
        GameObject go = Instantiate(bulletPrefab);
        go.transform.position = this.transform.Find("firePoint").transform.position;
        m_Animator.SetBool("Attack", false);
    }




    #endregion

    private void UpDataPlayerPro()
    {
        CharacterPropBase baseData = CharacterPropManager.Instance.GetCharcaterDataByName("reimu");
        if (baseData == null)
        {
            Debug.LogError("baseData is null");
            return;
        }
        Power = baseData.Power;
        DEF = baseData.DEF;
        DEX = baseData.DEX;
        VIT = baseData.VIT;
        Lucky = baseData.Lucky;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            string name = other.gameObject.name;
            int id = CommonHelper.Str2Int(name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (TalkPanel.isSpeak||EventStoryPanel.isEventSpeak)
                {
                    return;
                }

                StoryEventManager.Instance.ShowEventPanel_ChapterOne(id);
            }
        }

        if (other.CompareTag("Event"))
        {
            string name = other.gameObject.name;
            int id = CommonHelper.Str2Int(name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (EventStoryPanel.isEventSpeak)
                {
                    return;
                }

                StoryManager.Instacne.ShowEventStoryList(id);
            }
        }

        if (other.CompareTag("Test"))
        {
            //List<StoryData> dataList = new List<StoryData>();
            //dataList = StoryManager.Instacne.GetStage0State0List();  
            //StoryManager.Instacne.ShowStoryList(dataList);
            //Destroy(other);
        }

      
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            BattleCommoUIManager.Instance.speakLabelAlpha.gameObject.SetActive(false);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("specialBullet"))
        {
            int id = CommonHelper.Str2Int(collision.gameObject.name);
            ItemData data = ItemDataManager.Instance.GetItemDataByID(id);
            List<ItemData> dataList = ItemDataManager.Instance.GetHasItemList();
            ItemDataManager.Instance.AddItemToHasMaterialList(data);
            Destroy(collision.gameObject);
        }

        if (collision.transform.name == "Stage0Scene2")
        {
            BattleCamera.Instance.isRightStop = false;
            BattleCamera.Instance.isLeftStop = false;
            BattleCommoUIManager.Instance.ShowBlackShade();
            transform.position = new Vector3(-4.4f, -13.85f, 0);
        }

        if (collision.transform.name == "Stage0Scene3")
        {
            BattleCamera.Instance.isRightStop = false;
            BattleCamera.Instance.isLeftStop = false;
            BattleCommoUIManager.Instance.ShowBlackShade();
            transform.position = new Vector3(-4.4f, -23.9f, 0);
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
