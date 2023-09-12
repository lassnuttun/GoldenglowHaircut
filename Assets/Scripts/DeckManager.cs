using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DeckManager
{
    public static DeckManager Instance = new DeckManager();

    public LinkedList<string> deck;

    public void InitDeck()
    {
        deck = new LinkedList<string>();
        for (int i = 0; i < 10; i++)
        {
            deck.AddLast("Card0");
        }
        // for (int i = 0; i < 4; i++)
        // {
        //     deck.AddLast(new CardBase() { cardID = "Card1"});
        // }
        // deck.AddLast(new CardBase() { cardID = "Card2"});
    }
}
