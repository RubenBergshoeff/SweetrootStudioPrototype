using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Doozy.Engine;

public class BrokerSkillTest : UIDisplayController {

    [SerializeField] private BrokerSkillTestResult resultController = null;
    [SerializeField] private string uiEventString = "";

    [SerializeField] private OptionPointFlipOver flipoverLvl1 = null;
    [SerializeField] private OptionPointFlipOver flipoverLvl2 = null;
    [SerializeField] private OptionPointFlipOver flipoverLvl3 = null;

    protected override void OnShowing() {
        flipoverLvl1.ToStartPosition();
        flipoverLvl2.ToStartPosition();
        flipoverLvl3.ToStartPosition();

        flipoverLvl1.OnPointClicked += () => PickResult(1);
        flipoverLvl2.OnPointClicked += () => PickResult(2);
        flipoverLvl3.OnPointClicked += () => PickResult(3);
    }

    protected override void OnVisible() {
        flipoverLvl1.FlipUp();
        flipoverLvl2.FlipUp();
        flipoverLvl3.FlipUp();
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private void PickResult(int result) {
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }
}
