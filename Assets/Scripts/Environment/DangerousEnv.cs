using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousEnv : EnvironmentBase
{
    public DangerousEnvDisplay Display;

    public override EnvironmentDisplay Get()
    {
        return Display;
    }

    public override void Set(EnvironmentDisplay obj)
    {
        if (obj is  DangerousEnvDisplay)
        {
            Display = obj as DangerousEnvDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.AddComponent<DangerousEnvDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public DangerousEnv(string envID, string envName, string envDescription, int duration, CardBase origin)
        : base(envID, envName, envDescription, duration, null) { }
}
