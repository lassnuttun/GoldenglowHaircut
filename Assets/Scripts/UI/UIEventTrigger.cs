using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventTrigger : MonoBehaviour, IPointerClickHandler
{
    // 委托
    public Action<GameObject, PointerEventData> on_click;

    // 获得 obj 上的 UIEventTrigger 脚本，如果没有就新建一个
    public static UIEventTrigger Get(GameObject obj)
    {
        UIEventTrigger trig = obj.GetComponent<UIEventTrigger>();
        if (trig == null)
        {
            trig = obj.AddComponent<UIEventTrigger>();
        }
        return trig;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (on_click != null)
        {
            on_click.Invoke(gameObject, eventData);
        }
    }
}
