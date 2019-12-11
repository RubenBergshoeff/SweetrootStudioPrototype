using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.UI;
using System;
using TMPro;
using UnityEngine.UI;

public class BrokerSkillTestResult : UIDisplayController {

    [SerializeField] private Image visualContainer = null;
    [SerializeField] private TextMeshProUGUI textmeshVisual = null;

    [SerializeField] private TextMeshProUGUI textmeshSkillLevel = null;
    [SerializeField] private string uiEventStringDone = "";
    [Space]
    [Header("Skill Test Visuals")]
    [SerializeField] private VisualsSkillTest visualsLvl01;
    [SerializeField] private VisualsSkillTest visualsLvl02;
    [SerializeField] private VisualsSkillTest visualsLvl03;

    private int currentLevel;
    private BoterkroonSkillResult currentResult = null;
    private VisualsSkillTest currentVisuals;

    public void SetResult(int currentLevel) {
        this.currentLevel = currentLevel;
        if (currentLevel == 1) {
            currentVisuals = visualsLvl01;
        }
        if (currentLevel == 2) {
            currentVisuals = visualsLvl02;
        }
        if (currentLevel == 3) {
            currentVisuals = visualsLvl03;
        }
    }

    protected override void OnShowing() {
        textmeshSkillLevel.text = "Level " + currentLevel.ToString();
        SetVisual(currentVisuals.Introduction);
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
        currentResult = new BoterkroonSkillResult(currentLevel, currentScore, succeededTest);
        boterkroon.SkillResults.Add(currentResult);
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
        yield return new WaitForSeconds(5);

        float normalizedScore = (currentResult.Score - BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Total) / (BoterkroonScoreRequirements.GetMaxScoreFor(currentLevel).Total - BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Total);

        if (normalizedScore < 0.3f) {
            SetVisual(currentVisuals.Failed);
        }
        else {
            SetVisual(currentVisuals.Succeeded);
        }

        yield return new WaitForSeconds(5);

        GameEventMessage.SendEvent(uiEventStringDone);
    }

    private void SetVisual(VisualSkillTest visual) {
        visualContainer.sprite = visual.Image;
        textmeshVisual.text = visual.Text;
    }
}

[System.Serializable]
public class VisualsSkillTest {
    public VisualSkillTest Introduction = null;
    public VisualSkillTest Failed = null;
    public VisualSkillTest Succeeded = null;
}

[System.Serializable]
public class VisualSkillTest {
    public Sprite Image = null;
    public string Text = "";
}