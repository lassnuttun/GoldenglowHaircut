using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase
{
    public string EnemyID { get; private set; }
    public string EnemyName { get; private set; }
    public string EnemyDescription { get; private set; }
    public int EnemyMaxHP { get; private set; }
    public int EnemyCurHP { get; private set; }
    public int EnemyMaxSP { get; private set; }
    public int EnemyCurSP { get; private set; }

    public EnemyBase(string enemyID, string enemyName, string enemyDescription, int enemyMaxHP, int enemyMaxSP)
    {
        EnemyID = enemyID;
        EnemyName = enemyName;
        EnemyDescription = enemyDescription;
        EnemyMaxHP = enemyMaxHP;
        EnemyCurHP = EnemyMaxHP;
        EnemyMaxSP = enemyMaxSP;
        EnemyCurSP = 0;
    }

    public void IncHP(int deltaHP)
    {
        EnemyCurHP = Mathf.Min(EnemyMaxHP, Mathf.Max(EnemyCurHP + deltaHP, 0));
    }

    public void IncSP(int deltaSP) 
    {
        EnemyCurSP = Mathf.Min(EnemyMaxSP, Mathf.Max(EnemyCurSP + deltaSP, 0));
    }

    public abstract void Move1();
    public abstract void Move2();
    public abstract void Move3();
    public abstract void Move4();
    public abstract void Move5();
}
