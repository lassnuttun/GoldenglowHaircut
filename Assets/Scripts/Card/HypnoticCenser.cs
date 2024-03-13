using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoticCenser : EnvCreateCard<CalmEnv>
{
    HypnoticCenserDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is HypnoticCenserDisplay)
        {
            Display = obj as HypnoticCenserDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<HypnoticCenserDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public HypnoticCenser(CardConfigInfo info) : base(info)
    {
        Environment = new CalmEnv("E001", "安神", "使所有顾客SP减少 {0} 点", InitDuration, this);
    }
}
