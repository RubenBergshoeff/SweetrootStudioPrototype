using UnityEngine;
using Doozy.Engine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.UI;

public class BrokerTrainingResult : UIDisplayController {

    [SerializeField] private Image visualContainer = null;
    [SerializeField] private TextMeshProUGUI textmeshStorytext = null;
    [SerializeField] private Button continueButton = null;
    [SerializeField] private string uiEventStringDone = "";
    [SerializeField] private VisualSkillTest visualBaking = new VisualSkillTest();
    [SerializeField] private VisualSkillTest visualSword = new VisualSkillTest();
    [SerializeField] private VisualSkillTest visualNavigating = new VisualSkillTest();

    private BoterkroonSkills currentskill;
    private TrainingType currentType;
    private VisualSkillTest currentVisual;

    public void SetResult(BoterkroonSkills result, TrainingType trainingType) {
        this.currentskill = result;
        this.currentType = trainingType;
        this.currentVisual = GetVisual(result);
    }

    protected override void OnShowing() {
        CreateTrainingResult();
        textmeshStorytext.text = currentVisual.Lines[0];
        visualContainer.sprite = currentVisual.Image;
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void OnContinueClicked() {
        GameEventMessage.SendEvent(uiEventStringDone);
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {
        continueButton.onClick.RemoveListener(OnContinueClicked);
    }

    protected override void OnInvisible() {

    }

    private VisualSkillTest GetVisual(BoterkroonSkills result) {
        switch (result) {
            case BoterkroonSkills.Baking:
                return visualBaking;
            case BoterkroonSkills.Sword:
                return visualSword;
            case BoterkroonSkills.Research:
                return visualNavigating;
        }
        throw new NotImplementedException();
    }

    private void CreateTrainingResult() {
        int xpGain = 0;
        switch (currentType) {
            case TrainingType.Slow:
                xpGain = UnityEngine.Random.Range(BoterkroonValues.Values.NormalTrainingMinXPGain, BoterkroonValues.Values.NormalTrainingMaxXPGain);
                SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostNormalTraining;
                break;
            case TrainingType.Fast:
                float skillControl = Mathf.Max(0, GetSkillControl() - BoterkroonValues.Values.StartpointFastTrainingLerp);
                float skillControlLerpPoint = skillControl / (1 - BoterkroonValues.Values.StartpointFastTrainingLerp);
                xpGain = Mathf.FloorToInt(Mathf.Lerp(BoterkroonValues.Values.FastTrainingMinXPGain, BoterkroonValues.Values.FastTrainingMaxXPGain, skillControlLerpPoint));
                SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostFastTraining;
                break;
        }
        BoterkroonTrainingResult result = new BoterkroonTrainingResult(xpGain);
        SaveController.Instance.GameData.BoterKroon.GetTrainingResultsFor(currentskill).Add(result);
    }

    private float GetSkillControl() {
        int currentXPLevel = 0;
        foreach (var trainingResult in SaveController.Instance.GameData.BoterKroon.GetTrainingResultsFor(currentskill)) {
            currentXPLevel += trainingResult.GainedXP;
        }
        return currentXPLevel / BoterkroonValues.Values.MaxSkillXP;
    }
}
