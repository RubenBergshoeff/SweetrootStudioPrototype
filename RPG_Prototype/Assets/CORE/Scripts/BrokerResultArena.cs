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
    [SerializeField] private Button backButton = null;
    [SerializeField] private GameObject defeatedDisplay = null;
    [SerializeField] private GameObject easyWinDisplay = null;
    [SerializeField] private GameObject gainedDisplay = null;

    private EnemyData enemy;

    public override void SetResult(ResultDataBase result) {
        base.SetResult(result);
        enemy = result as EnemyData;
    }

    private void OnEnable() {
        defeatedDisplay.SetActive(false);
        easyWinDisplay.SetActive(false);
        gainedDisplay.SetActive(false);
        backButton.gameObject.SetActive(false);
        provenSkillLevelContainer.gameObject.SetActive(false);
        StartCoroutine(FightAnimation());
    }

    private IEnumerator FightAnimation() {
        yield return new WaitForSeconds(2.5f);

        ShowFightResult(enemy);

        yield return new WaitForSeconds(1.5f);

        backButton.gameObject.SetActive(true);
    }

    public void ShowFightResult(EnemyData enemy) {
        // enemy too weak
        if (playerCharacterController.Data.ProvenLevel >= enemy.Level + 1) {
            easyWinDisplay.SetActive(true);
            playerCharacterController.LowerMoodBy(1);
        }
        // enemy too strong
        else if (playerCharacterController.Data.PotentialLevel < enemy.Level - 1) {
            defeatedDisplay.SetActive(true);
            playerCharacterController.LowerMoodBy(1);
        }
        // proven skill
        else {
            playerCharacterController.Data.ProvenLevel = Mathf.Min(playerCharacterController.Data.PotentialLevel, enemy.Level + 1);
            gainedDisplay.SetActive(true);
        }

        provenSkillLevelContainer.gameObject.SetActive(true);
        UpdateProvenSkillLevelDisplay();
    }

    private void UpdateProvenSkillLevelDisplay() {
        provenSkillLevelField.text = playerCharacterController.Data.ProvenLevel.ToString();
    }
}
