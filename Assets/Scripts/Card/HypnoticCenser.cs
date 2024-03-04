﻿using System.Collections;
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

    public HypnoticCenser(string cardID, string cardName, string cardDescription, int cardCost, int cardHP, int cardSP, int duration,
    string envID, string envName, string envDescription)
    : base(cardID, cardName, cardDescription, cardCost, cardHP, cardSP, duration, envID, envName, envDescription)
    {
        Environment = new CalmEnv(envID, envName, envDescription, InitDuration);
    }

    public override void BindDisplayComponent(GameObject cardModel)
    {
        Display = cardModel.GetComponent<HypnoticCenserDisplay>();
        Display.Set(this);
        Display.InHand = FightManager.Instance.CardPiles[1].Contains(this);
    }
}
