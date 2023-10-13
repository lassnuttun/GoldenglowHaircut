using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DemoStart : MonoBehaviour
{
    // 所有 Manager 的初始化都需要修改
    void Start()
    {
        PlayerInfoManager.Instance.InitPlayerInfo();
        // 进入战斗的 Init 阶段
        FightManager.Instance.MoveOn(FightUnitType.Init);
    }
}
