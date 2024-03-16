using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IProperty<CardBase>
{
    public TextMeshProUGUI CardNameText;
    public TextMeshProUGUI CardDescriptionText;
    public TextMeshProUGUI CardCostText;
    public Image CardImage;
    private bool InHand;

    public virtual CardBase Get()
    {
        return null;
    }

    public virtual void Set(CardBase obj)
    {
    }

    private bool Check()
    {
        return FightManager.Instance.CurState is FightPlayerTurn;
    }

    public virtual void UpdateDisplayInfo()
    {
        CardBase card = Get();
        CardNameText.text = card.CardName;
        CardCostText.text = card.CardCost.ToString();
    }

    private int index;
    private Vector3 eulerAngle;
    private Vector2 anchoredPosition;
    // 需要在抽取手牌动画播放时禁用，尚未实现
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InHand)
        {
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.DOScale(1.3f * FightUI.CardScale, FightUI.CardInterval);

            eulerAngle = transform.localEulerAngles;
            rectTransform.DORotate(Vector3.zero, FightUI.CardInterval);

            anchoredPosition = rectTransform.anchoredPosition;
            rectTransform.DOAnchorPosY(145, FightUI.CardInterval);

            index = transform.GetSiblingIndex();
            rectTransform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InHand)
        {
            RectTransform rectTransform = transform.GetComponent<RectTransform>();
            rectTransform.DOScale(FightUI.CardScale, FightUI.CardInterval);
            rectTransform.DORotate(eulerAngle, FightUI.CardInterval);
            rectTransform.DOAnchorPos(anchoredPosition, FightUI.CardInterval);
            rectTransform.SetSiblingIndex(index);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Check() && InHand)
        {
            UIManager.Instance.ShowUI<ArrowDisplay>("Arrow");
            ArrowDisplay arrowDisplay = UIManager.Instance.GetUI<ArrowDisplay>("Arrow");
            arrowDisplay.SetStartPos(transform.position);
            // Cursor.visible = false;
            StopAllCoroutines();
            arrowDisplay.StartCoroutine(OnMouseRightDown(eventData));
        }
    }

    public IEnumerator OnMouseRightDown(PointerEventData eventData)
    {
        while (true)
        {
            if (Input.GetMouseButton(1))
            {
                break;
            }

            Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position);
            UIManager.Instance.GetUI<ArrowDisplay>("Arrow").SetEndPos(pos);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider && Input.GetMouseButtonDown(0))
            {
                CardBase Card = Get();
                if (TargetExist(hit, out EnemyBase enemy) == false || FightManager.Instance.UsableCheckForCard(Card) == false)
                {
                    break;
                }
                StopAllCoroutines();
                UIManager.Instance.CloseUI("Arrow");
                Card.Use(enemy);
            }
            yield return null;
        }
        UIManager.Instance.CloseUI("Arrow");
        yield break;
    }

    public virtual bool TargetExist(RaycastHit2D hit, out EnemyBase enemy)
    {
        EnemyDisplay enemyDisplay = hit.collider.gameObject.GetComponent<EnemyDisplay>();
        if (enemyDisplay == null)
        {
            enemy = null;
            return false;
        }
        enemy = enemyDisplay.Get();
        return true;
    }

    public void OnPointerUp(PointerEventData eventData) { }

    public void MoveFromDeckToHand()
    {
        InHand = true;
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector3(0, 0, 0);
        rectTransform.position = ui.DeckPilePos;

        ui.UpdateCardPos();
    }

    public void MoveFromHandToDiscard()
    {
        InHand = false;
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.DOMove(ui.DiscardPilePos, FightUI.CardInterval);
        rectTransform.DOScale(0, FightUI.CardInterval).OnComplete(() => { Destroy(gameObject, 1); });

        ui.UpdateCardPos();
    }

    public void MoveFromHandToSlot()
    {
        InHand = false;
        FightUI ui = UIManager.Instance.GetUI<FightUI>("FightUI");

        List<EnvironmentBase> list = FightManager.Instance.EnvList;
        int count = list.Count;
        RectTransform rectTransform = list[count - 1].Get().GetComponent<RectTransform>();
        transform.DOMove(rectTransform.position, FightUI.CardInterval);
        transform.DOScale(0, FightUI.CardInterval).OnComplete(() => { Destroy(gameObject, 1); });
        FightManager.Instance.CardPiles[1].Remove(Get());
        ui.UpdateCardPos();
    }
}
