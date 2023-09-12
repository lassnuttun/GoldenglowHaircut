using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Serialization;

public class FightCardManager
{
    public static FightCardManager Instance { get; private set; } = new FightCardManager();

    public static readonly int MaxHandPileCount = 10;

    public List<string> deckPile;

    public List<string> handPile;

    public List<string> discardPile;

    public List<Card> handPileUI;

    private static AssetBundle cardab;

    public void Init()
    {
        cardab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/card");
        deckPile = new List<string>(DeckManager.Instance.deck);
        handPile = new List<string>();
        discardPile = new List<string>();
        handPileUI = new List<Card>();
        Shuffle(deckPile);
    }

    public void Shuffle(List<string> pileToShuffle)
    {
        pileToShuffle = pileToShuffle.OrderBy(x => Guid.NewGuid()).ToList();
    }

    public void DrawCards(int count)
    {
        if (count + handPile.Count > MaxHandPileCount)
        {
            count = MaxHandPileCount - handPile.Count;
        }
        if (count < 0)
        {
            count = 0;
        }
        if (count > deckPile.Count + discardPile.Count)
        {
            count = deckPile.Count + discardPile.Count;
        }
        if (count > deckPile.Count)
        {
            deckPile.AddRange(discardPile);
            discardPile.Clear();
            Shuffle(deckPile);
        }
        for (int i = 0; i < count; i++)
        {
            handPile.Add(deckPile[deckPile.Count - i - 1]);
        }
        deckPile.RemoveRange(deckPile.Count - count, count);
    }

    public void UpdateHandPile()
    {
        var canvasTrans = GameObject.Find("Canvas").transform;

        List<Quaternion> rotations = new List<Quaternion>();
        int count = handPile.Count;
        if (count < 6)
        {
            if (count % 2  == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    rotations.Add(Quaternion.Euler(0, 0, -5 + 10 * (count / 2 - i)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    rotations.Add(Quaternion.Euler(0, 0, 10 * (count / 2 - i)));
                }
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                rotations.Add(Quaternion.Euler(0, 0, 25 - (float)50 / (count - 1) * i));
            }
        }

        for ( int i = 0; i < handPile.Count; i++)
        {
            var resource = cardab.LoadAsset(handPile[i]);
            GameObject cardObj = GameObject.Instantiate(resource, canvasTrans) as GameObject;
            var rectTrans = cardObj.GetComponent<RectTransform>();
            rectTrans.pivot = new Vector2(0.5f, -2.5f);
            rectTrans.position = Camera.main.WorldToScreenPoint(new Vector3(0, -15));
            rectTrans.rotation = rotations[i];

            handPileUI.Add(cardObj.AddComponent<Card>());
        }
    }
}
