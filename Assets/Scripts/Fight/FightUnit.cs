using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FightUnitType
{
    None,
    Init,
    PrePlayerTurn,
    PlayerTurn,
    EnemyTurn,
    Win,
    Lose
}

public abstract class FightUnit
{
    public virtual void Init() { }

    public virtual void OnUpdate() { }
}
