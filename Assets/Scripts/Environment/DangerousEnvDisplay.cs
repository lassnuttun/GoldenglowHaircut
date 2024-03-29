using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousEnvDisplay : EnvironmentDisplay
{
    public DangerousEnv Environment;

    public override EnvironmentBase Get()
    {
        return Environment;
    }

    public override void Set(EnvironmentBase obj)
    {
        if (obj is  DangerousEnv)
        {
            Environment = obj as DangerousEnv;
        }
    }

    public override void UpdateDisplayInfo()
    {
        base.UpdateDisplayInfo();
        DescripText.text = Environment.EnvDescription;
    }
}
