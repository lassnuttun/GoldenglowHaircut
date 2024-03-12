using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCutDisplay : CardDisplay
{
    public SuperCutCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is SuperCutCard)
        {
            Card = obj as SuperCutCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, Card.CardHP);
        base.UpdateDisplayInfo();
    }
}
