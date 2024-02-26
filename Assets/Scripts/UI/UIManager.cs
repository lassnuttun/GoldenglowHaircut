using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Transform LCanvasTransTool;
    public Transform UCanvasTransTool;
    private List<UIBase> UILoadedList; // 已加载的 UI

    private void Awake()
    {
        Instance = this;
        LCanvasTransTool = GameObject.Find("Canvas").transform;
        UCanvasTransTool = GameObject.Find("CanvasForUpperUI").transform;
        UILoadedList = new List<UIBase>();
    }

    public UIBase SearchLoadedUI(string ui_name)
    {
        for (int i = 0; i < UILoadedList.Count; i++)
        {
            if (UILoadedList[i].name == ui_name)
            {
                return UILoadedList[i];
            }
        }
        return null;
    }

    public UIBase ShowUI<T>(string ui_name, bool isUpper = false) where T : UIBase
    {
        UIBase ui = SearchLoadedUI(ui_name);
        if (ui == null)
        {
            Object resource = AssetBundleManager.LoadResource<Object>(ui_name, "ui");
            Transform canvas = isUpper ? UCanvasTransTool : LCanvasTransTool;
            GameObject new_ui = Instantiate(resource, canvas) as GameObject;
            new_ui.name = ui_name;
            ui = new_ui.GetComponent<T>();
            if (ui == null)
            {
                ui = new_ui.AddComponent<T>();
            }
            UILoadedList.Add(ui);
        }
        ui.Show();
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
            UILoadedList.Remove(ui);
            Destroy(ui.gameObject);
        }
    }

    public void CloseAllUI()
    {
        for (int i = UILoadedList.Count - 1; i >= 0; i--)
        {
            Destroy(UILoadedList[i].gameObject);
        }
        UILoadedList.Clear();
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
