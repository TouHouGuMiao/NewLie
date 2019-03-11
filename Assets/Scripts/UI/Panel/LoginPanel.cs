
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginPanel : IView
{
    public LoginPanel()
    {
        m_Layer = Layer.bottom;
    }

    private static bool loginButton_b=false;
    private static bool developerBtn_b = false;
    private static bool closeGameBtn_b = false;
    private static bool againGameBtn_b = false;
    private static bool HelpBtn_b = false;
    private static bool seetingsBtn_b = false;
    

    private UIButton loginButton;
    private UIButton developerBtn;
    private UIButton closeGameBtn;
    private UIButton againGameBtn;
    private UIButton HelpBtn;

    private UIButton seetingsBtn;

    private UISprite changeWordPic;
    private GameObject m_go;
    private List<UIButton> choiceBtnList = new List<UIButton>();
    private List<bool> eggChangeList = new List<bool>();

    private UISprite m_Dimon;

    private Transform gameChoice;
    private ParticleSystem paritcleSystem_Normal;
    private ParticleSystem particleSystem_Normal_1;
    private ParticleSystem.Particle[] particleArray_Normal;
    private ParticleSystem sakuraParticle_1;
    private ParticleSystem sakuraParticle_2;

    private bool isParticleSetting_Up = false;
    //private GameObject go;

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        paritcleSystem_Normal.gameObject.SetActive(true);
        sakuraParticle_1.gameObject.SetActive(false);
        sakuraParticle_2.gameObject.SetActive(false);
        particleSystem_Normal_1.gameObject.SetActive(false);
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = paritcleSystem_Normal.velocityOverLifetime;
        velocityOverLifetimeModule.enabled = false;
    }

    protected override void OnShow()
    {
        AudioManager.Instance.PlayBg_Source("LoginBGM");
        ParticleSystem.EmissionModule emission = paritcleSystem_Normal.emission;
        emission.rateOverTime = 4;
        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSetting_Pasue), "LoginBGM", 50.4f);

        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSetting_Up), "LoginBGM", 70.5f);

        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSetting_Normal1Up), "LoginBGM", 84.5f);

        AudioManager.Instance.LockBGMTimeEvent(new AudioEventDelegate(ParticleSakuRaShow), "LoginBGM", 101.5f);
    }
    public override void Update() {
        OnHoverChangeWord();
       
    }

    protected override void OnStart()
    {       
        loginButton = this.GetChild("LoginButton").GetComponent<UIButton>();
        HelpBtn = this.GetChild("LoginButton (3)").GetComponent<UIButton>();
        developerBtn = this.GetChild("LoginButton (4)").GetComponent<UIButton>();
        closeGameBtn = this.GetChild("LoginButton (5)").GetComponent<UIButton>();

        againGameBtn = this.GetChild("LoginButton (1)").GetComponent<UIButton>();
        seetingsBtn = this.GetChild("LoginButton (2)").GetComponent<UIButton>();
        m_go = this.GetChild("ButtonGrid").gameObject;
        changeWordPic = this.GetChild("ChangeWord").GetComponent<UISprite>();

        addElementInEggChangeList();
       // Debug.Log(eggChangeList[0]);
       // Debug.Log(eggChangeList.Count);
        OnLoginBtnHover();//鼠标悬浮选项放大的事件                       
        addDelegate();
        //ChangeCover();
       // ColorEgg();

        EventDelegate OnCloseBtn = new global::EventDelegate(OnCloseGameBtn);
        closeGameBtn.onClick.Add(OnCloseBtn);


        paritcleSystem_Normal = this.GetChild("NormalParticle").GetComponent<ParticleSystem>();
        particleArray_Normal= new ParticleSystem.Particle[paritcleSystem_Normal.main.maxParticles];

        particleSystem_Normal_1 = this.GetChild("NormalParticle1").GetComponent<ParticleSystem>();

        sakuraParticle_1 = this.GetChild("SakuraParticle1").GetComponent<ParticleSystem>();
        sakuraParticle_2 = this.GetChild("SakuraParticle2").GetComponent<ParticleSystem>();

    }


    void OnLoginBtnClick()
    {
        AudioManager.Instance.CloseBg_Source(); 
        GameStateManager.LoadScene(2);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "PlayerPanel";
    }

    private List<Transform> m_PicList=new List<Transform> ();
    void OnLoginBtnHover(){//鼠标悬浮选项放大的事件
      //  GameObject go = GameObject.Find("ButtonGrid");
        foreach (Transform child in m_go.transform) {
            m_PicList.Add(child);
        } 
        BtnControl();
        
    }
    void BtnControl() {//悬停鼠标功能控制

        foreach (Transform child in m_PicList)
        {
            child.gameObject.AddComponent<OnGamStarBunHover>();
        }
    }

    void OnDeveloperBtnClick() {//开发人员界面显示
        GUIManager.ShowView("DeveloperPanel");
        GUIManager.HideView("LoginPanel");
        //LoadingPanel.LoadingName("")
    }
    void OnCloseGameBtn() {
        Application.Quit();

    }//游戏整体退出
    void OnHelpPanelClick() {
        GUIManager.ShowView("GameHelpPanel");
    }


    void addDelegate() {
        EventDelegate OnLoginClick = new global::EventDelegate(OnLoginBtnClick);
        loginButton.onClick.Add(OnLoginClick);

        HelpBtn.onClick.Add(new EventDelegate(OnHelpPanelClick));

        EventDelegate OnDeveloperBtn = new global::EventDelegate(OnDeveloperBtnClick);
        developerBtn.onClick.Add(OnDeveloperBtn);

        EventDelegate OnCloseBtn = new global::EventDelegate(OnCloseGameBtn);
        closeGameBtn.onClick.Add(OnCloseBtn);
    }

    //void ColorEgg() {//彩蛋事件
    //    foreach (Transform child in m_go.transform) {
    //        choiceBtnList.Add(child.GetComponent<UIButton>());          
    //     // Debug.Log(child.name);
    //    }
    //    OnHoverChangeWord();
    //    //Debug.Log(choiceBtnList.Count);
    //}
    //int num=0;
    void OnHoverChangeWord() {
        if (loginButton.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "one";
            loginButton_b = true;
           
        }
       
        else if (HelpBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "four";
            HelpBtn_b = true;
        }
        else if (developerBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "five";
            developerBtn_b = true;
        }
        else if (closeGameBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "six";
            closeGameBtn_b = true;
        }
       else if (seetingsBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "zero";
            seetingsBtn_b = true;
        }
        else if (againGameBtn.state == UIButtonColor.State.Hover) {
            changeWordPic.spriteName = "two";
            againGameBtn_b = true;
        }
        ChangeCover();
    }
    bool isAllboolVauletrue=false;
    void ChangeCover() {

        if (eggChangeList[0]== true) {
            Debug.Log(1);
        }
        if (isAllboolVauletrue == true)
        {
            Debug.Log("更换封面");
        }
        
        // Debug.Log("isAllboolVauletrue="+isAllboolVauletrue);
    }
    void addElementInEggChangeList()
    {
        eggChangeList.Add(loginButton_b);
        eggChangeList.Add(developerBtn_b);
        eggChangeList.Add(closeGameBtn_b);
        eggChangeList.Add(againGameBtn_b);
        eggChangeList.Add(HelpBtn_b);
        eggChangeList.Add(seetingsBtn_b);
    }






    void ParticleSetting_Pasue()
    {

        ParticleSystem.EmissionModule emission = paritcleSystem_Normal.emission;

        emission.rateOverTime = 0;
        paritcleSystem_Normal.GetParticles(particleArray_Normal);
        for (int i = 0; i < particleArray_Normal.Length; i++)
        {

            particleArray_Normal[i].velocity = new Vector3(particleArray_Normal[i].velocity.x * 0.4f, particleArray_Normal[i].velocity.y * 0.4f, particleArray_Normal[i].velocity.z * 0.4f);
        }
        paritcleSystem_Normal.SetParticles(particleArray_Normal, particleArray_Normal.Length);
    
     
        
    }

    void ParticleSetting_Up()
    {
        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = paritcleSystem_Normal.velocityOverLifetime;
        velocityOverLifetimeModule.enabled = true;
        velocityOverLifetimeModule.y = 0.7f;
    }

    void ParticleSetting_Normal1Up()
    {
        particleSystem_Normal_1.gameObject.SetActive(true);
    }

    void ParticleSakuRaShow()
    {
        GUIManager.ShowView("CoverPanel");
        paritcleSystem_Normal.gameObject.SetActive(false);
        particleSystem_Normal_1.gameObject.SetActive(false);
        sakuraParticle_1.gameObject.SetActive(true);
        sakuraParticle_2.gameObject.SetActive(true);
    }

 





    //void InitDimon() {
    //    foreach (Transform child in m_PicList) {
    //        GameObject go = GameObject.Find("Dimon");
    //        GameObject go_Clone = GameObject.Instantiate(go);
    //        go_Clone.transform.SetParent(gameChoice, false);
    //        go_Clone.transform.localPosition = child.transform.localPosition + new Vector3(-950, -100, 0);

    //        //GameObject.Instantiate(go, child.transform.localPosition+new Vector3(0,100,0), child.transform.localRotation);

    //    }

    //}
}
 