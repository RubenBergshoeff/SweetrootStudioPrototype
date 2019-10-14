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
        moodSlider.SetValues(0, playerCharacterController.Data.CurrentMoodLevel / 3.0f);
        float powerProgress = GetProgressForStat(playerCharacterController.Data.StatPower);
        powerSlider.SetValues(0, powerProgress);
        powerLevelField.text = "level " + playerCharacterController.Data.StatPower.Level.ToString();
        float magicProgress = GetProgressForStat(playerCharacterController.Data.StatMagic);
        magicSlider.SetValues(0, magicProgress);
        magicLevelField.text = "level " + playerCharacterController.Data.StatMagic.Level.ToString();
    }

    private float GetProgressForStat(PlayerStat stat) {
        int relativeXPValue = stat.XP - PlayerStat.MinLevelXPs[stat.Level];
        int xpInLevel = PlayerStat.MinLevelXPs[stat.Level + 1] - PlayerStat.MinLevelXPs[stat.Level];
        return relativeXPValue / (float)xpInLevel;
    }
}
