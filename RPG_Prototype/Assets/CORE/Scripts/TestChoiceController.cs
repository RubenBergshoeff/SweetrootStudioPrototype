using UnityEngine;
using System.Collections;
using System;

public class TestChoiceController : BaseSubController {
    [SerializeField] private ObjectClickTracker skillCatTest = null;
    [SerializeField] private ObjectClickTracker skillTest = null;

    private void OnEnable() {
        skillCatTest.OnObjectClicked += OnSkillCatTestClicked;
        skillTest.OnObjectClicked += OnSkillTestClicked;
    }

    private void OnSkillTestClicked() {
        GameController.GoToScreen(Screens.SkillTestChoice);
    }

    private void OnSkillCatTestClicked() {
        GameController.GoToScreen(Screens.SkillCatTestChoice);
    }

    private void OnDisable() {
        skillCatTest.OnObjectClicked -= OnSkillCatTestClicked;
        skillTest.OnObjectClicked -= OnSkillTestClicked;
    }
}
