using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ArenaController : MonoBehaviour {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private TextMeshProUGUI provenSkillLevelField = null;

    [SerializeField] private UnityEvent easyWin = null;
    [SerializeField] private UnityEvent defeated = null;
    [SerializeField] private UnityEvent newProvenPowerLvl = null;

    public void StartFight(int relativeEnemySkillLevel) {
        int enemySkillLevel = playerCharacterController.Data.ProvenSkillLevel + relativeEnemySkillLevel;
        // enemy too weak
        if (playerCharacterController.Data.ProvenSkillLevel >= enemySkillLevel + 1) {
            easyWin.Invoke();
            UpdateProvenSkillLevelDisplay();
            return;
        }
        // enemy too strong
        if (playerCharacterController.Data.PotentialSkillLevel < enemySkillLevel - 1) {
            defeated.Invoke();
            UpdateProvenSkillLevelDisplay();
            return;
        }

        // proven new skill
        playerCharacterController.Data.ProvenSkillLevel = Mathf.Min(playerCharacterController.Data.PotentialSkillLevel, enemySkillLevel + 1);
        newProvenPowerLvl.Invoke();
        UpdateProvenSkillLevelDisplay();
    }

    private void UpdateProvenSkillLevelDisplay() {
        provenSkillLevelField.text = playerCharacterController.Data.ProvenSkillLevel.ToString();
    }
}
