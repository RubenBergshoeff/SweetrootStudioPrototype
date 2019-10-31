using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BrokerSkillTestMain : UIDisplayController {
    private List<ActiveSkillCategory> activeSkillCategories {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.ActiveSkillCategories;
        }
    }

    [SerializeField] private BrokerSkillTestCategory categoryTemplate = null;
    [SerializeField] private Transform categoryContainer = null;

    protected override void OnVisible() {
        UpdateView();
    }

    protected override void OnInvisible() {
    }

    private void UpdateView() {
        categoryTemplate.gameObject.SetActive(true);
        for (int i = categoryContainer.childCount - 1; i >= 0; i--) {
            if (categoryContainer.GetChild(i).gameObject != categoryTemplate.gameObject) {
                Destroy(categoryContainer.GetChild(i).gameObject);
                categoryContainer.GetChild(i).SetParent(null);
            }
        }

        foreach (var item in activeSkillCategories) {
            AddItem(item);
        }

        categoryTemplate.gameObject.SetActive(false);
    }

    private void AddItem(ActiveSkillCategory item) {
        BrokerSkillTestCategory newCategory = Instantiate(categoryTemplate.gameObject, categoryContainer).GetComponent<BrokerSkillTestCategory>();
        newCategory.Setup(item);
    }
}
