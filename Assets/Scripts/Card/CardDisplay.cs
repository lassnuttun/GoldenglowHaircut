using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public CardBase Card;
    public TextMeshProUGUI CardNameText;
    public TextMeshProUGUI CardDescriptionText;
    public TextMeshProUGUI CardCostText;
    public Image CardImage;
    public bool InHand;

    void Start() { }

    void Update() { }

    private bool Check()
    {
        return FightManager.Instance.CurState is FightPlayerTurn;
    }
    
    //private Vector3 originalPosition;
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if (Check())
    //    {
    //        originalPosition = transform.position;
    //        transform.position = eventData.position;
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (Check())
    //    {
    //        transform.position = eventData.position;
    //    }
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    if (Check())
    //    {
    //        transform.position = originalPosition;
    //    }
    //}

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

    private IEnumerator OnMouseRightDown(PointerEventData eventData)
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
                if (Card is EnvCreateCard)
                {
                    var envCard = Card as EnvCreateCard;
                    PlayerDisplay playerDisplay = gameObj.GetComponent<PlayerDisplay>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (playerDisplay == null || FightManager.Instance.UsableCheckForCard(envCard) == false)
                        {
                            break;
                        }
                        StopAllCoroutines();
                        UIManager.Instance.CloseUI("Arrow");
                        // 需要细化插入环境时的机制，如果已经存在相同的环境，应该如何处理？
                        if (envCard is HypnoticCenser)
                        {
                            FightManager.Instance.AddEnv((envCard as HypnoticCenser).Environment);
                        }
                        else
                        {
                            FightManager.Instance.AddEnv(envCard.Environment);
                        }
                        // 需要细化实现环境卡进入弃牌堆的机制
                        FightManager.Instance.RemoveCard(envCard, true);
                    }
                }
                else
                {
                    EnemyDisplay enemyDisplay = gameObj.GetComponent<EnemyDisplay>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (enemyDisplay == null || FightManager.Instance.UsableCheckForCard(Card) == false)
                        {
                            break;
                        }
                        StopAllCoroutines();
                        UIManager.Instance.CloseUI("Arrow");
                        EnemyBase enemy = enemyDisplay.Enemy;
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
            }
            yield return null;
        }
        // Cursor.visible = true;
        UIManager.Instance.CloseUI("Arrow");
    }

    public void OnPointerUp(PointerEventData eventData) { }
}
