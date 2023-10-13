using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.InitPlayerData();
        FightCardManager.Instance.Init();
        UIManager.Instance.ShowUI<FightUI>("FightUI");
        SubGameManager.Instance.Init("DemoGame");
        FightManager.Instance.MoveOn(FightUnitType.PlayerTurn);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
