using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDYLLDisplay : CardDisplay
{
    public DDYLLCard Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is DDYLLCard)
        {
            Card = obj as DDYLLCard;
        }
    }

    public override void UpdateDisplayInfo()
    {
        CardDescriptionText.text = string.Format(Card.CardDescription, Card.CardHP);
        base.UpdateDisplayInfo();
    }
}
