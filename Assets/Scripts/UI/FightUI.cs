using DG.Tweening;
using Newtonsoft.Json;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : UIBase
{
    public static readonly float CardScale = 0.12f;

    private Vector3 DiscardPilePos;

    private List<CardDisplay> HandPile;

    private PlayerDisplay Player;

    private List<EnemyDisplay> EnemyList;

    void Awake()
    {
        // AddListener 的语法还要研究一下
        transform.Find("endTurn").GetComponent<Button>().onClick.AddListener
        (
            () =>
            {
                if (FightManager.Instance.CurState is FightPlayerTurn)
                {
                    FightManager.Instance.MoveOn(FightUnitType.EnemyTurn);
                }
            }
        );
        DiscardPilePos = transform.Find("discardPile").GetComponent<RectTransform>().position;
        HandPile = new List<CardDisplay>();
        EnemyList = new List<EnemyDisplay>();
    }

    void Start()
    {
        Object resource = AssetBundleManager.LoadResource<Object>("Goldenglow", "skeleton");
        Transform canvas = UIManager.Instance.CanvasTransTool;
        GameObject playerModel = Instantiate(resource, canvas) as GameObject;
        Player = playerModel.AddComponent<PlayerDisplay>();
        Player.SkelGrap = playerModel.GetComponent<SkeletonGraphic>();

        List<EnemyBase> Enemies = FightManager.Instance.EnemyList;
        List<Vector2> enemyPos = new List<Vector2>();
        // case 1, case 2 的位置需要修改
        switch (Enemies.Count)
        {
            case 1:
                enemyPos.Add(new Vector2(-385.0f, -90.0f));
                break;
            case 2:
                break;
            case 3:
                enemyPos.Add(new Vector2(-578.0f, -90.0f));
                enemyPos.Add(new Vector2(-385.0f, -90.0f));
                enemyPos.Add(new Vector2(-192.0f, -90.0f));
                break;
        }

        Object CdBarRes = AssetBundleManager.LoadResource<Object>("cdBar", "ui");
        for (int i = 0; i < Enemies.Count; i++)
        {
            resource = AssetBundleManager.LoadResource<Object>(Enemies[i].EnemyID, "skeleton");
            GameObject enemyModel = Instantiate(resource, canvas) as GameObject;
            enemyModel.GetComponent<RectTransform>().anchoredPosition = enemyPos[i];
            EnemyDisplay enemy;
            if (Enemies[i] is Mlynar)
            {
                enemy = enemyModel.AddComponent<MlynarDisplay>();
            }
            else
            {
                enemy = enemyModel.AddComponent<EnemyDisplay>();
            }
            enemy.Enemy = Enemies[i];
            enemy.SkelGrap = enemyModel.GetComponent<SkeletonGraphic>();
            GameObject cdBar = Instantiate(CdBarRes, enemyModel.transform) as GameObject;
            enemy.CdBarObj = cdBar;
            enemy.EnemyNameText = cdBar.transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();
            EnemyList.Add(enemy);
        }
    }

    void Update()
    {
    }

    public void UpdateHandPilePos()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        int count = FightManager.Instance.CardPiles[1].Count;

        // 卡牌的位置、角度排布还需要进一步修改
        float r = 900;
        float y = -800;
        int countLimit = 6;
        float angleSpace = 10;

        List<float> rotations = new List<float>();

        if (count < countLimit)
        {
            if (count % 2 == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    rotations.Add(-angleSpace / 2 + angleSpace * (count / 2 - i));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    rotations.Add(angleSpace * (count / 2 - i));
                }
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                float totalAngle = angleSpace * (countLimit - 1);
                rotations.Add(totalAngle / 2 - totalAngle / (count - 1) * i);
            }
        }

        for (int i = count - 1; i >= 0; i--)
        {
            Object resource = AssetBundleManager.LoadResource<Object>(FightManager.Instance.CardPiles[1][i].CardID, "card");
            GameObject cardObj = Instantiate(resource, canvas) as GameObject;
            cardObj.GetComponent<RectTransform>().localScale = new Vector3(CardScale, CardScale, 1);
            cardObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Sin(rotations[i] * Mathf.Deg2Rad) * r, Mathf.Cos(rotations[i] * Mathf.Deg2Rad) * r + y);
            cardObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -rotations[i]));
            cardObj.GetComponent<CardDisplay>().Card = FightManager.Instance.CardPiles[1][i];
            HandPile.Add(cardObj.GetComponent<CardDisplay>());
        }
    }

    public void MoveFromHandToDiscard(CardDisplay card)
    {
        card.enabled = false;
        FightManager.Instance.CardPiles[2].Add(card.Card);
        FightManager.Instance.CardPiles[1].Remove(card.Card);
        UpdateHandPilePos();
        card.transform.DOMove(DiscardPilePos, 1.0f);
        card.transform.DOScale(0, 0.25f);
        Destroy(card.gameObject, 1);
    }

    public void MoveAllFromHandToDiscard()
    {
        foreach (var card in HandPile)
        {
            card.enabled = false;
            FightManager.Instance.CardPiles[2].Add(card.Card);
            FightManager.Instance.CardPiles[1].Remove(card.Card);
            card.transform.DOMove(DiscardPilePos, 0.25f);
            card.transform.DOScale(0, 0.25f);
            Destroy(card.gameObject, 1);
        }
        HandPile.Clear();
        UpdateHandPilePos();
    }

    public IEnumerator EnemyActionDisplay()
    {
        foreach (var enemy in EnemyList)
        {
            Debug.Log(enemy.Enemy.EnemyName);
            enemy.Move1();
            yield return new WaitForSeconds(3.0f);
        }
        FightManager.Instance.MoveOn(FightUnitType.PlayerTurn);
        yield break;
    }
}
