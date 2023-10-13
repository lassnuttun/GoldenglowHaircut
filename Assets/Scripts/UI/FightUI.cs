using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : UIBase
{
    public static readonly float cardScale = 0.12f;

    private Vector2 discardPilePos;

    private List<Card> handPile;

    void Awake()
    {
        transform.Find("endTurn").GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("end");
                if (FightManager.Instance.fightUnit is FightPlayerTurn)
                {
                    FightManager.Instance.MoveOn(FightUnitType.EnemyTurn);
                }
            }
        );
        discardPilePos = transform.Find("discardPile").GetComponent<RectTransform>().localPosition;
        handPile = new List<Card>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void UpdateHandPilePos()
    {
        Transform canvasTrans = GameObject.Find("Canvas").transform;
        int count = FightCardManager.Instance.handPile.Count;

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
            Object resource = AssetBundleManager.LoadResource<Object>(FightCardManager.Instance.handPile[i], "card");
            GameObject cardObj = GameObject.Instantiate(resource, canvasTrans) as GameObject;
            cardObj.GetComponent<RectTransform>().localScale = new Vector3(cardScale, cardScale, 1);
            cardObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Sin(rotations[i] * Mathf.Deg2Rad) * r, Mathf.Cos(rotations[i] * Mathf.Deg2Rad) * r + y);
            cardObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, -rotations[i]));
            handPile.Add(cardObj.GetComponent<Card>());
        }
    }

    public void MoveFromHandToDiscard(Card card)
    {
        card.enabled = false;
        FightCardManager.Instance.discardPile.Add(card.cardID);
        FightCardManager.Instance.handPile.Remove(card.cardID);
        UpdateHandPilePos();
        card.GetComponent<RectTransform>().DOAnchorPos(discardPilePos, 0.25f);
        card.transform.DOScale(0, 0.25f);
        Destroy(card.gameObject, 1);
    }

    public void MoveAllFromHandToDiscard()
    {
        for ( int i = handPile.Count - 1; i >= 0; i--)
        {
            MoveFromHandToDiscard(handPile[i]);
        }
    }
}
