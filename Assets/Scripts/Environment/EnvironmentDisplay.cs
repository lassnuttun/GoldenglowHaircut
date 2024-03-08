using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnvironmentDisplay : MonoBehaviour, IProperty<EnvironmentBase>
{
    public TextMeshProUGUI CurEnvText;
    public TextMeshProUGUI DescripText;

    public virtual EnvironmentBase Get()
    {
        return null;
    }

    public virtual void Set(EnvironmentBase obj)
    {
    }

    public virtual void Bind(GameObject gameObj, EnvironmentBase environment)
    {
        Set(environment);
        CurEnvText = gameObj.transform.Find("CurEnv").GetComponent<TextMeshProUGUI>();
        DescripText = gameObj.transform.Find("Description").GetComponent<TextMeshProUGUI>();
        UpdateDisplayInfo();
    }

    public virtual void UpdateDisplayInfo()
    {
        EnvironmentBase environment = Get();
        CurEnvText.text = string.Format("【{0}】：剩余 {1} 回合", environment.EnvName, environment.Duration.ToString());
    }

    public void AddToEnvSlot()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");
        ui.UpdateEnvPos(Get().Origin.Get().MoveFromHandToSlot);
    }

    public void RemoveFromEnvSlot()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.DOScale(0, FightUI.CardInterval).OnComplete(() => { Destroy(gameObject, 1); });
        ui.UpdateEnvPos();
    }
}
