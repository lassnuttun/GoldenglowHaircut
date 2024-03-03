using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypnoticCenser : EnvCreateCard
{
    public override CalmEnv Environment { get; set; }
    public HypnoticCenser(string cardID, string cardName, string cardDescription, int cardCost, int cardHP, int cardSP, int duration,
    string envID, string envName, string envDescription)
    : base(cardID, cardName, cardDescription, cardCost, cardHP, cardSP, duration, envID, envName, envDescription) 
    {
        Environment = new CalmEnv(envID, envName, envDescription, InitDuration);
    }
}
