using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCardData  {
    public TableCardData(int id)
    {
        prefab = ResourcesManager.Instance.LoadTableCard(id.ToString());
        this.id = id;
    }

    public enum TableCardType
    {
        Store,
        NomralBattle,
        Story,
        Event,
        Realx,
    }
    public int id;
    public GameObject prefab;
    public TableCardType cardType;

}
