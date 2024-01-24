using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum AnimationType
{
    None,
    Attack,
    Idle,
    Die,
    Start
}

public class PlayerDisplay : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;

    void Start()
    {
        ShowPlayer();
    }

    public void ShowPlayer()
    {
        gameObject.name = "playerModel";
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f);
        gameObject.transform.position = new Vector3(-5, -1.5f);
        _skeletonAnimation = transform.GetComponent<SkeletonAnimation>();
        _skeletonAnimation.AnimationState.SetAnimation(0, AnimationType.Start.ToString(), false);
        _skeletonAnimation.AnimationState.AddAnimation(0, AnimationType.Idle.ToString(), true, 0);
    }
}
