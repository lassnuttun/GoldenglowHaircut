using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCutCard : CardBase
{
    BasicCutDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is BasicCutDisplay)
        {
            Display = obj as BasicCutDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<BasicCutDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public BasicCutCard(CardConfigInfo info) : base(info) { }
}
