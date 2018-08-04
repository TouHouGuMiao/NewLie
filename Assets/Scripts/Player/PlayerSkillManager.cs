using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager
{
    private static PlayerSkillManager _Instance=null;
    public static PlayerSkillManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new PlayerSkillManager();
            }
            return _Instance;
        }
    }

    private PlayerPuGong m_PuGong;
    public CharacterPropBase playerPro;

    public void InitPlayerSkillManager()
    {
        playerPro = CharacterPropManager.Instance.GetCharcaterDataByName("reimu");
        m_PuGong = new global::PlayerPuGong();
        m_PuGong.Init();
    }


    public void ShowPuGong(Vector3 target,Transform yinYangYu1,Transform yinYangYu2)
    {
        m_PuGong.Show(target,yinYangYu1,yinYangYu2);
    }


}


class PlayerPuGong
{
    private GameObject PuGongButtle;
    private CharacterPropBase playerPro;

   public void Init()
    {
        PuGongButtle = ResourcesManager.Instance.LoadBullet("PlayerPuGongBullet");
        playerPro = PlayerSkillManager.Instance.playerPro;
    }


    public void Show(Vector3 target,Transform yinYangYu1,Transform yinYangYu2)
    {
        GameObject go = GameObject.Instantiate(PuGongButtle);
        go.transform.position = yinYangYu1.position+new Vector3(0.7f,0,0);
        go.transform.eulerAngles = Vector3.zero;
        StartLockBullet m_Bullet = go.GetComponent<StartLockBullet>();
        m_Bullet.injured = 10 + playerPro.bulletPower;
        m_Bullet.injured = CRTDepent(m_Bullet.injured);
        m_Bullet.HP = 40;
        m_Bullet.target = target;

        GameObject go2 = GameObject.Instantiate(PuGongButtle);
        go2.transform.position = yinYangYu2.position+new Vector3(0.7f, 0, 0);
        go2.transform.eulerAngles = new Vector3(0, 0, 0);
        StartLockBullet m_Bullet2 = go2.GetComponent<StartLockBullet>();
        m_Bullet2.target = target;
        m_Bullet2.injured = 10 + playerPro.bulletPower;
        m_Bullet2.injured = CRTDepent(m_Bullet2.injured);
        m_Bullet2.HP = 40;



    }

    private float CRTDepent( float injured)
    {
        float CRTCount =(float)(Random.Range(0, 1.0f));
        if (playerPro.CRT >= CRTCount)
        {
            injured = injured * playerPro.CRTPower;
            return injured;
        }
        else
        {
            return injured;
        }
    }
}
