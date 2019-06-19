using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStateManager
{
    private static EventStateManager _Instance = null;
    private Dictionary<string, EventDelegate> EventDic = new Dictionary<string, EventDelegate>();
    public static EventStateManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new EventStateManager();
            }
            return _Instance;
        }
    }

    public void GameEventTrigerManager(string name)
    {
        EventDelegate hander = null;
        if(!EventDic.TryGetValue(name,out hander))
        {
            Debug.LogError("eventCollider name has error + _" + name);
            return;
        }
        List<EventDelegate> list = new List<EventDelegate>();
        list.Add(hander);
        EventDelegate.Execute(list);  
    }

    void InitEventDic()
    {
        EventDic.Add("ShenShedoor", null);
    }

   /// <summary>
   /// 玩家续章开门之前的触发类事件状态设置
   /// </summary>
    public void BeforeOpenDoor_XuZhang()
    {
        AudioManager.Instance.PlayEffect_Source_NeedLoop("KnockDoor");
        EventDic["ShenShedoor"] = new EventDelegate(PlayerOpenDoor_CunMingLaiFang);
    }

    #region 剧情相关事件绑定
    /// <summary>
    /// 村民来访，玩家开门
    /// </summary>
    private void PlayerOpenDoor_CunMingLaiFang()
    {
        BattleCamera.Instance.MoveCamera_StopWhenRectCashPlayerOrCollider(BattleCamera.MoveEnum.right, 4, ShowTalk1_4);
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 27);
        EventDic["ShenShedoor"] = null;
        AudioManager.Instance.CloseEffect_Source();
    }

    private void ShowTalk1_4()
    {
        TalkManager.Instance.ShowTalkPanel(1, 4);
    }

    #endregion
}
