using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

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
        SkelGrap.AnimationState.SetAnimation(0, "Start", false);
        SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
}
