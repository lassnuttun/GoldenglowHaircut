using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnvCreateCardDisplay<TEnv> : CardDisplay where TEnv : EnvironmentBase
{
    public EnvCreateCard<TEnv> Card;

    public override CardBase Get()
    {
        return Card;
    }

    public override void Set(CardBase obj)
    {
        if (obj is EnvCreateCard<TEnv>)
        {
            Card = obj as EnvCreateCard<TEnv>;
        }
    }

    // 需要和父类的函数重新组合一下
    public override IEnumerator OnMouseRightDown(PointerEventData eventData)
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
                PlayerDisplay playerDisplay = hit.collider.gameObject.GetComponent<PlayerDisplay>();
                if (Input.GetMouseButtonDown(0))
                {
                    if (playerDisplay == null || FightManager.Instance.UsableCheckForCard(Card) == false)
                    {
                        break;
                    }
                    StopAllCoroutines();
                    UIManager.Instance.CloseUI("Arrow");
                    Card.Use();
                }
            }
            yield return null;
        }
        // Cursor.visible = true;
        UIManager.Instance.CloseUI("Arrow");
        yield break;
    }
}
