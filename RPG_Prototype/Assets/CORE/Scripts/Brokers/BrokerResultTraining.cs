using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;

public class BrokerResultTraining : BrokerResultBase {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private GameObject gainedDisplay = null;
    [SerializeField] private GameObject sliderMagicContainer = null;
    [SerializeField] private GameObject sliderPowerContainer = null;
    [SerializeField] private FillSlider fillSlider = null;
    [SerializeField] private TextMeshProUGUI sliderTextField = null;
    [SerializeField] private Button backButton = null;

    private TrainingData training;

    public override void SetResult(ResultDataBase result) {
        base.SetResult(result);
        training = result as TrainingData;
    }

    private void OnEnable() {
        gainedDisplay.SetActive(false);
        backButton.gameObject.SetActive(false);
        if (training == null) { return; }
        StartCoroutine(TrainingAnimation());
    }

    private IEnumerator TrainingAnimation() {
        SetStartDisplay();

        yield return new WaitForSeconds(1f);

        // apply training
        int oldXP = playerCharacterController.GetActiveSkillXP(training.TargetSkill);
        playerCharacterController.AddActiveSkillXP(training.TargetSkill, training.XPGainTraining);
        AnimateDisplay(oldXP, 1.3f);

        yield return new WaitForSeconds(1.8f);

        backButton.gameObject.SetActive(true);
    }

    private void AnimateDisplay(int oldXP, float time) {
        int XPCap = playerCharacterController.GetActiveSkillXPCap(training.TargetSkill);
        fillSlider.SetValues(
            0,
            (float)oldXP / XPCap,
            (float)playerCharacterController.GetActiveSkillXP(training.TargetSkill) / XPCap,
            time,
            training.TargetSkill.SkillCategory.color);

        sliderTextField.text = "Level " + playerCharacterController.GetActiveSkillLevel(training.TargetSkill);
    }

    private void SetStartDisplay() {
        fillSlider.SetValues(
            0,
            (float)playerCharacterController.GetActiveSkillXP(training.TargetSkill) / playerCharacterController.GetActiveSkillXPCap(training.TargetSkill),
            training.TargetSkill.SkillCategory.color);

        sliderTextField.text = "Level " + playerCharacterController.GetActiveSkillLevel(training.TargetSkill);
    }

    private float GetProgressForStat(int level, int xp) {
        int relativeXPValue = xp - PlayerStat.MinLevelXPs[level];
        int xpInLevel = PlayerStat.MinLevelXPs[level + 1] - PlayerStat.MinLevelXPs[level];
        return relativeXPValue / (float)xpInLevel;
    }
}
