using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPrePlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Pre Player Turn");
        FightManager.Instance.StartCoroutine(FightManager.Instance.DrawCards(FightManager.Instance.DrawCount));
        FightManager.Instance.StartCoroutine(FightManager.Instance.CountDownEnv());
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
