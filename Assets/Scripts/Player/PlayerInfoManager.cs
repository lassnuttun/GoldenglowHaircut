using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerInfoManager
{
    private PlayerInfoManager() { }
    private static PlayerInfoManager instance;
    public static PlayerInfoManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerInfoManager();
            }
            return instance;
        }
    }

    public List<CardBase> Deck;
    public Random.State RandState;
    public int MaxPw;
    public int Money;
    public int DrawCount;

    public void InitPlayerInfo()
    {
        Deck = new List<CardBase>();
        string file = AssetBundleManager.LoadResource<Object>("PlayerInfo", "config").ToString();
        PlayerInfo playerInfo = JsonConvert.DeserializeObject<PlayerInfo>(file);
        foreach (var card in playerInfo.cards)
        {
            file = AssetBundleManager.LoadResource<Object>(card.id, "config").ToString();
            CardConfigInfo cardConfigInfo = JsonConvert.DeserializeObject<CardConfigInfo>(file);
            if (card.id == "CardA000")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new BasicCutCard(cardConfigInfo));
                }
            }
            else if (card.id == "CardA001")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new BasicSootheCard(cardConfigInfo));
                }
            }
            else if (card.id == "CardA002")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new SuperCutCard(cardConfigInfo));
                }
            }
            else if (card.id == "CardA003")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new QXJJCard(cardConfigInfo));
                }
            }
            else if (card.id == "CardA004")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new JZDLCard(cardConfigInfo));
                }
            }
            else if (card.id == "CardA005")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new DDYLLCard(cardConfigInfo));
                }
            }
            else if (card.id == "CardE001")
            {
                for (int i = 0; i < card.cnt; i++)
                {
                    Deck.Add(new HypnoticCenser(cardConfigInfo));
                }
            }
        }

        MaxPw = 3;
        Money = 100;
        DrawCount = 5;
    }
}

public class CardConfigInfo
{
    public string id;
    public string name;
    public string desc;
    public int cost;
    public int hp;
    public int sp;
    public int dura;
    public string env;
    public int sa;
}

public class CardCount
{
    public string id;
    public int cnt;
}

public class PlayerInfo
{
    public List<CardCount> cards;
}