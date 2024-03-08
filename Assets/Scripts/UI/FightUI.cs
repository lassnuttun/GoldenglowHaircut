using DG.Tweening;
using Spine.Unity;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FightUI : UIBase
{
    public static readonly float CardScale = 0.12f;
    public static readonly float CardInterval = 0.25f;

    public Vector3 DeckPilePos;
    public Vector3 DiscardPilePos;

    public Transform Canvas;
    public Transform EnvSlots;
    public PlayerDisplay Player;
    public PowerDisplay Power;

    public static readonly float EnemyPosY = -110.0f;
    public static readonly List<List<Vector2>> EnemyPosLists = new List<List<Vector2>>
    {
        new List<Vector2> { },
        new List<Vector2> { new Vector2(-385.0f, EnemyPosY) },
        new List<Vector2> { new Vector2(-481.5f, EnemyPosY), new Vector2(-288.5f, EnemyPosY) },
        new List<Vector2> { new Vector2(-578.0f, EnemyPosY), new Vector2(-385.0f, EnemyPosY), new Vector2(-192.0f, EnemyPosY) }
    };
    // 不可以直接通过 CardRotateLists 访问数据，要通过 CardRotation()
    public static List<List<float>> CardRotateLists = new List<List<float>>
    {
        new List<float> { },
        new List<float> { 0 },
        new List<float> { 5, -5 },
        new List<float> { 10, 0, -10 },
        new List<float> { 15, 5, -5, -15 },
        new List<float> { 20, 10, 0, -10, -20 },
        new List<float> { 25, 15, 5, -5, -15, -25 }
    };
    public static readonly float SlotPosY = 0;
    public static readonly float SlotPosX = 131;
    public static readonly List<List<Vector2>> SlotPosLists = new List<List<Vector2>>
    {
        new List<Vector2> { },
        new List<Vector2> { new Vector2(SlotPosX *  0, SlotPosY) },
        new List<Vector2> { new Vector2(SlotPosX * -1, SlotPosY), new Vector2(SlotPosX * 1, SlotPosY) },
        new List<Vector2> { new Vector2(SlotPosX * -2, SlotPosY), new Vector2(SlotPosX * 0, SlotPosY), new Vector2(SlotPosX * 2, SlotPosY) },
        new List<Vector2> { new Vector2(SlotPosX * -3, SlotPosY), new Vector2(SlotPosX * -1, SlotPosY), new Vector2(SlotPosX * 1, SlotPosY), new Vector2(SlotPosX * 3, SlotPosY) }
    };

    void Awake()
    {
        DeckPilePos = transform.Find("deckPile").GetComponent<RectTransform>().position;
        DiscardPilePos = transform.Find("discardPile").GetComponent<RectTransform>().position;
        Canvas = UIManager.Instance.LCanvasTransTool;
        EnvSlots = transform.Find("envSlots");
        Power = transform.Find("power").GetComponent<PowerDisplay>();
    }

    void Start()
    {
        Object resource = AssetBundleManager.LoadResource<Object>("Goldenglow", "skeleton");
        GameObject playerModel = Instantiate(resource, Canvas) as GameObject;
        Player = playerModel.AddComponent<PlayerDisplay>();
        Player.SkelGrap = playerModel.GetComponent<SkeletonGraphic>();

        List<EnemyBase> Enemies = FightManager.Instance.EnemyList;
        int count = Enemies.Count;
        for (int i = 0; i < count; i++)
        {
            resource = AssetBundleManager.LoadResource<Object>(Enemies[i].EnemyID, "skeleton");
            GameObject enemyModel = Instantiate(resource, Canvas) as GameObject;
            enemyModel.GetComponent<RectTransform>().anchoredPosition = EnemyPosLists[count][i];
            Enemies[i].BindDisplayComponent(enemyModel);
        }
    }

    private List<float> CardRotation(int count)
    {
        for (int i = CardRotateLists.Count; i <= count; i++)
        {
            List<float> rotations = new List<float>();
            float totalAngle = 50;
            for (int j = 0; j < count; j++)
            {
                rotations.Add(totalAngle / 2 - totalAngle / (count - 1) * j);
            }
            CardRotateLists.Add(rotations);
        }
        return CardRotateLists[count];
    }

    private Vector3 CardPosition(float rotation)
    {
        float r = 900;
        float y = -800;
        return new Vector3(Mathf.Sin(rotation * Mathf.Deg2Rad) * r, Mathf.Cos(rotation * Mathf.Deg2Rad) * r + y);
    }

    public void UpdateCardPos()
    {
        List<CardBase> HandPile = FightManager.Instance.CardPiles[1];
        int count = HandPile.Count;
        List<float> rotations = CardRotation(count);
        for (int i = count - 1; i >= 0; i--)
        {
            RectTransform rectTransform = HandPile[i].Get().GetComponent<RectTransform>();
            rectTransform.DOAnchorPos(CardPosition(rotations[i]), CardInterval);
            rectTransform.DOScale(CardScale, CardInterval);
            rectTransform.DOLocalRotate(new Vector3(0, 0, -rotations[i]), CardInterval);
        }
    }

    public void UpdateEnvPos(TweenCallback tweenCallback = null)
    {
        List<EnvironmentBase> EnvList = FightManager.Instance.EnvList;
        int count = EnvList.Count;
        for (int i = 0; i < count; i++)
        {
            RectTransform rectTransform = EnvList[i].Get().GetComponent<RectTransform>();
            if (i == count - 1 && tweenCallback != null)
            {
                rectTransform.DOAnchorPos(SlotPosLists[count][i], CardInterval).OnComplete(tweenCallback);
            }
            else
            {
                rectTransform.DOAnchorPos(SlotPosLists[count][i], CardInterval);
            }
        }
    }

    public void UpdatePower()
    {
        Power.UpdateDisplayInfo(FightManager.Instance.Power);
    }

    public void BtnOnClickEndTurn()
    {
        if (FightManager.Instance.CurState is FightPlayerTurn)
        {
            FightManager.Instance.MoveOn(FightUnitType.EnemyTurn);
        }
    }

    public void BtnOnClickDeckPile()
    {
        PileExamineUI PEUI = UIManager.Instance.ShowUI<PileExamineUI>("PileExamineUI", true) as PileExamineUI;
        PEUI.ShowPile(true);
    }

    public void BtnOnClickDiscPile()
    {
        PileExamineUI PEUI = UIManager.Instance.ShowUI<PileExamineUI>("PileExamineUI", true) as PileExamineUI;
        PEUI.ShowPile(false);
    }
}
