using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightWin : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
    }
}
