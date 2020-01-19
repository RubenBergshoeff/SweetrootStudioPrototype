using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.UI;

public class HubController : UIDisplayController {
    [SerializeField] private ObjectClickTracker characterHouse = null;
    [SerializeField] private ObjectClickTracker trainingHouse = null;
    [SerializeField] private string uiEventStringToTraining = "";
    [SerializeField] private string uiEventStringToCharacter = "";
    [SerializeField] private Button toTestChoiceButton = null;

    private void OnCharacterHouseClicked() {
        GameEventMessage.SendEvent(uiEventStringToCharacter);
    }

    private void OnTrainingHouseClicked() {
        GameEventMessage.SendEvent(uiEventStringToTraining);
    }

    protected override void OnVisible() {
        characterHouse.OnObjectClicked += OnCharacterHouseClicked;
        trainingHouse.OnObjectClicked += OnTrainingHouseClicked;
    }

    protected override void OnInvisible() {
        characterHouse.OnObjectClicked -= OnCharacterHouseClicked;
        trainingHouse.OnObjectClicked -= OnTrainingHouseClicked;
    }

    protected override void OnShowing() {
        toTestChoiceButton.gameObject.SetActive(SaveController.Instance.GameData.BoterKroon.TrainingResultsBaking.Count > 0);
    }

    protected override void OnHiding() {

    }
}
