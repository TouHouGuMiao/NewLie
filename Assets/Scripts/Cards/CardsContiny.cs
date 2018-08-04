using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsContiny
{
    private static CardsContiny _Instance=null;
    public static CardsContiny Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CardsContiny();
            }
            return _Instance;
        }
    }

    private Dictionary<int, CardsItem> cardItemDic=new Dictionary<int, CardsItem> ();
    public BoxCollider cardCollider;
    public void OnHide()
    {
        cardItemDic.Clear();
    }

    public void UpDataCardItem(GameObject go)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            GameObject item = go.transform.GetChild(i).gameObject;
            CardsItem m_cardItem = item.GetComponent<CardsItem>();
            int id = CommonHelper.Str2Int(item.name);
            CardsItem found = null;
            if(!cardItemDic.TryGetValue(id,out found))
            {
                cardItemDic.Add(id, m_cardItem);
            }

            CardData groundData = GroundCardsManager.Instance.GetGroundDataById(id);
            CardsItem m_item = GetCardsItemById(id);
            m_item.cardPrefab.SetCardState(groundData);
        }
    }


    
    public CardsItem GetCardsItemById(int id)
    {
        if (cardItemDic == null)
        {
            Debug.LogError("cardItemDic is null");
            return null;
        }
        CardsItem item = null;
        if(cardItemDic.TryGetValue(id,out item))
        {
            return item;
        }

        else
        {
            Debug.LogError("not item in dic");
        }
        return null;

    }

    public void InitItemBG(GameObject panel)
    {
   
    }


}
