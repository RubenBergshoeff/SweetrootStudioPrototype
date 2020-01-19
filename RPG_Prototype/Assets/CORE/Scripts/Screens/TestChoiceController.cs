using UnityEngine;
using System.Collections;
using System;
using Doozy.Engine;

public class TestChoiceController : UIDisplayController {

    [SerializeField] private TutorializationController tutorialization = null;
    [SerializeField] private ObjectClickTracker skillCatTest = null;
    [SerializeField] private ObjectClickTracker skillTest = null;
    [SerializeField] private string uiEventStringToSkillCatTest = "";
    [SerializeField] private string uiEventStringToSkillTest = "";
    [SerializeField] private GameObject backButton = null;

    protected override void OnVisible() {
        skillCatTest.OnObjectClicked += OnSkillCatTestClicked;
        skillTest.OnObjectClicked += OnSkillTestClicked;
    }

    protected override void OnInvisible() {
        skillCatTest.OnObjectClicked -= OnSkillCatTestClicked;
        skillTest.OnObjectClicked -= OnSkillTestClicked;
    }

    private void OnSkillCatTestClicked() {
        if (tutorialization.TextTutorialActive) { return; }
        if (tutorialization.TutorialActive && SaveController.Instance.GameData.BoterKroon.SkillResults.Count > 0) {
            return;
        }
        GameEventMessage.SendEvent(uiEventStringToSkillCatTest);
    }

    private void OnSkillTestClicked() {
        if (tutorialization.TextTutorialActive) { return; }
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            return;
        }
        GameEventMessage.SendEvent(uiEventStringToSkillTest);
    }

    protected override void OnShowing() {
        backButton.SetActive(SaveController.Instance.GameData.BoterKroon.SkillResults.Count > 0);
    }

    protected override void OnHiding() {

    }
}
