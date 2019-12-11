using System.Collections;
using UnityEngine;
using Doozy.Engine;
using TMPro;
using System;

public class BrokerControlTestResult : UIDisplayController {

    [SerializeField] private TextMeshProUGUI textmeshSkillName = null;
    [SerializeField] private string uiEventStringDone = "";

    private BoterkroonSkills currentSkill;

    public void SetResult(BoterkroonSkills currentSkill) {
        this.currentSkill = currentSkill;
    }

    protected override void OnShowing() {
        textmeshSkillName.text = currentSkill.ToString();
    }

    protected override void OnVisible() {
        SaveController.Instance.GameData.BoterKroon.CreateControlResult(currentSkill);
        SaveController.Instance.GameData.BoterKroon.TurnsLeft -= 1;
        StartCoroutine(ControlTestAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {
    }

    private IEnumerator ControlTestAnimation() {
        yield return new WaitForSeconds(3);

        GameEventMessage.SendEvent(uiEventStringDone);
    }

}
