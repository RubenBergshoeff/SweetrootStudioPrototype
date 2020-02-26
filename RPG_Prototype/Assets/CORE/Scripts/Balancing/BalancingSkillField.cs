using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalancingSkillField : MonoBehaviour {
    [SerializeField] private BalanceCase balanceCase = BalanceCase.BestCase;
    [SerializeField] private GameObject failedIndicator = null;
    [SerializeField] private TextMeshProUGUI skillField = null;
    private BalancingDisplay balancingDisplay = null;

    private void Awake() {
        balancingDisplay = GetComponentInParent<BalancingDisplay>();
    }

    private void Update() {
        skillField.text = GetSkillScore();
    }

    private string GetSkillScore() {
        failedIndicator.SetActive(false);
        var results = balancingDisplay.GetBoterkroon(balanceCase).SkillResults;
        for (int i = results.Count - 1; i >= 0; i--) {
            if (results[i].Succeeded) {
                return (results[i].Score / 1000f).ToString();
            }
            else {
                failedIndicator.SetActive(true);
            }
        }
        return "0";
    }
}
