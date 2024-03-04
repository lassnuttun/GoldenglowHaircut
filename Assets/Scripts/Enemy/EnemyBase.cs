using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : IProperty<EnemyDisplay>
{
    public string EnemyID;
    public string EnemyName;
    public string EnemyDescription;
    public ConditionBar EnemyHP;
    public ConditionBar EnemySP;

    public virtual EnemyDisplay Get()
    {
        return null;
    }

    public virtual void Set(EnemyDisplay obj)
    {
    }

    public EnemyBase(string enemyID, string enemyName, string enemyDescription, int enemyMaxHP, int enemyMaxSP)
    {
        EnemyID = enemyID;
        EnemyName = enemyName;
        EnemyDescription = enemyDescription;
        EnemyHP = new ConditionBar(enemyMaxHP, 0);
        EnemySP = new ConditionBar(enemyMaxSP, 0);
    }

    public virtual void BindDisplayComponent(GameObject enemyModel)
    {
        Get().Bind(enemyModel, this);
    }

    public void HitBy(CardBase card, out int deltaHP, out int deltaSP)
    {
        deltaHP = card.CardHP;
        deltaSP = card.CardSP;
    }

    public virtual void ChangeState(CardBase card)
    {
        HitBy(card, out int deltaHP, out int deltaSP);
        EnemyHP.Inc(deltaHP);
        EnemySP.Inc(deltaSP);
    }

    public abstract void Move1();
    public abstract void Move2();
    public abstract void Move3();
    public abstract void Move4();
    public abstract void Move5();
}
