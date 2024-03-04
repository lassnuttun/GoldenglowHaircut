using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnvCreateCardDisplay<TEnv> : CardDisplay where TEnv : EnvironmentBase
{
    public new EnvCreateCard<TEnv> Card;

    public override CardBase GetCard() { return Card; }

    public override void SetCard(CardBase card)
    {
        if (card is EnvCreateCard<TEnv>)
        {
            Card = card as EnvCreateCard<TEnv>;
        }
    }

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
                // 使用的逻辑之后需要移到卡牌逻辑类中
                GameObject gameObj = hit.collider.gameObject;
                PlayerDisplay playerDisplay = gameObj.GetComponent<PlayerDisplay>();
                if (Input.GetMouseButtonDown(0))
                {
                    if (playerDisplay == null || FightManager.Instance.UsableCheckForCard(Card) == false)
                    {
                        break;
                    }
                    StopAllCoroutines();
                    UIManager.Instance.CloseUI("Arrow");
                    // 需要细化插入环境时的机制，如果已经存在相同的环境，应该如何处理？
                    if (Card is HypnoticCenser)
                    {
                        FightManager.Instance.AddEnv(Card.GetEnv());
                    }
                    // 需要细化实现环境卡进入弃牌堆的机制
                    FightManager.Instance.RemoveCard(Card, true);
                }
            }
            yield return null;
        }
        // Cursor.visible = true;
        UIManager.Instance.CloseUI("Arrow");
        yield break;
    }
}
