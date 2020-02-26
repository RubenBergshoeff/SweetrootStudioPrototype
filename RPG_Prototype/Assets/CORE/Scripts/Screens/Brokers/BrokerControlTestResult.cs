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
    }

    protected override void OnVisible() {
        StartCoroutine(ControlTestAnimation());
    }

    protected override void OnHiding() {

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

    private IEnumerator ControlTestAnimation() {
        yield return new WaitForSeconds(3);

        GameEventMessage.SendEvent(uiEventStringDone);
    }

}
