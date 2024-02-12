using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Spine;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI CardNameText;
    public TextMeshProUGUI CardDescriptionText;
    public TextMeshProUGUI CardCostText;
    public Image CardImage;

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
        transform.DOScale(1.3f * FightUI.CardScale, FightUI.CardInterval);
        eulerAngle = transform.localEulerAngles;
        transform.localEulerAngles = Vector3.zero;
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(FightUI.CardScale, FightUI.CardInterval);
        transform.localEulerAngles = eulerAngle;
        transform.SetSiblingIndex(index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.Instance.ShowUI<ArrowDisplay>("Arrow");
        ArrowDisplay arrowDisplay = UIManager.Instance.GetUI<ArrowDisplay>("Arrow");
        arrowDisplay.SetStartPos(transform.position);
        // Cursor.visible = false;
        StopAllCoroutines();
        StartCoroutine(OnMouseRightDown(eventData));
    }

    private IEnumerator OnMouseRightDown(PointerEventData eventData)
    {
        while (true)
        {
            if (Input.GetMouseButton(1))
            {
                break;
            }

            Vector3 pos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GameObject.Find("Canvas").GetComponent<RectTransform>(), eventData.position, null, out pos))
            {
                UIManager.Instance.GetUI<ArrowDisplay>("Arrow").SetEndPos(pos);
                RayCheck();
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
            Debug.Log(hit.transform.name);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
