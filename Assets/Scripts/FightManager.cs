using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance { get; private set; }
    public FightUnit CurState;
    public ConditionBar Power;

    public static readonly int MaxHandPileCount = 10;
    public int DrawCount;
    public List<List<CardBase>> CardPiles;

    public List<EnemyBase> EnemyList;

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
        Power = new ConditionBar(PlayerInfoManager.Instance.MaxPw, PlayerInfoManager.Instance.MaxPw);
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
            if (enemyConfig.enemyID == "Mlynar")
            {
                EnemyList.Add(new Mlynar(enemyConfig.enemyID, enemyConfig.enemyName, "NULL", enemyConfig.enemyHP, enemyConfig.enemySP));
            }
        }
    }

    void Shuffle()
    {
        for (int i = CardPiles[0].Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // 不同子类交换是否会导致问题
            (CardPiles[0][j], CardPiles[0][i]) = (CardPiles[0][i], CardPiles[0][j]);
        }
    }

    public IEnumerator DrawCards(int count)
    {
        count = Mathf.Min(count, MaxHandPileCount - CardPiles[1].Count);
        if (count <= 0)
        {
            yield break;
        }
        FightUI fightUI = UIManager.Instance.GetUI<FightUI>("FightUI");
        if (count >= CardPiles[0].Count)
        {
            for (int i = CardPiles[0].Count - 1; i >= 0; i--)
            {
                CardPiles[1].Add(CardPiles[0][i]);
                fightUI.AddCard();
                yield return new WaitForSeconds(FightUI.CardInterval);
                count--;
            }
            CardPiles[0].Clear();
            CardPiles[0].AddRange(CardPiles[2]);
            CardPiles[2].Clear();
            Shuffle();
            count = Mathf.Min(count, CardPiles[0].Count);
        }

        int len = CardPiles[0].Count;
        for (int i = 0; i < count; i++)
        {
            CardPiles[1].Add(CardPiles[0][len - i - 1]);
            fightUI.AddCard();
            yield return new WaitForSeconds(FightUI.CardInterval);
        }
        CardPiles[0].RemoveRange(len - count, count);
        yield break;
    }

    public void RemoveCard(int index, bool isUse = false)
    {
        int count = Instance.CardPiles[1].Count;
        if (index < 0 || index >= count)
        {
            return;
        }

        CardBase card = CardPiles[1][index];
        if (isUse)
        {
            Instance.Power.Inc(-card.CardCost);
            UIManager.Instance.GetUI<FightUI>("FightUI").transform.Find("power").GetComponent<PowerDisplay>().UpdateDisplayInfo(Power);
        }
        CardPiles[1].RemoveAt(index);
        CardPiles[2].Add(card);
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(card.Display);
    }

    public IEnumerator RemoveAllCard()
    {
        foreach (var card in CardPiles[1].AsEnumerable().Reverse()) 
        {
            Instance.RemoveCard(CardPiles[1].IndexOf(card));
            yield return new WaitForSeconds(FightUI.CardInterval);
        }
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

    public IEnumerator EnemyAction()
    {
        foreach (var enemy in EnemyList)
        {
            enemy.Move1();
            yield return new WaitForSeconds(1.5f);
        }
        Instance.MoveOn(FightUnitType.PlayerTurn);
        yield break;
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

    public int enemyHP;

    public int enemySP;
}
