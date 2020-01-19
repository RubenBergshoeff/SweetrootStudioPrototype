using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Doozy.Engine;

public class BrokerSkillTest : UIDisplayController {

    [SerializeField] private TutorializationController tutorialization = null;
    [SerializeField] private BrokerSkillTestResult resultController = null;
    [SerializeField] private string uiEventString = "";
    [SerializeField] private GameObject backButton = null;

    [SerializeField] private OptionPointFlipOver flipoverLvl1 = null;
    [SerializeField] private OptionPointFlipOver flipoverLvl2 = null;
    [SerializeField] private OptionPointFlipOver flipoverLvl3 = null;
    private bool isVisible;

    private void Start() {
        flipoverLvl1.OnObjectClicked += () => PickResult(1);
        flipoverLvl2.OnObjectClicked += () => PickResult(2);
        flipoverLvl3.OnObjectClicked += () => PickResult(3);
    }

    protected override void OnShowing() {
        isVisible = true;
        backButton.SetActive(SaveController.Instance.GameData.BoterKroon.SkillResults.Count > 0);
        flipoverLvl1.ToStartPosition();
        flipoverLvl2.ToStartPosition();
        flipoverLvl3.ToStartPosition();
    }

    protected override void OnVisible() {
        flipoverLvl1.FlipUp();
        flipoverLvl2.FlipUp();
        flipoverLvl3.FlipUp();
    }

    protected override void OnHiding() {
        isVisible = false;
    }

    protected override void OnInvisible() {

    }

    private void PickResult(int result) {
        if (isVisible == false) { return; }
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            if (result != 1) { return; }
        }
        if (tutorialization.TextTutorialActive) { return; }
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }
}
