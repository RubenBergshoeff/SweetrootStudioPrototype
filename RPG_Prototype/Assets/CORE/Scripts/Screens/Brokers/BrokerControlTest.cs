using System.Collections;
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
        if (boterkroon.IsRoyalLocked == false) {
            SetupOption(flipoverRoyal, BoterkroonSkills.Royal);
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
        if (boterkroon.IsRoyalLocked == false) {
            flipoverRoyal.FlipUp();
        }
    }

    protected override void OnHiding() {
    }

    protected override void OnInvisible() {
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;
        if (boterkroon.IsBakingLocked == false) {
            flipoverBaking.OnPointClicked -= () => PickResult(BoterkroonSkills.Baking);
        }
        if (boterkroon.IsSwordLocked == false) {
            flipoverSword.OnPointClicked -= () => PickResult(BoterkroonSkills.Sword);
        }
        if (boterkroon.IsRoyalLocked == false) {
            flipoverRoyal.OnPointClicked -= () => PickResult(BoterkroonSkills.Royal);
        }
    }

    private void SetupOption(OptionPointFlipOver option, BoterkroonSkills skill) {
        option.gameObject.SetActive(true);
        option.OnPointClicked += () => PickResult(skill);
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