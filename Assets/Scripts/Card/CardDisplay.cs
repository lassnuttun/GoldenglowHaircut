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
    public bool InHand;

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

    private int index;
    private Vector3 eulerAngle;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InHand)
        {
            transform.DOScale(1.3f * FightUI.CardScale, FightUI.CardInterval);
            eulerAngle = transform.localEulerAngles;
            transform.localEulerAngles = Vector3.zero;
            index = transform.GetSiblingIndex();
            transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InHand)
        {
            transform.DOScale(FightUI.CardScale, FightUI.CardInterval);
            transform.localEulerAngles = eulerAngle;
            transform.SetSiblingIndex(index);
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

    public virtual IEnumerator OnMouseRightDown(PointerEventData eventData)
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
            if (hit.collider)
            {
                // 使用的逻辑之后需要移到卡牌逻辑类中
                GameObject gameObj = hit.collider.gameObject;
                EnemyDisplay enemyDisplay = gameObj.GetComponent<EnemyDisplay>();
                if (Input.GetMouseButtonDown(0))
                {
                    CardBase Card = Get();
                    if (enemyDisplay == null || FightManager.Instance.UsableCheckForCard(Card) == false)
                    {
                        break;
                    }
                    StopAllCoroutines();
                    UIManager.Instance.CloseUI("Arrow");
                    EnemyBase enemy = enemyDisplay.Get();
                    enemy.ChangeState(Card);
                    FightManager.Instance.RemoveCard(Card, true);
                    if (enemy.EnemyHP.ReachMax())
                    {
                        enemyDisplay.CutComplete();
                    }
                    else if (enemy.EnemySP.ReachMax())
                    {

                    }
                }
            }
            yield return null;
        }
        // Cursor.visible = true;
        UIManager.Instance.CloseUI("Arrow");
        yield break;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
