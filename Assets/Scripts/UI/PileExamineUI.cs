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
        var DeckPile = FightManager.Instance.CardPiles[0];
        ScrollViewTrans.gameObject.SetActive(true);
        BtnBackTrans.gameObject.SetActive(true);
        RectTransform content = ScrollViewTrans.GetComponent<ScrollRect>().content;
        foreach (var card in DeckPile)
        {
            Object resource = AssetBundleManager.LoadResource<Object>(card.CardID, "card");
            GameObject cardObj = Instantiate(resource, content) as GameObject;
            card.BindDisplayComponent(cardObj);
        }
        base.Show();
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
