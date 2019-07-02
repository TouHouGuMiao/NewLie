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

    public void GameEventSet(string name)
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
        EventDic.Add("ShenSheMoveScene", null);
        EventDic.Add("CunZiInvestigate", null);
        EventDic.Add("CunMingShot", null);
    }

   /// <summary>
   /// 玩家续章开门之前的触发类事件状态设置
   /// </summary>
    public void BeforeOpenDoor_XuZhang()
    {
        AudioManager.Instance.PlayEffect_Source_NeedLoop("KnockDoor");
        EventDic["ShenShedoor"] = new EventDelegate(PlayerOpenDoor_CunMingLaiFang);
       
    }

    public void WhenSeeWithCunMingOver()
    {
        EventDic["ShenSheMoveScene"] = new EventDelegate(ShowEvent_GoToCunZi);
    }

    public void ShenSheMoveScene_SetNull()
    {
        EventDic["ShenSheMoveScene"] = null;
    }

    public void WhenGoToCunZi()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(-10.54f, -37.16f, 0);
        BattleCamera.Instance.SetBattleCameraRightStop(false);
        BattleCamera.Instance.SetBattleCameraLetStop(false);
        BattleCamera.Instance.SetCameraReturnPlayer(new Vector3 (3,2,0));
        EventDic["CunZiInvestigate"] = new EventDelegate(ShowEvent_InCunZiAutoInves);
        EventDic["CunMingShot"] = new EventDelegate(CunMingShot);
    }


    #region 剧情相关事件绑定
   /// <summary>
   /// 
   /// </summary>
    private void CunMingShot()
    {
        BattleCamera.Instance.MoveCamera_StopWhenRectCashPlayerOrCollider(BattleCamera.MoveEnum.right, 1.0F, ShowCunMingShotOne);
        EventDic["CunMingShot"] = null;
    }


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

    private void ShowCunMingShotOne()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 5);
    }

    private void ShowEvent_InCunZiAutoInves()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 3);
        CameraManager.Instance.Feature(  NPCAnimatorManager.BGEnmu.Village,"investigate");
        Dictionary<int, Dictionary<string, EventDelegate>> skillDic = new Dictionary<int, Dictionary<string, EventDelegate>>();
        Dictionary<string, EventDelegate> ideaDic = new Dictionary<string, EventDelegate>();
        ideaDic.Add("Sence", new EventDelegate(IdeaInCunziWhenFrist));
        skillDic.Add(6, ideaDic);
        SkillManager.Instance.UpdataAndShowSkillUsePanel(skillDic, false, true);
        EventDic["CunZiInvestigate"] =null;
    }

    private void ShowEvent_GoToCunZi()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 51);
    }


    private void ShowTalk1_4()
    {
        TalkManager.Instance.ShowTalkPanel(1, 4);
    }



    #endregion

    #region 杂项方法绑定


    void IdeaInCunZiWhenFrist_Result()
    {
        CharacterPropBase data = CharacterPropManager.Instance.GetPlayerProp();
        if (data.Idea <= DiceCheckPanel.diceValue)
        {
            StoryEventManager.Instance.ShowEventPanel_ChapterOne(4, 4);         
        }
       
    }

    #endregion


    #region 绑定技能方法

    void IdeaInCunziWhenFrist()
    {
        DiceManager.Instance.ShowDicePanel(10, 0.01f, IdeaInCunZiWhenFrist_Result);
        SkillManager.Instance.MoveSkillInSkillUsePanel(6);

    }


    #endregion
}
