using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QXJJCard : CardBase
{
    public QXJJDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is QXJJDisplay)
        {
            Display = obj as QXJJDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<QXJJDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public QXJJCard(CardConfigInfo info) : base(info) { }

    public override void UseStep2(EnemyBase target = null)
    {
        target.ChangeState(this, out _, out _); 
        foreach (CardBase card1 in FightManager.Instance.CardPiles[1])
        {
            if (card1.CardHP > 0)
            {
                card1.CardHP += 3;
                card1.Get()?.UpdateDisplayInfo();
            }
        }
    }
}
