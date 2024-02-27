using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PileExamineUI : UIBase
{
    public Transform ScrollViewTrans;
    public Transform BtnBackTrans;

    public override void Show()
    {
        Camera.main.GetComponent<PostProcessVolume>().enabled = true;
        base.Show();
    }

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

    public override void Close()
    {
        Camera.main.GetComponent<PostProcessVolume>().enabled = false;
        base.Close();
    }

    public void BtnOnClickBack()
    {
        Close();
    }
}
