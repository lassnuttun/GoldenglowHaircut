using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mlynar : EnemyBase
{
    public MlynarDisplay Display { get; private set; }
    public int ThornVal { get; private set; }
    public int ThornInc { get; private set; }

    public Mlynar(string enemyID, string enemyName, string enemyDescription, int enemyMaxHP, int enemyMaxSP) : base(enemyID, enemyName, enemyDescription, enemyMaxHP, enemyMaxSP) 
    {
        ThornVal = 1;
        ThornInc = 3;
    }

    public override void ChangeState(CardBase card)
    {
        base.ChangeState(card);
        Display.UpdateDisplayInfo(EnemyHP, EnemySP);
    }

    public override void BindDisplayComponent(GameObject enemyModel)
    {
        Display = enemyModel.AddComponent<MlynarDisplay>();
        Display.SkelGrap = enemyModel.GetComponent<SkeletonGraphic>();
        Object CdBarRes = AssetBundleManager.LoadResource<Object>("cdBar", "ui");
        GameObject cdBar = GameObject.Instantiate(CdBarRes, enemyModel.transform) as GameObject;
        Display.CdBarObj = cdBar;
        Display.EnemyNameText = cdBar.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();

        Display.EnemyNameText.text = EnemyName;
        Display.SkelGrap.AnimationState.SetAnimation(0, "Start", false);
        Display.SkelGrap.AnimationState.AddAnimation(0, "Idle", true, 0);

        Display.UpdateDisplayInfo(EnemyHP, EnemySP);
    }

    public override void Move1()
    {
        ThornVal += ThornInc;
        EnemySP.Inc(10);
        Display.Move1();
        Display.UpdateDisplayInfo(EnemyHP, EnemySP);
    }

    public override void Move2()
    {
        throw new System.NotImplementedException();
    }

    public override void Move3()
    {
        throw new System.NotImplementedException();
    }

    public override void Move4()
    {
        throw new System.NotImplementedException();
    }

    public override void Move5()
    {
        throw new System.NotImplementedException();
    }
}
