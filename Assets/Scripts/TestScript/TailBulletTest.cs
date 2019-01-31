using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBulletTest : MonoBehaviour {
    public GameObject depentSphere;
    private GameObject cloneBullet;
    private TweenPosition tpBullet;

    private bool isClick=false;
    private bool isMiss = false;


    private ScalePostionTest m_SP;
    public GameObject tailBullet;

    public AnimationCurve tpCurve;
    public AnimationCurve depentCurve;

    public Vector3 depentPoint_From = Vector3.one;
    public Vector3 depentPoint_To = Vector3.one;
    public float depentPoint_Duration = 0;
    public float depentPoint_Delay = 0;

    public float delta = 0;

    public Vector3 tp_From = Vector3.one;
    public Vector3 tp_To = Vector3.one;

    public Vector3 begin_From = Vector3.one;
    public Vector3 begin_To = Vector3.one;
    public float begin_Duration = 0;
    public float begin_Delay = 0;

    public Vector3 click_From = Vector3.one;
    public Vector3 click_To = Vector3.one;
    public float click_Duration = 0;
    public float click_Delay = 0;
    private TweenScale tsSphere;

    private void Awake()
    {
        
    }

    private void Start()
    {
        TweenAlpha ta = this.gameObject.AddComponent<TweenAlpha>();
        ta.delay = depentPoint_Delay;
        ta.duration = 0.2f;
        ta.from = 0;
        ta.to = 1;
        ta.ignoreTimeScale = false;

        DepentSphereInit();
        BulletInit();
        InitSP();
        transform.position = new Vector3(tp_To.x, tp_To.y, 0);
    }


    private void DepentSphereInit()
    {
        tsSphere = depentSphere.AddComponent<TweenScale>();
        tsSphere.animationCurve = depentCurve;
        tsSphere.from = depentPoint_From;
        tsSphere.to = depentPoint_To;
        tsSphere.delay = depentPoint_Delay;
        tsSphere.ignoreTimeScale = false;
        tsSphere.duration = depentPoint_Duration + delta;
        EventDelegate OnFished = new EventDelegate(OnTsFinshed);
        tsSphere.onFinished.Add(OnFished);
    }


    private void BulletInit()
    {
        GameObject go = Instantiate(tailBullet).gameObject;
        cloneBullet = go;
        go.transform.position = new Vector3(tp_From.x, tp_From.y, 0);
        Vector3 tempPos = go.transform.InverseTransformPoint(new Vector3(tp_To.x, tp_To.y, 0));
        float angle = Mathf.Atan2(tempPos.y, tempPos.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        tpBullet = go.AddComponent<TweenPosition>();
        tpBullet.animationCurve = tpCurve;
        tpBullet.from = tp_From;
        tpBullet.to = tp_To ;
        tpBullet.delay = depentPoint_Delay;
        tpBullet.duration = depentPoint_Duration;
        tpBullet.ignoreTimeScale = false;

    }

    private void InitSP()
    {
        m_SP = cloneBullet.transform.Find("tail").GetComponent<ScalePostionTest>();
        m_SP.from = begin_From;
        m_SP.to = begin_To;
        m_SP.Duration = begin_Duration;
        m_SP.delay = begin_Delay;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("mouse"))
        {
            if (isClick)
            {
                if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.C))
                {
                    if (m_SP.IsActive)
                    {
                        isMiss = true;
                    }
                }

                if (!isMiss)
                {
                    if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
                    {
                        AudioManager.Instance.PlayEffect_Source("waterAudio");
                        m_SP.from = click_From;
                        m_SP.to = click_To;
                        m_SP.Duration = click_Duration;
                        m_SP.ResetToBegin();

                    }
                    if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.C))
                    {
                        tsSphere.gameObject.SetActive(false);
                        gameObject.SetActive(false);

                    }
                }




            }
        }
    }

    private void OnMouseOver()
    {
        

    }

    


    IEnumerator DepentSphereSetFalse()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    void OnTsFinshed()
    {
        isClick = true;
        StartCoroutine(DepentSphereSetFalse());
    }


}
