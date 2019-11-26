using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TapReactionGameController : TrainingGameController {

    private float maxGametime {
        get {
            return maxWaitTime + allowedReactionTime + 1;
        }
    }

    [SerializeField] private float minWaitTime = 6;
    [SerializeField] private float maxWaitTime = 22;
    [SerializeField] private float allowedReactionTime = 4f;
    [SerializeField] private int gainedXPPerItem = 10;
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
        foreach (TapReactionItem item in reactionItems) {
            item.Setup(minWaitTime, maxWaitTime, allowedReactionTime);
            item.OnTapped += OnItemTapped;
            item.OnTimedOut += OnItemTimedOut;
        }
    }

    private void OnItemTimedOut(TapReactionItem item) {
        itemsDone++;
        itemsLate++;
    }

    private void OnItemTapped(TapReactionItem item) {
        if (item.IsCorrect) {
            OnXPGain?.Invoke(gainedXPPerItem);
            itemsDone++;
            itemsCorrect++;
        }
        else if (item.IsCorrect == false && item.IsTimedOut == false) {
            itemsDone++;
            itemsEarly++;
        }
    }

    public override void StartTraining() {
        isTrainingRunning = true;
    }

    public override void Cleanup() {
        isTrainingRunning = false;
    }

    private void Update() {
        if (isTrainingRunning == false) { return; }

        trainingRunTime += Time.deltaTime;

        foreach (TapReactionItem item in reactionItems) {
            item.UpdateItem(Time.deltaTime);
        }

        if (trainingRunTime > maxGametime || itemsDone == reactionItems.Count) {
            TapReactionFeedbackData feedbackData = new TapReactionFeedbackData();
            feedbackData.CookiesBurnt = itemsLate;
            feedbackData.CookiesCorrect = itemsCorrect;
            feedbackData.CookiesRaw = itemsEarly;
            OnGameFinished?.Invoke(feedbackData);
            isTrainingRunning = false;
        }
    }
}

public class TapReactionFeedbackData : TrainingGameResultFeedbackData {
    public int CookiesBurnt = 0;
    public int CookiesCorrect = 0;
    public int CookiesRaw = 0;
}