using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLose : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
    }
}
