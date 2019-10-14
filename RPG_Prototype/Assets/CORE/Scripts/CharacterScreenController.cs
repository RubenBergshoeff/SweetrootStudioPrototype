using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScreenController : MonoBehaviour {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private TextMeshProUGUI provenPowerLvlField = null;
    [SerializeField] private TextMeshProUGUI foodLvlField = null;

    private void OnEnable() {
        provenPowerLvlField.text = playerCharacterController.Data.ProvenLevel.ToString();
        foodLvlField.text = playerCharacterController.Data.CurrentMoodLevel.ToString();
    }
}
