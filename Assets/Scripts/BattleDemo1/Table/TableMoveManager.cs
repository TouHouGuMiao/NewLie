using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 桌面移动管理
/// </summary>
public class TableMoveManager  
{
    private static TableMoveManager _instance = null;
    public static TableMoveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TableMoveManager();
                
            }
            return _instance;
        }
    }

    private Dictionary<Vector2, TableInfoData> TableDataDic = new Dictionary<Vector2, TableInfoData>();


    /// <summary>
    /// 初始化棋盘
    /// </summary>
    /// <param name="line"></param>
    /// <param name="row"></param>
    ///                     
    public void InitTable(int line,int row)
    {
        for (int i = 0; i < line; i++)
        {
            for (int j = 0; j < row; j++)
            {
                TableInfoData data = new TableInfoData();
                data.index = new Vector2(i, j);
                data.position = new Vector3(-62.541f + j* 3.79f, -6.1f, 3.35f - i*4.44f);
                TableDataDic.Add(data.index,data);
            }
        }
    }

    public void AddGameObjectToTable(Vector2 index,TableCardData tableCardData)
    {
        TableInfoData data = null;
        if(!TableDataDic.TryGetValue(index,out data))
        {
            Debug.LogError("tableDic not has this!"+"it is"+" "+index);
        }
        data.data = tableCardData;
        GameObject go = GameObject.Instantiate(data.data.prefab);
        data.card = go;
        go.name = data.data.id.ToString();
        go.transform.position = data.position;
    }
}
