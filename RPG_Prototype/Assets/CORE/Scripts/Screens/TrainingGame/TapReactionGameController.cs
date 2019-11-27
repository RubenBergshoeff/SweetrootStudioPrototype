using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class TapReactionGameController : TrainingGameController {

    [SerializeField] private float maxGametime = 30;
    [SerializeField] private float minWaitTime = 2;
    [SerializeField] private float maxWaitTime = 5;
    [SerializeField] private float allowedReactionTime = 3f;
    [SerializeField] private int gainedXPPerCorrectItem = 1;
    [SerializeField] private int gainedXPPerFailedItem = 10;
    [SerializeField] private Button doughBowlButton = null;
    [SerializeField] private List<TapReactionItem> reactionItems = new List<TapReactionItem>();

    private ActiveTraining activeTraining = null;
    private bool isTrainingRunning = false;
    private float trainingRunTime = 0;
    private int itemsDone = 0;
    private int itemsCorrect = 0;
    private int itemsEarly = 0;
    private int itemsLate = 0;

    public override void Setup(ActiveTraining training) {
        activeTraining = training;
        trainingRunTime = 0;
        itemsDone = 0;
        itemsCorrect = 0;
        itemsLate = 0;
        itemsEarly = 0;
        doughBowlButton.onClick.AddListener(TryAddItem);
        foreach (TapReactionItem item in reactionItems) {
            item.Disable();
        }
    }

    private void TryAddItem() {
        foreach (TapReactionItem item in reactionItems) {
            if (item.IsActive == false) {
                item.Setup(minWaitTime, maxWaitTime, allowedReactionTime);
                item.OnTapped += OnItemTapped;
                return;
            }
        }
    }

    public override void StartTraining() {
        isTrainingRunning = true;
    }

    private void OnItemTapped(TapReactionItem item, TapReactionState state) {
        switch (state) {
            case TapReactionState.Early:
                OnXPGain?.Invoke(gainedXPPerFailedItem);
                itemsEarly++;
                break;
            case TapReactionState.Correct:
                OnXPGain?.Invoke(gainedXPPerCorrectItem);
                itemsCorrect++;
                break;
            case TapReactionState.Late:
                OnXPGain?.Invoke(gainedXPPerFailedItem);
                itemsLate++;
                break;
        }
        item.OnTapped -= OnItemTapped;
    }

    public override void Cleanup() {
        isTrainingRunning = false;
        doughBowlButton.onClick.RemoveListener(TryAddItem);
        foreach (TapReactionItem item in reactionItems) {
            item.Disable();
        }
    }

    private void Update() {
        if (isTrainingRunning == false) { return; }

        trainingRunTime += Time.deltaTime;

        if (trainingRunTime > maxGametime) {
            TapReactionFeedbackData feedbackData = new TapReactionFeedbackData();
            feedbackData.CookiesBurnt = itemsLate;
            feedbackData.CookiesCorrect = itemsCorrect;
            feedbackData.CookiesRaw = itemsEarly;
            isTrainingRunning = false;
            OnGameFinished?.Invoke(feedbackData);
        }
    }
}

public class TapReactionFeedbackData : TrainingGameResultFeedbackData {
    public int CookiesBurnt = 0;
    public int CookiesCorrect = 0;
    public int CookiesRaw = 0;
}