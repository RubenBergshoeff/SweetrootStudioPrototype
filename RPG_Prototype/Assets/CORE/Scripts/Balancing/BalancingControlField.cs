using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalancingControlField : MonoBehaviour {
    [SerializeField] private BalanceCase balanceCase = BalanceCase.BestCase;
    [SerializeField] private BoterkroonSkills targetSkill = BoterkroonSkills.Baking;
    [SerializeField] private TextMeshProUGUI controlField = null;
    private BalancingDisplay balancingDisplay = null;

    private void Awake() {
        balancingDisplay = GetComponentInParent<BalancingDisplay>();
    }

    private void Update() {
        controlField.text = GetSkillControl();
    }

    private string GetSkillControl() {
        var results = balancingDisplay.GetBoterkroon(balanceCase).GetControlResultsFor(targetSkill);
        if (results.Count == 0) {
            return "0";
        }
        else {
            return (results[results.Count - 1].TotalXP / 1000f).ToString();
        }
    }
}
