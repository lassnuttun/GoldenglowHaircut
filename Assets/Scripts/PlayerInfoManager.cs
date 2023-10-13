using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using UnityEngine;
using System.Text;

public class PlayerInfoManager
{
    public static PlayerInfoManager Instance { get; private set; } = new PlayerInfoManager();

    public LinkedList<string> Deck { get; private set; }

    public string Seed { get; private set; }

    public Random Rand { get; private set; }

    public int MaxPw { get; private set; }

    public int Money { get; private set; }

    public void InitPlayerInfo()
    {
        Deck = new LinkedList<string>();
        for (int i = 0; i < 4; i++)
        {
            Deck.AddLast("Card0");
        }
        for (int i = 0; i < 4; i++)
        {
            Deck.AddLast("Card1");
        }
        Deck.AddLast("Card2");

        // Seed = RNGCSP.GenerateRandomSeed(20);
        Seed = "hXry5r61Qep5YbESfogJ";
        Random.InitState(Seed.GetHashCode());

        MaxPw = 3;
        Money = 100;
    }
}

sealed class RNGCSP
{
    public static string GenerateRandomSeed(int len)
    {
        return GenerateRandomSeed(null, len);
    }

    public static string GenerateRandomSeed(string keyPool, int len)
    {
        if (keyPool == null || keyPool.Length < 8)
        {
            keyPool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        }
        int keyPoolLen = keyPool.Length;

        StringBuilder sb = new StringBuilder();
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            decimal maxValue = (decimal)long.MaxValue;
            byte[] buffer = new byte[8];
            for (int i = 0; i < len; i++)
            {
                rng.GetBytes(buffer);
                int index = (int)(System.Math.Abs(System.BitConverter.ToInt64(buffer, 0)) / maxValue * keyPoolLen);
                sb.Append(keyPool[index]);
            }
        }

        return sb.ToString();
    }
}
