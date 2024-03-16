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
        AddPotato();
    }

    public override void TakeAction()
    {
        switch (StepCnt)
        {
            case 1:
                MarkAsBomb(0);
                break;
            case 2:
                ModifySP(15);
                // 危险环境还没写
                break;
            case 3:
                MarkAsBomb(0);
                MarkAsBomb(1);
                break;
            case 4:
                // 将所有环境卡变成土豆还没写
                break;
            case 5:
                MarkAsBomb(0);
                MarkAsBomb(1);
                MarkAsBomb(2);
                break;
            case 6:
                MarkAsBomb(0);
                break;
            case 7:
                ModifySP(30);
                break;
            default:
                ModifySP(10);
                MarkAsBomb(0);
                MarkAsBomb(1);
                MarkAsBomb(2);
                MarkAsBomb(3);
                break;
        }
        ExplodeBomb();
        StepCnt++;
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
        Display.MarkAsBomb(list[index].Get());
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
                    item.Key.ExplodeBomb();
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

    public void TransformAllEnv()
    {

    }
}
