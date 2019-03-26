using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager
{
    private static DiceManager _instance = null;
    public static  DiceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DiceManager();
            }
            return _instance;
        }
    }
   

    public void ShowDicePanel(int[]DiceNumerArray,float rate)
    {
        DicePanel.DiceNumerArray = DiceNumerArray;
        DicePanel.rate = rate;
        GUIManager.ShowView("DicePanel");
    }

    /// <summary>
    /// 第一个参数为骰子的面数，目前分为六面，十面
    /// </summary>
    /// <param name="diceType"></param>
    /// <param name="rate"></param>
    public void ShowDicePanel(int diceType, float rate)
    {
        int[] DiceNumerArray=new int[diceType];
        if (diceType == 6)
        {
            DiceNumerArray = GetIndexRandomNum(1, 6);
        }
        else if (diceType == 10)
        {
            DiceNumerArray = GetIndexRandomNum(0, 9);
        }
        DicePanel.DiceNumerArray = DiceNumerArray;
        DicePanel.rate = rate;
        GUIManager.ShowView("DicePanel");
    }


    public int GetDiceValue()
    {
        int value = DicePanel.diceValue;
        if (value > 9)
        {
            Debug.LogError("value has erorr" + value);
        }
        return value;
    }

    public int[] GetIndexRandomNum(int minValue, int maxValue)
    {
        System.Random random = new System.Random();
        int sum = Mathf.Abs(maxValue+1 - minValue);//计算数组范围
        int site = sum;//设置索引范围
        int[] index = new int[sum];
        int[] result = new int[sum];
        int temp = 0;
        for (int i = minValue; i <=maxValue; i++)
        {
            index[temp] = i;
            temp++;
        }
        for (int i = 0; i < sum; i++)
        {
            int id = random.Next(0, site - 1);
            result[i] = index[id];
            index[id] = index[site - 1];//因id随机到的数已经获取到了，用最后的一个数来替换它
            site--;//缩小索引范围
          
        }
        return result;
    }

    public void DepentClickSource(int diceNumber)
    {
        int id = StoryEventManager.Instance.GetNowEventDataId();
        int index = StoryEventManager.Instance.GetNowEventDataIndex();
        if (id == 1)
        {
            if (index == 4)
            {
                PropStatureDice_One(diceNumber);
            }

            if (index == 5)
            {
                PropStatureDice_Two(diceNumber);
            }

            if (index == 10)
            {
                PropPowerDice_One(diceNumber);
            }

            if (index == 13)
            {
                PropPowerDice_Two(diceNumber);
            }

            if (index == 14)
            {
                PropPowerDice_Thrid(diceNumber);
            }
        }
    }

    /// <summary>
    /// 写在这里的事件，将会在骰点结束后触发
    /// </summary>
    /// <param name="diceNumber"></param>
    #region 骰属性事件
    void PropStatureDice_One(int diceNumber)
    {
        SurePropertyPanel.dice1 = diceNumber;
        SurePropertyPanel.SureState = CreatSureState.Stature_State_Dice1;
    }

    void PropStatureDice_Two(int diceNumber)
    {
        SurePropertyPanel.dice2 = diceNumber;
        SurePropertyPanel.SureState = CreatSureState.Stature_State_Dice2;
    }

    void PropPowerDice_One(int diceNumber)
    {
        SurePropertyPanel.dice1 = diceNumber;
        SurePropertyPanel.SureState = CreatSureState.Power_State_Dice1;
    }

    void PropPowerDice_Two(int diceNumber)
    {
        SurePropertyPanel.dice2 = diceNumber;
        SurePropertyPanel.SureState = CreatSureState.Power_State_Dice2;
    }

    void PropPowerDice_Thrid(int diceNumber)
    {
        SurePropertyPanel.dice3 = diceNumber;
        SurePropertyPanel.SureState = CreatSureState.Power_State_Dice3;
    }
    #endregion
}
