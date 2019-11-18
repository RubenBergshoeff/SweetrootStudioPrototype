using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : BaseSubController {
    [SerializeField] private ObjectClickTracker characterHouse = null;
    [SerializeField] private ObjectClickTracker trainingHouse = null;

    private void OnEnable() {
        characterHouse.OnObjectClicked += OnCharacterHouseClicked;
        trainingHouse.OnObjectClicked += OnTrainingHouseClicked;
    }

    private void OnDisable() {
        characterHouse.OnObjectClicked -= OnCharacterHouseClicked;
        trainingHouse.OnObjectClicked -= OnTrainingHouseClicked;
    }

    private void OnCharacterHouseClicked() {
        Debug.Log("character house clicked");
    }

    private void OnTrainingHouseClicked() {
        GameController.GoToScreen(Screens.TrainingChoice);
    }
}
