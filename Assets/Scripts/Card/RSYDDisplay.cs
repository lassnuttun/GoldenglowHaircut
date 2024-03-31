using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSYDDisplay : CardDisplay
{
    public RSYDCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is RSYDCard)
        {
            Card = obj as RSYDCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, Card.CardFT);
        base.UpdateDisplayInfo();
    }
}
