using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mlynar : EnemyBase
{
    public MlynarDisplay Display;
    public int ThornVal;
    public int ThornInc;

    public override EnemyDisplay Get()
    {
        return Display;
    }

    public override void Set(EnemyDisplay obj)
    {
        if (obj is MlynarDisplay)
        {
            Display = obj as MlynarDisplay;
        }
    }

    public Mlynar(EnemyConfigInfo enemyConfig) : base(enemyConfig)
    {
        ThornVal = 1;
        ThornInc = 3;
    }

    public override void BindDisplayComponent(GameObject enemyModel)
    {
        Display = enemyModel.AddComponent<MlynarDisplay>();
        base.BindDisplayComponent(enemyModel);
    }

    public override void TakeAction()
    {
        ThornVal += ThornInc;
        EnemySP.Inc(10);
        Display.Move1();
        Display.UpdateDisplayInfo();
    }
}
