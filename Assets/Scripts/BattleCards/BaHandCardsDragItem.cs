using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaHandCardsDragItem : UIDragDropItem
{
    protected override void OnDragDropMove(Vector2 delta, GameObject surface = null)
    {
        base.OnDragDropMove(delta, surface);
      
    }

    protected override void OnDragDropEnd()
    {
        base.OnDragDropEnd();
        if (Input.GetMouseButtonUp(0))
        {
            mTrans.gameObject.SetActive(false);
            InstanteHeroModel();
            int id = CommonHelper.Str2Int(mTrans.name);
            BaGroundCardManager.Instance.DeleteHandCardDataById(id);
            HeroData data = new global::HeroData();
            data = HeroDataManager.Instance.GetHeroData(id);
            CardData cardData = new global::CardData();
            cardData.isHas = true;
            cardData.num = 1;
            cardData.heroData = data;
            BaGroundCardManager.Instance.InPlaneHeroDataList.Add(cardData);
        }
    }


    private void InstanteHeroModel()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject go = ResourcesManager.Instance.LoadHeroModel("koishi");
        GameObject prefab = Instantiate(go).gameObject;
        prefab.transform.position = mouseWorldPosition;
    }
}
