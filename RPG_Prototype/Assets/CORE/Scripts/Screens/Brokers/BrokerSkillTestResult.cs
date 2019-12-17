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
    [SerializeField] private VisualsSkillTest visualsLvl01 = new VisualsSkillTest();
    [SerializeField] private VisualsSkillTest visualsLvl02 = new VisualsSkillTest();
    [SerializeField] private VisualsSkillTest visualsLvl03 = new VisualsSkillTest();

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
        currentResult = SaveController.Instance.GameData.BoterKroon.CreateSkillTestResult(currentLevel);
        SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostSkillTest;
        StartCoroutine(ControlTestAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

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
    public string[] Lines = new string[0];
}