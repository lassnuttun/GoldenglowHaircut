using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnvCreateCardDisplay<TEnv> : CardDisplay where TEnv : EnvironmentBase
{
    public EnvCreateCard<TEnv> Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is EnvCreateCard<TEnv>)
        {
            Card = obj as EnvCreateCard<TEnv>;
        }
    }

    public override bool TargetExist(RaycastHit2D hit, out EnemyBase enemy)
    {
        enemy = null;
        PlayerDisplay playerDisplay = hit.collider.gameObject.GetComponent<PlayerDisplay>();
        if (playerDisplay == null)
        {
            return false;
        }
        return true;
    }
}
