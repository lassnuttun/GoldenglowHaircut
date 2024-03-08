using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Player Turn");
        FightManager.Instance.Power.Inc(FightManager.Instance.Power.MaxValue);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
