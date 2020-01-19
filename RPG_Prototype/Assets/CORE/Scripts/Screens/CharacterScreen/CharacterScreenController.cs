using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI.Base;
using System;

public class CharacterScreenController : UIDisplayController {

    [SerializeField] private SkillResultController skillResultController = null;
    [SerializeField] private GameObject backButton = null;
    [SerializeField] private TutorializationController tutorialization = null;

    protected override void OnShowing() {
        BoterkroonSkillResult lastSkillResult = GetLastSucceededResult();
        skillResultController.UpdateView(lastSkillResult);
    }

    protected override void OnVisible() {
        if (HasFailedNewSkilltest()) {
            Popups.ShowPopup(Popups.BOTERKROON_FAILEDSKILLTEST);
        }
        if (HasDoneControlWithoutTraining()) {
            Popups.ShowPopup(Popups.BOTERKROON_CONTROLWITHOUTTRAINING);
        }
        if (HasUnlockedNewSkill()) {
            Popups.ShowPopup(Popups.BOTERKROON_NEWSKILL);
        }
        UpdateNewStateLastSkillTest();
    }

    private bool HasDoneControlWithoutTraining() {
        bool bakingControlWithoutTraining = HasDoneControlWithoutTraining(BoterkroonSkills.Baking);
        bool swordControlWithoutTraining = HasDoneControlWithoutTraining(BoterkroonSkills.Sword);
        bool researchControlWithoutTraining = HasDoneControlWithoutTraining(BoterkroonSkills.Research);
        return bakingControlWithoutTraining || swordControlWithoutTraining || researchControlWithoutTraining;
    }

    private bool HasDoneControlWithoutTraining(BoterkroonSkills skill) {
        var results = SaveController.Instance.GameData.BoterKroon.GetControlResultsFor(skill);
        if (results.Count < 2) {
            if (results.Count > 0 && results[results.Count - 1].TotalXP == 0) {
                results[results.Count - 1].HasBeenCheckedForDoubleControl = true;
                return true;
            }
            return false;
        }
        if (results[results.Count - 1].HasBeenCheckedForDoubleControl == false) {
            results[results.Count - 1].HasBeenCheckedForDoubleControl = true;
            if (results[results.Count - 1].TotalXP == results[results.Count - 2].TotalXP) {
                return true;
            }
        }
        return false;
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
            return lastResult.UnlockResearch || lastResult.UnlockSword || lastResult.UnlockBaking;
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

    private void Update() {
        backButton.SetActive(tutorialization.TextTutorialActive == false);
    }
}
