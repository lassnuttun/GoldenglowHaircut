using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;

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
    public void OnPointerEnter(PointerEventData eventData)
    {
        // transform.DOScale(1.5f, 0.25f);
        GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
    }
}
