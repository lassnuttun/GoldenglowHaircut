using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCutCard : CardBase
{
    public SuperCutDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is SuperCutDisplay)
        {
            Display = obj as SuperCutDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<SuperCutDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public SuperCutCard(CardConfigInfo info) : base(info) { }

    public override void UseStep2(EnemyBase target = null)
    {
        for (int i = 0; i < 3; i++)
        {
            target.ChangeState(this, out _, out _);
        }
    }
}
