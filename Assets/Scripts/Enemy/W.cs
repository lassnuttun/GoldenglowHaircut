using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class W : EnemyBase
{
    public WDisplay Display;
    public int StepCnt;
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
        StepCnt = 1;
        EnvToExplode = new Dictionary<EnvironmentBase, int>();
    }

    public override void BindDisplayComponent(GameObject enemyModel)
    {
        Display = enemyModel.AddComponent<WDisplay>();
        base.BindDisplayComponent(enemyModel);
        AddPotato(1);
    }

    public override void TakeAction()
    {
        switch (StepCnt)
        {
            case 1:
                MarkAsBomb(new List<int> { 0 });
                break;
            case 2:
                ModifySP(15);
                AddDangerEnv();
                break;
            case 3:
                MarkAsBomb(new List<int> { 0, 1 });
                break;
            case 4:
                PotatoMagic();
                break;
            case 5:
                MarkAsBomb(new List<int> { 0, 1, 2 });
                break;
            case 6:
                MarkAsBomb(new List<int> { 0 });
                break;
            case 7:
                ModifySP(30);
                ExplodeBomb();
                break;
            default:
                ModifySP(10);
                MarkAsBomb(new List<int> { 0, 1, 2, 3 });
                break;
        }
        StepCnt++;
    }

    public void AddPotato(int cnt)
    {
        if (cnt <= 0)
        {
            return;
        }
        for (int i = 0; i < cnt; i++)
        {
            PotatoEnv potatoEnv = new PotatoEnv("E000", "奇怪的土豆", "", 100, null);
            potatoEnv.AddToEnvSlot();
        }
        Display.AddPotato();
    }

    public void MarkAsBomb(List<int> indices)
    {
        var list = FightManager.Instance.EnvList;
        var displays = new List<EnvironmentDisplay>();
        foreach (var index in indices)
        {
            if (EnvToExplode.TryGetValue(list[index], out _))
            {
                continue;
            }
            EnvToExplode.Add(list[index], 2);
            displays.Add(list[index].Get());
        }
        Display.MarkAsBomb(displays);
    }

    public void AddDangerEnv()
    {
        DangerousEnv dangerEnv = new DangerousEnv("E004", "危险环境", "造成的修剪值减半", 2, null);
        dangerEnv.AddToEnvSlot();
        Display.AddDangerEnv();
    }

    public void ExplodeBomb()
    {
        var list = FightManager.Instance.EnvList;
        var bomb = new List<EnvironmentDisplay>();
        var trash = new List<EnvironmentBase>();
        foreach (var env in EnvToExplode.Keys.ToList())
        {
            if (list.Contains(env))
            {
                if (--EnvToExplode[env] <= 0)
                {
                    env.ExplodeBomb();
                    bomb.Add(env.Get());
                    trash.Add(env);
                }
            }
            else
            {
                trash.Add(env);
            }
        }

        foreach (var env in trash)
        {
            EnvToExplode.Remove(env);
        }

        Display.ExplodeBomb(bomb);
    }

    public void PotatoMagic()
    {
        var list = FightManager.Instance.EnvList;
        int cnt = list.Count;
        
        foreach (var env in list.AsEnumerable().Reverse())
        {
            env.ExplodeBomb();
            env.Get().RemoveFromEnvSlot(false);
        }

        for (int i = 0; i < cnt; i++)
        {
            PotatoEnv potatoEnv = new PotatoEnv("E000", "奇怪的土豆", "", 100, null);
            potatoEnv.AddToEnvSlot();
        }

        Display.PotatoMagic();
    }
}
