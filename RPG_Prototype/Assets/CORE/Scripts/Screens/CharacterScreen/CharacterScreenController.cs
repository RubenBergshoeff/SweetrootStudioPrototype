using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI.Base;
using System;

public class CharacterScreenController : UIDisplayController {

    [SerializeField] private SkillResultController skillResultController = null;

    protected override void OnShowing() {
        BoterkroonSkillResult lastSkillResult = GetLastSucceededResult();
        skillResultController.UpdateView(lastSkillResult);
    }

    protected override void OnVisible() {
        if (HasFailedNewSkilltest()) {
            Popups.ShowPopup(Popups.BOTERKROON_FAILEDSKILLTEST);
        }
        if (HasUnlockedNewSkill()) {
            Popups.ShowPopup(Popups.BOTERKROON_NEWSKILL);
        }
        UpdateNewStateLastSkillTest();
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private bool HasFailedNewSkilltest() {
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            return false;
        }
        BoterkroonSkillResult lastResult = SaveController.Instance.GameData.BoterKroon.SkillResults[SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1];
        if (lastResult.IsNew) {
            return !lastResult.Succeeded;
        }
        return false;
    }

    private bool HasUnlockedNewSkill() {
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            return false;
        }
        BoterkroonSkillResult lastResult = SaveController.Instance.GameData.BoterKroon.SkillResults[SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1];
        if (lastResult.IsNew) {
            return lastResult.UnlockResearch || lastResult.UnlockSword;
        }
        return false;
    }

    private void UpdateNewStateLastSkillTest() {
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            return;
        }
        BoterkroonSkillResult lastResult = SaveController.Instance.GameData.BoterKroon.SkillResults[SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1];
        lastResult.IsNew = false;
    }

    private BoterkroonSkillResult GetLastSucceededResult() {
        for (int i = SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1; i >= 0; i--) {
            if (SaveController.Instance.GameData.BoterKroon.SkillResults[i].Succeeded) {
                return SaveController.Instance.GameData.BoterKroon.SkillResults[i];
            }
        }
        return new BoterkroonSkillResult(1, 0, true);
    }
}
