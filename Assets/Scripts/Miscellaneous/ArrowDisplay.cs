using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDisplay : UIBase
{
    public void SetStartPos(Vector3 pos)
    {
        transform.GetChild(transform.childCount - 1).position = pos;
    }

    public void SetEndPos(Vector3 pos) 
    {
        transform.GetChild(0).position = pos;

        Vector2 startPos = transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition;
        Vector2 endPos = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        Vector2 midPos1 = (endPos - startPos) * new Vector2(-0.3f, 0.8f) + startPos;
        Vector2 midPos2 = (endPos - startPos) * new Vector2(0.1f, 1.4f) + startPos;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();
            float t = Mathf.Log(1 + (float) i / (float) (transform.childCount - 1), 2);
            child.anchoredPosition = t * t * t * startPos + t * t * (1 - t) * midPos1 + t * (1 - t) * (1 - t) * midPos2 + (1 - t) * (1 - t) * (1 - t) * endPos;
            if (i != 0)
            {
                RectTransform prev = transform.GetChild(i - 1).GetComponent<RectTransform>();
                Vector2 delta = prev.anchoredPosition - child.anchoredPosition;
                float angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg;
                prev.eulerAngles = new Vector3(0.0f, 0.0f, -angle);
            }
            else
            {
                child.eulerAngles = transform.GetChild(i + 1).GetComponent<RectTransform>().eulerAngles;
            }
        }
    }
}
