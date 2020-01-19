using System.Collections;
using UnityEngine;
using Doozy.Engine;
using TMPro;
using System;
using UnityEngine.UI;

public class BrokerControlTestResult : UIDisplayController {

    [SerializeField] private Image visualContainer = null;
    [SerializeField] private TextMeshProUGUI textmeshStorytext = null;
    [SerializeField] private string uiEventStringDone = "";
    [SerializeField] private VisualSkillTest visualBaking = new VisualSkillTest();
    [SerializeField] private VisualSkillTest visualSword = new VisualSkillTest();
    [SerializeField] private VisualSkillTest visualNavigating = new VisualSkillTest();
    [SerializeField] private Button continueButton = null;

    private BoterkroonSkills currentSkill;
    private VisualSkillTest currentVisual;

    public void SetResult(BoterkroonSkills currentSkill) {
        this.currentSkill = currentSkill;
        this.currentVisual = GetVisual(currentSkill);
    }

    protected override void OnShowing() {
        SaveController.Instance.GameData.BoterKroon.CreateControlResult(currentSkill);
        SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostControlTest;
        textmeshStorytext.text = currentVisual.Text;
        visualContainer.sprite = currentVisual.Image;

        continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnContinueButtonClicked() {
        GameEventMessage.SendEvent(uiEventStringDone);
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {
        continueButton.onClick.RemoveListener(OnContinueButtonClicked);
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

}
