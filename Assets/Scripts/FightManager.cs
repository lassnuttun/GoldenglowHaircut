using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance { get; private set; }
    public FightUnit CurState { get; private set; }

    public int MaxPW { get; private set; }
    public int CurPW { get; set; } // 临时

    public static readonly int MaxHandPileCount = 10;
    public int DrawCount { get; private set; }
    public List<List<CardBase>> CardPiles { get; private set; }

    public List<EnemyBase> EnemyList { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void InitFight(string gameID)
    {
        InitPlayerData();
        InitEnemyData(gameID);
    }

    void InitPlayerData()
    {
        PlayerInfoManager.Instance.InitPlayerInfo();
        MaxPW = PlayerInfoManager.Instance.MaxPw; 
        CurPW = MaxPW; 
        DrawCount = PlayerInfoManager.Instance.DrawCount;
        CardPiles = new List<List<CardBase>>(3)
        {
            PlayerInfoManager.Instance.Deck,
            new List<CardBase>(),
            new List<CardBase>()
        };
        Shuffle();
    }

    void InitEnemyData(string gameID)
    {
        EnemyList = new List<EnemyBase>();
        string file = AssetBundleManager.LoadResource<Object>(gameID, "config").ToString();
        SubgameConfigInfo gameConfig = JsonConvert.DeserializeObject<SubgameConfigInfo>(file);

        for (int i = 0; i < gameConfig.enemies.Length; i++)
        {
            file = AssetBundleManager.LoadResource<Object>(gameConfig.enemies[i], "config").ToString();
            EnemyConfigInfo enemyConfig = JsonConvert.DeserializeObject<EnemyConfigInfo>(file);
            EnemyList.Add(new EnemyBase(enemyConfig.enemyID, enemyConfig.enemyName, "NULL", enemyConfig.enemyHP, enemyConfig.enemySP));
        }
    }

    void Shuffle()
    {
        for (int i = CardPiles[0].Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // 不同子类交换是否会导致问题
            CardBase tmp = CardPiles[0][i];
            CardPiles[0][i] = CardPiles[0][j];
            CardPiles[0][j] = tmp;
        }
    }

    public void DrawCards(int count)
    {
        Debug.Log("Draw Cards");
        count = Mathf.Min(count, MaxHandPileCount - CardPiles[1].Count);
        if (count <= 0)
        {
            return;
        }

        if (count >= CardPiles[0].Count)
        {
            CardPiles[1].AddRange(CardPiles[0]);
            CardPiles[0].Clear();
            count -= CardPiles[0].Count;
            CardPiles[0].AddRange(CardPiles[2]);
            CardPiles[2].Clear();
            Shuffle();
            count = Mathf.Min(count, CardPiles[0].Count);
        }

        int len = CardPiles[0].Count;
        for (int i = 0; i < count; i++)
        {
            CardPiles[1].Add(CardPiles[0][len - i - 1]);
        }
        CardPiles[0].RemoveRange(len - count, count);
    }

    public void MoveOn(FightUnitType type)
    {
        switch (type)
        {
            case FightUnitType.None: break;
            case FightUnitType.Init:
                CurState = new FightInit(); break;
            case FightUnitType.PlayerTurn:
                CurState = new FightPlayerTurn(); break;
            case FightUnitType.EnemyTurn:
                CurState = new FightEnemyTurn(); break;
            case FightUnitType.Win:
                CurState = new FightWin(); break;
            case FightUnitType.Lose:
                CurState = new FightLose(); break;
        }
        CurState.Init();
    }

    void Update()
    {
        if (CurState != null)
        {
            CurState.OnUpdate();
        }
    }
}

sealed class SubgameConfigInfo
{
    public string subgameID;

    public string subgameName;

    public string[] enemies;
}

sealed class EnemyConfigInfo
{
    public string enemyID;

    public string enemyName;

    public uint enemyHP;

    public uint enemySP;
}
