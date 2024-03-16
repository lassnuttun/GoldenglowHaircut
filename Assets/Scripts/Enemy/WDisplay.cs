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
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }

    // 需要像 ExplodeBomb 一样，结束完骨骼动画之后再加上炸弹标签
    public void MarkAsBomb()
    {
        SkelGrap.AnimationState.SetAnimation(0, "Skill_2", false);
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }

    public void ExplodeBomb(EnvironmentDisplay display)
    {
        SkelGrap.AnimationState.SetAnimation(0, "Skill_3", false).End += (TrackEntry trackEntry) =>
        {
            display.Explode();
        };
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
}
