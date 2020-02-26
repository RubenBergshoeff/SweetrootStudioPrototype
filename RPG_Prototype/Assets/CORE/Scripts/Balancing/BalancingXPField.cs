using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum BalanceCase {
    WorstCase,
    BestCase
}

public class BalancingXPField : MonoBehaviour {
    [SerializeField] private GameObject lockedIndicator = null;
    [SerializeField] private BalanceCase balanceCase = BalanceCase.BestCase;
    [SerializeField] private BoterkroonSkills targetSkill = BoterkroonSkills.Baking;
    [SerializeField] private TextMeshProUGUI xpField = null;
    private BalancingDisplay balancingDisplay = null;

    private void Awake() {
        balancingDisplay = GetComponentInParent<BalancingDisplay>();
    }

    private void Update() {
        lockedIndicator.SetActive(!balancingDisplay.GetBoterkroon(balanceCase).IsSkillActive(targetSkill));
        xpField.text = GetSkillXP();
    }

    private string GetSkillXP() {
        int totalXP = 0;
        foreach (var item in balancingDisplay.GetBoterkroon(balanceCase).GetTrainingResultsFor(targetSkill)) {
            totalXP += item.GainedXP;
        }
        return totalXP.ToString();
    }
}
