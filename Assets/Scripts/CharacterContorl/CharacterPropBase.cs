using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPropBase:MonoBehaviour
{
    public float HP
    {
        get
        {
            return (25 + 1.5f* VIT);
        }
    }
    public float speed
    {
        get
        {
            return(400 + 0.3f * DEX + 0.05f * Power)*0.01f;//自带0.01的实际数据修正
        }
        
    }
    public float CRT
    {
        get
        {
            return (float)(0.02+0.001*Lucky+0.0015*DEX+0.0005*Power);
        }
    }//暴击

    public float CRTPower
    {
        get
        {
            return 2.0f;
        }
    }


    public float bulletPower
    {
        get
        {
            return (float)(0.8*DEX+1.2*Power+0.3*VIT);
        }

    }

    public float defenseLV
    {
        get
        {
            float defens= (1 - (0.004f * DEF + 0.002f * VIT + 0.001f * Lucky));
            if (defens <= 0.1f)
            {
                return 0.1f;
            }
            else
            {
                return defens;
            }
       
            
        }
    }

    public float DEF;//防御
    public float Power;
    public float DEX;//敏捷
    public float VIT;//体质
    public float Lucky;
    

}
