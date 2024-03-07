using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmEnvDisplay : EnvironmentDisplay
{
    public CalmEnv Environment;

    public override EnvironmentBase Get()
    {
        return Environment;
    }

    public override void Set(EnvironmentBase obj)
    {
        if (obj is CalmEnv)
        {
            Environment = obj as CalmEnv;
        }
    }

    public override void UpdateDisplayInfo()
    {
        base.UpdateDisplayInfo();
        DescripText.text = string.Format(Environment.EnvDescription, Environment.SPDecPerTurn.ToString(), Environment.Duration.ToString());
    }
}
