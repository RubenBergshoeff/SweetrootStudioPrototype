using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;

public class BrokerTrainingResult : BrokerBaseResult {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private FillSlider fillSlider = null;
    [SerializeField] private TextMeshProUGUI sliderTextField = null;
    [SerializeField] private Button backButton = null;

    private ActiveTraining activeTraining;

    public override void SetResult(ActiveResultData result) {
        base.SetResult(result);
        activeTraining = result as ActiveTraining;
        Debug.Log(activeTraining.Data.Name);
        Debug.Log(activeTraining.Training.TargetSkill.Name);
    }

    protected override void OnVisible() {
        backButton.gameObject.SetActive(false);
        StartCoroutine(TrainingAnimation());
    }

    protected override void OnInvisible() {
    }

    private IEnumerator TrainingAnimation() {
        SetStartDisplay();

        yield return new WaitForSeconds(1f);

        // apply training
        int oldXP = playerCharacterController.GetActiveSkillXP(activeTraining.Training.TargetSkill);
        playerCharacterController.AddActiveSkillXP(activeTraining.Training.TargetSkill, activeTraining.Training.XPGainTraining);
        AnimateDisplay(oldXP, 1.3f);

        yield return new WaitForSeconds(1.8f);

        backButton.gameObject.SetActive(true);
    }

    private void AnimateDisplay(int oldXP, float time) {
        int XPCap = playerCharacterController.GetActiveSkillXPCap(activeTraining.Training.TargetSkill);
        fillSlider.SetValues(
            0,
            (float)oldXP / XPCap,
            (float)playerCharacterController.GetActiveSkillXP(activeTraining.Training.TargetSkill) / XPCap,
            time,
            activeTraining.Training.TargetSkill.SkillCategory.color);

        sliderTextField.text = "Level " + playerCharacterController.GetActiveSkillLevel(activeTraining.Training.TargetSkill);
    }

    private void SetStartDisplay() {
        fillSlider.SetValues(
            0,
            (float)playerCharacterController.GetActiveSkillXP(activeTraining.Training.TargetSkill) / playerCharacterController.GetActiveSkillXPCap(activeTraining.Training.TargetSkill),
            activeTraining.Training.TargetSkill.SkillCategory.color);

        sliderTextField.text = "Level " + playerCharacterController.GetActiveSkillLevel(activeTraining.Training.TargetSkill);
    }
}
