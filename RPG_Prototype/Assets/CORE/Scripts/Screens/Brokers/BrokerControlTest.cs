﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Doozy.Engine;

public class BrokerControlTest : UIDisplayController {

    [SerializeField] private BrokerControlTestResult resultController = null;
    [SerializeField] private string uiEventString = "";

    [SerializeField] private OptionPointFlipOver flipoverBaking = null;
    [SerializeField] private OptionPointFlipOver flipoverSword = null;
    [SerializeField] private OptionPointFlipOver flipoverRoyal = null;

    protected override void OnShowing() {
        HideOptions();
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;
        if (boterkroon.IsBakingLocked == false) {
            SetupOption(flipoverBaking, BoterkroonSkills.Baking);
        }
        if (boterkroon.IsSwordLocked == false) {
            SetupOption(flipoverSword, BoterkroonSkills.Sword);
        }
        if (boterkroon.IsResearchLocked == false) {
            SetupOption(flipoverRoyal, BoterkroonSkills.Research);
        }
    }

    protected override void OnVisible() {
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;

        if (boterkroon.IsBakingLocked == false) {
            flipoverBaking.FlipUp();
        }
        if (boterkroon.IsSwordLocked == false) {
            flipoverSword.FlipUp();
        }
        if (boterkroon.IsResearchLocked == false) {
            flipoverRoyal.FlipUp();
        }
    }

    protected override void OnHiding() {
    }

    protected override void OnInvisible() {
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;
        if (boterkroon.IsBakingLocked == false) {
            flipoverBaking.OnObjectClicked -= () => PickResult(BoterkroonSkills.Baking);
        }
        if (boterkroon.IsSwordLocked == false) {
            flipoverSword.OnObjectClicked -= () => PickResult(BoterkroonSkills.Sword);
        }
        if (boterkroon.IsResearchLocked == false) {
            flipoverRoyal.OnObjectClicked -= () => PickResult(BoterkroonSkills.Research);
        }
    }

    private void SetupOption(OptionPointFlipOver option, BoterkroonSkills skill) {
        option.gameObject.SetActive(true);
        option.OnObjectClicked += () => PickResult(skill);
        option.ToStartPosition();
    }

    private void PickResult(BoterkroonSkills result) {
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }

    private void HideOptions() {
        flipoverBaking.gameObject.SetActive(false);
        flipoverSword.gameObject.SetActive(false);
        flipoverRoyal.gameObject.SetActive(false);
    }
}