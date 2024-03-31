using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSYDCard : CardBase
{
    public RSYDDisplay Display;

    public override CardDisplay Get()
    {
        return Display;
    }

    public override void Set(CardDisplay obj)
    {
        if (obj is RSYDDisplay)
        {
            Display = obj as RSYDDisplay;
        }
    }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<RSYDDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public RSYDCard(CardConfigInfo info) : base(info) { }

    public override void UseStep2(EnemyBase target = null)
    {
        IEnumerator drawCardsCoroutine = FightManager.Instance.DrawCards(2, false);
        FightManager.Instance.StartCoroutine(drawCardsCoroutine);

        while (drawCardsCoroutine.MoveNext()) { }

        int handCount = FightManager.Instance.CardPiles[1].Count;

        for (int i = handCount - 2; i < handCount; i++)
        {
            CardBase card = FightManager.Instance.CardPiles[1][i];
            if (card.CardHP > 0)
            {

                card.CardHP += handCount-1;
                card.Get()?.UpdateDisplayInfo();
            }
        }
    }
}
