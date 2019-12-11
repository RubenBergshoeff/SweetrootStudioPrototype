using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.UI;
using System;
using TMPro;

public class BrokerSkillTestResult : UIDisplayController {

    [SerializeField] private TextMeshProUGUI textmeshSkillLevel = null;
    [SerializeField] private string uiEventStringDone = "";

    private int currentLevel;

    public void SetResult(int currentLevel) {
        this.currentLevel = currentLevel;
    }

    protected override void OnShowing() {
        textmeshSkillLevel.text = "Level " + currentLevel.ToString();
    }

    protected override void OnVisible() {
        CalculateResult();
        SaveController.Instance.GameData.BoterKroon.TurnsLeft -= 4;
        StartCoroutine(ControlTestAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {
    }

    private void CalculateResult() {
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;

        float currentScore = 0;
        float skillScore = 0;
        bool succeededTest = true;

        if (boterkroon.IsBakingLocked == false) {
            succeededTest = GetSkillScore(BoterkroonSkills.Baking, out skillScore);
            currentScore += skillScore;
        }
        if (succeededTest && boterkroon.IsSwordLocked == false) {
            succeededTest = GetSkillScore(BoterkroonSkills.Sword, out skillScore);
            currentScore += skillScore;
        }
        if (succeededTest && boterkroon.IsRoyalLocked == false) {
            succeededTest = GetSkillScore(BoterkroonSkills.Royal, out skillScore);
            currentScore += skillScore;
        }
        Debug.Log(currentScore);
        BoterkroonSkillResult result = new BoterkroonSkillResult(currentLevel, currentScore, succeededTest);
        boterkroon.SkillResults.Add(result);
    }

    private bool GetSkillScore(BoterkroonSkills skill, out float skillScore) {
        float maxScore = BoterkroonScoreRequirements.GetMaxScoreFor(currentLevel).Skill(skill);
        Debug.Log(skill.ToString() + maxScore.ToString());
        float minScore = BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Skill(skill);
        skillScore = Mathf.Min(maxScore, GetLastScoreFor(skill));
        return minScore <= skillScore;
    }

    private int GetLastScoreFor(BoterkroonSkills skill) {
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;
        if (boterkroon.HasNewTrainingFor(skill)) {
            boterkroon.CreateControlResult(skill);
        }
        var controlResults = boterkroon.GetControlResultsFor(skill);
        if (controlResults.Count == 0) {
            return 0;
        }
        foreach (var result in controlResults) {
            result.IsNew = false;
        }
        return controlResults[controlResults.Count - 1].TotalXP;
    }

    private IEnumerator ControlTestAnimation() {
        yield return new WaitForSeconds(3);

        GameEventMessage.SendEvent(uiEventStringDone);
    }

}
