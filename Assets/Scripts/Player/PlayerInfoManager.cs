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

    public List<CardBase> Deck { get; private set; }
    public Random.State RandState { get; private set; }
    public int MaxPw { get; private set; }
    public int Money { get; private set; }
    public int DrawCount { get; private set; }

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
            else
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