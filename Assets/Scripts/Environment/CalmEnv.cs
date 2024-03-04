using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmEnv : EnvironmentBase
{
    public int SPDecPerTurn = 3;

    public CalmEnv(string envID, string envName, string envDescription, int duration)
        : base(envID, envName, envDescription, duration) { }

    public override void UpdateDisplayInfo()
    {
        Display.CurEnvText.text = string.Format("【{0}】：剩余 {1} 回合", EnvName, Duration.ToString());
        Display.DescripText.text = string.Format(EnvDescription, SPDecPerTurn.ToString(), Duration.ToString());
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
