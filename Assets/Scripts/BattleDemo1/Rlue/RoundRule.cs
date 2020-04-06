using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRule:MonoBehaviour {
    public enum RoundState
    {
        UseCardState,
        BattleState,
    }

    private int cost = 1;
    public int roundCount = 1;
    public RoundState roundState = RoundState.UseCardState;
    private int pValue=0;

    public static RoundRule Instance;

    private bool isComputerTime = false;

    private float roundTime=0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (roundTime<=0&&isComputerTime)
        {
            isComputerTime=false;
            PlayerBattlePanel.ShowHandCardPanel();
        }

        if (isComputerTime)
        {
            roundTime -= Time.deltaTime * 1*PlayerBattleRule.Instance.timeScale;
            PlayerBattlePanel.timeLabel.text = ((int)(roundTime)).ToString();
        }
        else
        {
            roundTime = -1;
        }
    }

    public void ChangePValue(int changeValue)
    {
        pValue += changeValue;
        PlayerBattlePanel.UpdatePValueLabel(pValue);
    }

    public void RounBattleStart()
    {
        HandCardPanel.DrawCard(3);
        cost = 1;
        roundCount = 1;
    }

    public void SetNextRoundTime(float time)
    {
        roundCount++;
        roundTime = time;
        isComputerTime = true;

    }

  

    
    public int GetPValue()
    {
        int temp = 0;
        temp = pValue;
        return pValue;
    }
    


}
