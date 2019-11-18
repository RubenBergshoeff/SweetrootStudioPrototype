using UnityEngine;
using System.Collections;
using System;
using Doozy.Engine;

public class TestChoiceController : UIDisplayController {
    [SerializeField] private ObjectClickTracker skillCatTest = null;
    [SerializeField] private ObjectClickTracker skillTest = null;
    [SerializeField] private string uiEventStringToSkillCatTest = "";
    [SerializeField] private string uiEventStringToSkillTest = "";

    protected override void OnVisible() {
        skillCatTest.OnObjectClicked += OnSkillCatTestClicked;
        skillTest.OnObjectClicked += OnSkillTestClicked;
    }

    protected override void OnInvisible() {
        skillCatTest.OnObjectClicked -= OnSkillCatTestClicked;
        skillTest.OnObjectClicked -= OnSkillTestClicked;
    }

    private void OnSkillCatTestClicked() {
        GameEventMessage.SendEvent(uiEventStringToSkillCatTest);
    }

    private void OnSkillTestClicked() {
        GameEventMessage.SendEvent(uiEventStringToSkillTest);
    }
}
