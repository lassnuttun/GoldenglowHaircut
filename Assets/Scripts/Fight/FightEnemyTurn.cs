using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightEnemyTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("Fight Enemy Turn");
        int count = FightManager.Instance.CardPiles[1].Count;
        for (int i = count - 1; i >= 0; i--)
        {
            FightManager.Instance.RemoveCard(i);
        }
        FightManager.Instance.StartCoroutine(FightManager.Instance.EnemyAction());
    }

    public override void OnUpdate() 
    {
        base.OnUpdate();
    }
}
