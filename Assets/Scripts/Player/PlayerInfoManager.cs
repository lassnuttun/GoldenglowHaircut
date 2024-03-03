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
        for (int i = 0; i < 4; i++)
        {
            Deck.Add(new CardBase("Card0", "长崎素世", "稍作安抚，减少 5 点敏感值", 1, 0, -5));
        }
        for (int i = 0; i < 40; i++)
        {
            Deck.Add(new HypnoticCenser("CardE001", "安眠迷香", "创造一个持续 {0} 回合的【安神】环境", 1, 5, 0, 4, "E001", "安神", "回合结束时，所有顾客SP减 {0}"));
        }
        Deck.Add(new CardBase("Card2", "若叶睦", "粗暴修剪，增加 30 点修剪值，增加 5 点敏感值", 1, 30, 5));

        MaxPw = 3;
        Money = 100;
        DrawCount = 5;
    }
}
