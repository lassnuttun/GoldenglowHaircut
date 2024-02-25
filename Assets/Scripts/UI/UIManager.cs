using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Transform CanvasTransTool { get; private set; }
    private List<UIBase> uiLoadedList; // 已加载的 UI

    private void Awake()
    {
        Instance = this;
        CanvasTransTool = GameObject.Find("Canvas").transform;
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
            Object resource = AssetBundleManager.LoadResource<Object>(ui_name, "ui");
            GameObject new_ui = Instantiate(resource, CanvasTransTool) as GameObject;
            new_ui.name = ui_name;
            ui = new_ui.GetComponent<T>();
            if (ui == null)
            {
                ui = new_ui.AddComponent<T>();
            }
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

    public T GetUI<T>(string ui_name) where T : UIBase
    {
        UIBase ui = SearchLoadedUI(ui_name);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;
    }
}
