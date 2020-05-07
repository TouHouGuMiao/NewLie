using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 战斗属性管理器，管理弹幕的属性变化
/// </summary>
public class BattlePropManager : MonoBehaviour {
	public static BattlePropManager instance;
	public float scale=1;
	public float massValue=1;
	public float speedValue=1;
	public float powerValue=1;


	public void ChangeBattleCard_Count(CardBase data, int changeValue)
	{
		data.countMax += changeValue;
	}

	public void ChangeBattleCard_CountPercentage(CardBase data, float changePercentage)
	{
		data.countMax =(int)((data.countMax) * (1 + changePercentage));
	}

	public void ChangeBattleCard_Scale(CardBase data,float changeValue)
	{
		data.scale += changeValue;
	}

	public void ChangeBattleCard_Mass(CardBase data,float changeValue)
	{
		data.mass = data.mass + changeValue;
	}

	public void ChangeBattleCard_MassPercentage(CardBase data, float changePercentage)
	{
		data.mass = (data.mass) * (1 + changePercentage);
	}

	public void ChangeBattleCard_Speed(CardBase data, float changeValue)
	{
		data.speed = data.speed + changeValue;
	}

	public void ChangeBattleCard_SpeedPercentage(CardBase data, float changePercentage)
	{
		data.speed = (data.speed) * (1 + changePercentage);
	}


	public void ChangeBattleCard_Power(CardBase data, float changeValue)
	{
		data.power = data.power + changeValue;
	}

	public void ChangeBattleCard_PowerPercentage(CardBase data, float changePercentage)
	{
		data.power = (data.power) * (1 + changePercentage);
	}




	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
