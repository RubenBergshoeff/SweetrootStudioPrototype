using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.UI;

public class BrokerControlTestResult : BrokerBaseResult {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private FillSlider fillSlider = null;
    [SerializeField] private TextMeshProUGUI sliderTextField = null;
    [SerializeField] private Button backButton = null;

    private ActiveSkillLevel activeSkillLevel;
    private TrainingData newTrainingData;

    public override void SetResult(ActiveResultData activeResult) {
        base.SetResult(activeResult);
        activeSkillLevel = activeResult as ActiveSkillLevel;
    }

    private void OnEnable() {
        backButton.gameObject.SetActive(false);
        if (activeSkillLevel == null) { return; }
        StartCoroutine(ControlTestAnimation());
    }

    private IEnumerator ControlTestAnimation() {
        SetStartDisplay();

        yield return new WaitForSeconds(1f);

        // test character skill
        float completionLevel = playerCharacterController.GetActiveSkillXP(activeSkillLevel.SkillLevel.Skill) / (float)activeSkillLevel.SkillLevel.XPCap;
        AnimateDisplay(1.3f);

        yield return new WaitForSeconds(1.8f);

        if (completionLevel >= 1) {
            activeSkillLevel.IsCompleted = true;
            SetStartDisplay();
        }
        activeSkillLevel.LastCompletionLevel = Mathf.Min(completionLevel, 1);

        yield return new WaitForSeconds(0.3f);

        backButton.gameObject.SetActive(true);
    }

    private void AnimateDisplay(float time) {
        int XPCap = playerCharacterController.GetActiveSkillXPCap(activeSkillLevel.SkillLevel.Skill);
        fillSlider.SetValues(
            0,
            0,
            (float)playerCharacterController.GetActiveSkillXP(activeSkillLevel.SkillLevel.Skill) / XPCap,
            time,
            activeSkillLevel.SkillLevel.Skill.SkillCategory.color);

        sliderTextField.text = "Level " + playerCharacterController.GetActiveSkillLevel(activeSkillLevel.SkillLevel.Skill);
    }

    private void SetStartDisplay() {
        fillSlider.SetValues(
            0,
            0,
            activeSkillLevel.SkillLevel.Skill.SkillCategory.color);

        sliderTextField.text = "Level " + playerCharacterController.GetActiveSkillLevel(activeSkillLevel.SkillLevel.Skill);
    }
}
