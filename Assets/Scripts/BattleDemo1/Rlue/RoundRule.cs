using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RoundRule:MonoBehaviour {
    private GameObject lingFuKu;
    public enum RoundState
    {
        UseCardState,
        BattleState,
    }
    private List<CardBase> lingFuKuList = new List<CardBase>(); 
    private int cost = 1;
    public int roundCount = 1;
    private static int pValue=10000;

    public static RoundRule Instance;

    private bool isComputerTime = false;

    private float roundTime=0;

    private int LingFuKuAddCount = 0;
    private GameObject doorGird;
    public float animaotrSpeed = 2.5f;
    private int doorCount = 0;
    void Awake()
    {
        Instance = this;
    }

    void Start() { 
        lingFuKu = GameObject.FindWithTag("LingFuKu");
        doorGird = GameObject.FindWithTag("Door");
    }
    public static bool CompltePValue(int value)
    {
        bool canUse = true;
        if (pValue + value < 0)
        {
            canUse = false;
        }
        return canUse;
    }
    public void ChangeLingFuKuXiaoLv_Per(float value)
    {
        animaotrSpeed = (1 + value) * animaotrSpeed;
        for (int i = 0; i < doorGird.transform.childCount; i++)
        {
            GameObject go = doorGird.transform.GetChild(i).gameObject;
            LingFuOfDoor lfo = go.GetComponent<LingFuOfDoor>();
            lfo.delay = (1 - value) * lfo.delay;
        }
    }

    public void DepentOpenDoor()
    {
        if (LingFuKuAddCount >= 50 && doorCount <= 0)
        {
            doorCount++;
            OpenLingFuKuOfDoor();
        }

        if (LingFuKuAddCount >= 200 && doorCount <= 1)
        {
            doorCount++;
            OpenLingFuKuOfDoor();
        }

        if (LingFuKuAddCount >= 450 && doorCount <= 2)
        {
            doorCount++;
            OpenLingFuKuOfDoor();
        }


        if (LingFuKuAddCount >= 750 && doorCount <= 3)
        {
            doorCount++;
            OpenLingFuKuOfDoor();
        }

        if (LingFuKuAddCount >= 1050 && doorCount <= 4)
        {
            doorCount++;
            OpenLingFuKuOfDoor();
        }

        if (LingFuKuAddCount >= 1450 && doorCount <= 5)
        {
            doorCount++;
            OpenLingFuKuOfDoor();
        }

    }

    public void DoorFire()
    {
     
        for (int i = 0; i < doorGird.transform.childCount; i++)
        {
            GameObject go = doorGird.transform.GetChild(i).gameObject;
            if (go.activeSelf)
            {
                LingFuOfDoor door = go.GetComponent<LingFuOfDoor>();
                door.StartFire();
            }
        }
    }

    public void DoorStopFire()
    {
        for (int i = 0; i < doorGird.transform.childCount; i++)
        {
            GameObject go = doorGird.transform.GetChild(i).gameObject;
            if (go.activeSelf)
            {
                LingFuOfDoor door = go.GetComponent<LingFuOfDoor>();
                door.StopFire();
            }
        }
    }

    private void OpenLingFuKuOfDoor()
    {
        for (int i = 0; i < doorGird.transform.childCount; i++)
        {
            GameObject go = doorGird.transform.GetChild(i).gameObject;
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return;
            }
        }
    }

    public void AddLingFuToKu(CardBase data)
    {
        lingFuKuList.Add(data);
        LingFuKuAddCount++;
        SetNumberCount(lingFuKu.transform.Find("Grid").GetComponent<UIGrid>(), lingFuKuList.Count);
    }
    System.Random getIndexRD = new System.Random();
    public CardBase GetDateFormLingFuKu()
    {
        CardBase data = null;
        int index = getIndexRD.Next(lingFuKuList.Count);
        data = lingFuKuList[index];
        lingFuKuList.Remove(data);
        SetNumberCount(lingFuKu.transform.Find("Grid").GetComponent<UIGrid>(), lingFuKuList.Count);
        return data;
    }
    
    private RoundState currentState= RoundState.UseCardState;
    private RoundState lastState = RoundState.UseCardState;
    void Update()
    {
        if (roundTime<=0&&isComputerTime)
        {
            isComputerTime=false;
            PlayerBattlePanel.ShowHandCardPanel();
            currentState = RoundState.UseCardState;
        }

        if (isComputerTime)
        {
            if (PlayerBattleRule.Instance.IsReduceTime())
            {
                return;
            }
            roundTime -= Time.deltaTime * 1*PlayerBattleRule.Instance.timeScale;
            PlayerBattlePanel.timeLabel.text = ((int)(roundTime)).ToString();
            currentState = RoundState.BattleState;
        }
        else
        {
            roundTime = -1;
        }
        WhenStateChange();
    }

    private IEnumerator OnMoveToRoundState()
    {
        yield return new WaitForSeconds(0.75f);
        DepentOpenDoor();
    }

    private void WhenStateChange()
    {
        if (lastState != currentState)
        {
            if(currentState== RoundState.BattleState)
            {
                CameraMoveToBattleSence();
                PlayAnimator();
                PlayerBattlePanel.ShowTimeLabel();
                DoorFire();
                lastState = currentState;
                //AudioManager.Instance.LerpBGVolme(0.8f, 0.8f);
            }

            if(currentState == RoundState.UseCardState)
            {
                PlayerBattlePanel.CloseTimelabel();
                CameraMoveToRoundSence();
                DestorAllBulletAndStopAniamtor();
                lastState = currentState;
                DoorStopFire();
                HandCardPanel.ClearAddKeyCodeContioner();
                StartCoroutine(OnMoveToRoundState());
                //AudioManager.Instance.LerpBGVolme(0.3f,0.8f);
            }
        }
    }
    private void PlayAnimator()
    {
        Animator playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        Animator enemyAnimator = GameObject.FindWithTag("enemy").GetComponent<Animator>();
        enemyAnimator.enabled = true;
        playerAnimator.enabled = true;
        playerAnimator.speed = 1;
        enemyAnimator.speed = 1;
    }

    private void DestorAllBulletAndStopAniamtor()
    {
        GameObject bulletParent = GameObject.FindWithTag("Bullet");
        for (int i = 0; i < bulletParent.transform.childCount; i++)
        {
            GameObject go = bulletParent.transform.GetChild(i).gameObject;
            Destroy(go);
        }
        Animator playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        Animator enemyAnimator = GameObject.FindWithTag("enemy").GetComponent<Animator>();
        playerAnimator.speed=0;
        enemyAnimator.speed = 0;
    }

    private void CameraMoveToRoundSence()
    {
        Transform camera = GameObject.FindWithTag("MainCamera").transform;
        TweenPosition tp = camera.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.from = camera.transform.position;
        tp.to = new Vector3(0.07f, 6.2f, -1.6f);
        tp.delay = 0;
        tp.onFinished.Clear();
        tp.duration = 0.75f;
        tp.ResetToBeginning();

        TweenRotation tr = camera.GetComponent<TweenRotation>();
        tr.enabled = true;
        tr.from = camera.rotation.eulerAngles;
        tr.to = new Vector3(84.00002f, -6.123075e-09f, -3.99363e-14f);
        tr.delay = 0;
        tr.duration = 0.75f;
        tr.onFinished.Clear();
        tr.ResetToBeginning();

        GameObject cover = GameObject.FindWithTag("xiangKuang").transform.Find("cover").gameObject;
        TweenAlpha ta = cover.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.from = ta.GetComponent<SpriteRenderer>().color.a;
        ta.to = 0.34f;
        ta.delay = 0;
        ta.duration = 0.75f;
        ta.onFinished.Clear();
        ta.ResetToBeginning();

    }

    private void CameraMoveToBattleSence()
    {
        Transform camera = GameObject.FindWithTag("MainCamera").transform;
        TweenPosition tp = camera.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.from = camera.transform.position;
        tp.to = new Vector3(0.07f, 2.13f, 0.69f);
        tp.delay = 0;
        tp.onFinished.Clear();
        tp.duration = 0.75f;
        tp.ResetToBeginning();

        TweenRotation tr = camera.GetComponent<TweenRotation>();
        tr.enabled = true;
        tr.from = camera.rotation.eulerAngles;
        tr.to = new Vector3(90, 0, 0);
        tr.delay = 0;
        tr.duration = 0.75f;
        tr.onFinished.Clear();
        tr.ResetToBeginning();

        GameObject cover = GameObject.FindWithTag("xiangKuang").transform.Find("cover").gameObject;
        TweenAlpha ta = cover.GetComponent<TweenAlpha>();
        ta.enabled = true;
        ta.from = ta.GetComponent<SpriteRenderer>().color.a;
        ta.to = 0;
        ta.delay = 0;
        ta.duration = 0.75f;
        ta.onFinished.Clear();
        ta.ResetToBeginning();

    }

    public static void ChangePValue(int changeValue)
    {
        pValue += changeValue;
        PlayerBattlePanel.UpdatePValueLabel(pValue);
    }

    public void ChangePValueForChainBoom()
    {
        RoundRule.ChangePValue(1);
        GameObject.Destroy(TweenPosition.current.gameObject, 0.1f);
    }

    public void RounBattleStart()
    {
        HandCardPanel.DrawCard(3);
        cost = 1;
        roundCount = 1;
    }

    public void SetNextRoundTime(float time)
    {
        roundCount++;
        roundTime = time;
        isComputerTime = true;

    }
    public void SetNumberCount(UIGrid grid, int count)
    {
        SpriteRenderer wanSprite = grid.transform.Find("wan").GetComponent<SpriteRenderer>();
        SpriteRenderer qianSprite = grid.transform.Find("qian").GetComponent<SpriteRenderer>();
        SpriteRenderer baiSprite = grid.transform.Find("bai").GetComponent<SpriteRenderer>();
        SpriteRenderer shiSprite = grid.transform.Find("shi").GetComponent<SpriteRenderer>();
        SpriteRenderer geSprite = grid.transform.Find("ge").GetComponent<SpriteRenderer>();
        int wan = count / 10000;
        if (wan != 0)
        {
            wanSprite.sprite = ResourcesManager.Instance.LoadNumberSprite(wan.ToString());
            wanSprite.gameObject.SetActive(true);
        }
        else
        {
            wanSprite.gameObject.SetActive(false);
        }
        int qian = (count % 10000) / 1000;
        if (qian != 0)
        {
            if ( wan == 0)
                qianSprite.sprite = ResourcesManager.Instance.LoadNumberSprite(qian.ToString());
            qianSprite.gameObject.SetActive(true);
        }
        else
        {
            qianSprite.gameObject.SetActive(false);
        }
        int bai = (count % 1000) / 100;
        if (bai != 0)
        {
            baiSprite.sprite = ResourcesManager.Instance.LoadNumberSprite(bai.ToString());
            baiSprite.gameObject.SetActive(true);
        }
        else
        {
            if ( qian == 0 && wan == 0)
                baiSprite.gameObject.SetActive(false);
        }
        int shi = (count % 100) / 10;
        if (shi != 0)
        {
            shiSprite.sprite = ResourcesManager.Instance.LoadNumberSprite(shi.ToString());
            shiSprite.gameObject.SetActive(true);
        }
        else
        {
            if ( bai == 0 && qian == 0 && wan == 0)
                shiSprite.gameObject.SetActive(false);
        }
        int ge = (count % 10);
        if (ge != 0)
        {
            geSprite.sprite = ResourcesManager.Instance.LoadNumberSprite(ge.ToString());
            geSprite.gameObject.SetActive(true);
        }
        else
        {
            if(shi==0&&bai==0&&qian==0&&wan==0)
            geSprite.gameObject.SetActive(false);
        }
        grid.Reposition();
    }



    public int GetPValue()
    {
        int temp = 0;
        temp = pValue;
        return pValue;
    }
    


}
