using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class BrokerBaseResult : UIDisplayController {
    [SerializeField] private TMPro.TextMeshProUGUI resultNameContainer = null;
    [SerializeField] private Image resultVisualContainer = null;

    public virtual void SetResult(ActiveBaseData activeResult) {
        resultNameContainer.text = activeResult.Data.Name;
        resultVisualContainer.sprite = activeResult.Data.Visual;
    }

    protected override void OnShowing() {

    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }
}
