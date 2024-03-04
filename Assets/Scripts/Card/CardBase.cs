using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase : IProperty<CardDisplay>
{
    public string CardID;
    public string CardName;
    public string CardDescription;
    public int CardCost;
    public int CardHP;
    public int CardSP;

    public virtual CardDisplay Get()
    {
        return null;
    }

    public virtual void Set(CardDisplay obj)
    {
    }

    public CardBase()
    {
        CardID = "NULL";
        CardName = "NULL";
        CardDescription = "NULL";
        CardCost = 0;
        CardHP = 0;
        CardSP = 0;
    }

    public CardBase(string cardID, string cardName, string cardDescription, int cardCost, int cardHP, int cardSP)
    {
        CardID = cardID;
        CardName = cardName;
        CardDescription = cardDescription;
        CardCost = cardCost;
        CardHP = cardHP;
        CardSP = cardSP;
    }

    public virtual void BindDisplayComponent(GameObject cardModel)
    {
        CardDisplay display = Get();
        display = cardModel.GetComponent<CardDisplay>();
        // Display.CardNameText.text = CardName;
        // Display.CardCostText.text = CardCost.ToString();
        // Display.CardDescriptionText.text = CardDescription;
        display.Set(this);
        display.InHand = FightManager.Instance.CardPiles[1].Contains(this);
    }

    public virtual bool TryUse()
    {
        return true;
    }
}
