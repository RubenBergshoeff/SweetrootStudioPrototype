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
    [SerializeField] private FillSlider sliderMagic = null;
    [SerializeField] private FillSlider sliderPower = null;
    [SerializeField] private TextMeshProUGUI magicLevelField = null;
    [SerializeField] private TextMeshProUGUI powerLevelField = null;
    [SerializeField] private Button backButton = null;

    private TrainingData training;

    public override void SetResult(ResultDataBase result) {
        base.SetResult(result);
        training = result as TrainingData;
        UpdateDisplay();
    }

    private void OnEnable() {
        gainedDisplay.SetActive(false);
        backButton.gameObject.SetActive(false);
        StartCoroutine(TrainingAnimation());
    }

    private IEnumerator TrainingAnimation() {
        yield return new WaitForSeconds(2.5f);

        // apply training
        playerCharacterController.AddXP(training.StatType, training.XP);
        UpdateDisplay();

        yield return new WaitForSeconds(1f);

        backButton.gameObject.SetActive(true);
    }

    private void UpdateDisplay() {
        switch (training.StatType) {
            case StatType.Power:
                sliderMagicContainer.SetActive(false);
                sliderPowerContainer.SetActive(true);
                float powerProgress = GetProgressForStat(playerCharacterController.LevelPower, playerCharacterController.XPPower);
                sliderPower.SetValues(0, powerProgress);
                powerLevelField.text = "level " + playerCharacterController.LevelPower.ToString();
                break;
            case StatType.Magic:
                sliderPowerContainer.SetActive(false);
                sliderMagicContainer.SetActive(true);
                float magicProgress = GetProgressForStat(playerCharacterController.LevelMagic, playerCharacterController.XPMagic);
                sliderMagic.SetValues(0, magicProgress);
                magicLevelField.text = "level " + playerCharacterController.LevelMagic.ToString();
                break;
        }
    }

    private float GetProgressForStat(int level, int xp) {
        int relativeXPValue = xp - PlayerStat.MinLevelXPs[level];
        int xpInLevel = PlayerStat.MinLevelXPs[level + 1] - PlayerStat.MinLevelXPs[level];
        return relativeXPValue / (float)xpInLevel;
    }
}
