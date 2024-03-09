using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSootheDisplay : CardDisplay
{
    public BasicSootheCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is BasicSootheCard)
        {
            Card = obj as BasicSootheCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, -Card.CardSP);
        base.UpdateDisplayInfo();
    }
}
