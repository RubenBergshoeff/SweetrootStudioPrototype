using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BrokerButton : MonoBehaviour {

    [SerializeField] private BrokerBase broker = null;
    [SerializeField] protected ActiveBaseData activeResultData = null;
    private Button button;

    [Header("Editor")]
    [SerializeField] private TMPro.TextMeshProUGUI nameField = null;
    [SerializeField] private Image visual = null;

    public void SetupButton(BrokerBase broker, ActiveBaseData resultData) {
        this.broker = broker;
        this.activeResultData = resultData;
        UpdateView();
    }

    private void OnEnable() {
        if (button == null) {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(OnClickResult);
    }

    private void OnClickResult() {
        broker.PickResult(activeResultData);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnClickResult);
    }

    protected virtual void UpdateView() {
        if (nameField == null || visual == null) { return; }

        if (activeResultData == null || activeResultData.Data == null) {
            gameObject.name = "BrokerButton";
            nameField.text = "button";
            visual.sprite = null;
        }
        else {
            gameObject.name = "BrokerButton - " + activeResultData.Data.Name;
            nameField.text = activeResultData.Data.Name;
            visual.sprite = activeResultData.Data.Visual;
        }
    }
}
