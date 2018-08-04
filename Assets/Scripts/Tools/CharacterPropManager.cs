using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }

            return _Instance;
        }
    }


    public Dictionary<string,CharacterPropBase> CharacterPropDic=new Dictionary<string, CharacterPropBase> ();

    public void InitCharacterDic()
    {
        GameObject reimuPrefab = null;
        string path = "characters/reimu";
        reimuPrefab = Resources.Load(path, typeof(GameObject)) as GameObject;

        if (reimuPrefab == null)
        {
            Debug.LogError("remimuPrefab is null");
            return;
        }

        CharacterPropBase reimuProBase = reimuPrefab.GetComponent<CharacterPropBase>();
        if (reimuProBase == null)
        {
            Debug.LogError("reimuProBase is null");
            return;
        }
        CharacterPropDic.Add("reimu", reimuProBase);


        GameObject marisaPrefab = null;
        path = "characters/marisa";
        marisaPrefab = Resources.Load(path, typeof(GameObject)) as GameObject;

        if (marisaPrefab == null)
        {
            Debug.LogError("marisaPrefab is null");
            return;
        }

        CharacterPropBase marisaProBase = marisaPrefab.GetComponent<CharacterPropBase>();
        if (marisaProBase == null)
        {
            Debug.LogError("marisaProBase is null");
            return;
        }
        CharacterPropDic.Add("marisa", marisaProBase);
    }

    public CharacterPropBase GetCharcaterDataByName(string name)
    {
        CharacterPropBase data = null;

        if(!CharacterPropDic.TryGetValue(name,out data))
        {
            Debug.LogError(name + "not in dic!");
            return null;
        }

        return data;

    }

}
