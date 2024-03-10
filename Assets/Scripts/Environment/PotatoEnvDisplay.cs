using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoEnvDisplay : EnvironmentDisplay
{
    public PotatoEnv Environment;

    public override EnvironmentBase Get()
    {
        return Environment;
    }

    public override void Set(EnvironmentBase obj)
    {
        if (obj is PotatoEnv)
        {
            Environment = obj as PotatoEnv;
        }
    }
}
