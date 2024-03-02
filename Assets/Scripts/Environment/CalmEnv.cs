using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmEnv : EnvironmentBase
{
    public int SPDecPerTurn = 3;

    public CalmEnv(string envID, string envName, string envDescription, int duration)
        : base(envID, envName, envDescription, duration) { }

    public override void ApplyEndTurn()
    {
        foreach(var Enemy in FightManager.Instance.EnemyList)
        {
            // 需要考虑其他环境影响
            Enemy.EnemySP.Inc(-SPDecPerTurn);
        }
    }
}
