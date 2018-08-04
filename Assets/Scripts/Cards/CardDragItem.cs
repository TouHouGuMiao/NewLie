using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDragItem : UIDragDropItem
{


    protected override void OnDragStart()
    {
        int id = CommonHelper.Str2Int(this.gameObject.name);
        CardData data = XMLDataManager.Instance.GetHasCardDataById(id);
        CardData groundData = GroundCardsManager.Instance.GetGroundDataById(id);
        if (data == null || groundData == null)
        {
            Debug.LogError("data or groundData is null");
            return;
        }
        if (data.num == 0)
        {
            return;
        }

        if (groundData.heroData.starLv < 3)
        {
            if (groundData.num == 1 && data.num <= 1)
            {
                return;
            }

            if (groundData.num == 2)
            {
                return;
            }
        }
        if (groundData.heroData.starLv == 3)
        {
            if (groundData.num == 1)
            {
                return;
            }
        }

        base.OnDragStart();
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.name == "GroundGrid")
            {
                int id = CommonHelper.Str2Int(mTrans.name);
                CardData data = new CardData();
                data.heroData = HeroDataManager.Instance.GetHeroData(id);
                data.isHas = true;
                data.num = 1;
                GroundCardsManager.Instance.AddCardData(data);

                CardData groundData = GroundCardsManager.Instance.GetGroundDataById(id);
                CardsItem item= CardsContiny.Instance.GetCardsItemById(id);
                item.cardPrefab.SetCardState(groundData);
            }
        }
        GroundCardsManager.Instance.m_Collider.enabled = false;
    }

  

    protected override void OnClone(GameObject original)
    {
        base.OnClone(original);
    }

    protected override void OnDragDropMove(Vector2 delta, GameObject surface = null)
    {
        base.OnDragDropMove(delta, surface);
        GroundCardsManager.Instance.m_Collider.enabled = true;
    }

    public override void StartDragging()
    {
        base.StartDragging();
        
    }
}
