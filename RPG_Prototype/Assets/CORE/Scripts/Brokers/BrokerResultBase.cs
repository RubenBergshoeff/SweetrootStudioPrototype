using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BrokerResultBase : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI resultNameContainer = null;
    [SerializeField] private Image resultVisualContainer = null;

    public virtual void SetResult(ResultDataBase result) {
        resultNameContainer.text = result.Name;
        resultVisualContainer.sprite = result.Visual;
    }
}
