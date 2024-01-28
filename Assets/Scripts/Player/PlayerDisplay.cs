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
    public SkeletonGraphic SkelGrap;

    void Start()
    {
        ShowPlayer();
    }

    public void ShowPlayer()
    {
        gameObject.name = "playerModel";
        SkelGrap.AnimationState.SetAnimation(0, AnimationType.Start.ToString(), false);
        SkelGrap.AnimationState.AddAnimation(0, AnimationType.Idle.ToString(), true, 0);
    }
}
