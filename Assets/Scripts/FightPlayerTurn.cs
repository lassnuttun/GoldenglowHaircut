using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayerTurn : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.curPW = FightManager.Instance.maxPW;
        FightCardManager.Instance.DrawCards(FightManager.Instance.drawCount);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHandPilePos();
    }

    public override void OnUpdate() { }
}
