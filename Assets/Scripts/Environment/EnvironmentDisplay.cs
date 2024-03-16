using DG.Tweening;
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
        CurEnvText.text = string.Format("{0}：{1}回合", environment.EnvName, environment.Duration.ToString());
    }

    public void AddToEnvSlot()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");
        CardBase origin = Get().Origin;
        if (origin != null)
        {
            ui.UpdateEnvPos(origin.Get().MoveFromHandToSlot);
        }
        else
        {
            ui.UpdateEnvPos();
        }
    }

    public void RemoveFromEnvSlot()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");
        transform.DOScale(0, FightUI.CardInterval).OnComplete(() => { Destroy(gameObject, 1); });
        // 环境卡进入弃牌堆的特效还没想好怎么加
        ui.UpdateEnvPos();
    }

    public void MarkAsBomb()
    {
        Object resource = AssetBundleManager.LoadResource<Object>("BombTag", "env");
        GameObject gameObj = GameObject.Instantiate(resource, transform) as GameObject;
        gameObj.transform.localPosition = new Vector3(100, 0, 0);
        gameObj.transform.SetSiblingIndex(1);
    }

    public void Explode()
    {
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");

        Object resource = AssetBundleManager.LoadResource<Object>("Explosion", "particle");
        GameObject gameObj = GameObject.Instantiate(resource, transform) as GameObject;
        gameObj.transform.GetComponent<ParticleSystem>().Play();

        // transform.DOScale(0, FightUI.CardInterval);
        transform.DOScale(0, FightUI.CardInterval).OnComplete(() => { Destroy(gameObject, 1); });
        ui.UpdateEnvPos();
    }
}
