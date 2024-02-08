using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Player Turn");
        FightManager.Instance.CurPW = FightManager.Instance.MaxPW;
        FightManager.Instance.DrawCards(FightManager.Instance.DrawCount);
        // UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHandPilePos();
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
    }
}
