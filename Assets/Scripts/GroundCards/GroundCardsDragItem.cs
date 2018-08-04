using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCardsDragItem : UIDragDropItem
{


    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        UIPanel clonePanel = mTrans.gameObject.GetComponent<UIPanel>();
        UILabel numLabel = mTrans.Find("num").GetComponent<UILabel>();
        numLabel.enabled = false;
        clonePanel.depth = clonePanel.depth + 2;
        CardsContiny.Instance.cardCollider.enabled = true;
    }
    protected override void OnDragDropMove(Vector2 delta, GameObject surface = null)
    {
        base.OnDragDropMove(delta, surface);
    }


    protected override void OnDragDropRelease(GameObject surface)
    {

        if (surface.name== "Grid")
        {

            int id = CommonHelper.Str2Int(this.gameObject.name);


            CardData data = new CardData();
            data.heroData = HeroDataManager.Instance.GetHeroData(id);
            data.num = 1;
            data.isHas = true;
            GroundCardsManager.Instance.DelteCardData(data);

            CardData groundData = GroundCardsManager.Instance.GetGroundDataById(id);
            CardsItem item = CardsContiny.Instance.GetCardsItemById(id);
            item.cardPrefab.SetCardState(groundData);
        } 
        
        base.OnDragDropRelease(surface);
        CardsContiny.Instance.cardCollider.enabled = false;
    }

}
