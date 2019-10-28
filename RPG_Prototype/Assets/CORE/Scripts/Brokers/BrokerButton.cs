using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BrokerButton : MonoBehaviour {

    [SerializeField] private BrokerBase broker = null;
    [SerializeField] private ResultDataBase resultData = null;
    private Button button;

    [Header("Editor")]
    [SerializeField] private TMPro.TextMeshProUGUI nameField = null;
    [SerializeField] private Image visual = null;

    public void SetupButton(BrokerBase broker, ResultDataBase resultData) {
        this.broker = broker;
        this.resultData = resultData;
        UpdateView();
    }

    private void OnEnable() {
        if (button == null) {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(OnClickResult);
    }

    private void OnClickResult() {
        broker.PickResult(resultData);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnClickResult);
    }

    private void OnValidate() {
        UpdateView();
    }

    private void UpdateView() {
        if (nameField == null || visual == null) { return; }

        if (resultData == null) {
            gameObject.name = "BrokerButton";
            nameField.text = "button";
            visual.sprite = null;
        }
        else {
            gameObject.name = "BrokerButton - " + resultData.Name;
            nameField.text = resultData.Name;
            visual.sprite = resultData.Visual;
        }
    }
}
