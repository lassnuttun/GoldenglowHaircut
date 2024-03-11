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

    protected void LoadModel()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");
        Object resource = AssetBundleManager.LoadResource<Object>(CardID, "card");
        GameObject cardObj = GameObject.Instantiate(resource, ui.Canvas) as GameObject;
        BindDisplayComponent(cardObj);
    }

    public virtual CardDisplay Get() { return null; }

    public virtual void Set(CardDisplay obj) { }

    public virtual void BindDisplayComponent(GameObject gameObj)
    {
        CardDisplay display = Get();
        display.Set(this);
        display.UpdateDisplayInfo();
    }

    public CardBase(CardConfigInfo info)
    {
        CardID = info.id;
        CardName = info.name;
        CardDescription = info.desc;
        CardCost = info.cost;
        CardHP = info.hp;
        CardSP = info.sp;
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
        LoadModel();
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
