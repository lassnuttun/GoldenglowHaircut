﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string cardID;
    public string cardName;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private int index;
    private Vector3 eulerAngle;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.3f * FightUI.cardScale, 0.25f);

        eulerAngle = transform.localEulerAngles;
        transform.localEulerAngles = Vector3.zero;
        // transform.DOLocalRotate(Vector3.zero, 0.1f);

        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(FightUI.cardScale, 0.25f);

        // transform.DOLocalRotate(eulerRotate, 0.1f);
        transform.localEulerAngles = eulerAngle;

        transform.SetSiblingIndex(index);
    }
}
