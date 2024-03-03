using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCreateCard : CardBase
{
    public virtual EnvironmentBase Environment { get; set; }
    public int InitDuration;

    public EnvCreateCard(string cardID, string cardName, string cardDescription, int cardCost, int cardHP, int cardSP, int duration,
        string envID, string envName, string envDescription) 
        : base(cardID, cardName, cardDescription, cardCost, cardHP, cardSP) 
    {
        InitDuration = duration;
    }
}
