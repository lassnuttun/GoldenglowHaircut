using Spine;
using System;
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

    public void AddPotato()
    {
        SkelGrap.AnimationState.SetAnimation(0, "Skill_1", false);
    }

    public void MarkAsBomb()
    {
        SkelGrap.AnimationState.SetAnimation(0, "Skill_2", false);
    }

    public void ExplodeBomb(EnvironmentDisplay display)
    {
        SkelGrap.AnimationState.SetAnimation(0, "Skill_3", false);
        SkelGrap.AnimationState.Complete += (TrackEntry trackEntry) =>
        {
            display.Explode();
        };
    }
}
