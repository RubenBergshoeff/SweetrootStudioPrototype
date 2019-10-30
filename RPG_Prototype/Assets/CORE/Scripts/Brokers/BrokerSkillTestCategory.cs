using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BrokerSkillTestCategory : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI nameField = null;
    [SerializeField] private TMPro.TextMeshProUGUI testLevelField = null;
    [SerializeField] private Button levelUpButton = null;
    [SerializeField] private Button levelDownButton = null;

    private ActiveSkillCategory activeSkillCategory = null;
    private int selectedIndex = 0;

    public void Setup(ActiveSkillCategory activeSkillCategory) {
        this.activeSkillCategory = activeSkillCategory;
        UpdateView();
        UpdateSelection();
    }

    private void OnEnable() {
        levelUpButton.onClick.AddListener(OnTestLevelUpButton);
        levelDownButton.onClick.AddListener(OnTestLevelDownButton);
    }

    private void OnDisable() {
        levelUpButton.onClick.RemoveListener(OnTestLevelUpButton);
        levelDownButton.onClick.RemoveListener(OnTestLevelDownButton);
    }

    public void OnTestLevelUpButton() {
        selectedIndex++;
        UpdateView();
        UpdateSelection();
    }

    public void OnTestLevelDownButton() {
        selectedIndex--;
        UpdateView();
        UpdateSelection();
    }

    private void UpdateSelection() {
        activeSkillCategory.SelectedTest = activeSkillCategory.Category.Tests[selectedIndex];
    }

    private void UpdateView() {
        selectedIndex = 0;
        if (activeSkillCategory.LastResult != null) {
            for (int i = 0; i < activeSkillCategory.Category.Tests.Count; i++) {
                if (activeSkillCategory.Category.Tests[i] == activeSkillCategory.LastResult.Test) {
                    selectedIndex = i;
                    break;
                }
            }
        }
        nameField.text = activeSkillCategory.Category.Name;
        testLevelField.text = activeSkillCategory.Category.Tests[selectedIndex].Level.ToString();
        levelDownButton.interactable = false;
        levelUpButton.interactable = false;
        if (selectedIndex > 0) {
            levelDownButton.interactable = true;
        }
        if (selectedIndex < activeSkillCategory.Category.Tests.Count - 1) {
            levelUpButton.interactable = true;
        }
    }
}
