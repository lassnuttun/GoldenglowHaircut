using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalmEnv : EnvironmentBase
{
    public CalmEnvDisplay Display;
    public int SPDecPerTurn = 3;

    public override EnvironmentDisplay Get()
    {
        return Display;
    }

    public override void Set(EnvironmentDisplay obj)
    {
        if (obj is CalmEnvDisplay)
        {
            Display = obj as CalmEnvDisplay;
        }
    }

    public CalmEnv(string envID, string envName, string envDescription, int duration, CardBase origin)
        : base(envID, envName, envDescription, duration, origin) { }

    public override void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.AddComponent<CalmEnvDisplay>();
        base.BindDisplayComponent(gameObj);
    }

    public override void ApplyEndTurn()
    {
        foreach (var Enemy in FightManager.Instance.EnemyList)
        {
            // 需要考虑其他环境影响
            Enemy.EnemySP.Inc(-SPDecPerTurn);
            Enemy.Get().UpdateDisplayInfo();
        }
        base.ApplyEndTurn();
    }
}
