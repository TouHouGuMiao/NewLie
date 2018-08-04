using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsItem :MonoBehaviour
{
    private CardData cardData;
    public CardsPrefab cardPrefab;

    private void Awake()
    {
        cardPrefab = new CardsPrefab();
    }

    public void UpdataItem()
    {
        int id = CommonHelper.Str2Int(this.gameObject.name);
        cardData = XMLDataManager.Instance.GetHasCardDataById(id);
        cardPrefab.PrefabInit(this.gameObject, cardData);
        cardPrefab.InitSetting();
    }

}
