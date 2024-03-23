using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvCreateCard<T> : CardBase where T : EnvironmentBase
{
    public T Environment;
    public int InitDuration;

    public EnvCreateCard(CardConfigInfo info) : base(info)
    {
        InitDuration = info.dura;
    }

    public override void UseStep2(EnemyBase target = null)
    {
        Environment.Duration = InitDuration;
        Environment.AddToEnvSlot();
    }

    public override void UseStep3()
    {

    }
}
