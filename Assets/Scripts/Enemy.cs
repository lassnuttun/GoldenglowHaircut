using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using System;
using TMPro;

public class Enemy : MonoBehaviour
{
    // private SkinnedMeshRenderer _meshRenderer;
    private SkeletonAnimation _skeletonAnimation;

    public uint maxHP;
    public uint curHP;
    public GameObject CdBarObj;

    void Start()
    {
        gameObject.transform.localScale = new Vector3(-0.8f, 0.8f);
        _skeletonAnimation = transform.GetComponent<SkeletonAnimation>();
        _skeletonAnimation.AnimationState.SetAnimation(0, AnimationType.Start.ToString(), false);
        _skeletonAnimation.AnimationState.AddAnimation(0, AnimationType.Idle.ToString(), true, 0);

        CdBarObj = UIManager.Instance.GenerateCdBar();
        CdBarObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 4.5f, 0));
        CdBarObj.name = "cdBar_" + gameObject.name;
        
        Image hpFill = CdBarObj.transform.Find("hpBar").Find("fill").GetComponent<Image>();
        hpFill.fillAmount = (float)curHP / (float)maxHP;
        TextMeshProUGUI hpText = CdBarObj.transform.Find("hpText").GetComponent<TextMeshProUGUI>();
        hpText.text = curHP.ToString() + "/" + maxHP.ToString();
    }

    void Update()
    {
    }
}
