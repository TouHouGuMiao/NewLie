using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingmanManager
{
    private static WingmanManager _Instance=null;
    private GameObject wingmanPrefab;
    private Transform player;


    private Dictionary<int, WingmanData> WingmanDataDic;
    public static WingmanManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new WingmanManager();
            }
            return _Instance;
        }
    }

    public void Init()
    {
        string path = "WingmanPrefab/Wingman";
        wingmanPrefab = Resources.Load(path, typeof (GameObject))as GameObject;
        player = GameObject.FindWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("player is null");
            return;
        }

        WingmanData data = new global::WingmanData();
        data.id = 0;
        data.name = "";
        data.spriteName = "";
        data.tempTime = 1.0f;
        data.bulletName = "StarBullet";
        if (WingmanDataDic == null)
        {
            WingmanDataDic = new Dictionary<int, WingmanData>();
        }
        WingmanDataDic.Add(data.id,data);
    }

    public void ShowWingman(WingmanData data,int cout)
    {

        IEnumeratorManager.Instance.StartCoroutine(SetWingmanCout(data, cout));
       
    }

    public WingmanData GetDataById(int id)
    {
        WingmanData data = null;
        if(!WingmanDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("not wingmanData in dic!");
            return null;
        }
        return data;
    }


    #region  PingPang僚机循环设置
    private void PingPangOneStep(TweenPosition tp)
    {
        SpriteRenderer m_Render = tp.GetComponent<SpriteRenderer>();
        m_Render.sortingLayerName = "Bounds";
    }

    private void PingPangSecondStep(TweenPosition tp)
    {
        SpriteRenderer m_Render = tp.GetComponent<SpriteRenderer>();
        m_Render.sortingLayerName = "warriorCenter";
    }

    private void CreatWingmanReturn(WingmanData data,int cout,TweenPosition tp)
    {
        IEnumeratorManager.Instance.StartCoroutine(CreatWingmanIEnuerator(data, cout));
    }

    IEnumerator CreatWingmanIEnuerator(WingmanData data, int cout)
    {
        float tempTime = 2.0f / cout;
        GameObject wingman = player.Find("wingman").gameObject;
        int nowCout = wingman.transform.childCount;
        if (nowCout >= cout)
        {
            IEnumeratorManager.Instance.StopAllCoroutines();
        }
        yield return new WaitForSeconds(tempTime);
        AddOneWingman(data);
        TweenPosition m_TP = wingman.transform.GetChild(nowCout-1).GetComponent<TweenPosition>();
        m_TP.PingPangCoutSet = null;

        nowCout = wingman.transform.childCount;
        GameObject go = wingman.transform.GetChild(nowCout-1).gameObject;
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.tp = tp;
        tp.data = data;
        tp.cout = cout;
        tp.PingPangCoutSet += CreatWingmanReturn;
    }

    #endregion


    void AddOneWingman(WingmanData data)
    {
        GameObject parent = player.transform.Find("wingman").gameObject;
        GameObject wingaman = GameObject.Instantiate(wingmanPrefab);

        wingaman.transform.SetParent(parent.transform, false);
        TweenPosition tp = wingaman.GetComponent<TweenPosition>();
        tp.tp = tp;
        //Sprite sprite = ResourcesManager.Instance.LoadWingmanSprite(data.spriteName);
        tp.PingPangOneStop += PingPangOneStep;
        tp.PingPangTwoStop += PingPangSecondStep;
        WingmanInfo info = wingaman.GetComponent<WingmanInfo>();
        info.SetBullet(data.bulletName);
        info.tempTime = data.tempTime;
    }
     
    IEnumerator SetWingmanCout(WingmanData data,int cout)
    {
        GameObject parent = player.transform.Find("wingman").gameObject;
        float nowCout = parent.transform.childCount;
        for (int i = 0; i < nowCout; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            GameObject.Destroy(child);
        }

        float tempTime = 2.0f / cout;
        
        for (int i = 0; i < cout; i++)
        {

            GameObject wingaman = GameObject.Instantiate(wingmanPrefab);
          
            wingaman.transform.SetParent(parent.transform, false);
            TweenPosition tp = wingaman.GetComponent<TweenPosition>();
            tp.tp = tp;
            //Sprite sprite = ResourcesManager.Instance.LoadWingmanSprite(data.spriteName);
            tp.PingPangOneStop += PingPangOneStep;
            tp.PingPangTwoStop += PingPangSecondStep;
            WingmanInfo info = wingaman.GetComponent<WingmanInfo>();
            info.SetBullet(data.bulletName);
            info.tempTime = data.tempTime;
            yield return new WaitForSeconds(tempTime);
        }
    }



    //恢复 到 指定数目的僚机 （僚机单个离开自机的顺序 按第一个 第二个 第三个。。。才能使用的恢复方法）
    public void CreatWingmanReturnCout(WingmanData data, int cout)
    {
        GameObject wingman = player.Find("wingman").gameObject;
        int nowCout = wingman.transform.childCount;
        if (nowCout > cout)
        {
            Debug.LogError("cout is error!");
            return;
        }

        if (nowCout == 0)
        {
            AddOneWingman(data);
        }

        nowCout = wingman.transform.childCount;
        GameObject go = wingman.transform.GetChild(nowCout-1).gameObject;
        TweenPosition tp = go.GetComponent<TweenPosition>();
        tp.tp = tp;
        tp.data = data;
        tp.cout = cout;
        tp.PingPangCoutSet += CreatWingmanReturn;


    }

    
    



}
