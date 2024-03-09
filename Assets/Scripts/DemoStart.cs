using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class DemoStart : MonoBehaviour
{
    public string Seed;

    // 所有 Manager 的初始化都需要修改
    void Start()
    {
        // Seed = RNGCSP.GenerateRandomSeed(20);
        Seed = "hXry5r61Qep5YbESfogJ";
        Random.InitState(Seed.GetHashCode());

        PlayerInfoManager.Instance.InitPlayerInfo();
        // 进入战斗的 Init 阶段
        FightManager.Instance.MoveOn(FightUnitType.Init);
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
            decimal maxValue = long.MaxValue;
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
