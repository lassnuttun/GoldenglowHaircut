using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W : EnemyBase
{
    public WDisplay Display;

    public override EnemyDisplay Get()
    {
        return Display;
    }

    public override void Set(EnemyDisplay obj)
    {
        if (obj is WDisplay)
        {
            Display = obj as WDisplay;
        }
    }

    public W(EnemyConfigInfo enemyConfig) : base(enemyConfig) { }

    public override void BindDisplayComponent(GameObject enemyModel)
    {
        Display = enemyModel.AddComponent<WDisplay>();
        base.BindDisplayComponent(enemyModel);
    }

    public override void Move1()
    {
        AddPotato();
    }

    public void AddPotato()
    {
        PotatoEnv potatoEnv = new PotatoEnv("E000", "奇怪的土豆", "", 100, null);
        potatoEnv.AddToEnvSlot();
    }

    public void MarkAsBomb()
    {

    }
}
