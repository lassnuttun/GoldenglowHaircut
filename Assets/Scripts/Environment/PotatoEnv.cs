using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoEnv : EnvironmentBase
{
    public PotatoEnvDisplay Display;

    public override EnvironmentDisplay Get()
    {
        return Display;
    }

    public override void Set(EnvironmentDisplay obj)
    {
        if (obj is PotatoEnvDisplay)
        {
            Display = obj as PotatoEnvDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.AddComponent<PotatoEnvDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public PotatoEnv(string envID, string envName, string envDescription, int duration, CardBase origin)
        : base(envID, envName, envDescription, duration, origin) { }
}
