using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightCardManager
{
    public static FightCardManager Instance { get; private set; } = new FightCardManager();

    public static readonly int MaxHandPileCount = 10;

    public List<string> deckPile;

    public List<string> handPile;

    public List<string> discardPile;

    public void Init()
    {
        deckPile = new List<string>(PlayerInfoManager.Instance.Deck);
        handPile = new List<string>();
        discardPile = new List<string>();
        Shuffle();
    }

    public void Shuffle()
    {
        System.Random rand = new System.Random(PlayerInfoManager.Instance.Seed.GetHashCode());
        for (int i = deckPile.Count - 1; i > 0; i--)
        {
            int j = rand.Next();
            Debug.Log(j);
            j %= i + 1;
            // int j = rand.Next(0, i + 1);
            string tmp = deckPile[i];
            deckPile[i] = deckPile[j];
            deckPile[j] = tmp;
        }
    }

    public void DrawCards(int count)
    {
        if (count + handPile.Count > MaxHandPileCount)
        {
            count = MaxHandPileCount - handPile.Count;
        }
        if (count <= 0)
        {
            return;
        }

        if (count > deckPile.Count)
        {
            handPile.AddRange(deckPile);
            deckPile.Clear();
            count -= deckPile.Count;

            deckPile.AddRange(discardPile);
            discardPile.Clear();
            Shuffle();

            if (count > deckPile.Count)
            {
                count = deckPile.Count;
            }
        }

        // 注意是从后往前取的
        for (int i = 0; i < count; i++)
        {
            handPile.Add(deckPile[deckPile.Count - i - 1]);
        }
        deckPile.RemoveRange(deckPile.Count - count, count);
    }
}
