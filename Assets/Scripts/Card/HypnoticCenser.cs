using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HypnoticCenser : EnvCreateCard<CalmEnv>
{
    public HypnoticCenser(string cardID, string cardName, string cardDescription, int cardCost, int cardHP, int cardSP, int duration,
    string envID, string envName, string envDescription)
    : base(cardID, cardName, cardDescription, cardCost, cardHP, cardSP, duration, envID, envName, envDescription) 
    {
        Environment = new CalmEnv(envID, envName, envDescription, InitDuration);
    }

    public override void BindDisplayComponent(GameObject cardModel)
    {
        Display = cardModel.GetComponent<HynoticCensorDisplay>();
        Display.SetCard(this);
        Display.InHand = FightManager.Instance.CardPiles[1].Contains(this);
    }
}
