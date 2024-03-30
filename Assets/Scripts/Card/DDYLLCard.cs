using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDYLLCard : CardBase
{
    public DDYLLDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is DDYLLDisplay)
        {
            Display = obj as DDYLLDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<DDYLLDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public DDYLLCard(CardConfigInfo info) : base(info) { }

    public override void UseStep2(EnemyBase target = null)
    {
        for (int i = 0; i < 5; i++)
        {
            target.ChangeState(this, out _, out _);
        }
    }
}
