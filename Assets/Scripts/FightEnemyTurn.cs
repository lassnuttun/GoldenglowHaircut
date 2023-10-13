using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        UIManager.Instance.GetUI<FightUI>("FightUI").MoveAllFromHandToDiscard();
    }

    public override void OnUpdate() { }
}
