using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalancingTrainSkillButton : MonoBehaviour {
    [SerializeField] private BoterkroonSkills targetSkill = BoterkroonSkills.Baking;
    [SerializeField] private TrainingType trainingType = TrainingType.Slow;

    private BalancingDisplay balancingDisplay = null;

    private void OnEnable() {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        balancingDisplay = GetComponentInParent<BalancingDisplay>();
    }

    private void OnButtonClicked() {
        balancingDisplay.ApplyTraining(targetSkill, trainingType);
    }

    private void OnDisable() {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }
}
