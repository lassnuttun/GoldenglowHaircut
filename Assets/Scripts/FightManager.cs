using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance { get; private set; }

    public FightUnit fightUnit;

    public int maxPW;

    public int curPW;

    public int drawCount;

    void Awake()
    {
        Instance = this;
    }

    public void InitPlayerData()
    {
        maxPW = 3;
        curPW = maxPW;
        drawCount = 5;
    }

    public void MoveOn(FightUnitType type)
    {
        switch (type)
        {
            case FightUnitType.None: break;
            case FightUnitType.Init:
                fightUnit = new FightInit(); break;
            case FightUnitType.PlayerTurn:
                fightUnit = new FightPlayerTurn(); break;
            case FightUnitType.EnemyTurn:
                fightUnit = new FightEnemyTurn(); break;
            case FightUnitType.Win:
                fightUnit = new FightWin(); break;
            case FightUnitType.Lose:
                fightUnit = new FightLose(); break;
        }
        fightUnit.Init();
    }

    void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }
}
