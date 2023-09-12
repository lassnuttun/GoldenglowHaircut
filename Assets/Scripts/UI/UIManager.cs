using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // 直接 new 是否有区别，待验证
    private Transform canvasTransTool;
    private List<UIBase> uiLoadedList; // 已加载的 UI

    private static AssetBundle uiab;

    private void Awake()
    {
        uiab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ui");
        Instance = this;
        canvasTransTool = GameObject.Find("Canvas").transform;
        uiLoadedList = new List<UIBase>();
    }

    public UIBase SearchLoadedUI(string ui_name)
    {
        for (int i = 0; i < uiLoadedList.Count; i++)
        {
            if (uiLoadedList[i].name == ui_name)
            {
                return uiLoadedList[i];
            }
        }
        return null;
    }

    public UIBase ShowUI<T>(string ui_name) where T : UIBase
    {
        UIBase ui = SearchLoadedUI(ui_name);
        if (ui != null)
        {
            ui.Show();
        }
        else
        {
            var resource = uiab.LoadAsset(ui_name);
            GameObject new_ui = Instantiate(resource, canvasTransTool) as GameObject;
            new_ui.name = ui_name;
            ui = new_ui.AddComponent<T>();
            ui.transform.position = Vector3.zero;
            uiLoadedList.Add(ui);
        }
        return ui;
    }

    public void HideUI(string ui_name)
    {
        UIBase ui = SearchLoadedUI(ui_name);
        if (ui != null)
        {
            ui.Hide();
        }
    }

    public void CloseUI(string ui_name)
    {
        UIBase ui = SearchLoadedUI(ui_name);
        if (ui != null)
        {
            uiLoadedList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    public void CloseAllUI()
    {
        for (int i = uiLoadedList.Count - 1; i >= 0; i--)
        {
            Destroy(uiLoadedList[i].gameObject);
        }
        uiLoadedList.Clear();
    }

    public T GetUIScript<T>(string ui_name) where T : UIBase
    {
        UIBase ui = SearchLoadedUI(ui_name);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;
    }

    public GameObject GenerateCdBar()
    {
        var resource = uiab.LoadAsset("cdBar");
        GameObject cdBar = Instantiate(resource, canvasTransTool) as GameObject;
        cdBar.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f);
        return cdBar;
    }
}
