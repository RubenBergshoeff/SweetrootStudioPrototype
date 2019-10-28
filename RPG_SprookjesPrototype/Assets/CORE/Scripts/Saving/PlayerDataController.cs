using UnityEngine;
using System.Collections;

public class PlayerDataController : MonoBehaviour {

    public PlayerData PlayerData {
        get {
            return SaveController.Instance.GameData.PlayerData;
        }
    }

    [SerializeField] private TMPro.TextMeshProUGUI intField = null;
    [SerializeField] private TMPro.TextMeshProUGUI stringField = null;

    private void OnEnable() {
        SaveController.OnInitializedAction += OnDataInitialized;
        if (SaveController.IsInitialized) {
            OnDataInitialized();
        }
    }

    private void OnDataInitialized() {
        SaveController.OnInitializedAction -= OnDataInitialized;
        SaveController.Instance.OnLoadAction += OnLoad;
        UpdateDisplay();
    }

    private void OnDisable() {
        if (SaveController.IsInitialized == false) { return; }
        SaveController.Instance.OnLoadAction -= OnLoad;
    }

    public void SetRandomNumber() {
        LogController.LogAction(Instigator.User, "set random number");
        PlayerData.SomeIntVariable = Random.Range(0, 50);
        UpdateDisplay();
    }

    public void SetRandomText() {
        LogController.LogAction(Instigator.User, "set random text");
        PlayerData.SomeStringVariable = "SomeText" + Random.Range(0, 50).ToString();
        UpdateDisplay();
    }

    private void OnLoad() {
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        intField.text = PlayerData.SomeIntVariable.ToString();
        stringField.text = PlayerData.SomeStringVariable;
    }
}
