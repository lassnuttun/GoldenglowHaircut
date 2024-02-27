using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;

public class EnemyDisplay : MonoBehaviour
{
    public EnemyBase Enemy;
    public TextMeshProUGUI EnemyNameText;
    // public TextMeshProUGUI EnemyDescriptionText;
    public SkeletonGraphic SkelGrap;
    public GameObject CdBarObj;

    public Image HPFill;
    public Image SPFill;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI SPText;

    void Start() { }

    void Update() { }

    public virtual void Move1() { }
    public virtual void Move2() { }
    public virtual void Move3() { }
    public virtual void Move4() { }
    public virtual void Move5() { }

    public virtual void Bind(GameObject enemyModel, EnemyBase enemy)
    {
        Enemy = enemy;

        SkelGrap = enemyModel.GetComponent<SkeletonGraphic>();
        Object CdBarRes = AssetBundleManager.LoadResource<Object>("cdBar", "ui");
        GameObject cdBar = Instantiate(CdBarRes, enemyModel.transform) as GameObject;
        CdBarObj = cdBar;
        EnemyNameText = cdBar.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();
        HPFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        SPFill = CdBarObj.transform.Find("spBar").Find("fill").GetComponent<Image>();
        HPText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        SPText = CdBarObj.transform.Find("spText").GetComponent<TextMeshProUGUI>();

        EnemyNameText.text = Enemy.EnemyName;
        SkelGrap.AnimationState.SetAnimation(0, "Start", false);
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);

        UpdateDisplayInfo(Enemy.EnemyHP, Enemy.EnemySP);
    }

    public void UpdateDisplayInfo(ConditionBar EnemyHP, ConditionBar EnemySP)
    {
        HPFill.fillAmount = EnemyHP.Percent();
        HPText.text = EnemyHP.ToString();
        SPFill.fillAmount = EnemySP.Percent();
        SPText.text = EnemySP.ToString();
    }
}
