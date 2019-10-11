using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ArenaBrokerController : MonoBehaviour {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;

    [SerializeField] private TextMeshProUGUI notEnoughFoodText = null;
    [SerializeField] private Button easyButton = null;
    [SerializeField] private Button mediumButton = null;
    [SerializeField] private Button hardButton = null;
    [SerializeField] private int foodCost = 2;

    private void OnEnable() {
        if (playerCharacterController.Data.CurrentFoodAmount < foodCost) {
            ShowNotEnoughtFood();
        } else {
            ShowOptions();
        }
    }

    private void ShowOptions() {
        notEnoughFoodText.gameObject.SetActive(false);

        if (playerCharacterController.Data.ProvenSkillLevel < 1) {
            easyButton.gameObject.SetActive(false);
        } else {
            easyButton.gameObject.SetActive(true);
            easyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy lvl " + (playerCharacterController.Data.ProvenSkillLevel - 1).ToString();
        }

        mediumButton.gameObject.SetActive(true);
        hardButton.gameObject.SetActive(true);
        mediumButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy lvl " + (playerCharacterController.Data.ProvenSkillLevel).ToString();
        hardButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy lvl " + (playerCharacterController.Data.ProvenSkillLevel + 2).ToString();
    }

    private void ShowNotEnoughtFood() {
        easyButton.gameObject.SetActive(false);
        mediumButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);

        notEnoughFoodText.gameObject.SetActive(true);
    }
}
