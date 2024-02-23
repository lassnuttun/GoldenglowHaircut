using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DeckPileDisplay : MonoBehaviour
{
    private DepthOfField depthOfField;
    private PostProcessVolume processVolume;

    // Start is called before the first frame update
    void Start()
    {
        //depthOfField = ScriptableObject.CreateInstance<DepthOfField>();
        //depthOfField.enabled.Override(true);
        //depthOfField.focalLength.Override(1.0f);
        //processVolume = PostProcessManager.instance.QuickVolume(9, 100, depthOfField);
        //List<PostProcessVolume> list = new List<PostProcessVolume>();
        //PostProcessLayer layer = Camera.main.transform.GetComponent<PostProcessLayer>();
        //PostProcessManager.instance.GetActiveVolumes(layer, list, false, false);
    }

    // Update is called once per frame
    void Update() { }

    public void ShowDeckPile()
    {
        var DeckPile = FightManager.Instance.CardPiles[0];
        Transform canvas = GameObject.Find("CanvasForUpperUI").transform;
        foreach (var card in DeckPile)
        {
            Object resource = AssetBundleManager.LoadResource<Object>(card.CardID, "card");
            GameObject cardObj = Instantiate(resource, canvas) as GameObject;
            card.BindDisplayComponent(cardObj);
            RectTransform rectTransform = cardObj.GetComponent<RectTransform>();
        };
    }

    void OnDestory()
    {
        //RuntimeUtilities.DestroyVolume(processVolume, true, true);
    }
}
