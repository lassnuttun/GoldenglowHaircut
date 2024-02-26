using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PileExamineUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnOnClickBack()
    {
        Camera.main.GetComponent<PostProcessVolume>().enabled = false;
        Close();
    }
}
