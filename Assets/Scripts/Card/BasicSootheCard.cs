using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSootheCard : CardBase
{
    BasicSootheDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is BasicSootheDisplay)
        {
            Display = obj as BasicSootheDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<BasicSootheDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public BasicSootheCard(CardConfigInfo info) : base(info) { }
}
