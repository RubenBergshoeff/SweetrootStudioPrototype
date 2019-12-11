using Doozy.Engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSwitchController : UIDisplayController {
    [SerializeField] private string uiEventStringToCharacter = "";
    [SerializeField] private string uiEventStringToNewCharacter = "";

    protected override void OnShowing() {
        if (SaveController.Instance.GameData.BoterKroon.IsNew) {
            GameEventMessage.SendEvent(uiEventStringToNewCharacter);
        }
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
