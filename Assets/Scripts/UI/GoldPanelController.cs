using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldPanelController : MonoBehaviour
{
    public TextMeshProUGUI GoldText;

    private void LateUpdate()
    {
        if (CurrencyManager.Instance)
        {
            GoldText.SetText("$" + JSONDataManager.Instance.JSONDATA.Currency);
        }
    }
}
