using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : UIDisplayController {
    [SerializeField] private Button resetDataButton;

    protected override void OnShowing() {
        resetDataButton.onClick.AddListener(OnResetDataButtonClicked);
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {
        resetDataButton.onClick.RemoveListener(OnResetDataButtonClicked);
    }

    protected override void OnInvisible() {

    }

    private void OnResetDataButtonClicked() {
        SaveController.Instance.ResetGame();
        SceneManager.LoadScene("Main");
    }
}
