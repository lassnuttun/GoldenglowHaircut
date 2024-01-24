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
    public SkeletonAnimation SkeletonAnimation;
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
        gameObject.transform.localScale = new Vector3(-0.8f, 0.8f);

        EnemyNameText.text = Enemy.EnemyName;

        SkeletonAnimation.AnimationState.SetAnimation(0, AnimationType.Start.ToString(), false);
        SkeletonAnimation.AnimationState.AddAnimation(0, AnimationType.Idle.ToString(), true, 0);

        CdBarObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 4.5f, 0));
        Image hpFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        hpFill.fillAmount = (float)Enemy.EnemyCurHP / (float)Enemy.EnemyMaxHP;
        TextMeshProUGUI hpText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        hpText.text = Enemy.EnemyCurHP.ToString() + "/" + Enemy.EnemyMaxHP.ToString();
    }
}
