using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public UIEventTrigger Register(string name)
    {
        Transform trans_tool = transform.Find(name);
        return UIEventTrigger.Get(trans_tool.gameObject);
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Close()
    {
        UIManager.Instance.CloseUI(gameObject.name);
    }
}
