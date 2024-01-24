using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardBase Card;
    public TextMeshProUGUI CardNameText;
    public TextMeshProUGUI CardDescriptionText;
    public TextMeshProUGUI CardCostText;
    public Image CardImage;

    void Start()
    {
        ShowCard();
    }

    void Update()
    {

    }

    public void ShowCard()
    {
        // Debug.Log(Card.CardName);
        CardNameText.text = Card.CardName;
        CardDescriptionText.text = Card.CardDescription;
        CardCostText.text = Card.CardCost.ToString();
    }

    
    private Vector3 originalPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
        transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPosition;
    }


    private int index;
    private Vector3 eulerAngle;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.3f * FightUI.CardScale, 0.25f);

        eulerAngle = transform.localEulerAngles;
        transform.localEulerAngles = Vector3.zero;
        // transform.DOLocalRotate(Vector3.zero, 0.1f);

        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(FightUI.CardScale, 0.25f);

        // transform.DOLocalRotate(eulerRotate, 0.1f);
        transform.localEulerAngles = eulerAngle;

        transform.SetSiblingIndex(index);
    }
}
