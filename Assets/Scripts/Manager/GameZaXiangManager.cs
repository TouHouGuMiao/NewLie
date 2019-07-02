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
        CoverPanel.duration = 2;
        CoverPanel.fadeIn = false;
        CoverPanel.needAuteHide = true;
        GUIManager.ShowView("CoverPanel");
      
    }

    public void ShowCover(float time,bool neddAuteHide)
    {
        CoverPanel.duration = time;
        CoverPanel.needAuteHide = neddAuteHide;
        CoverPanel.fadeIn = false;
        GUIManager.ShowView("CoverPanel");
    }

    public void ShowCover_FadeIn(float time)
    {
        CoverPanel.duration = time;
        CoverPanel.fadeIn = true;
        CoverPanel.needAuteHide = false;
        GUIManager.ShowView("CoverPanel");
    }

    public void ShowCover_FadeIn(float time,EventDelegate OnFadeFished)
    {
        CoverPanel.duration = time;
        CoverPanel.OnCoverFished = OnFadeFished;
        CoverPanel.fadeIn = true;
        CoverPanel.needAuteHide = false;
        GUIManager.ShowView("CoverPanel");
       
    }
}
