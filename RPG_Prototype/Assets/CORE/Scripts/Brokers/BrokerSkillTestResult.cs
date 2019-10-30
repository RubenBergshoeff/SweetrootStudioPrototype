using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BrokerSkillTestResult : MonoBehaviour {
    private List<ActiveSkillCategory> activeSkillCategories {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.ActiveSkillCategories;
        }
    }

    [SerializeField] private BrokerSkillTestResultCategory categoryTemplate = null;
    [SerializeField] private Transform categoryContainer = null;

    private List<BrokerSkillTestResultCategory> categoryResults = new List<BrokerSkillTestResultCategory>();

    private void OnEnable() {
        Cleanup();
        CreateNewCategoryResults();
        StartCoroutine(ShowTestResults());
    }

    private void Cleanup() {
        for (int i = categoryResults.Count - 1; i >= 0; i--) {
            Destroy(categoryResults[i].gameObject);
            categoryResults[i].transform.SetParent(null);
            categoryResults.RemoveAt(i);
        }
    }

    private void CreateNewCategoryResults() {
        foreach (var item in activeSkillCategories) {
            AddItem(item);
        }
    }

    private void AddItem(ActiveSkillCategory item) {
        BrokerSkillTestResultCategory newCategory = Instantiate(categoryTemplate.gameObject, categoryContainer).GetComponent<BrokerSkillTestResultCategory>();
        newCategory.Setup(item);
        categoryResults.Add(newCategory);
    }

    private IEnumerator ShowTestResults() {
        yield return new WaitForSeconds(0.5f);
        // create while loop where every second a new category bar is filled until all have been filled, then enable back button
    }
}
