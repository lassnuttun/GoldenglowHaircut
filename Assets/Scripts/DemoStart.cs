using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoStart : MonoBehaviour
{
    void Start()
    {
        DeckManager.Instance.InitDeck();

        FightCardManager.Instance.Init();

        FightCardManager.Instance.DrawCards(4);

        FightCardManager.Instance.UpdateHandPile();

        SubGameManager.Instance.Init("DemoGame");

        UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
}
