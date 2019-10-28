using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScreenController : MonoBehaviour {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private FillSlider moodSlider = null;
    [SerializeField] private FillSlider powerSlider = null;
    [SerializeField] private TextMeshProUGUI powerLevelField = null;
    [SerializeField] private FillSlider magicSlider = null;
    [SerializeField] private TextMeshProUGUI magicLevelField = null;

    private void OnEnable() {
        moodSlider.SetValues(0, playerCharacterController.LevelMood / 3.0f);
        float powerProgress = GetProgressForStat(playerCharacterController.LevelPower, playerCharacterController.XPPower);
        powerSlider.SetValues(0, powerProgress);
        powerLevelField.text = "level " + playerCharacterController.LevelPower.ToString();
        float magicProgress = GetProgressForStat(playerCharacterController.LevelMagic, playerCharacterController.XPMagic);
        magicSlider.SetValues(0, magicProgress);
        magicLevelField.text = "level " + playerCharacterController.LevelMagic.ToString();
    }

    private float GetProgressForStat(int level, int xp) {
        int relativeXPValue = xp - PlayerStat.MinLevelXPs[level];
        int xpInLevel = PlayerStat.MinLevelXPs[level + 1] - PlayerStat.MinLevelXPs[level];
        return relativeXPValue / (float)xpInLevel;
    }
}
