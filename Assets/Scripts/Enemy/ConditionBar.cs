using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ConditionBar
{
    public int MaxValue;
    public int CurValue;

    public ConditionBar(int Max, int Cur)
    {
        MaxValue = Max;
        CurValue = Cur;
    }

    public bool ReachMax()
    {
        return CurValue >= MaxValue;
    }

    public void Inc(int DeltaValue)
    {
        CurValue = Mathf.Min(MaxValue, Mathf.Max(CurValue + DeltaValue, 0));
    }

    public float Percent()
    {
        return (float)CurValue / MaxValue;
    }

    public override string ToString()
    {
        return CurValue.ToString() + "/" + MaxValue.ToString();
    }
}
