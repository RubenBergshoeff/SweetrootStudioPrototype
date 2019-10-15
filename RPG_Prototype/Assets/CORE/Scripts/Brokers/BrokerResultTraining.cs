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
        if (training.StatType == StatType.Magic) {
            sliderPowerContainer.SetActive(false);
            sliderMagicContainer.SetActive(true);
            float magicProgress = GetProgressForStat(playerCharacterController.Data.StatMagic);
            sliderMagic.SetValues(0, magicProgress);
            magicLevelField.text = "level " + playerCharacterController.Data.StatMagic.Level.ToString();
        }
        else if (training.StatType == StatType.Power) {
            sliderMagicContainer.SetActive(false);
            sliderPowerContainer.SetActive(true);
            float powerProgress = GetProgressForStat(playerCharacterController.Data.StatPower);
            sliderPower.SetValues(0, powerProgress);
            powerLevelField.text = "level " + playerCharacterController.Data.StatPower.Level.ToString();
        }
    }

    private void OnEnable() {
        gainedDisplay.SetActive(false);
        backButton.gameObject.SetActive(false);
        StartCoroutine(TrainingAnimation());
    }

    private IEnumerator TrainingAnimation() {
        yield return new WaitForSeconds(2.5f);

        // apply training
        switch (training.StatType) {
            case StatType.Power:
                playerCharacterController.Data.StatPower.XP += training.XP;
                float powerProgress = GetProgressForStat(playerCharacterController.Data.StatPower);
                sliderPower.SetValues(0, powerProgress);
                powerLevelField.text = "level " + playerCharacterController.Data.StatPower.Level.ToString();
                break;
            case StatType.Magic:
                playerCharacterController.Data.StatMagic.XP += training.XP;
                float magicProgress = GetProgressForStat(playerCharacterController.Data.StatMagic);
                sliderMagic.SetValues(0, magicProgress);
                magicLevelField.text = "level " + playerCharacterController.Data.StatMagic.Level.ToString();
                break;
        }

        yield return new WaitForSeconds(1f);

        backButton.gameObject.SetActive(true);
    }

    private float GetProgressForStat(PlayerStat stat) {
        int relativeXPValue = stat.XP - PlayerStat.MinLevelXPs[stat.Level];
        int xpInLevel = PlayerStat.MinLevelXPs[stat.Level + 1] - PlayerStat.MinLevelXPs[stat.Level];
        return relativeXPValue / (float)xpInLevel;
    }
}
