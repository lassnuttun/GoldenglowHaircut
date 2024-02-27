using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerDisplay : UIBase
{
    public TextMeshProUGUI PowerText;

    public void UpdateDisplayInfo(ConditionBar Power)
    {
        PowerText.text = Power.ToString();
    }
}
