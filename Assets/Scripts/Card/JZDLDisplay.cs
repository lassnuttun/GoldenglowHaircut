using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JZDLDisplay : CardDisplay
{
    public JZDLCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is JZDLCard)
        {
            Card = obj as JZDLCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, Card.CardHP);
        base.UpdateDisplayInfo();
    }
}
