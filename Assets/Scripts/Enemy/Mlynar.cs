using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mlynar : EnemyBase
{
    public int ThornVal { get; private set; }
    public int ThornInc { get; private set; }

    public Mlynar(string enemyID, string enemyName, string enemyDescription, int enemyMaxHP, int enemyMaxSP) : base(enemyID, enemyName, enemyDescription, enemyMaxHP, enemyMaxSP) 
    {
        ThornVal = 1;
        ThornInc = 3;
    }

    public override void Move1()
    {
        ThornVal += ThornInc;
    }

    public override void Move2()
    {
        throw new System.NotImplementedException();
    }

    public override void Move3()
    {
        throw new System.NotImplementedException();
    }

    public override void Move4()
    {
        throw new System.NotImplementedException();
    }

    public override void Move5()
    {
        throw new System.NotImplementedException();
    }
}
