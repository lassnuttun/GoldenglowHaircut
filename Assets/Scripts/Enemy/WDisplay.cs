using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDisplay : EnemyDisplay
{
    public W Enemy;

    public override EnemyBase Get() { return Enemy; }

    public override void Set(EnemyBase obj)
    {
        if (obj is W)
        {
            Enemy = obj as W;
        }
    }
}
