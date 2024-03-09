using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;
using Spine;

public abstract class EnemyDisplay : MonoBehaviour, IProperty<EnemyBase>
{
    public TextMeshProUGUI EnemyNameText;
    // public TextMeshProUGUI EnemyDescriptionText;
    public SkeletonGraphic SkelGrap;
    public GameObject CdBarObj;

    public Image HPFill;
    public Image SPFill;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI SPText;

    public virtual EnemyBase Get()
    {
        return null;
    }
    public virtual void Set(EnemyBase obj)
    {
    }

    public virtual void Move1() { }
    public virtual void Move2() { }
    public virtual void Move3() { }
    public virtual void Move4() { }
    public virtual void Move5() { }

    public virtual void Bind(GameObject enemyModel, EnemyBase enemy)
    {
        Set(enemy);
        SkelGrap = enemyModel.GetComponent<SkeletonGraphic>();
        Object CdBarRes = AssetBundleManager.LoadResource<Object>("cdBar", "ui");
        GameObject cdBar = Instantiate(CdBarRes, enemyModel.transform) as GameObject;
        CdBarObj = cdBar;
        EnemyNameText = cdBar.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();
        HPFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        SPFill = CdBarObj.transform.Find("spBar").Find("fill").GetComponent<Image>();
        HPText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        SPText = CdBarObj.transform.Find("spText").GetComponent<TextMeshProUGUI>();

        EnemyNameText.text = enemy.EnemyName;
        SkelGrap.AnimationState.SetAnimation(0, "Start", false);
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);

        UpdateDisplayInfo();
    }

    public void UpdateDisplayInfo()
    {
        EnemyBase enemy = Get();
        HPFill.fillAmount = enemy.EnemyHP.Percent();
        HPText.text = enemy.EnemyHP.ToString();
        SPFill.fillAmount = enemy.EnemySP.Percent();
        SPText.text = enemy.EnemySP.ToString();
    }

    public virtual void CutComplete()
    {
        FightManager.Instance.EnemyList.Remove(Get());
        SkelGrap.AnimationState.SetAnimation(0, "Die", false);
        SkelGrap.AnimationState.Complete += (TrackEntry trackEntry) =>
        {
            if (trackEntry.Animation.Name == "Die")
            {
                Destroy(gameObject);
                if (FightManager.Instance.EnemyList.Count == 0)
                {
                    FightManager.Instance.MoveOn(FightUnitType.Win);
                    UIManager.Instance.ShowUI<WinUI>("WinUI", true);
                }
            }
        };
    }
}
