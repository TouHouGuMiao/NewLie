using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理BattleUIPanel界面与战斗 共同需要用到UI，例如血条
/// </summary>
public class BattleCommoUIManager
{
    private static BattleCommoUIManager _Instance=null;
    public static BattleCommoUIManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new BattleCommoUIManager();
            }
            return _Instance;
        }
    }

    public UISlider HPSlider;
    public UISlider BossSlider;
    public UILabel BossNameLabel;
    public TweenAlpha speakLabelAlpha;

    private UIWidget playerHPWiget;
    private UIWidget playerHPBGWiget;
    private CharacterPropBase playerBaseData;

    private UISprite bulletIcon;
    private UILabel bulletNum;

    public void InitUI(GameObject panel)
    {
        HPSlider = panel.transform.FindRecursively("PlayerHPSlider").GetComponent<UISlider>();
        playerHPBGWiget = panel.transform.FindRecursively("HPBG").GetComponent<UIWidget>();
        playerHPWiget = panel.transform.FindRecursively("HP").GetComponent<UIWidget>();
        playerBaseData = CharacterPropManager.Instance.GetCharcaterDataByName("reimu");
        BossSlider = panel.transform.FindRecursively("BossHPSlider").GetComponent<UISlider>();
        BossNameLabel = panel.transform.FindRecursively("BossName").GetComponent<UILabel>();
        speakLabelAlpha = panel.transform.FindRecursively("SpeakLabel").GetComponent<TweenAlpha>();


        bulletIcon = panel.transform.FindRecursively("bulletIcon").GetComponent<UISprite>();
        bulletNum = panel.transform.FindRecursively("bulletNum").GetComponent<UILabel>();


    }
    /// <summary>
    /// 玩家HP因为属性而变动的时候调用
    /// </summary>
    public void UpdataSlider_Player()
    { 
        int width = (int)(playerBaseData.HP * 10);
        float moreCount = width - playerHPWiget.width;

        playerHPWiget.width = width;
        playerHPBGWiget.width = width + 30;
        playerHPWiget.transform.localPosition += new Vector3(0.5f * moreCount, 0, 0);
        playerHPBGWiget.transform.localPosition += new Vector3(0.5f * moreCount, 0, 0);
    }
    /// <summary>
    /// 因为受到伤害而造成的血条减少，或者血条回复调用该方法
    /// count代表改动数值，sign 只能写1或者-1代表减少或者回复,HP_now为现有的HP,HP_pro为属性中的HP
    /// 该方法用来计算血条的Slider应该减少或增加多少，并且返回计算后应该有的血量
    /// </summary>
    public float UpdataHP_Player(float HP_now,float HP_pro,float count,int sign)
    {
        float HPcount = HP_now + sign * count;
        if (HPcount >= HP_pro)
        {
            HPcount = HP_pro;
        }

        float index = (HPcount / HP_pro);
    
        HPSlider.value = index;

        return HPcount;
    }

    public float UpdataHP_Boss(string name,float HP_now, float count, int sign)
    {
        
        CharacterPropBase boseeBaseData = CharacterPropManager.Instance.GetCharcaterDataByName(name);
        float HPcount = HP_now + sign * count;

        if (HPcount >= boseeBaseData.HP)
        {
            HPcount = boseeBaseData.HP;
        }

        if (HPcount <= 0)
        {
            HPcount = 0;
        }

        float index = (HPcount / boseeBaseData.HP);
     
        BossSlider.value = index;
        return HPcount;
    }


    public void ChangeBulletType(int id,string num)
    {
        ItemData data = ItemDataManager.Instance.GetItemDataByID(id);
        bulletIcon.spriteName = data.icon;
        bulletIcon.MakePixelPerfect();
        bulletNum.text = "x" + num;
    }

}
