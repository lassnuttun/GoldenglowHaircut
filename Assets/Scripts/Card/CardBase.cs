using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡牌基类
public class CardBase
{
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
}
