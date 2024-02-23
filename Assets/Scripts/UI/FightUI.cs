using DG.Tweening;
using Newtonsoft.Json;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : UIBase
{
    public static readonly float CardScale = 0.12f;
    public static readonly float CardInterval = 0.25f;

    private Vector3 DeckPilePos;
    private Vector3 DiscardPilePos;

    private PlayerDisplay Player;

    void Awake()
    {
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
        transform.Find("deckPile").GetComponent<Button>().onClick.AddListener
        (
            () => 
            {
                transform.Find("deckPile").GetComponent<DeckPileDisplay>().ShowDeckPile();
            }
        ) ;
        DeckPilePos = transform.Find("deckPile").GetComponent<RectTransform>().position;
        DiscardPilePos = transform.Find("discardPile").GetComponent<RectTransform>().position;
    }

    void Start()
    {
        // 需要替换成 UIManager 的 api
        Object resource = AssetBundleManager.LoadResource<Object>("Goldenglow", "skeleton");
        Transform canvas = UIManager.Instance.CanvasTransTool;
        GameObject playerModel = Instantiate(resource, canvas) as GameObject;
        Player = playerModel.AddComponent<PlayerDisplay>();
        Player.SkelGrap = playerModel.GetComponent<SkeletonGraphic>();

        List<EnemyBase> Enemies = FightManager.Instance.EnemyList;
        List<Vector2> enemyPos = new List<Vector2>();
        // case 2 的位置需要修改
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
        for (int i = 0; i < Enemies.Count; i++)
        {
            resource = AssetBundleManager.LoadResource<Object>(Enemies[i].EnemyID, "skeleton");
            GameObject enemyModel = Instantiate(resource, canvas) as GameObject;
            enemyModel.GetComponent<RectTransform>().anchoredPosition = enemyPos[i];
            Enemies[i].BindDisplayComponent(enemyModel);
        }
    }

    void Update() { }

    private List<float> CardRotation(int count)
    {
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

        return rotations;
    }

    private Vector3 CardPosition(float rotation)
    {
        float r = 900;
        float y = -800;
        return new Vector3(Mathf.Sin(rotation * Mathf.Deg2Rad) * r, Mathf.Cos(rotation * Mathf.Deg2Rad) * r + y);
    }

    public void AddCard()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        List<CardBase> HandPile = FightManager.Instance.CardPiles[1];
        int count = HandPile.Count;

        List<float> rotations = CardRotation(count);

        Object resource = AssetBundleManager.LoadResource<Object>(HandPile[count - 1].CardID, "card");
        GameObject cardObj = Instantiate(resource, canvas) as GameObject;
        HandPile[count - 1].BindDisplayComponent(cardObj);
        RectTransform rectTransform = cardObj.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(0, 0, 0);
        rectTransform.position = DeckPilePos;

        for (int i = count - 1; i >= 0; i--)
        {
            rectTransform = HandPile[i].Display.GetComponent<RectTransform>();
            rectTransform.DOAnchorPos(CardPosition(rotations[i]), CardInterval);
            rectTransform.DOScale(CardScale, CardInterval);
            rectTransform.DOLocalRotate(new Vector3(0, 0, -rotations[i]), CardInterval);
        }
    }

    public void RemoveCard(CardDisplay card)
    {
        card.enabled = false;
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.DOMove(DiscardPilePos, CardInterval);
        rectTransform.DOScale(0, CardInterval).OnComplete(() => { Destroy(card.gameObject, 1); });

        List<CardBase> HandPile = FightManager.Instance.CardPiles[1];
        int count = HandPile.Count;
        List<float> rotations = CardRotation(count);

        for (int i = 0; i < count; i++)
        {
            rectTransform = HandPile[i].Display.GetComponent<RectTransform>();
            rectTransform.DOAnchorPos(CardPosition(rotations[i]), CardInterval);
            rectTransform.DOLocalRotate(new Vector3(0, 0, -rotations[i]), CardInterval);
        }
    }
}
