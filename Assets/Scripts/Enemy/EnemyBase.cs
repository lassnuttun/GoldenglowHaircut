using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase
{
    public string EnemyID { get; private set; }
    public string EnemyName { get; private set; }
    public string EnemyDescription { get; private set; }
    public uint EnemyMaxHP { get; private set; }
    public uint EnemyCurHP { get; private set; }
    public uint EnemyMaxSP { get; private set; }
    public uint EnemyCurSP { get; private set; }

    public EnemyBase(string enemyID, string enemyName, string enemyDescription, uint enemyMaxHP, uint enemyMaxSP)
    {
        EnemyID = enemyID;
        EnemyName = enemyName;
        EnemyDescription = enemyDescription;
        EnemyMaxHP = enemyMaxHP;
        EnemyCurHP = EnemyMaxHP;
        EnemyMaxSP = enemyMaxSP;
        EnemyCurSP = 0;
    }
}
