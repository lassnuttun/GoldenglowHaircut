using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Enemy Turn");
        FightManager.Instance.StartCoroutine(FightManager.Instance.RemoveAllCard());
        FightManager.Instance.StartCoroutine(FightManager.Instance.EnemyAction());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
