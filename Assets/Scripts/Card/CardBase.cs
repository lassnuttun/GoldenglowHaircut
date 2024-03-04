using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase
{
    public CardDisplay Display;
    public string CardID;
    public string CardName;
    public string CardDescription;
    public int CardCost;
    public int CardHP;
    public int CardSP;

    public CardBase()
    {
        CardID = "NULL";
        CardName = "NULL";
        CardDescription = "NULL";
        CardCost = 0;
        CardHP = 0;
        CardSP = 0;
    }

    public CardBase(string cardID, string cardName, string cardDescription, int cardCost,  int cardHP, int cardSP)
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
        Display = cardModel.GetComponent<CardDisplay>();
        // Display.CardNameText.text = CardName;
        // Display.CardCostText.text = CardCost.ToString();
        // Display.CardDescriptionText.text = CardDescription;
        Display.SetCard(this);
        Display.InHand = FightManager.Instance.CardPiles[1].Contains(this);
    }

    public virtual bool TryUse()
    {
        return true;
    }

    public virtual EnvironmentBase GetEnv()
    {
        return null;
    }
}
