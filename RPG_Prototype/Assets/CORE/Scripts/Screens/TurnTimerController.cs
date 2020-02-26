using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TurnTimerController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timerTextMesh = null;

    private void OnEnable() {
        SaveController.Instance.GameData.BoterKroon.OnTurnsChanged += OnTurnsChanged;
        OnTurnsChanged(SaveController.Instance.GameData.BoterKroon.TurnsLeft);
    }

    private void OnTurnsChanged(int turnsLeft) {
        timerTextMesh.text = turnsLeft.ToString();
    }

    private void OnDisable() {
        SaveController.Instance.GameData.BoterKroon.OnTurnsChanged -= OnTurnsChanged;
    }
}
