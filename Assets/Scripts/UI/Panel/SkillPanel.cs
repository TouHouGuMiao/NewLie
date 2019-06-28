using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : IView
{
    private UILabel points;
    //private UILabel canAddPoints;
    private UIButton addPointsBtn;
    private UIButton minusPointsBtn;
    private UIButton startBtn;
    private UIButton choseSkillBtn;
    private static GameObject cardContainer;
    private GameObject skillpointsChange;
    private UILabel skillName;
    private UILabel canUsePoints;
    private UIButton completeBtn;
    private GameObject SkillNameContainer;
    private Transform SkillDesInfo;
    private Transform ChanageSkillWidget;
    private GameObject NextSceneBtn;
    private GameObject ErrorTips;

    private List<Vector3> backV_List = new List<Vector3>();
    private List<Vector3> backR_List = new List<Vector3>();   
    //public static int skillPoints=10;//技能职业加点
    int pointts;
    int currentPoints = 0;

    
    protected override void OnDestroy()
    {
       
    }

    protected override void OnHide()
    {
        
    }

    protected override void OnShow()
    {      
        cardContainer.gameObject.SetActive(false);
        //pointts = SkillPanel.skillPoints;    
        pointts = Skill.CanUsePoints;//总的点数
        startBtn.gameObject.SetActive(true);
    }

    public override void Update()
    {
       
    }
    protected override void OnStart()
    {
        points = this.GetChild("points").GetComponent<UILabel>();
        //canAddPoints = this.GetChild("canAddPoints").GetComponent<UILabel>();
        addPointsBtn = this.GetChild("addPoints").GetComponent<UIButton>();
        addPointsBtn.button_type = UIButton.button_Class.add_button;
        minusPointsBtn = this.GetChild("minusPoints").GetComponent<UIButton>();
        minusPointsBtn.button_type = UIButton.button_Class.minus_button;
        startBtn = this.GetChild("StartBtn").GetComponent<UIButton>();
        cardContainer = this.GetChild("SkillCardContainer").gameObject;
        choseSkillBtn = this.GetChild("ChoseSkillBtn").GetComponent<UIButton>();
        skillpointsChange = this.GetChild("ListenSkill").gameObject;
        skillName = this.GetChild("Label").GetComponent<UILabel>();
        canUsePoints = this.GetChild("canUsePoints").GetComponent<UILabel>();
        completeBtn = this.GetChild("CompleteBtn").GetComponent<UIButton>();
        SkillNameContainer = this.GetChild("SkillNameContainer").gameObject;
        SkillDesInfo = this.GetChild("ShowSkillDes").transform;       
        ChanageSkillWidget = this.GetChild("ChanageSkillWidget").transform;
        NextSceneBtn = this.GetChild("NextSceneBtn").gameObject;
        ErrorTips = this.GetChild("ErrorTips").gameObject;


        AddDelegate();  
        AddClickDelegate();      
    }
    int c_Point;
    bool isAllCardInReset_Value = false;
    bool isAllCardInReset() {//判断卡片是否全在原来的地方
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            if (cardContainer.transform.GetChild(i).localPosition != backV_List[i])
            {
                isAllCardInReset_Value = false;
                return isAllCardInReset_Value;
            }
            else {
                isAllCardInReset_Value = true;
            }
        }
        return isAllCardInReset_Value;
    }
    void PlayClickPoints_Effect() {
        AudioManager.Instance.PlayEffect_Source("clickAddPoints");
    }
    void OnClickAddPointsBtn() {
        PlayClickPoints_Effect();
        if (pointts == 0)
        {
            Debug.LogError("技能点数不够");
        }
        else
        {
            c_Point = System.Int32.Parse(points.text);
            //currentPoints++;
            c_Point++;
            points.text = c_Point.ToString();
            if (pointts > 0)
            {
                pointts--;
                canUsePoints.text = pointts.ToString();
            }

        }
    }
    void OnClickMinusPointsBtn() {
        PlayClickPoints_Effect();
        if (System.Int32.Parse(points.text)==0)
        {
            Debug.LogError("can't minus points");
        }
        else
        {
            c_Point=System.Int32.Parse(points.text);
            c_Point--;
            //currentPoints--;           
            points.text = c_Point.ToString();
            //Debug.LogError(Skill.CanUsePoints + "总的");
            //Debug.LogError(pointts + "pointts得值")                      
                pointts++;
                canUsePoints.text = pointts.ToString();
               // Debug.LogError(pointts + "当前");
            
            

        }

    }
    void OpenOtherSkillCard() {
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            if (cardContainer.transform.GetChild(i).name!= currentGO.name) {
                cardContainer.transform.GetChild(i).localScale = new Vector3(0, 0, 0);
                cardContainer.transform.GetChild(i).gameObject.SetActive(true);
                OpenOtherSkillCard_Tween(cardContainer.transform.GetChild(i).gameObject);
            }
        }
    }
    void MakeCardFlyBack()
    {       
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            if (cardContainer.transform.GetChild(i).name != currentGO.name)
            {
               
                    MakeCardFlyBack_Tween(cardContainer.transform.GetChild(i).gameObject, i);
              
            }
            else {
                if (currentGO.name==cardContainer.transform.GetChild(cardContainer.transform.childCount-1).name) {
                    MakeCardFlyBack_Tween(cardContainer.transform.GetChild(i).gameObject, i);
                }
            }
            
        }
    }
   
    void MakeCardFlyBack_Tween(GameObject go, int i)
    {              
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.delay = 0.3f * i;
        tp.duration = 1f;
        tp.onFinished.Clear();
        tp.from = go.transform.localPosition;
        tp.to = backV_List[i];
        tp.ResetToBeginning();

        TweenRotation tr = go.GetComponent<TweenRotation>();
        tr.enabled = true;
        tr.delay = 0.3f * i;
        tr.duration = 1f;
        tr.onFinished.Clear();
        tr.from = go.transform.localRotation.eulerAngles;
        tr.to = backR_List[i];
        tr.ResetToBeginning();
               
        if (i >= cardContainer.transform.childCount - 1)
        {
            tp.onFinished.Add(new EventDelegate(ShowNextBtn));
        }     
    }
    void ShowNextBtn() {
        NextSceneBtn.GetComponent<UIButton>().enabled = true;
        NextSceneBtn.SetActive(true);
    }
    void OpenOtherSkillCard_Tween(GameObject go)
    {
        TweenScale ts = go.GetComponent<TweenScale>();
        ts.enabled = true;
        ts.duration = 0.5f;
        ts.delay = 0.2f;
        ts.from = new Vector3(0, 0, 0);
        ts.to = new Vector3(70, 70, 180);
        ts.onFinished.Clear();
        ts.ResetToBeginning();
    }
    private bool isAllCardBeClicked = false;
    bool isAllSkillCardBeClicked() {
        for (int i = 0; i < SkillNameContainer.transform.childCount; i++) {
            //Debug.LogError()
            if (SkillNameContainer.transform.GetChild(i).GetComponent<UILabel>().alpha != 0.4f)
            {
                isAllCardBeClicked = false;
                return isAllCardBeClicked;
            }
            else {
                isAllCardBeClicked = true;              
            }
        }
        return isAllCardBeClicked;
    }
    void OnClickCompleteBtm() {     
        AlphaCloseDes_Info();
        BackToSkillCardPos(currentGO);
        OpenAddCardEnabled();      
        Skill.CanUsePoints = pointts;
        if (currentGO.name.Contains("0"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(1);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(1);
        }
        else if (currentGO.name.Contains("1"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(2);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);           
            OnFinishCompleteBtn(2);
        }
        else if (currentGO.name.Contains("2"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(3);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(3);
        }
        else if (currentGO.name.Contains("3"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(4);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(4);
        }
        else if (currentGO.name.Contains("4")) {
            Skill s = SkillManager.Instance.GetSkillDataById(5);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(5);
        }
        else if (currentGO.name.Contains("5"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(6);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(6);
        }
        else if (currentGO.name.Contains("6"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(7);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(7);
        }
        else if (currentGO.name.Contains("7"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(8);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(8);
        }
        else if (currentGO.name.Contains("8"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(9);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(9);
        }
        else if (currentGO.name.Contains("9"))
        {
            Skill s = SkillManager.Instance.GetSkillDataById(10);
            s.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s);
            OnFinishCompleteBtn(10);
        }
    }
    void OnFinishCompleteBtn(int id) {
        for (int i = 0; i < SkillNameContainer.transform.childCount; i++) {
            if (i == id-1) {
                SkillNameContainer.transform.GetChild(i).GetComponent<UILabel>().alpha = 0.4f;
                return;
            }
        }
    }
    void OnTwiceChosePoints(int id) {
        for (int i = 0; i < SkillNameContainer.transform.childCount + 1; i++)
        {
            if (i == id - 1)
            {
                SkillNameContainer.transform.GetChild(i).GetComponent<UILabel>().alpha = 1;
                return;
            }
        }
    }
    void LightName(GameObject go) {
        if (go.name.Contains("0"))
        {
            OnTwiceChosePoints(1);
        }
        else if (go.name.Contains("1"))
        {
            OnTwiceChosePoints(2);
        }
        else if (go.name.Contains("2"))
        {
            OnTwiceChosePoints(3);
        }
        else if (go.name.Contains("3"))
        {
            OnTwiceChosePoints(4);
        }
        else if (go.name.Contains("4"))
        {
            OnTwiceChosePoints(5);
        }
        else if (go.name.Contains("5"))
        {
            OnTwiceChosePoints(6);
        }
        else if (go.name.Contains("6"))
        {
            OnTwiceChosePoints(7);
        }
        else if (go.name.Contains("7"))
        {
            OnTwiceChosePoints(8);
        }
        else if (go.name.Contains("8"))
        {
            OnTwiceChosePoints(9);
        }
        else if (go.name.Contains("9"))
        {
            OnTwiceChosePoints(10);
        }
     
    }
    void ChoiceAddOrMinus(UIButton button)
    {
        if (button.button_type == UIButton.button_Class.add_button)
        {
            OnClickAddPointsBtn();
        }
        else if (button.button_type == UIButton.button_Class.minus_button)
        {
            OnClickMinusPointsBtn();
        }
    }
    void OnClickStartBtn() {
       InitSkillCard();      
    }
    void OnClickNextSceneBtn() {
        if (isAllSkillCardBeClicked() == true)
        {
            GUIManager.ShowView("CoverPanel");
            GUIManager.HideView("SkillPanel");
            GameStateManager.LoadScene(4);
        }
        else {
            CloseAllCardEnabeld();
           cardContainer.SetActive(false);           
            ErrorTips.SetActive(true);
            ErrorTips.GetComponent<UIButton>().onClick.Add(new EventDelegate(OnClickErrorTips));
        }
    }
    void OnClickErrorTips() {
        ErrorTips.SetActive(false);       
        OpenAddCardEnabled();
        cardContainer.SetActive(true);
    }
    
    void OnClickChoseSkillBtn() {
        choseSkillBtn.gameObject.SetActive(false);             
        if (currentGO.name.Contains("0"))
        {
            MoveToScreenLeft();            
            CloseAllCardEnabeld();
            UpdateSkillInfo(1);
        }
        else if (currentGO.name.Contains("1"))
        {
            MoveToScreenLeft();          
            CloseAllCardEnabeld();
            UpdateSkillInfo(2);
        }
        else if (currentGO.name.Contains("2")) {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(3);
        }
        else if (currentGO.name.Contains("3"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(4);
        }
        else if (currentGO.name.Contains("4"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(5);
        }
        else if (currentGO.name.Contains("5"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(6);
        }
        else if (currentGO.name.Contains("6"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(7);
        }
        else if (currentGO.name.Contains("7"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(8);
        }
        else if (currentGO.name.Contains("8"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(9);
        }
        else if (currentGO.name.Contains("9"))
        {
            MoveToScreenLeft();
            CloseAllCardEnabeld();
            UpdateSkillInfo(10);
        }


    }
    void CloseAllCardEnabeld() {
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            cardContainer.transform.GetChild(i).GetComponent<UIButton>().enabled = false;
        }
    }
    void OpenAddCardEnabled() {
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            cardContainer.transform.GetChild(i).GetComponent<UIButton>().enabled = true;
        }
    }
    void UpdateSkillInfo(int id) {
        
        Skill s=SkillManager.Instance.GetSkillDataById(id);      
        skillName.text = s.data.Name;
        canUsePoints.text = pointts.ToString();
        points.text = s.data.SkillPoints.ToString();
        SkillDesInfo.GetChild(0).GetComponent<UILabel>().text = s.data.Des;
       
    }
    void AlphaShowDes_Info() {         
        TweenAlpha ta = ChanageSkillWidget.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.delay = 0.2f;
        ta.duration = 0.3f;
        ta.onFinished.Clear();
        ta.from = 0;
        ta.to = 1;
        ta.ResetToBeginning();       
    }
    void AlphaCloseDes_Info()
    {      
        TweenAlpha ta = ChanageSkillWidget.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.delay = 0.2f;
        ta.duration = 0.3f;
        ta.onFinished.Clear();
        ta.from = 1;
        ta.to = 0;       
        ta.ResetToBeginning();
    }
    void MoveToScreenLeft() {      
        Vector3 vt = new Vector3(-4.7f, 0.85f, 3.5f);
        Vector3 vr = new Vector3(0, 125, 0);
        TweenPosition tp = currentGO.GetComponent<TweenPosition>();
        TweenRotation tr = currentGO.GetComponent<TweenRotation>();

        tp.enabled = true;
        tp.delay = 0.2f;
        tp.duration = 0.5f;
        tp.onFinished.Clear();
        tp.from = currentGO.transform.localPosition;
        tp.to = vt;
        tp.ResetToBeginning();
        
        tr.enabled = true;
        tr.delay = 0.2f;
        tr.duration = 0.5f;
        tr.onFinished.Clear();
        tr.from = currentGO.transform.localRotation.eulerAngles;
        tr.to = vr;
        tr.ResetToBeginning();

        tp.onFinished.Add(new EventDelegate(InitSkillCard_2));
    }
    void OpenSkillChose() {
        InitSkillCard_2();
    }    
    void MakeCardFly(GameObject go) {
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.delay = 0.2f;
        tp.duration = 1f;
        tp.onFinished.Clear();
        tp.from = go.transform.localPosition;
        tp.to = new Vector3(-15, 35, 80);
        tp.ResetToBeginning();
        tp.onFinished.Add(new EventDelegate(PlayCardEffectMusic));
    }
    void AddClickDelegate() {//添加按钮的点击方法      
        startBtn.onClick.Add(new EventDelegate(OnClickStartBtn));
        choseSkillBtn.onClick.Add(new EventDelegate(OnClickChoseSkillBtn));
        completeBtn.onClick.Add(new EventDelegate(OnClickCompleteBtm));
        NextSceneBtn.GetComponent<UIButton>().onClick.Add(new EventDelegate(OnClickNextSceneBtn));
    }
    void AddDelegate() {//添加委托
      addPointsBtn.skillBtnDelegate += ChoiceAddOrMinus;
      minusPointsBtn.skillBtnDelegate += ChoiceAddOrMinus;
    }
    void InitSkillCard_2() {
        NextSceneBtn.SetActive(false);
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            GameObject go = cardContainer.transform.GetChild(i).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            TweenRotation tr = go.GetComponent<TweenRotation>();
            if (go.name != currentGO.name)
            {

                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.onFinished.Clear();
                tp.delay = i * 0.3f;
                tp.duration = 0.8f;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(5.5f, 12.3f + i * 0.1f, 80);
                tp.ResetToBeginning();


                tr.enabled = true;
                tr.delay = i * 0.2f;
                tr.duration = 0.8f;
                tr.onFinished.Clear();
                tr.from = go.transform.rotation.eulerAngles;
                tr.to = new Vector3(80, 0, 1.1722f);
                tr.ResetToBeginning();


            }
            if (go.name == currentGO.name)
            {
                if (i >= cardContainer.transform.childCount - 1)
                {
                    tp = cardContainer.transform.GetChild(cardContainer.transform.childCount - 2).GetComponent<TweenPosition>();
                    tp.onFinished.Add(new EventDelegate(AlphaShowDes_Info));
                }
            }
            if (i >= cardContainer.transform.childCount - 1)
            {
                tp.onFinished.Add(new EventDelegate(AlphaShowDes_Info));

            }
        }

    }
    void InitSkillCard()
    {
        startBtn.gameObject.SetActive(false);
        cardContainer.gameObject.SetActive(true);
       
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {

            GameObject go = cardContainer.transform.GetChild(i).gameObject;
            
           if(i%2==0)
            {
                go.transform.localPosition = new Vector3(-50, 35, 80);
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.onFinished.Clear();
                tp.delay = i * 0.3f;
                tp.duration = 1.0f;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(5.5f, 12.3f+ i * 0.1f, 80);
                tp.ResetToBeginning();

                TweenRotation tr = go.GetComponent<TweenRotation>();
                tr.enabled = true;
                tr.delay = i * 0.2f;
                tr.duration = 1.0f;
                tr.onFinished.Clear();
                tr.from = go.transform.rotation.eulerAngles;
                tr.to = new Vector3(80, 0, 1.1722f);
                tr.ResetToBeginning();
            }            
            if(i%2!=0)
            {
                go.transform.localPosition = new Vector3(50, 35, 80);
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.onFinished.Clear();
                tp.delay = i * 0.3f;
                tp.duration = 1.0f;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(5.5f, 12.3f + i * 0.1f, 80);
                tp.ResetToBeginning();

                TweenRotation tr = go.GetComponent<TweenRotation>();
                tr.enabled = true;
                tr.delay = i * 0.2f;
                tr.duration = 1.0f;
                tr.onFinished.Clear();
                tr.from = go.transform.rotation.eulerAngles;
                tr.to = new Vector3(80, 0, 1.1722f);
                tr.ResetToBeginning();
            }

            if (i >= cardContainer.transform.childCount - 1)
            {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.onFinished.Add(new EventDelegate(CardMoveToScreen));
            }
            else
            {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.onFinished.Add(new EventDelegate(PlayCardEffectMusic));
            }
        }      
    }

    private bool isCardMoveToScreenOver=false;
    void CardMoveToScreen() {
        if (isCardMoveToScreenOver)
        {
            return;
        }
        for (int i = 0; i < cardContainer.transform.childCount; i++){
           // int index = i - 5;
            GameObject go = cardContainer.transform.GetChild(i).gameObject;
           // GameObject go = cardContainer.transform.GetChild(index).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            TweenRotation tr = go.GetComponent<TweenRotation>();
            if (go.transform.localPosition.y < 12.75f)
            {
                if (go.transform.localPosition.y < 12.5f)
                {
                   
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.onFinished.Clear();
                    tp.delay = 0.3f * i + 1f;
                    tp.duration = 0.5f;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(-7.7f + i * 4f, 3.7f, 11.8f);
                    tp.ResetToBeginning();

                    
                    tr.enabled = true;
                    tr.delay = 0.3f * i + 1f;
                    tr.duration = 0.5f;
                    tr.onFinished.Clear();
                    tr.from = go.transform.rotation.eulerAngles;
                    tr.to = new Vector3(0, 150, 0);
                    tr.ResetToBeginning();
                }
                if (go.transform.localPosition.y == 12.5f)
                {
                   
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.onFinished.Clear();
                    tp.delay = 0.3f * i + 1f;
                    tp.duration = 0.5f;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(-7.7f + i * 4f, 3.7f, 11.8f);

                    tp.ResetToBeginning();
                   
                    tr.enabled = true;
                    tr.delay = 0.3f * i + 1f;
                    tr.duration = 0.5f;
                    tr.onFinished.Clear();
                    tr.from = go.transform.rotation.eulerAngles;
                    tr.to = new Vector3(0, 180, 0);
                    tr.ResetToBeginning();
                }
                if (go.transform.localPosition.y > 12.5f)
                {
                    
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.onFinished.Clear();
                    tp.delay = 0.3f * i + 1f;
                    tp.duration = 0.5f;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(-7.7f + i * 4f, 3.7f, 11.8f);
                    tp.ResetToBeginning();

                   
                    tr.enabled = true;
                    tr.delay = 0.3f * i + 1f;
                    tr.duration = 0.5f;
                    tr.onFinished.Clear();
                    tr.from = go.transform.rotation.eulerAngles;
                    tr.to = new Vector3(0, -150, 0);
                    tr.ResetToBeginning();
                }
            }          
            if (i >= cardContainer.transform.childCount - 6)
            {

                tp.onFinished.Add(new EventDelegate(CardMoveToScreen_2));
                break;
            }
        }
            isCardMoveToScreenOver = true;
    }
    void CardMoveToScreen_2() {
        for (int j = 5; j < cardContainer.transform.childCount; j++) {      
            int index = j - 5;
            //int index_1 = cardContainer.transform.childCount - 1 - j;
            GameObject go = cardContainer.transform.GetChild(j).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            TweenRotation tr = go.GetComponent<TweenRotation>();
            if (go.transform.localPosition.y > 12.75f)
            {
                           
                if (go.transform.localPosition.y < 13.0f)
                {
                    
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.onFinished.Clear();
                    tp.delay = 0.3f * index + 1f;
                    tp.duration = 0.5f;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(-7.7f + index * 4f, -1.5f, 11.8f);
                    tp.ResetToBeginning();

                    
                    tr.enabled = true;
                    tr.delay = 0.3f * index + 1f;
                    tr.duration = 0.5f;
                    tr.onFinished.Clear();
                    tr.from = go.transform.rotation.eulerAngles;
                    tr.to = new Vector3(0, 150, 0);
                    tr.ResetToBeginning();                    
                }
                if (go.transform.localPosition.y == 13.0f)
                {
                   
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.onFinished.Clear();
                    tp.delay = 0.3f * index + 1f;
                    tp.duration = 0.5f;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(-7.7f + index * 4f, -1.5f, 11.8f);
                    tp.ResetToBeginning();

                    
                    tr.enabled = true;
                    tr.delay = 0.3f * index + 1f;
                    tr.duration = 0.5f;
                    tr.onFinished.Clear();
                    tr.from = go.transform.rotation.eulerAngles;
                    tr.to = new Vector3(0, 180, 0);
                    tr.ResetToBeginning();
                  
                }
                if (go.transform.localPosition.y > 13.0f)
                {
                   
                    tp.enabled = true;
                    tp.from = go.transform.localPosition;
                    tp.onFinished.Clear();
                    tp.delay = 0.3f * index + 1f;
                    tp.duration = 0.5f;
                    tp.from = go.transform.localPosition;
                    tp.to = new Vector3(-7.7f + index * 4f, -1.5f, 11.8f);
                    tp.ResetToBeginning();

                    
                    tr.enabled = true;
                    tr.delay = 0.3f * index + 1f;
                    tr.duration = 0.5f;
                    tr.onFinished.Clear();
                    tr.from = go.transform.rotation.eulerAngles;
                    tr.to = new Vector3(0, -150, 0);
                    tr.ResetToBeginning();
                
                }
            }
            if (j >= cardContainer.transform.childCount - 1)
            {
                tp.onFinished.Add(new EventDelegate(OnFinshedMoveToScreen));
            }          
        }
    }
    void OnFinshedMoveToScreen() {
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            backV_List.Add(cardContainer.transform.GetChild(i).localPosition);
            backR_List.Add(cardContainer.transform.GetChild(i).localRotation.eulerAngles);
            cardContainer.transform.GetChild(i).GetComponent<UIButton>().onClick.Add(new EventDelegate(OncClickSkillCard));          
        }
        SkillNameContainer.SetActive(true);
        NextSceneBtn.SetActive(true);
    }

    int index;
    List<GameObject> list_Go = new List<GameObject>();
    void AddGOToList() {
      
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            list_Go.Add(cardContainer.transform.GetChild(i).gameObject);

        }
    }
    void PlayCardEffectMusic() {
        AudioManager.Instance.PlayEffect_Source("cardMove");      
    }
    void PlayEffectMusic_2() {
        AudioManager.Instance.PlayEffect_Source("cardMove");
       // NextSceneBtn.GetComponent<UIButton>().enabled = true;
    }
    private GameObject currentGO;//用于得到当前是哪张技能卡片
    void OncClickSkillCard() {//点击卡片放大方法          
        list_Go.Clear();
        NextSceneBtn.GetComponent<UIButton>().enabled = false;
        Vector3 vt = new Vector3(0.45f, 0.85f, 3.5f);
        Vector3 vr = new Vector3(0, 180, 0);
        GameObject gc = UIButton.current.gameObject;      
        AddGOToList();
        for (int i = 0; i < list_Go.Count; i++) {
            if (gc.name == list_Go[i].name) {
                list_Go.Remove(list_Go[i]);
                for (int j = 0; j < list_Go.Count; j++)
                {
                    if (list_Go[j].transform.localPosition == vt)
                    {
                        return;
                    }                  
            }
            }
            
        }
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            if (UIButton.current.name == cardContainer.transform.GetChild(i).name)
            {                
                index = i;
                break;
            }         
        }
        if (gc.transform.localPosition!=vt) {
            currentGO = gc;
            LightName(currentGO);
            TweenPosition tp = gc.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.delay = 0.2f;
            tp.duration = 0.3f;
            tp.onFinished.Clear();
            tp.from = gc.transform.localPosition;           
            tp.to = vt;
            tp.ResetToBeginning();

            TweenRotation tr = gc.GetComponent<TweenRotation>();
            tr.enabled = true;
            tr.delay = 0.2f;
            tr.duration = 0.3f;
            tr.onFinished.Clear();
            tr.from = gc.transform.localRotation.eulerAngles;
            tr.to = vr;           
            tr.ResetToBeginning();

            tr.onFinished.Add(new EventDelegate(SetChoseBtnStart));

        }
        else   {
            TweenPosition tp = gc.GetComponent<TweenPosition>();           
            tp.enabled = true;
            tp.delay = 0.2f;
            tp.duration = 0.3f;
            tp.onFinished.Clear();
            tp.from = vt;
            tp.to =backV_List[index];
            tp.ResetToBeginning();

            TweenRotation tr = gc.GetComponent<TweenRotation>();
            tr.enabled = true;
            tr.delay = 0.2f;
            tr.duration = 0.3f;
            tr.onFinished.Clear();
            tr.from = vr;
            tr.to = backR_List[index];           
            tr.ResetToBeginning();

            tr.onFinished.Add(new EventDelegate(SetChoseBtnClose));
        }     
}
    void BackToSkillCardPos(GameObject go) {
        NextSceneBtn.SetActive(false);
        Vector3 vt = new Vector3(-4.7f, 0.85f, 3.5f);
        Vector3 vr = new Vector3(0, 180, 0);
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.delay = 0.2f;
        tp.duration = 1f;
        tp.onFinished.Clear();
        tp.from = vt;
        tp.to = backV_List[index];
        tp.ResetToBeginning();

        TweenRotation tr = go.GetComponent<TweenRotation>();
        tr.enabled = true;
        tr.delay = 0.2f;
        tr.duration = 1f;
        tr.onFinished.Clear();
        tr.from = vr;
        tr.to = backR_List[index];
        tr.onFinished.Add(new EventDelegate(SetChoseBtnClose));
        tr.ResetToBeginning();
        
        tp.onFinished.Add(new EventDelegate(MakeCardFlyBack));
    }
    void SetChoseBtnStart() {
        choseSkillBtn.gameObject.SetActive(true);
        SkillNameContainer.SetActive(false);
       // NextSceneBtn.GetComponent<UIButton>().enabled = false;      
    }
    void SetChoseBtnClose() {
        choseSkillBtn.gameObject.SetActive(false);
        SkillNameContainer.SetActive(true);   
    }
}
