using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvCreateCard<T> : CardBase where T : EnvironmentBase
{
    public T Environment;
    public int InitDuration;

    public EnvCreateCard(string cardID, string cardName, string cardDescription, int cardCost, int cardHP, int cardSP, int duration,
        string envID, string envName, string envDescription)
        : base(cardID, cardName, cardDescription, cardCost, cardHP, cardSP)
    {
        InitDuration = duration;
    }

    public override void UseStep2(EnemyBase target = null)
    {
        Environment.AddToEnvSlot();
    }

    public override void UseStep3()
    {

    }
}
