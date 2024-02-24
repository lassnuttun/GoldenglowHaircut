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
                if (Input.GetMouseButtonDown(0))
                {
                    StopAllCoroutines();
                    UIManager.Instance.CloseUI("Arrow");
                    var Enemy = hit.collider.gameObject.GetComponent<EnemyDisplay>().Enemy;
                    Enemy.ChangeState(Card);
                    FightManager.Instance.RemoveCard(FightManager.Instance.CardPiles[1].IndexOf(Card));
                }
            }
            yield return null;
        }
        // Cursor.visible = true;
        UIManager.Instance.CloseUI("Arrow");
    }

    private void RayCheck()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(GameObject.Find("Canvas").GetComponent<RectTransform>(), Input.mousePosition, null, out Vector3 pos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();
                UIManager.Instance.CloseUI("Arrow");
                var Enemy = hit.collider.gameObject.GetComponent<EnemyDisplay>().Enemy;
                Enemy.ChangeState(Card);
                FightManager.Instance.RemoveCard(FightManager.Instance.CardPiles[1].IndexOf(Card));
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData) { }
}
