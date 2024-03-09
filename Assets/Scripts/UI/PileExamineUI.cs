using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PileExamineUI : UpperUI
{
    public Transform ScrollViewTrans;
    public Transform BtnBackTrans;

    public void ShowPile(bool DeckOrDisc)
    {
        var Pile = FightManager.Instance.CardPiles[DeckOrDisc ? 0 : 2];
        ScrollViewTrans.gameObject.SetActive(true);
        BtnBackTrans.gameObject.SetActive(true);
        RectTransform content = ScrollViewTrans.GetComponent<ScrollRect>().content;
        foreach (var card in Pile)
        {
            Object resource = AssetBundleManager.LoadResource<Object>(card.CardID, "card");
            GameObject cardObj = Instantiate(resource, content) as GameObject;
            cardObj.transform.localScale = new Vector3(0.125f, 0.125f, 0);
            card.BindDisplayComponent(cardObj);
        }
    }

    public void BtnOnClickBack()
    {
        Close();
    }
}
