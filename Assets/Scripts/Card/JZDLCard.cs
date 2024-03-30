using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZDLCard : CardBase
{
    public JZDLDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is JZDLDisplay)
        {
            Display = obj as JZDLDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<JZDLDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public JZDLCard(CardConfigInfo info) : base(info) { }

    public override void UseStep2(EnemyBase target = null)
    {
        target.ChangeState(this, out int deltaHP, out _);
        int x = 0;

        foreach (CardBase card1 in FightManager.Instance.CardPiles[1])
        {
            if (card1 != this && card1.CardHP > 0)
            {
                x++;
            }
        }
        if (x > 0)
        {
            int y = Random.Range(0, x);
            foreach (CardBase card2 in FightManager.Instance.CardPiles[1])
            {
                if (card2 != this && card2.CardHP > 0)
                {
                    if (y == 0)
                    {
                        card2.CardHP += deltaHP;
                        card2.Get()?.UpdateDisplayInfo();
                    }
                    y--;
                }
            }
        }
    }
}