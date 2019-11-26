using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class HubController : UIDisplayController {
    [SerializeField] private ObjectClickTracker characterHouse = null;
    [SerializeField] private ObjectClickTracker trainingHouse = null;
    [SerializeField] private string uiEventStringToTraining = "";
    [SerializeField] private string uiEventStringToCharacter = "";
    [SerializeField] private string uiEventStringToNewCharacter = "";
    [SerializeField] private string uiEventStringToEndCharacter = "";

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
        foreach (var character in SaveController.Instance.GameData.CharacterCollection.Characters) {
            if (character.HasBeenIntroduced == false) {
                GameEventMessage.SendEvent(uiEventStringToNewCharacter);
                return;
            }
        }
    }

    protected override void OnHiding() {

    }
}
