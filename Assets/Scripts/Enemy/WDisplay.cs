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
        SkelGrap.AnimationState.SetAnimation(0, "Skill_1", false).End += (TrackEntry trackEntry) =>
        {
        };
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }

    public void MarkAsBomb(List<EnvironmentDisplay> displays)
    {
        SkelGrap.AnimationState.SetAnimation(0, "Skill_2", false).End += (TrackEntry trackEntry) =>
        {
            foreach (var display in displays)
            {
                display.MarkAsBomb();
            }
            Enemy.ExplodeBomb();
        };
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }

    public void ExplodeBomb(List<EnvironmentDisplay> displays)
    {
        if (displays.Count == 0)
        {
            SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
            return;
        }
        SkelGrap.AnimationState.SetAnimation(0, "Skill_3", false).End += (TrackEntry trackEntry) =>
        {
            foreach (var display in displays)
            {
                display.ExplodeBomb();
                // 需要移动到敌人的脚本逻辑里
                foreach (var enemy in FightManager.Instance.EnemyList)
                {
                    enemy.ModifySP(30);
                }
            }
        };
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
}
