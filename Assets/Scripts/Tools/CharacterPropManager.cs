using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PropEventDelegate();

public enum PropType
{
    Stature,
    Power,
    VIT,
    Lucky,
    IQ,
    SkillPoint,
    Money,
    Idea,
    WeiYan,
    preesure,
}
/// <summary>
/// 本类用来管理人物的属性值
/// </summary>
public class CharacterPropManager
{
    private static CharacterPropManager _Instance = null;
    
    public static CharacterPropManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CharacterPropManager();
                _Instance.Init();
            }

            return _Instance;
        }
    }

    

    private CharacterPropBase playerCurrentProp = new CharacterPropBase();

    public Dictionary<string,CharacterPropBase> CharacterPropDic=new Dictionary<string, CharacterPropBase> ();
    public PropEventDelegate OnPropChangeFished=null;

    public CharacterPropBase GetPlayerProp()
    {
        CharacterPropBase characterPropBase = new CharacterPropBase();
        characterPropBase.preesure = 100;
        characterPropBase.Idea = 100;
        return characterPropBase;
    }  

    public void Init()
    {
        playerCurrentProp.preesure = 100;
    }

    public CharacterPropBase GetPlayerCureentProp()
    {
        return playerCurrentProp;
    }

    public void ChangePlayerCurrentProp(PropType type,float value,PropEventDelegate hander)
    {
        if(type== PropType.preesure)
        {
            playerCurrentProp.preesure = value;
            BattleUIPanel.ShowChangePressureSlider(hander);
        }
    }



}
