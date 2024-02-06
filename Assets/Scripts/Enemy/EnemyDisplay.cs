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

    public virtual void ShowEnemy()
    {
        EnemyNameText.text = Enemy.EnemyName;

        SkelGrap.AnimationState.SetAnimation(0, AnimationType.Start.ToString(), false);
        SkelGrap.AnimationState.AddAnimation(0, AnimationType.Idle.ToString(), true, 0);

        Image hpFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        hpFill.fillAmount = (float)Enemy.EnemyCurHP / (float)Enemy.EnemyMaxHP;
        TextMeshProUGUI hpText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        hpText.text = Enemy.EnemyCurHP.ToString() + "/" + Enemy.EnemyMaxHP.ToString();
    }

    public virtual void Move1() { }
    public virtual void Move2() { }
    public virtual void Move3() { }
    public virtual void Move4() { }
    public virtual void Move5() { }

    public void HitBy(CardBase card, out int deltaHP, out int deltaSP) 
    {
        deltaHP = card.CardHP;
        deltaSP = card.CardSP;
    }

    public virtual void ChangeState(CardBase card) 
    {
        HitBy(card, out int deltaHP, out int deltaSP);
        Enemy.IncHP(deltaHP); 
        Enemy.IncSP(deltaSP);
    }
}
