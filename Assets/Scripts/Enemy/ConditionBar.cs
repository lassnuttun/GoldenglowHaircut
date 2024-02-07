using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ConditionBar
{
    public int MaxValue { get; private set; }
    public int CurValue { get; private set; }

    public ConditionBar(int Max, int Cur)
    {
        MaxValue = Max;
        CurValue = Cur;
    }

    public void Inc(int DeltaValue)
    {
        CurValue = Mathf.Min(MaxValue, Mathf.Max(CurValue + DeltaValue, 0));
    }
}
