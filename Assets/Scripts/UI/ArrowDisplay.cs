﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDisplay : UIBase
{
    private static Vector3 BaseScale = new Vector3(0.7f, 0.7f, 0);
    private static readonly float Factor = 0.9f;
    private static readonly float BaseRotation = 270;

    public void SetStartPos(Vector3 pos)
    {
        transform.GetChild(transform.childCount - 1).position = pos;
    }

    public void SetEndPos(Vector3 pos) 
    {
        transform.GetChild(0).position = new Vector3(pos.x, pos.y, 0);

        Vector2 startPos = transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition;
        Vector2 endPos = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        Vector2 midPos1 = (endPos - startPos) * new Vector2(-0.3f, 0.8f) + startPos;
        Vector2 midPos2 = (endPos - startPos) * new Vector2(0.1f, 1.4f) + startPos;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();
            float t = Mathf.Log(1 + (float) i / (float) (transform.childCount - 1), 2);
            child.anchoredPosition = t * t * t * startPos + t * t * (1 - t) * midPos1 + t * (1 - t) * (1 - t) * midPos2 + (1 - t) * (1 - t) * (1 - t) * endPos;
            child.localScale = BaseScale * Mathf.Pow(Factor, i);
            if (i != 0)
            {
                RectTransform prev = transform.GetChild(i - 1).GetComponent<RectTransform>();
                Vector2 delta = prev.anchoredPosition - child.anchoredPosition;
                float angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg;
                prev.eulerAngles = new Vector3(0.0f, 0.0f, -angle + BaseRotation);
            }
            else
            {
                child.eulerAngles = transform.GetChild(i + 1).GetComponent<RectTransform>().eulerAngles;
            }
        }
    }
}
