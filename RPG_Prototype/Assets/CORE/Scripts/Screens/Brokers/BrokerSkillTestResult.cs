using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.UI;
using System;

public class BrokerSkillTestResult : BrokerBaseResult {

    //[SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private SkillTestResult skillTestResult = null;
    [SerializeField] private string uiEventStringDone = "";
    [SerializeField] private SkillTestController[] skillTestControllers = new SkillTestController[0];

    private List<UnlockResult> newUnlocks = new List<UnlockResult>();

    private ActiveSkillCategory activeSkillCategory;
    private SkillTestController activeSkillTestController = null;

    public override void SetResult(ActiveBaseData activeResult) {
        base.SetResult(activeResult);
        activeSkillCategory = activeResult as ActiveSkillCategory;
    }

    protected override void OnVisible() {
        base.OnVisible();
        //    ResolveTests();
        //    StartCoroutine(ShowTestResults());
        StartTest();
    }

    private void StartTest() {
        activeSkillTestController = GetSkillTestController(activeSkillCategory.Data);
        if (activeSkillTestController == null) {
            throw new System.ArgumentException("No skilltest for category: " + activeSkillCategory.Category.Name);
        }
        activeSkillTestController.OnTestDone += OnTestDone;
        activeSkillTestController.StartSequence();
    }

    private void OnTestDone(SkillCategoryTestResult result) {
        activeSkillTestController.OnTestDone -= OnTestDone;
        activeSkillCategory.TestResults.Add(result);

        skillTestResult.SetResult(activeSkillCategory);
        GameEventMessage.SendEvent(uiEventStringDone);
    }

    private SkillTestController GetSkillTestController(BaseData data) {
        foreach (var controller in skillTestControllers) {
            if (controller.Target == data) {
                return controller;
            }
        }
        return null;
    }

    private void ResolveTests() {
        newUnlocks = new List<UnlockResult>();

        SkillCategoryTestResult result = new SkillCategoryTestResult();
        //result.Test = activeSkillCategory.SelectedTest;
        int score = activeSkillCategory.GetScore();
        //if (score < activeSkillCategory.SelectedTest.MinCategoryScore) {
        //    score = activeSkillCategory.LastResult == null ? 0 : activeSkillCategory.LastResult.Score;
        //}
        result.Score = score;// Mathf.Min(score, activeSkillCategory.SelectedTest.MaxCategoryScore);

        int previousScore = activeSkillCategory.LastResult == null ? 0 : activeSkillCategory.LastResult.Score;

        foreach (var potentialUnlock in activeSkillCategory.Category.PotentialUnlocks) {
            if (potentialUnlock.RequiredScore > previousScore && potentialUnlock.RequiredScore <= result.Score) {
                newUnlocks.Add(potentialUnlock);
            }
        }

        activeSkillCategory.TestResults.Add(result);

        for (int i = newUnlocks.Count - 1; i >= 0; i--) {
            UnlockReturn unlockReturn = SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.HandleUnlock(newUnlocks[i].ResultToUnlock);
            if (unlockReturn != UnlockReturn.NewUnlock) {
                newUnlocks.RemoveAt(i);
            }
        }
    }


    private IEnumerator ShowTestResults() {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("TestResult Score " + activeSkillCategory.LastResult.Score);

        if (newUnlocks.Count > 0) {
            foreach (var newUnlock in newUnlocks) {
                if (newUnlock.ShowPopup) {
                    Debug.Log("Unlocked " + newUnlock.ResultToUnlock.Name);
                    //ShowPopup(newUnlock);
                }
            }
        }

        //yield return new WaitForSeconds(0.5f);

        //skillTestResult.SetResult(activeSkillCategory, newUnlocks);
        //GameEventMessage.SendEvent(uiEventStringDone);
    }

    //private void ShowPopup(UnlockResult newUnlock) {
    //    UIPopup popup = UIPopup.GetPopup(Popups.UNLOCK_POPUP);

    //    if (popup == null) { return; }

    //    PopupUnlock popupUnlock = popup.GetComponent<PopupUnlock>();
    //    popupUnlock.Setup(newUnlock);

    //    popup.Show();
    //}
}
