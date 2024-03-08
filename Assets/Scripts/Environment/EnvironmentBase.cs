using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentBase : IProperty<EnvironmentDisplay>
{
    public string EnvID;
    public string EnvName;
    public string EnvDescription;

    public int Duration;
    public CardBase Origin;

    protected void LoadModel()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");
        Object resource = AssetBundleManager.LoadResource<Object>(EnvID, "env");
        GameObject gameObj = GameObject.Instantiate(resource, ui.EnvSlots) as GameObject;
        BindDisplayComponent(gameObj);
        RectTransform rectTransform = gameObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(1000, 0);
    }

    public virtual EnvironmentDisplay Get()
    {
        return null;
    }

    public virtual void Set(EnvironmentDisplay obj) { }

    public virtual void BindDisplayComponent(GameObject gameObj)
    {
        Get().Bind(gameObj, this);
    }

    public EnvironmentBase(string envID, string envName, string envDescription, int duration, CardBase origin)
    {
        EnvID = envID;
        EnvName = envName;
        EnvDescription = envDescription;
        Duration = duration;
        Origin = origin;
    }

    public virtual void ApplyCalcHP() { }

    public virtual void ApplyCalcSP() { }

    public virtual void ApplyEndTurn()
    {
        Duration--;
    }

    public virtual void AddToEnvSlot()
    {
        List<EnvironmentBase> list = FightManager.Instance.EnvList;
        if (list.Count >= FightManager.MaxEnvCount)
        {
            list[0].RemoveFromEnvSlot();
        }
        list.Add(this);
        LoadModel();
        Get().AddToEnvSlot();
    }

    public virtual void RemoveFromEnvSlot()
    {
        List<EnvironmentBase> list = FightManager.Instance.EnvList;
        for (int j = list.IndexOf(this) + 1; j < list.Count; j++)
        {
            list[j - 1] = list[j];
        }
        list.RemoveAt(list.Count - 1);
        Get().RemoveFromEnvSlot();
    }
}
