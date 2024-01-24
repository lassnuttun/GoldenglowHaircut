using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.InitFight("DemoGame");
        UIManager.Instance.ShowUI<FightUI>("FightUI");
        FightManager.Instance.MoveOn(FightUnitType.PlayerTurn);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
