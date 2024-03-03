using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBase
{
    public string EnvID;
    public string EnvName;
    public string EnvDescription;

    public int Duration;

    public CardBase EnvCard;

    public EnvSlotDisplay Display;

    public EnvironmentBase(string envID, string envName, string envDescription, int duration)
    {
        EnvID = envID;
        EnvName = envName;
        EnvDescription = envDescription;
        Duration = duration;
    }

    public void BindDisplayComponent(GameObject gameObj)
    {
        Display = gameObj.GetComponent<EnvSlotDisplay>();
        Display.Environment = this;
    }

    public virtual void ApplyCalcHP() { }

    public virtual void ApplyCalcSP() { }

    public virtual void ApplyEndTurn() { }
}
