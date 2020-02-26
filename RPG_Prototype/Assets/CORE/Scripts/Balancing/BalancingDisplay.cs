using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingDisplay : MonoBehaviour {

    [SerializeField] private ActiveBoterkroonData worstCaseScenario;
    [SerializeField] private ActiveBoterkroonData bestCaseScenario;

    private void Awake() {
        ResetBalancing();
    }

    public void ResetBalancing() {
        worstCaseScenario = new ActiveBoterkroonData(BoterkroonValues.Values.TurnAmountStart);
        bestCaseScenario = new ActiveBoterkroonData(BoterkroonValues.Values.TurnAmountStart);
    }

    public ActiveBoterkroonData GetBoterkroon(BalanceCase balanceCase) {
        switch (balanceCase) {
            case BalanceCase.WorstCase:
                return worstCaseScenario;
            case BalanceCase.BestCase:
                return bestCaseScenario;
        }
        throw new NotImplementedException();
    }

    public void ApplyTraining(BoterkroonSkills skill, TrainingType type) {
        switch (type) {
            case TrainingType.Slow:
                worstCaseScenario.GetTrainingResultsFor(skill).Add(new BoterkroonTrainingResult(BoterkroonValues.Values.NormalTrainingMinXPGain));
                bestCaseScenario.GetTrainingResultsFor(skill).Add(new BoterkroonTrainingResult(BoterkroonValues.Values.NormalTrainingMaxXPGain));
                worstCaseScenario.TurnsLeft -= BoterkroonValues.Values.CostNormalTraining;
                bestCaseScenario.TurnsLeft -= BoterkroonValues.Values.CostNormalTraining;
                break;
            case TrainingType.Fast:
                float skillControl = Mathf.Max(0, GetSkillControl(worstCaseScenario, skill) - BoterkroonValues.Values.StartpointFastTrainingLerp);
                float skillControlLerpPoint = skillControl / (1 - BoterkroonValues.Values.StartpointFastTrainingLerp);
                int xpGain = Mathf.FloorToInt(Mathf.Lerp(BoterkroonValues.Values.FastTrainingMinXPGain, BoterkroonValues.Values.FastTrainingMaxXPGain, skillControlLerpPoint));
                worstCaseScenario.GetTrainingResultsFor(skill).Add(new BoterkroonTrainingResult(xpGain));
                worstCaseScenario.TurnsLeft -= BoterkroonValues.Values.CostFastTraining;

                skillControl = Mathf.Max(0, GetSkillControl(bestCaseScenario, skill) - BoterkroonValues.Values.StartpointFastTrainingLerp);
                skillControlLerpPoint = skillControl / (1 - BoterkroonValues.Values.StartpointFastTrainingLerp);
                xpGain = Mathf.FloorToInt(Mathf.Lerp(BoterkroonValues.Values.FastTrainingMinXPGain, BoterkroonValues.Values.FastTrainingMaxXPGain, skillControlLerpPoint));
                bestCaseScenario.GetTrainingResultsFor(skill).Add(new BoterkroonTrainingResult(xpGain));
                bestCaseScenario.TurnsLeft -= BoterkroonValues.Values.CostFastTraining;
                break;
        }
    }

    public void ApplyControlTest(BoterkroonSkills skill) {
        worstCaseScenario.CreateControlResult(skill);
        bestCaseScenario.CreateControlResult(skill);
        worstCaseScenario.TurnsLeft -= BoterkroonValues.Values.CostControlTest;
        bestCaseScenario.TurnsLeft -= BoterkroonValues.Values.CostControlTest;
    }

    public void ApplySkillTest(int level) {
        worstCaseScenario.CreateSkillTestResult(level);
        bestCaseScenario.CreateSkillTestResult(level);
    }

    private float GetSkillControl(ActiveBoterkroonData data, BoterkroonSkills currentskill) {
        int currentXPLevel = 0;
        foreach (var trainingResult in data.GetTrainingResultsFor(currentskill)) {
            currentXPLevel += trainingResult.GainedXP;
        }
        return (currentXPLevel / (float)BoterkroonValues.Values.MaxSkillXP);
    }
}
