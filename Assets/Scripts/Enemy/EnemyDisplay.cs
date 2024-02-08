using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;
using System;

public class EnemyDisplay : MonoBehaviour
{
    // public EnemyBase Enemy;
    public TextMeshProUGUI EnemyNameText;
    // public TextMeshProUGUI EnemyDescriptionText;
    public SkeletonGraphic SkelGrap;
    public GameObject CdBarObj;

    void Start() { }

    void Update() { }

    public virtual void Move1() { }
    public virtual void Move2() { }
    public virtual void Move3() { }
    public virtual void Move4() { }
    public virtual void Move5() { }

    public void UpdateDisplayInfo(ConditionBar EnemyHP, ConditionBar EnemySP)
    {
        Image hpFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        hpFill.fillAmount = (float)EnemyHP.CurValue / (float)EnemyHP.MaxValue;
        TextMeshProUGUI hpText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        hpText.text = EnemyHP.CurValue.ToString() + "/" + EnemyHP.MaxValue.ToString();

        Image spFill = CdBarObj.transform.Find("spBar").Find("fill").GetComponent<Image>();
        spFill.fillAmount = (float)EnemySP.CurValue / (float)EnemySP.MaxValue;
        TextMeshProUGUI spText = CdBarObj.transform.Find("spText").GetComponent<TextMeshProUGUI>();
        spText.text = EnemySP.CurValue.ToString() + "/" + EnemySP.MaxValue.ToString();
    }
}
