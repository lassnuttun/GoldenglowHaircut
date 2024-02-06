using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Enemy Turn");
        var fightUI = UIManager.Instance.GetUI<FightUI>("FightUI");
        fightUI.MoveAllFromHandToDiscard();
        fightUI.StartCoroutine(fightUI.EnemyActionDisplay());
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
    }
}
