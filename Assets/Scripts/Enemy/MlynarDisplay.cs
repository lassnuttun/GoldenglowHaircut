using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MlynarDisplay : EnemyDisplay
{
    public Mlynar Enemy;

    public override EnemyBase Get() { return Enemy; }

    public override void Set(EnemyBase obj)
    {
        if (obj is Mlynar)
        {
            Enemy = obj as Mlynar;
        }
    }

    public override void Move1()
    {
        SkelGrap.AnimationState.SetAnimation(0, "Idle_2", false);
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
}
