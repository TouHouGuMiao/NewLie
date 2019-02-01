using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏杂项管理
/// </summary>
public class GameZaXiangManager
{
    private static GameZaXiangManager _Instance = null;
    public static GameZaXiangManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameZaXiangManager();
            }
            return _Instance;
        }
    }

    public void ShowCover()
    {
        GUIManager.ShowView("CoverPanel");
    }
}
