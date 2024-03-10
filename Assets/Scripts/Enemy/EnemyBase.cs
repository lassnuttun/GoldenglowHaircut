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

    public EnemyBase(EnemyConfigInfo enemyConfig)
    {
        EnemyID = enemyConfig.id;
        EnemyName = enemyConfig.name;
        EnemyDescription = enemyConfig.desc;
        EnemyHP = new ConditionBar(enemyConfig.hp, 0);
        EnemySP = new ConditionBar(enemyConfig.sp, 0);
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

    // 需要加入环境卡对应的接口吗？
    public virtual void ChangeState(CardBase card)
    {
        HitBy(card, out int deltaHP, out int deltaSP);
        EnemyHP.Inc(deltaHP);
        EnemySP.Inc(deltaSP);
        Get().UpdateDisplayInfo();
    }

    // 后续需要把这个移到具体子类内部 用状态机维护下一次该采取什么行动
    public virtual void Move1() { }
    public virtual void Move2() { }
    public virtual void Move3() { }
    public virtual void Move4() { }
    public virtual void Move5() { }
}
