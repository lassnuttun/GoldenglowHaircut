using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Player Turn");
        FightManager.Instance.Power.Inc(FightManager.Instance.Power.MaxValue);
        FightManager.Instance.StartCoroutine(FightManager.Instance.DrawCards(FightManager.Instance.DrawCount));
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
    }
}
