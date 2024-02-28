using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UpperUI : UIBase
{
    public override void Show()
    {
        Camera.main.GetComponent<PostProcessVolume>().enabled = true;
        base.Show();
    }

    public override void Close()
    {
        Camera.main.GetComponent<PostProcessVolume>().enabled = false;
        base.Close();
    }
}
