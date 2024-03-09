using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCutDisplay : CardDisplay
{
    public BasicCutCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is BasicCutCard)
        {
            Card = obj as BasicCutCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, Card.CardHP);
        base.UpdateDisplayInfo();
    }
}
