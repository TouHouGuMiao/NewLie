using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickButton : UIButton
{
    private int count = 0;



    protected override void OnClick()
    {
        count++;
        if (count == 2)
        {
            DeleteData();
            base.OnClick();
            count = 0;
        }
    }




    private void DeleteData()
    {
        int id = CommonHelper.Str2Int(this.gameObject.name);
        CardData data = new CardData();
        data.heroData = HeroDataManager.Instance.GetHeroData(id);
        data.num = 1;
        data.isHas = false;
        GroundCardsManager.Instance.DelteCardData(data);

        CardData groundData = GroundCardsManager.Instance.GetGroundDataById(id);
        CardsItem item = CardsContiny.Instance.GetCardsItemById(id);
        item.cardPrefab.SetCardState(groundData);
    }
}
