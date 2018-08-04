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

    public void ShowDrawFiveStarBullet(Transform marisaTF)
    {
        drawFiveStars.ShowSkill(marisaTF);
    }

    public void ShowRotateBullet(Transform marisaVec)
    {
        rotateBullet.ShowSkill(marisaVec);
    }

    public void ShowXiaoSanBullet(Vector3 marisaVec)
    {
        xiaoSanBullet.ShowSkill(marisaVec);
    }

    public void ShowHeiDongBianYuanBullet(Vector3 marisaVec)
    {
        heiDongBianYuanBullet.ShowSkill(marisaVec);
    }

    public void ShowHeiDongInBullet(Vector3 marisaVec)
    {
        heiDongInBullet.ShowSkill(marisaVec);
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
        m_star.target = target;

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
