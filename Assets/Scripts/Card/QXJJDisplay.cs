using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QXJJDisplay : CardDisplay
{
    public QXJJCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is QXJJCard)
        {
            Card = obj as QXJJCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, Card.CardHP);
        base.UpdateDisplayInfo();
    }
}
