using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.UI;
using Doozy.Engine.UI;

public class SkillTestResult : UIDisplayController {

    [SerializeField] private GameObject failFeedbackContainer = null;
    [SerializeField] private TextMeshProUGUI textmeshFailedSkillName = null;
    [SerializeField] private Image imageContainerFailedSkill = null;

    private ActiveSkillCategory activeSkillCategory;

    public void SetResult(ActiveSkillCategory activeSkillCategory) {
        this.activeSkillCategory = activeSkillCategory;
    }

    protected override void OnShowing() {
        failFeedbackContainer.SetActive(false);
    }

    protected override void OnVisible() {
        //if (activeSkillCategory.LastResult.FailedSkill != null) {
        //    ShowFailedFeedback(activeSkillCategory.LastResult.FailedSkill);
        //}

        //foreach (var unlock in activeSkillCategory.LastResult.Unlocks) {
        //    var unlockReturn = SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.HandleUnlock(unlock.ResultToUnlock);
        //    if (unlockReturn == UnlockReturn.NewUnlock && unlock.ShowPopup) {
        //        ShowPopup(unlock);
        //    }
        //}
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private void ShowFailedFeedback(Skill failedSkill) {
        failFeedbackContainer.SetActive(true);
        textmeshFailedSkillName.text = failedSkill.Name;
        imageContainerFailedSkill.sprite = failedSkill.Visual;
    }


    private void ShowPopup(UnlockResult newUnlock) {
        UIPopup popup = UIPopup.GetPopup(Popups.UNLOCK_POPUP);

        if (popup == null) { return; }

        PopupUnlock popupUnlock = popup.GetComponent<PopupUnlock>();
        popupUnlock.Setup(newUnlock);

        popup.Show();
    }
}
