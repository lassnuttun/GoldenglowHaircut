using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class W : EnemyBase
{
    public WDisplay Display;

    private Dictionary<EnvironmentBase, int> EnvToExplode;

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

    public W(EnemyConfigInfo enemyConfig) : base(enemyConfig)
    {
        EnvToExplode = new Dictionary<EnvironmentBase, int>();
    }

    public override void BindDisplayComponent(GameObject enemyModel)
    {
        Display = enemyModel.AddComponent<WDisplay>();
        base.BindDisplayComponent(enemyModel);
    }

    public override void Move1()
    {
        // AddPotato();
        MarkAsBomb(0);
        ExplodeBomb();
    }

    public void AddPotato()
    {
        PotatoEnv potatoEnv = new PotatoEnv("E000", "奇怪的土豆", "", 100, null);
        potatoEnv.AddToEnvSlot();
        Display.AddPotato();
    }

    public void MarkAsBomb(int index)
    {
        List<EnvironmentBase> list = FightManager.Instance.EnvList;
        if (index >= list.Count)
        {
            return;
        }
        if (EnvToExplode.TryGetValue(list[index], out _))
        {
            return;
        }
        EnvToExplode.Add(list[index], 2);
        list[index].Get().MarkAsBomb();
        Display.MarkAsBomb();
    }

    public void ExplodeBomb()
    {
        List<EnvironmentBase> list = FightManager.Instance.EnvList;
        foreach (var item in EnvToExplode.AsEnumerable().Reverse())
        {
            if (list.Contains(item.Key))
            {
                if (--EnvToExplode[item.Key] <= 0)
                {
                    item.Key.Explode();
                    EnvToExplode.Remove(item.Key);
                    Display.ExplodeBomb(item.Key.Get());
                }
            }
            else
            {
                EnvToExplode.Remove(item.Key);
            }
        }
    }
}
