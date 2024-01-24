using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using UnityEngine;
using System.Text;

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
        CardBase card0 = new CardBase("Card0", "长崎素世", "稍作安抚，减少 5 点敏感值", 1);
        CardBase card1 = new CardBase("Card1", "千早爱音", "简单修剪，增加 5 点修剪值", 1);
        CardBase card2 = new CardBase("Card2", "若叶睦", "粗暴修剪，增加 10 点修剪值，增加 5 点敏感值", 1);
        for (int i = 0; i < 4; i++)
        {
            Deck.Add(card0);
        }
        for (int i = 0; i < 4; i++)
        {
            Deck.Add(card1);
        }
        Deck.Add(card2);

        MaxPw = 3;
        Money = 100;
        DrawCount = 5;
    }
}
