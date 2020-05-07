using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCardDataManager 
{
    private static TableCardDataManager _intance = null;
    public static TableCardDataManager Instance
    {
        get
        {
            if (_intance == null)
            {
                _intance = new TableCardDataManager();
                _intance.InitDic();
            }
            return _intance;
        }
    }

    private Dictionary<int, TableCardData> TableCardDataDic = new Dictionary<int, TableCardData>();

    public TableCardData GetDateById(int id)
    {
        TableCardData data = null;
        if(!TableCardDataDic.TryGetValue(id,out data))
        {
            Debug.LogError("not has this data");
        }
        return data;
    }

    public List<TableCardData> GetRandemEvent(int count)
    {
        List<TableCardData> eventList = new List<TableCardData>();
        foreach (TableCardData item in TableCardDataDic.Values)
        {
            if(item.cardType== TableCardData.TableCardType.Event)
            {
                eventList.Add(item);
            }
        }
        eventList = OtherHelper.Instance.Shuffle<TableCardData>(eventList);
        List<TableCardData> list = new List<TableCardData>();
        for (int i = 0; i < count; i++)
        {
            list.Add(eventList[i]);
        }
        return list;
    }




    private void InitDic()
    {
        TableCardData data0 = new TableCardData(0);
        data0.cardType = TableCardData.TableCardType.NomralBattle;
        TableCardDataDic.Add(data0.id,data0);

        TableCardData data1 = new TableCardData(1);
        data1.cardType = TableCardData.TableCardType.Event;
        TableCardDataDic.Add(data1.id, data1);

        TableCardData data2 = new TableCardData(2);
        data2.cardType = TableCardData.TableCardType.Event;
        TableCardDataDic.Add(data2.id, data2);


        TableCardData data3 = new TableCardData(3);
        data3.cardType = TableCardData.TableCardType.Event;
        TableCardDataDic.Add(data3.id, data3);

        TableCardData data4 = new TableCardData(4);
        data4.cardType = TableCardData.TableCardType.Event;
        TableCardDataDic.Add(data4.id, data4);

        TableCardData data5 = new TableCardData(5);
        data5.cardType = TableCardData.TableCardType.Realx;
        TableCardDataDic.Add(data5.id, data5);


        TableCardData data6 = new TableCardData(6);
        data6.cardType = TableCardData.TableCardType.Store;
        TableCardDataDic.Add(data6.id, data6);


        TableCardData data7 = new TableCardData(7);
        data7.cardType = TableCardData.TableCardType.Story;
        TableCardDataDic.Add(data7.id, data7);
    }
}

