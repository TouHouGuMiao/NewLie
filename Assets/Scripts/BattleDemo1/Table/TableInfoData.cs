using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 将桌面卡牌的摆放位置视为一个战旗方格单位
/// </summary>
public class TableInfoData  {
    public Vector3 position;
    /// <summary>
    /// 该对象在棋盘的序列号
    /// </summary>
    public Vector2 index;
    /// <summary>
    /// 是否允许点击前往该方格
    /// </summary>
    public bool canMove = false;

    /// <summary>
    /// 数据对应的卡牌
    /// </summary>
    public GameObject card;

    public TableCardData data;
}
