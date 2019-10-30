using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BrokerBaseResult : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI resultNameContainer = null;
    [SerializeField] private Image resultVisualContainer = null;

    public virtual void SetResult(ActiveResultData activeResult) {
        resultNameContainer.text = activeResult.Data.Name;
        resultVisualContainer.sprite = activeResult.Data.Visual;
    }
}
