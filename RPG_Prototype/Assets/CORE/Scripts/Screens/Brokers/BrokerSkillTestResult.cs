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
        StartCoroutine(ControlTestAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {
    }

    private void CalculateResult() {
        ActiveBoterkroonData boterkroon = SaveController.Instance.GameData.BoterKroon;

        float currentScore = 0;

        if (boterkroon.IsBakingLocked == false) {
            currentScore += GetLastScoreFor(BoterkroonSkills.Baking) / (float)boterkroon.MaxSkillXP;
        }
        if (boterkroon.IsSwordLocked == false) {
            currentScore += GetLastScoreFor(BoterkroonSkills.Sword) / (float)boterkroon.MaxSkillXP;
        }
        if (boterkroon.IsRoyalLocked == false) {
            currentScore += GetLastScoreFor(BoterkroonSkills.Royal) / (float)boterkroon.MaxSkillXP;
        }

        BoterkroonSkillResult result = new BoterkroonSkillResult(currentLevel, currentScore);
        boterkroon.SkillResults.Add(result);
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
