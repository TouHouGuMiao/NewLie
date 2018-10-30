using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarisaSkillManager
{
    private static MarisaSkillManager _instance = null;

    public static MarisaSkillManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MarisaSkillManager();
            }
            return _instance;
        }
    }


    public CharacterPropBase marisaPro;

    private MarisaPuGong PuGong;
    private MarisaSphere SphereBullets;
    private MarisaNormalStar NormalStar;
    private MarisaBoom boom;
    private MarisaMixBullet MixBullets;
    private MarisaPingBoom PingBoom;
    private MarisaDrawFiveStars drawFiveStars;
    private MarisaRotateBullet rotateBullet;
    private MarisaXiaoSan xiaoSanBullet;
    private MarisaHeiDongBianYuan heiDongBianYuanBullet;
    private MarisaHeiDongIn heiDongInBullet;
    private MarisaLockStarBoom lockBullet;
    private MarisaBoomInTarget boomInTarget;
    private MarisaZhiXianInstateBullet instateBullet;
    private MarisaTailLockBullet tailLockBullet;
    private MarisaRotateOutBullet rotateOutBullet;
    private MarisaMoPao moPao;
    private MarisaDemoSphere demoSphere;
    public void InitMarisaSkills()
    {
        marisaPro = CharacterPropManager.Instance.GetCharcaterDataByName("marisa");
      
        PuGong = new global::MarisaPuGong();
        SphereBullets = new MarisaSphere();
        NormalStar = new MarisaNormalStar();
        boom = new global::MarisaBoom();
        MixBullets = new MarisaMixBullet();
        PingBoom = new MarisaPingBoom();
        drawFiveStars = new global::MarisaDrawFiveStars();
        rotateBullet = new global::MarisaRotateBullet();
        xiaoSanBullet = new global::MarisaXiaoSan();
        heiDongBianYuanBullet = new global::MarisaHeiDongBianYuan();
        heiDongInBullet = new global::MarisaHeiDongIn();
        lockBullet = new global::MarisaLockStarBoom();
        boomInTarget = new MarisaBoomInTarget();
        instateBullet = new global::MarisaZhiXianInstateBullet();
        tailLockBullet = new global::MarisaTailLockBullet();
        rotateOutBullet = new global::MarisaRotateOutBullet();
        moPao = new global::MarisaMoPao();
        demoSphere = new global::MarisaDemoSphere();
        PuGong.Init();
        SphereBullets.InitSkill();
        NormalStar.InitSkill();
        boom.InitSkill();
        MixBullets.init();
        PingBoom.Init();
        drawFiveStars.Init();
        rotateBullet.Init();
        xiaoSanBullet.Init();
        heiDongBianYuanBullet.Init();
        heiDongInBullet.Init();
        lockBullet.Init();
        boomInTarget.Init();
        instateBullet.Init();
        tailLockBullet.Init();
        rotateOutBullet.Init();
        moPao.Init();
        demoSphere.Init();
    }

    public void ShowPuGong(Transform target,Transform point)
    {
        PuGong.Show(target,point);
    }

    public void ShowSphereBullets(Vector2 marisaVec)
    {
        SphereBullets.ShowSkill(marisaVec);
    }

    public void ShowNorStarBulltes(Transform target,Transform point)
    {
        NormalStar.ShowSkill(target, point);
    }

    public void ShowBoomBulltes(Vector2 marisaVec)
    {
        boom.ShowSkill(marisaVec);
    }

    public void ShowMixBullet(Transform marisaTF)
    {
        MixBullets.ShowSkill(marisaTF);
    }

    public void ShowPingBoomBullet(Vector3 pointVec,Transform marisaTF)
    {
        PingBoom.ShowSkill(pointVec, marisaTF); 
    }

    public void ShowDrawFiveStarBullet(Vector3 centerVec,float radius)
    {
        drawFiveStars.ShowSkill(centerVec, radius);
    }

    public void ShowRotateBullet(Transform marisaVec,Transform targetTF)
    {
        rotateBullet.ShowSkill(marisaVec, targetTF);
    }

    public void ShowXiaoSanBullet(Vector3 marisaVec)
    {
        xiaoSanBullet.ShowSkill(marisaVec);
    }

    public void ShowHeiDongBianYuanBullet(Transform marisaTF)
    {
        heiDongBianYuanBullet.ShowSkill(marisaTF);
    }

    public void ShowHeiDongInBullet(Vector3 marisaVec)
    {
        heiDongInBullet.ShowSkill(marisaVec);
    }

    public void ShowLockBoomBullet(Transform point,Transform target)
    {
        lockBullet.ShowSkill(point, target);
    }

    public void ShowBoomInTarget(Vector3 showVec)
    {
        boomInTarget.ShowSkill(showVec);
    }

    public void ShowZhiXianInstateBullet(Vector3 instateVec,Vector3 moveVec,Vector3 targetVec,bool isInstate=false,bool isRange=false)
    {
        instateBullet.ShowSkill(instateVec,moveVec, targetVec, isInstate,isRange);
    }

    public void ShowTailLockBullet(Transform marisaTF,Transform target)
    {
        tailLockBullet.ShowSkill(marisaTF, target);
    }

    public void ShowRotateOutBullet(Vector3 centerVec)
    {
        rotateOutBullet.ShowSkill(centerVec);
    }

    public void ShowMoPao(Transform point,Vector3 targetPos)
    {
        moPao.ShowSkill(point, targetPos);
    }

    public void ShowDemoSphere(Vector3 centerVec)
    {
        demoSphere.ShowSkill(centerVec);
    }
}

class MarisaPuGong
{
    private GameObject PuGongBullet;
    private CharacterPropBase marisaPro;

    public void Init()
    {
        PuGongBullet = ResourcesManager.Instance.LoadBullet("StarBullet");
        marisaPro = CharacterPropManager.Instance.GetCharcaterDataByName("marisa");
    }

    public void Show(Transform target,Transform point)
    {
        StarBullet m_star = new global::StarBullet();
        GameObject go = GameObject.Instantiate(PuGongBullet) as GameObject;
        go.transform.position = point.transform.position;
        m_star = go.GetComponent<StarBullet>();
        m_star.m_Type = BulletBase.BulletTpye.emptyBullet;
        //m_star.target = target;

        m_star.injured = 10F+marisaPro.bulletPower;
        m_star.injured = CRTDepent(m_star.injured);
        //临时处理
        m_star.HP = 150f;
    }

    private float CRTDepent(float injured)
    {
        float CRTCount = (float)(Random.Range(0, 1.0f));
        if (marisaPro.CRT >= CRTCount)
        {
            injured = injured * marisaPro.CRTPower;
            return injured;
        }
        else
        {
            return injured;
        }
    }
}
