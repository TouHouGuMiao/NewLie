using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : IView
{


    private UIButton ShopBtn;
    private UIButton ensureBtn;//error界面的关闭按钮
    private UIButton succeseBtn;
    private UIButton noNameBtn;

    private UIInput input;  

    private GameObject succeseSprite;
    private GameObject errorSprite;
    private GameObject noNameSprite;


    private string playerName;

    public PlayerPanel()
    {
        m_Layer = Layer.city;
    }

    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        input.value = null;
    }

    protected override void OnShow()
    {
        
    }

    protected override void OnStart()
    {

        ShopBtn = this.GetChild("ShopBtn").GetComponent<UIButton>();
        ensureBtn = this.GetChild("quit").GetComponent<UIButton>();
        noNameBtn = this.GetChild("ensure").GetComponent<UIButton>();
        succeseBtn = this.GetChild("succese").GetComponent<UIButton>();

        input = this.GetChild("Input").GetComponent<UIInput>();     

        succeseSprite = this.GetChild("SucceseBg").gameObject;
        errorSprite = this.GetChild("ErrorBg").gameObject;
        noNameSprite = this.GetChild("noName").gameObject;

        input.onSubmit.Add(new EventDelegate(OnSubmit));


        AddEventDelete();

       
    }


    private void AddEventDelete()
    {
       ensureBtn.onClick.Add(new EventDelegate(OnEnsureErrorBtnClick));
        succeseBtn.onClick.Add(new EventDelegate(OnEnsureSucceseBtnClick));
        ShopBtn.onClick.Add(new EventDelegate(OnSpeakPanelClick));
        noNameBtn.onClick.Add(new EventDelegate(OnEnsureNoNameBtnClick));

    }



    void OnBattleBtnClick()
    {

        GameStateManager.LoadScene(3);
        GUIManager.ShowView("LoadingPanel");
        LoadingPanel.LoadingName = "BattleUIPanel";
    }

    void OnCardsGroundBtnClick()
    {
        GUIManager.ShowView("CardsPanel");
    }

    void OnSpeakPanelClick()
    {
        if (islegal)
        {
            GUIManager.ShowView("CoverPanel");
            GUIManager.HideView("PlayerPanel");
            GUIManager.ShowView("SurePropertyPanel");
            //StoryEventManager.Instance.ShowEventPanel_ChapterOne(1, 0);
            //GameStateManager.LoadScene(4);
        }
        else {
            if (noNameSprite.activeInHierarchy == false) {
                noNameSprite.SetActive(true);
            }
        }
    }

    void OnEnsureErrorBtnClick() {
        if (errorSprite.activeInHierarchy == true) {
            errorSprite.SetActive(false);
        }
    }

    void OnEnsureSucceseBtnClick() {
        if (succeseSprite.activeInHierarchy == true) {
            succeseSprite.SetActive(false);
        }
    }
    void OnEnsureNoNameBtnClick() {
        if (noNameSprite.activeInHierarchy == true) {
            noNameSprite.SetActive(false);
        }
    }
    private bool islegal = false;
    void OnSubmit()
    {
        playerName = input.value;
        bool isChinese_s = IsChinese();
        bool isEnglish_s = IsEnglish();
        bool isEmpty_s = IsEmptyName();
        if (isEmpty_s) {
            if (errorSprite.activeInHierarchy == false)
            {
                errorSprite.SetActive(true);
                islegal = false;
                return;
            }
        }
        if (isChinese_s)
        {                     
           
        if (playerName.Length <= 5 && (!playerName.Contains("灵梦")) && (!playerName.Contains("博丽")))
            {

                if (succeseSprite.activeInHierarchy == false)
                {
                    succeseSprite.SetActive(true);
                    islegal = true;
                }
            }
        

        else
        {
            //Debug.LogError("重新再注册");
            if (errorSprite.activeInHierarchy == false)
            {
                errorSprite.SetActive(true);
                islegal = false;
                return;
            }
        }
    }     
        else if (isEnglish_s)
        {
            if (playerName.Length <= 6 &&(!playerName.Contains("reimu")) && (!playerName.Contains("Reimu")))
            {
                if (succeseSprite.activeInHierarchy == false)
                {
                    succeseSprite.SetActive(true);
                    islegal = true;
                }
            }
            else
            {
                if (errorSprite.activeInHierarchy == false)
                {
                    errorSprite.SetActive(true);
                    islegal = false;
                    return;
                }
            }
        }          
       else
        {
            Debug.LogError(1111);
            Debug.LogError(playerName.Length);

            if ((!playerName.Contains("灵梦")) && (!playerName.Contains("reimu")) && (!playerName.Contains("Reimu")) && playerName.Length <= 6)
            {

                succeseSprite.SetActive(true);
                islegal = true;
                Debug.Log("111");
            }
            if (playerName.Contains("灵梦") || playerName.Contains("reimu") || playerName.Contains("Reimu"))
            {
                if (errorSprite.activeInHierarchy == false)
                {
                    errorSprite.SetActive(true);
                    islegal = false;
                    Debug.Log("222");
                    return;
                }

            }

        }
    }
       // Debug.Log("名字真好听");   
    public string GetPalyerName() {
        if (playerName == null)
        {
            Debug.LogError("角色名为空");
            return null;
          
        }
        return playerName;
    }
    private bool isAllChinese = false;
    private bool isAllEnglish = false;
    private bool isAllBlank = false;
    private bool isEmpty = false;
    bool IsChinese() {
        char[] textArry = playerName.ToCharArray();
        if (textArry.Length == 0) {
            isAllChinese = false;
            return isAllChinese;
        }
        for (int i = 0; i < textArry.Length; i++) {
            if (textArry[i] >= 0x4e00 && textArry[i] <= 0x9fbb)
            {
                isAllChinese = true;
            }
            else {
                isAllChinese = false;
                return isAllChinese;
            }
        }
        return isAllChinese;
    }
    bool IsEnglish()
    {
        char[] textArry = playerName.ToCharArray();
        if (textArry.Length == 0)
        {
            isAllEnglish = false;
            return isAllEnglish;
        }
        for (int i = 0; i < textArry.Length; i++)
        {
            if ((textArry[i] >= 'a' && textArry[i] <= 'z')||(textArry[i]>='A'&&textArry[i]<='Z'))
            {
                isAllEnglish = true;
            }
            else
            {
                isAllEnglish = false;
                return isAllEnglish;
            }
        }
        return isAllEnglish;
    }
    bool IsBlank() {
        char[] textArry = playerName.ToCharArray();
        if (textArry.Length == 0)
        {
            isAllBlank = false;
            return isAllBlank;
        }
        for (int i = 0; i < textArry.Length; i++) {
            if (textArry[i] == ' ')
            {
               isAllBlank = true;
            }
            else {
                isAllBlank = false;
                return isAllBlank;
            }
        }
        return isAllBlank;
    }
    bool IsEmptyName() {
        char[] textArry = playerName.ToCharArray();
        if (textArry.Length == 0)
        {
            isEmpty = true;
            return isEmpty;
        }
        else {
            isEmpty = false;
        }
        return isEmpty;
    }
}
