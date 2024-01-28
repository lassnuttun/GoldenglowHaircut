using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;
using System;

public class EnemyDisplay : MonoBehaviour
{
    public EnemyBase Enemy;
    public TextMeshProUGUI EnemyNameText;
    // public TextMeshProUGUI EnemyDescriptionText;
    public SkeletonGraphic SkelGrap;
    public GameObject CdBarObj;

    void Start()
    {
        ShowEnemy();
    }

    void Update()
    {
    }

    public void ShowEnemy()
    {
        EnemyNameText.text = Enemy.EnemyName;

        SkelGrap.AnimationState.SetAnimation(0, AnimationType.Start.ToString(), false);
        SkelGrap.AnimationState.AddAnimation(0, AnimationType.Idle.ToString(), true, 0);

        Image hpFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        hpFill.fillAmount = (float)Enemy.EnemyCurHP / (float)Enemy.EnemyMaxHP;
        TextMeshProUGUI hpText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        hpText.text = Enemy.EnemyCurHP.ToString() + "/" + Enemy.EnemyMaxHP.ToString();
    }
}
