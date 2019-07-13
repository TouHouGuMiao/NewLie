using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollectionsPanel : IView {
    private UIButton nextBtn;
    private UIButton lastBtn;
    private int page;
    private Transform cardContainer;
    private Transform infoContainer;
    private List<EventCardData> m_CardList = new List<EventCardData>();

    private UIAtlas atla;
    public override void Update()
    {     
    }
    protected override void OnDestroy()
    {
        
    }

    protected override void OnHide()
    {
        DestoryCardsWhileHide();
    }

    protected override void OnShow()
    {
        // isActive = false;
        SystemPanel.CardCollectionsIsActive = true;
        atla = ResourcesManager.Instance.LoadAtlas("EventSpritesAtlas");
        infoContainer.transform.GetChild(3).GetComponent<UISprite>().atlas = atla;
        page = 1;
        m_CardList = CardManager.Instance.GetCollectionList();
        InitCard();
        InitInfo();
    }
    private int MaxPage {

        get {
            if (m_CardList.Count <= 6)
            {
                return 1;
            }
            else {
                if (m_CardList.Count % 6 == 0)
                {
                    return m_CardList.Count / 6;
                }
                else {
                    return m_CardList.Count / 6 + 1;
                }
            }
        }
    }

    void CloseBtnEnabled() {
        nextBtn.enabled = false;
        lastBtn.enabled = false;
        
    }
    void CloseCardClick() {
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            if (cardContainer.transform.GetChild(i).GetComponent<UIButton>().enabled == true)
            {
                cardContainer.transform.GetChild(i).GetComponent<UIButton>().enabled = false;
            }
        }
    }
    void StartBtnEnabled() {
        ShowFirstInfo(6 * (page - 1));
        nextBtn.enabled = true;
        lastBtn.enabled = true;
       
    }
    void StartCardClick() {
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {
            if (cardContainer.transform.GetChild(i).GetComponent<UIButton>().enabled == false)
            {
                cardContainer.transform.GetChild(i).GetComponent<UIButton>().enabled = true;
            }
        }
    }
    protected override void OnStart()
    {       
        nextBtn = this.GetChild("nextPage").GetComponent<UIButton>();
        lastBtn = this.GetChild("lastPage").GetComponent<UIButton>();
        cardContainer = this.GetChild("CardContainers").transform;
        infoContainer = this.GetChild("InfoContainer").transform;
        AddBtnDelegate();
    }
    private void InitCard() {
        CloseBtnEnabled();
        for (int i = 0; i <6; /*m_CardList.Count;*/ i++) {
            GameObject go = ResourcesManager.Instance.LoadEventCollectionsCard(i.ToString());
            GameObject card = GameObject.Instantiate(go);
            card.transform.SetParent(cardContainer);
            card.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            card.name = i.ToString();
            //card.transform.localPosition = new Vector3(-18.5f + (i * 1.5f), 3.9f, 0.5f);
            card.transform.localPosition = new Vector3(-4.5f+i*1.5f, 3.9f, -10);
            card.transform.localRotation = Quaternion.Euler(0, 180, 0);
            card.GetComponent<UIButton>().enabled = true;
            card.GetComponent<UIButton>().onClick.Add(new EventDelegate(OnClickCard));           
            TweenPosition tp = card.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.onFinished.Clear();
            tp.delay = 1 +0.2f*i;
            tp.duration=0.8f;
            tp.from = tp.transform.localPosition;
            tp.to = new Vector3(-4.5f + i * 1.5f, 3.9f, 0.5f);
            tp.ResetToBeginning();
            tp.onFinished.Add(new EventDelegate(PlayEffectMusic));
            if (i >= 5) {
                tp.onFinished.Clear();
                tp.onFinished.Add(new EventDelegate(StartBtnEnabled));
            }
        }                  
    }
    private void InitInfo() {
        if (cardContainer.transform.childCount == 0&&m_CardList.Count==0)
        {
            return;
        }
        else {
            //infoContainer.transform.GetChild(3).GetComponent<UISprite>().atlas = atla;
            infoContainer.transform.GetChild(3).GetComponent<UISprite>().spriteName = m_CardList[0].spriteName;
            infoContainer.transform.GetChild(2).GetComponent<UILabel>().text = m_CardList[0].Des;
        }
    }
    void OnClickCard() {
        Debug.LogError(UIButton.current.name);
        //infoContainer.transform.GetChild(3).GetComponent<UISprite>().atlas = atla;
        infoContainer.transform.GetChild(3).GetComponent<UISprite>().spriteName = m_CardList[CommonHelper.Str2Int(UIButton.current.name)].spriteName;
        infoContainer.transform.GetChild(2).GetComponent<UILabel>().text = m_CardList[CommonHelper.Str2Int(UIButton.current.name)].Des;
    }
    private void AddBtnDelegate() {
        nextBtn.onClick.Add(new EventDelegate(OnClickNextPageBtn));
        lastBtn.onClick.Add(new EventDelegate(OnClickLastPageBtn));
    }
    void OnClickLastPageBtn() {
        CloseBtnEnabled();
        CloseCardClick();
        isActive = false;
        if (page == 1)
        {
            nextBtn.enabled = true;
            lastBtn.enabled = false;
            StartCardClick();
            return;
        }
        else {           
            page--;           
            MakeCardBack_LastPage();
        }
    }
    void PlaySouSouEffect() {
        AudioManager.Instance.PlayEffect_Source("sousousou");
    }
    void PlayEffectMusic() {
        AudioManager.Instance.PlayEffect_Source("collectionsCard");
    }
    void OnClickNextPageBtn() {
        CloseBtnEnabled();
        CloseCardClick();
        isActive = false;
        if (page == MaxPage)
        {
            nextBtn.enabled = false;
            lastBtn.enabled = true;
            StartCardClick();
            return;
        }
        else
        {            
            page++;
            MakeCardBack_NextPage();           
        }       
        //IEnumeratorManager.Instance.StartCoroutine(ChangeNextPage());               
    }
    void ShowFirstInfo(int index) {
        infoContainer.transform.GetChild(3).GetComponent<UISprite>().spriteName = m_CardList[index].spriteName;
        infoContainer.transform.GetChild(2).GetComponent<UILabel>().text = m_CardList[index].Des;
    }
    bool isActive = false;
     void ChangeNextPage() {
        for (int i = 6 * (page - 1); i < 6 * page; i++)
        {
            if (i >= m_CardList.Count)
            {                              
                break;
            }          
                GameObject go = ResourcesManager.Instance.LoadEventCollectionsCard(m_CardList[i].id.ToString());
                GameObject card = GameObject.Instantiate(go);
                card.transform.SetParent(cardContainer);
                card.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                card.transform.localPosition = new Vector3(-4.5f + (i - (6 * (page - 1))) * 1.5f, 3.9f, -10);
                card.transform.localRotation = Quaternion.Euler(0, 180, 0);
                card.name = m_CardList[i].id.ToString();
                card.GetComponent<UIButton>().enabled = true;
                card.GetComponent<UIButton>().onClick.Add(new EventDelegate(OnClickCard));

                TweenPosition tp = card.GetComponent<TweenPosition>();
                tp.enabled = true;
                tp.onFinished.Clear();
                tp.delay = 0.5f + 0.2f * (i - (6 * (page - 1))) * 1.5f;
                tp.duration = 0.8f;
                tp.from = tp.transform.localPosition;
                tp.to = new Vector3(-4.5f + (i - (6 * (page - 1))) * 1.5f, 3.9f, 0.5f);
                tp.ResetToBeginning();
                tp.onFinished.Add(new EventDelegate(PlayEffectMusic));
            if (m_CardList.Count >= 6 * page)
            {
                if (i >= 6 * page - 1)
                {
                    tp.onFinished.Clear();
                    tp.onFinished.Add(new EventDelegate(StartBtnEnabled));
                }
            }
            else {
                if (i >= m_CardList.Count - 1) {
                    tp.onFinished.Clear();
                    tp.onFinished.Add(new EventDelegate(StartBtnEnabled));
                }
            }
        }
    }
    private List<GameObject> needDestroylist = new List<GameObject>();
    void MakeCardBack_NextPage() {
        for (int i = cardContainer.transform.childCount-1; i >= 0; i--) {
            GameObject go = cardContainer.transform.GetChild(i).gameObject;
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.onFinished.Clear();
            tp.delay = 0.2f * (5-i);
            tp.duration = 0.3f;
            tp.from = tp.transform.localPosition;
            tp.to = new Vector3(8.0f, 3.9f, -10.0f);
            tp.ResetToBeginning();
            needDestroylist.Add(go);
            tp.onFinished.Add(new EventDelegate(PlaySouSouEffect));
            if (i <= 0) {
                // DestroyCards();
                //tp.onFinished.Add(new EventDelegate(ChangeNextPage));
                tp.onFinished.Add(new EventDelegate(DestroyCards));
            }
        }
    }
    void MakeCardBack_LastPage() {       
        for (int i = 0; i < cardContainer.transform.childCount; i++)
        {           
            GameObject go = cardContainer.transform.GetChild(i).gameObject;           
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.enabled = true;
            tp.onFinished.Clear();
            tp.delay = 0.2f * i;
            tp.duration = 0.3f;
            tp.from = tp.transform.localPosition;
            tp.to = new Vector3(-8.0f, 3.9f, -10.0f);
            tp.ResetToBeginning();
            needDestroylist.Add(go);
            tp.onFinished.Add(new EventDelegate(PlaySouSouEffect));
            if (i >= cardContainer.transform.childCount-1)
            {
                tp.onFinished.Clear();
                tp.onFinished.Add(new EventDelegate(DestroyCards));
            }
        }
    }
    bool isClearUpAll = false;
    void DestroyCards()
    {
        for (int i = 0; i < needDestroylist.Count; i++)
        {
            GameObject go = needDestroylist[i];
            GameObject.Destroy(go);
            if (i >= needDestroylist.Count - 1)
            {
                Debug.LogError("zadsasdas");
                ChangeNextPage();
            }            
        }
        needDestroylist.Clear();
    }
    void DestoryCardsWhileHide() {
        for (int i = 0; i < cardContainer.transform.childCount; i++) {
            GameObject go = cardContainer.transform.GetChild(i).gameObject;
            GameObject.Destroy(go);
        }
    }
}
