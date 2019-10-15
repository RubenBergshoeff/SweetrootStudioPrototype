using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using UnityEngine.UI;

public class BrokerResultArena : BrokerResultBase {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private Transform provenSkillLevelContainer = null;
    [SerializeField] private TextMeshProUGUI provenSkillLevelField = null;
    [SerializeField] private MultiLevelSlider slider = null;
    [SerializeField] private Button backButton = null;
    [SerializeField] private GameObject defeatedDisplay = null;
    [SerializeField] private GameObject easyWinDisplay = null;
    [SerializeField] private GameObject gainedDisplay = null;
    [SerializeField] private GameObject newTrainingResult = null;
    [SerializeField] private Image arenaBackgroundContainer = null;

    private EnemyData enemy;
    private TrainingData newTrainingData;

    public override void SetResult(ResultDataBase result) {
        base.SetResult(result);
        enemy = result as EnemyData;
        arenaBackgroundContainer.sprite = enemy.ArenaImage;
    }

    private void OnEnable() {
        newTrainingData = null;
        newTrainingResult.SetActive(false);
        defeatedDisplay.SetActive(false);
        easyWinDisplay.SetActive(false);
        gainedDisplay.SetActive(false);
        backButton.gameObject.SetActive(false);
        provenSkillLevelContainer.gameObject.SetActive(false);
        slider.UpdateValues(0.05f, 0.05f, 0.05f);
        StartCoroutine(FightAnimation());
    }

    private IEnumerator FightAnimation() {
        yield return new WaitForSeconds(2.5f);

        ShowFightResult(enemy);

        yield return new WaitForSeconds(1.5f);

        if (newTrainingData != null) {
            newTrainingResult.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
        backButton.gameObject.SetActive(true);
    }

    public void ShowFightResult(EnemyData enemy) {
        slider.UpdateValues((playerCharacterController.Data.CurrentMoodLevel / 2.0f) / (float)enemy.TotalPoints,
            playerCharacterController.Data.StatPower.Level / (float)enemy.TotalPoints,
            playerCharacterController.Data.StatMagic.Level / (float)enemy.TotalPoints);

        // enemy too weak
        if (playerCharacterController.Data.ProvenLevel >= enemy.Level + 1) {
            easyWinDisplay.SetActive(true);
            playerCharacterController.LowerMoodBy(1);
            if (enemy.NewTrainingData != null) {
                newTrainingData = enemy.NewTrainingData;
            }
        }
        // enemy too strong
        else if (playerCharacterController.Data.TotalPoints + (playerCharacterController.Data.CurrentMoodLevel / 2.0f) < enemy.TotalPoints - 2) {
            defeatedDisplay.SetActive(true);
            playerCharacterController.LowerMoodBy(1);
        }
        // proven skill
        else {
            playerCharacterController.Data.ProvenLevel = Mathf.Min(playerCharacterController.Data.TotalPoints / 2, enemy.Level + 1);
            provenSkillLevelField.text = playerCharacterController.Data.ProvenLevel.ToString();
            gainedDisplay.SetActive(true);
            if (enemy.NewTrainingData != null) {
                newTrainingData = enemy.NewTrainingData;
            }
        }

        provenSkillLevelContainer.gameObject.SetActive(true);
        UpdateProvenSkillLevelDisplay();
    }

    private void UpdateProvenSkillLevelDisplay() {
        provenSkillLevelField.text = playerCharacterController.Data.ProvenLevel.ToString();
    }
}
