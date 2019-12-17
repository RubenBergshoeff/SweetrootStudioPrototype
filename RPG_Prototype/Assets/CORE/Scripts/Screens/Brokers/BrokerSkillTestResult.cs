using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.UI;
using System;
using TMPro;
using UnityEngine.UI;

public class BrokerSkillTestResult : UIDisplayController {

    public bool IsDone { get; private set; }
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
    private VisualSkillTest currentVisual;

    private bool introductionFinished = false;
    private int lineItterator = 0;
    private bool showStory = false;
    private bool skipWaitTime = false;
    private float lastVisualChangeTime = 0;

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
        currentVisual = currentVisuals.Introduction;
        lineItterator = 0;
        UpdateVisual();
    }

    protected override void OnVisible() {
        currentResult = SaveController.Instance.GameData.BoterKroon.CreateSkillTestResult(currentLevel);
        SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostSkillTest;

        showStory = true;
        //StartCoroutine(ControlTestAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private void Update() {
        if (showStory == false || IsDone) { return; }

        if (Input.GetMouseButtonDown(0)) {
            skipWaitTime = true;
        }

        if (/*lastVisualChangeTime + 5 > Time.time &&*/ skipWaitTime == false) {
            return;
        }

        skipWaitTime = false;

        if (lineItterator != 0) {
            UpdateVisual();
        }
        else {
            if (introductionFinished == false) {
                introductionFinished = true;
                float normalizedScore = (currentResult.Score - BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Total) / (BoterkroonScoreRequirements.GetMaxScoreFor(currentLevel).Total - BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Total);

                if (normalizedScore < 0.3f) {
                    currentVisual = (currentVisuals.Failed);
                    UpdateVisual();
                }
                else {
                    currentVisual = (currentVisuals.Succeeded);
                    UpdateVisual();
                }
            }
            else {
                IsDone = true;
                GameEventMessage.SendEvent(uiEventStringDone);
            }
        }
    }

    private void UpdateVisual() {
        SetVisual(currentVisual, lineItterator);
        lineItterator++;
        if (currentVisual.Lines.Length <= lineItterator) {
            lineItterator = 0;
        }
        lastVisualChangeTime = Time.time;
    }

    //private IEnumerator ControlTestAnimation() {
    //    yield return new WaitForSeconds(5);

    //    float normalizedScore = (currentResult.Score - BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Total) / (BoterkroonScoreRequirements.GetMaxScoreFor(currentLevel).Total - BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Total);

    //    if (normalizedScore < 0.3f) {
    //        SetVisual(currentVisuals.Failed);
    //    }
    //    else {
    //        SetVisual(currentVisuals.Succeeded);
    //    }

    //    yield return new WaitForSeconds(5);

    //    GameEventMessage.SendEvent(uiEventStringDone);
    //}

    private void SetVisual(VisualSkillTest visual, int line) {
        visualContainer.sprite = visual.Image;
        textmeshVisual.text = visual.Lines[line];
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