using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSwitchController : UIDisplayController {
    [SerializeField] private string uiEventStringToCharacter = "";
    [SerializeField] private string uiEventStringToNewCharacter = "";
    [SerializeField] private string uiEventStringToTestChoice = "";

    protected override void OnShowing() {
        if (SaveController.Instance.GameData.BoterKroon.IsNew) {
            GameEventMessage.SendEvent(uiEventStringToNewCharacter);
        }
        //else if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
        //    GameEventMessage.SendEvent(uiEventStringToTestChoice);
        //}
        else {
            GameEventMessage.SendEvent(uiEventStringToCharacter);
        }
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }
}
