using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalButtonTest : MonoBehaviour
{
    Transform battleCollider;
    private TweenPosition tpBullet;
    private TweenScale tsSphere;

    private GameObject cloneBullet;
    public AnimationCurve curve;
    public Vector3 from = Vector3.zero;
    public Vector3 to = Vector3.zero;

    public Vector3 scaleFrom = Vector3.zero;
    public Vector3 scaleTo = Vector3.zero;

    public float duration = 0;
    public float delay = 0;

    private bool isClick = false;

    public GameObject bullet;
    public GameObject depentSphere;

    public float delta = 0;
    private void Awake()
    {
        battleCollider = GameObject.FindWithTag("BattleCollider").transform;

    }

    private void DepentSphereInit()
    {
        tsSphere = depentSphere.AddComponent<TweenScale>();
        tsSphere.animationCurve = curve;
        tsSphere.from = scaleFrom;
        tsSphere.to = scaleTo;
        tsSphere.delay = delay;
        tsSphere.ignoreTimeScale = false;
        tsSphere.duration = duration + delta;
        EventDelegate OnFished = new EventDelegate(OnTsFinshed);
        tsSphere.onFinished.Add(OnFished);
    }


    private void BulletInit()
    {
        GameObject go = Instantiate(bullet).gameObject;
        cloneBullet = go;
        tpBullet = go.AddComponent<TweenPosition>();
        tpBullet.animationCurve = curve;
        tpBullet.from = from;
        tpBullet.to = to;
        tpBullet.delay = delay;
        tpBullet.duration = duration;
        tpBullet.ignoreTimeScale = false;

    }
    // Use this for initialization
    void Start()
    {
        TweenAlpha ta = this.gameObject.AddComponent<TweenAlpha>();
        ta.delay = delay;
        ta.duration = 0.2f;
        ta.from = 0;
        ta.to = 1;
        ta.ignoreTimeScale = false;
        DepentSphereInit();
        BulletInit();
        transform.position = new Vector3(to.x, to.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float factor = tsSphere.Factor;
        if (factor >= 0.9)
        {
            isClick = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (isClick)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
            {
                tsSphere.gameObject.SetActive(false);
                gameObject.SetActive(false);
                AudioManager.Instance.PlayEffect_Source("waterAudio");
                AddFanTanForce();
            }

        }
    }

 
    private void AddFanTanForce()
    {
        cloneBullet.GetComponent<TweenPostionTest>().enabled = false;
        Vector3 targetPos = new Vector3(from.x, from.y, 0);
        Vector3 targetVec = Vector3.Normalize(cloneBullet.transform.position - battleCollider.position);
        this.GetComponent<Collider>().enabled = false;
        Rigidbody rgbody = cloneBullet.transform.GetComponent<Rigidbody>();
        rgbody.isKinematic = false;
        rgbody.useGravity = true;
        rgbody.AddForce(targetVec * 1000);
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
