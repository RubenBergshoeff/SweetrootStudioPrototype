using Doozy.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokerTraining : UIDisplayController {

    [SerializeField] private BrokerTrainingResult resultController = null;
    [SerializeField] private string uiEventString = "";

    private BrokerButtonTraining[] brokerButtons;

    protected override void OnShowing() {
        if (brokerButtons == null) {
            brokerButtons = GetComponentsInChildren<BrokerButtonTraining>();
        }
        foreach (var button in brokerButtons) {
            if (SaveController.Instance.GameData.BoterKroon.IsSkillActive(button.TargetSkill)) {
                button.gameObject.SetActive(true);
                button.OnButtonClicked += PickResult;
            }
            else {
                button.gameObject.SetActive(false);
            }
        }
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {
        foreach (var button in brokerButtons) {
            if (SaveController.Instance.GameData.BoterKroon.IsSkillActive(button.TargetSkill)) {
                button.gameObject.SetActive(true);
                button.OnButtonClicked -= PickResult;
            }
        }
    }

    protected override void OnInvisible() {

    }

    private void PickResult(BoterkroonSkills skill, TrainingType trainingType) {
        resultController.SetResult(skill, trainingType);
        GameEventMessage.SendEvent(uiEventString);
    }
}
