using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsPrefab
{
    public GameObject gameObject;
    public UISprite icon;
    public UISprite rankSprite;
    public UILabel numLabel;
    public CardData m_CardData;
    public UILabel tips;



    public void PrefabInit(GameObject item, CardData data)
    {
        gameObject = item;
        rankSprite = item.GetComponent<UISprite>();
        icon = item.transform.FindRecursively("icon").GetComponent<UISprite>();
        numLabel = item.transform.FindRecursively("Label").GetComponent<UILabel>();
        tips = item.transform.FindRecursively("tip").GetComponent<UILabel>();

        int id = CommonHelper.Str2Int(item.name);
        m_CardData = data;
    }

    public void InitSetting()
    {
        if (m_CardData == null)
        {
            Debug.LogError("data is null");
            return;
        }
        rankSprite.spriteName = "ItemRank_" + m_CardData.heroData.starLv.ToString();
        icon.spriteName = m_CardData.heroData.spriteName;
        icon.MakePixelPerfect();
        numLabel.text = "x" + m_CardData.num.ToString();
    }

    public void SetCardState(CardData groundData)
    {
        if (groundData == null || m_CardData == null)
        {
            Debug.LogError("groundData or cardData is null");
            return;
        }
        if (m_CardData.heroData.starLv < 3)
        {
            if (m_CardData.num > 1 && groundData.num == 2)
            {
                tips.text = "套牌限制：2";
                tips.gameObject.SetActive(true);
                numLabel.gameObject.SetActive(false);
            }

            else if (m_CardData.num == 1 && groundData.num == 1)
            {
                tips.text = "已用完";
                tips.gameObject.SetActive(true);
                numLabel.gameObject.SetActive(false);
            }
            else
                ReInitState();
        }

        else if (m_CardData.heroData.starLv == 3)
        {
            if (m_CardData.num >= 1 && groundData.num == 1)
            {
                tips.text = "套牌限制：1";
                tips.gameObject.SetActive(true);
                numLabel.gameObject.SetActive(false);
            }

            else if (m_CardData.num == 1 && groundData.num == 1)
            {
                tips.text = "已用完";
                tips.gameObject.SetActive(true);
                numLabel.gameObject.SetActive(false);
            }
            else
                ReInitState();
        }
        
    }

    public void ReInitState()
    {
        numLabel.gameObject.SetActive(true);
        tips.gameObject.SetActive(false);
    }

}
