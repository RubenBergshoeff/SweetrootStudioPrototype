using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.UI;

public class BrokerSkillTestResult : UIDisplayController {
    private List<ActiveSkillCategory> activeSkillCategories {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.ActiveSkillCategories;
        }
    }

    [SerializeField] private UnityEngine.UI.Button backButton = null;
    [SerializeField] private BrokerSkillTestResultCategory categoryTemplate = null;
    [SerializeField] private Transform categoryContainer = null;

    private List<BrokerSkillTestResultCategory> categoryResults = new List<BrokerSkillTestResultCategory>();
    private List<UnlockResult> newUnlocks = new List<UnlockResult>();

    protected override void OnVisible() {
        StartCoroutine(ShowTestResults());
    }

    protected override void OnInvisible() {
    }

    protected override void OnShowing() {
        Cleanup();
        ResolveTests();
        CreateNewCategoryResults();
        backButton.gameObject.SetActive(false);
    }

    protected override void OnHiding() {
    }

    private void ResolveTests() {
        newUnlocks = new List<UnlockResult>();
        foreach (var activeSkillCategory in activeSkillCategories) {
            SkillCategoryTestResult result = new SkillCategoryTestResult();
            result.Test = activeSkillCategory.SelectedTest;
            int score = activeSkillCategory.GetScore();
            if (score < activeSkillCategory.SelectedTest.MinCategoryScore) {
                score = activeSkillCategory.LastResult == null ? 0 : activeSkillCategory.LastResult.Score;
            }
            result.Score = Mathf.Min(score, activeSkillCategory.SelectedTest.MaxCategoryScore);

            int previousScore = activeSkillCategory.LastResult == null ? 0 : activeSkillCategory.LastResult.Score;

            foreach (var potentialUnlock in activeSkillCategory.Category.PotentialUnlocks) {
                if (potentialUnlock.RequiredScore > previousScore && potentialUnlock.RequiredScore <= result.Score) {
                    newUnlocks.Add(potentialUnlock);
                }
            }

            activeSkillCategory.TestResults.Add(result);
        }

        for (int i = newUnlocks.Count - 1; i >= 0; i--) {
            UnlockReturn unlockReturn = SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.HandleUnlock(newUnlocks[i].ResultToUnlock);
            if (unlockReturn != UnlockReturn.NewUnlock) {
                newUnlocks.RemoveAt(i);
            }
        }
    }

    private void Cleanup() {
        for (int i = categoryResults.Count - 1; i >= 0; i--) {
            Destroy(categoryResults[i].gameObject);
            categoryResults[i].transform.SetParent(null);
            categoryResults.RemoveAt(i);
        }
    }

    private void CreateNewCategoryResults() {
        categoryTemplate.gameObject.SetActive(true);
        foreach (var item in activeSkillCategories) {
            AddItem(item);
        }
        categoryTemplate.gameObject.SetActive(false);
    }

    private void AddItem(ActiveSkillCategory item) {
        BrokerSkillTestResultCategory newCategory = Instantiate(categoryTemplate.gameObject, categoryContainer).GetComponent<BrokerSkillTestResultCategory>();
        newCategory.Setup(item);
        categoryResults.Add(newCategory);
    }

    private IEnumerator ShowTestResults() {
        yield return new WaitForSeconds(0.5f);

        foreach (var categoryResult in categoryResults) {
            categoryResult.Animate(1f);
            yield return new WaitForSeconds(1.2f);
        }

        if (newUnlocks.Count > 0) {
            foreach (var newUnlock in newUnlocks) {
                if (newUnlock.ShowPopup) {
                    ShowPopup(newUnlock);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        backButton.gameObject.SetActive(true);
    }

    private void ShowPopup(UnlockResult newUnlock) {
        UIPopup popup = UIPopup.GetPopup(Popups.UNLOCK_POPUP);

        if (popup == null) { return; }

        PopupUnlock popupUnlock = popup.GetComponent<PopupUnlock>();
        popupUnlock.Setup(newUnlock);

        popup.Show();
    }
}
