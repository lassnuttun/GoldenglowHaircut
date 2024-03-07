using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public abstract class CardBase : IProperty<CardDisplay>
{
    public string CardID;
    public string CardName;
    public string CardDescription;
    public int CardCost;
    public int CardHP;
    public int CardSP;

    protected abstract void LoadCardModel();

    public virtual CardDisplay Get() { return null; }

    public virtual void Set(CardDisplay obj) { }

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

    public void Use(EnemyBase target = null)
    {
        UseStep1();
        UseStep2(target);
        UseStep3();
    }

    public void UseStep1()
    {
        FightManager.Instance.Power.Inc(-CardCost);
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
    }

    public virtual void UseStep2(EnemyBase target = null)
    {
        target.ChangeState(this);
        target.Get().UpdateDisplayInfo();
    }

    public virtual void UseStep3()
    {
        DiscardFromHandPile();
    }

    public void DrawFromDeckPile()
    {
        List<List<CardBase>> cards = FightManager.Instance.CardPiles;
        cards[0].Remove(this);
        cards[1].Add(this);
        LoadCardModel();
        Get().MoveFromDeckToHand();
    }

    public void DiscardFromHandPile()
    {
        List<List<CardBase>> cards = FightManager.Instance.CardPiles;
        cards[1].Remove(this);
        cards[2].Add(this);
        Get().MoveFromHandToDiscard();
    }
}
