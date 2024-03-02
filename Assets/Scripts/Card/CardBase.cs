using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase
{
    public CardDisplay Display { get; private set; }
    public string CardID { get; private set; }
    public string CardName { get; private set; }
    public string CardDescription { get; private set; }
    public int CardCost { get; private set; }
    public int CardHP { get; private set; }
    public int CardSP { get; private set; }

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

    public void BindDisplayComponent(GameObject cardModel)
    {
        Display = cardModel.GetComponent<CardDisplay>();
        // Display.CardNameText.text = CardName;
        // Display.CardCostText.text = CardCost.ToString();
        // Display.CardDescriptionText.text = CardDescription;
        Display.Card = this;
        Display.InHand = FightManager.Instance.CardPiles[1].Contains(this);
    }

    public virtual bool TryUse()
    {
        return true;
    }
}
