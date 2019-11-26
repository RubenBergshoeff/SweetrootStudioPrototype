using Doozy.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewCharacterScreenController : UIDisplayController {

    [SerializeField] private TextMeshProUGUI textmeshCharacterName = null;
    [SerializeField] private Image imageCharacterIcon = null;

    [SerializeField] private string uiEventStringToHub = "";

    protected override void OnShowing() {
        CheckForNewCharacters();
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private void CheckForNewCharacters() {
        foreach (var character in SaveController.Instance.GameData.CharacterCollection.Characters) {
            if (character.HasBeenIntroduced == false) {
                IntroduceNewCharacter(character);
                return;
            }
        }
        Debug.LogWarning("No new character found, returning to hub");
        GameEventMessage.SendEvent(uiEventStringToHub);
    }

    private void IntroduceNewCharacter(ActiveCharacterData character) {
        character.HasBeenIntroduced = true;
        textmeshCharacterName.text = character.CharacterData.Name;
        imageCharacterIcon.sprite = character.CharacterData.Visual;
    }
}
