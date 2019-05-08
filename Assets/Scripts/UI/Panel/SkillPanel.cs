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

    private List<Vector3> backV_List = new List<Vector3>();
    private List<Vector3> backR_List = new List<Vector3>();
    private Dictionary<int, Skill> SkillDic;
    public static int skillPoints=10;//技能职业加点
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
        SkillDic=SkillManager.Instance.GetSkillDataInDic();
        cardContainer.gameObject.SetActive(false);
        //pointts = SkillPanel.skillPoints;    
        pointts = Skill.CanUsePoints;//总的点数
        startBtn.gameObject.SetActive(true);
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
        SkillDesInfo.gameObject.GetComponent<UISprite>().alpha =1;
       
       // canAddPoints.text = skillPoints.ToString();
        AddDelegate();
        
         AddClickDelegate();
        
        //addPointsBtn.onClick.Add(new EventDelegate(OnClickAddPointsBtn));
        //minusPointsBtn.onClick.Add(new EventDelegate(OnClickMinusPointsBtn));
    }
    int c_Point;
    void OnClickAddPointsBtn() {


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
    void OpenOtherSkillCard_Tween(GameObject go)
    {
        TweenScale ts = go.GetComponent<TweenScale>();
        ts.enabled = true;
        ts.duration = 0.5f;
        ts.delay = 0.2f;
        ts.from = new Vector3(0, 0, 0);
        ts.to = new Vector3(80, 80, 80);
        ts.onFinished.Clear();
        ts.ResetToBeginning();

    }
    void OnClickCompleteBtm() {
        SkillDesInfo.gameObject.SetActive(false);
        //OpenOtherSkillCard();
        BackToSkillCardPos(currentGO);
        OpenAddCardEnabled();
        skillpointsChange.SetActive(false);
        completeBtn.gameObject.SetActive(false);
        Skill.CanUsePoints = pointts;
        if (currentGO.name.Contains("0"))
        {
            Skill s1 = SkillManager.Instance.GetSkillDataById(1);
            s1.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s1);
            OnFinishCompleteBtn(1);
        }
        else if (currentGO.name.Contains("1"))
        {
            Skill s2 = SkillManager.Instance.GetSkillDataById(2);
            s2.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s2);
            Skill s5 = SkillManager.Instance.GetSkillDataById(2);
            Debug.LogError(s2.data.SkillPoints);
            OnFinishCompleteBtn(2);
        }
        else if (currentGO.name.Contains("2"))
        {
            Skill s3 = SkillManager.Instance.GetSkillDataById(3);
            s3.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s3);
            OnFinishCompleteBtn(3);
        }
        else if (currentGO.name.Contains("3"))
        {
            Skill s4 = SkillManager.Instance.GetSkillDataById(4);
            s4.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s4);
            OnFinishCompleteBtn(4);
        }
        else if (currentGO.name.Contains("4")) {
            Skill s5 = SkillManager.Instance.GetSkillDataById(5);
            s5.data.SkillPoints = c_Point;
            SkillManager.Instance.UpDataSkillData(s5);
            OnFinishCompleteBtn(5);
        }
    }
    void OnFinishCompleteBtn(int id) {
        for (int i = 0; i < SkillNameContainer.transform.childCount+1; i++) {
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
        if (go.name.Contains("0")) {
            OnTwiceChosePoints(1);
        }
        else if (currentGO.name.Contains("1"))
        {
            OnTwiceChosePoints(2);
        }
        else if (currentGO.name.Contains("2"))
        {
            OnTwiceChosePoints(3);
        }
        else if (currentGO.name.Contains("3"))
        {
            OnTwiceChosePoints(4);
        }
        else if (currentGO.name.Contains("4"))
        {
            OnTwiceChosePoints(5);
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
    void OnClickChoseSkillBtn() {
        choseSkillBtn.gameObject.SetActive(false);
        //
        //Debug.LogError(SkillDic[1].data.ID + "h");
        if (currentGO.name.Contains("0"))
        {
            MoveToScreenLeft();
            //currentGO.GetComponent<UIButton>().enabled = false;
            CloseAllCardEnabeld();
            UpdateSkillInfo(1);
        }
        else if (currentGO.name.Contains("1"))
        {
            MoveToScreenLeft();
            //currentGO.GetComponent<UIButton>().enabled = false;
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
        //Skill s = SkillDic[id];
        skillName.text = s.data.Name;
        canUsePoints.text = pointts.ToString();
        points.text = s.data.SkillPoints.ToString();
        SkillDesInfo.GetChild(0).GetComponent<UILabel>().text = s.data.Des;
       
    }
    void AlphaShowDes_Info() {
        SkillDesInfo.gameObject.SetActive(true);
        TweenAlpha ta = SkillDesInfo.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.delay = 0.2f;
        ta.duration = 0.3f;
        ta.onFinished.Clear();
        ta.from = 0;
        ta.to = 1;
        ta.ResetToBeginning();
    }
    void MoveToScreenLeft() {
        Vector3 vt = new Vector3(-4.7f, 0.85f, 3.5f);
        TweenPosition tp = currentGO.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.delay = 0.2f;
        tp.duration = 0.3f;
        tp.onFinished.Clear();
        tp.from = currentGO.transform.localPosition;
        tp.to = vt;
        tp.ResetToBeginning();
        tp.onFinished.Add(new EventDelegate(OpenSkillChose));
    }
    void OpenSkillChose() {
        //SkillDesInfo.gameObject.SetActive(true);      
        skillpointsChange.SetActive(true);
        completeBtn.gameObject.SetActive(true);
        AlphaShowDes_Info();
        CloseOtherSkillCard();
    }
    void CloseOtherSkillCard() {
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            if (cardContainer.transform.GetChild(i).name != currentGO.name) {
                cardContainer.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void AddClickDelegate() {//添加按钮的点击方法      
        startBtn.onClick.Add(new EventDelegate(OnClickStartBtn));
        choseSkillBtn.onClick.Add(new EventDelegate(OnClickChoseSkillBtn));
        completeBtn.onClick.Add(new EventDelegate(OnClickCompleteBtm));
    }
    void AddDelegate() {//添加委托
      addPointsBtn.skillBtnDelegate += ChoiceAddOrMinus;
      minusPointsBtn.skillBtnDelegate += ChoiceAddOrMinus;
    }
    void InitSkillCard()
    {
        startBtn.gameObject.SetActive(false);
        cardContainer.gameObject.SetActive(true);
       
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {

            GameObject go = cardContainer.transform.GetChild(i).gameObject;
            UIButton btn = go.GetComponent<UIButton>();
           
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
            //if (go.transform.localPosition.x >= -7)
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
                PlayCardEffectMusic();
            }
        }
       // list_Go.Clear();
    }

    private bool isCardMoveToScreenOver=false;
    void CardMoveToScreen() {
        if (isCardMoveToScreenOver)
        {
            return;
        }
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            
            
            GameObject go = cardContainer.transform.GetChild(i).gameObject;
            UIButton btn = go.GetComponent<UIButton>();
            if (go.transform.localPosition.y < 12.5f)
            {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.onFinished.Clear();
                tp.delay = 0.3f * i + 1f;
                tp.duration = 0.5f;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(-10.0f + i * 5.0f, 3.2f, 11.8f);

                tp.ResetToBeginning();
                TweenRotation tr = go.GetComponent<TweenRotation>();
                tr.enabled = true;
                tr.delay = 0.3f * i + 1f;
                tr.duration = 0.5f;
                tr.onFinished.Clear();
                tr.from = go.transform.rotation.eulerAngles;
                tr.to = new Vector3(0, 150, 0);
                tr.ResetToBeginning();
            }
            if (go.transform.localPosition.y == 12.5f) {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.onFinished.Clear();
                tp.delay = 0.3f * i + 1f;
                tp.duration = 0.5f;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(-10.0f + i * 5.0f, 3.2f, 11.8f);

                tp.ResetToBeginning();
                TweenRotation tr = go.GetComponent<TweenRotation>();
                tr.enabled = true;
                tr.delay = 0.3f * i + 1f;
                tr.duration = 0.5f;
                tr.onFinished.Clear();
                tr.from = go.transform.rotation.eulerAngles;
                tr.to = new Vector3(0, 180, 0);
                tr.ResetToBeginning();
            }
            if (go.transform.localPosition.y > 12.5f) {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.from = go.transform.localPosition;
                tp.onFinished.Clear();
                tp.delay = 0.3f * i + 1f;
                tp.duration = 0.5f;
                tp.from = go.transform.localPosition;
                tp.to = new Vector3(-10.0f + i * 5.0f, 3.2f, 11.8f);

                tp.ResetToBeginning();
                TweenRotation tr = go.GetComponent<TweenRotation>();
                tr.enabled = true;
                tr.delay = 0.3f * i + 1f;
                tr.duration = 0.5f;
                tr.onFinished.Clear();
                tr.from = go.transform.rotation.eulerAngles;
                tr.to = new Vector3(0, -150, 0);
                tr.ResetToBeginning();
            }
            if (i >= cardContainer.transform.childCount - 1)
            {
                TweenPosition tp = go.GetComponent<TweenPosition>();
                tp.onFinished.Add(new EventDelegate(OnFinshedMoveToScreen));

            }
           
        }
        isCardMoveToScreenOver = true;
    }
    
    void OnFinshedMoveToScreen() {
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            backV_List.Add(cardContainer.transform.GetChild(i).localPosition);
            backR_List.Add(cardContainer.transform.GetChild(i).localRotation.eulerAngles);
            cardContainer.transform.GetChild(i).GetComponent<UIButton>().onClick.Add(new EventDelegate(OncClickSkillCard));          
        }
        SkillNameContainer.SetActive(true);
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
    private GameObject currentGO;//用于得到当前是哪张技能卡片
    void OncClickSkillCard() {//点击卡片放大方法    
        list_Go.Clear();
        Vector3 vt = new Vector3(0.45f, 0.85f, 3.5f);
        Vector3 vr = new Vector3(0, 180, 0);
        GameObject gc = UIButton.current.gameObject;
        //if (gc.GetComponent<UIButton>().enabled == false)
        //{
        //    gc.GetComponent<UIButton>().enabled = true;
        //    Debug.LogError(11112);
        //}
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
                Debug.LogError("11111" + UIButton.current.name);
                index = i;
                break;
            }
            //else {
            //    Debug.LogError("name is not exist"+ "  "+index+"index"+"  "+"UIButtonName+"+ UIButton.current.name);
                
            //}
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
            tr.onFinished.Add(new EventDelegate(SetChoseBtnStart));
            tr.ResetToBeginning();
                   
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
            tr.onFinished.Add(new EventDelegate(SetChoseBtnClose));
            tr.ResetToBeginning();
        }
       //PlayCardEffectMusic();
}
    void BackToSkillCardPos(GameObject go) {
        Vector3 vt = new Vector3(-4.7f, 0.85f, 3.5f);
        Vector3 vr = new Vector3(0, 180, 0);
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.delay = 0.2f;
        tp.duration = 0.5f;
        tp.onFinished.Clear();
        tp.from = vt;
        tp.to = backV_List[index];
        tp.ResetToBeginning();

        TweenRotation tr = go.GetComponent<TweenRotation>();
        tr.enabled = true;
        tr.delay = 0.2f;
        tr.duration = 0.5f;
        tr.onFinished.Clear();
        tr.from = vr;
        tr.to = backR_List[index];
        tr.onFinished.Add(new EventDelegate(SetChoseBtnClose));
        tr.ResetToBeginning();

        tp.onFinished.Add(new EventDelegate(OpenOtherSkillCard));
    }
    void SetChoseBtnStart() {
        choseSkillBtn.gameObject.SetActive(true);
        choseSkillBtn.GetComponent<UISprite>().alpha = 0;       
        TweenAlpha ta = choseSkillBtn.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.delay = 0.2f;
        ta.duration = 0.3f;
        ta.onFinished.Clear();
        ta.to = 1;      
        ta.from = 0;
        ta.ResetToBeginning();
        //choseSkillBtn.gameObject.SetActive(false);
    }
    void SetChoseBtnClose() {        
        TweenAlpha ta = choseSkillBtn.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.delay = 0.2f;
        ta.duration = 0.3f;
        ta.onFinished.Clear();
        ta.to = 0;
        ta.from = 1;
        ta.ResetToBeginning();       
    }
}
