using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TrainingGameResult : UIDisplayController {

    [SerializeField] private TextMeshProUGUI skillNameField = null;
    [SerializeField] private TextMeshProUGUI xpAmountField = null;
    [SerializeField] private Image skillSpriteImage = null;

    private TrainingGameResultFeedbackData feedbackData;

    protected override void OnVisible() {

    }

    protected override void OnInvisible() {

    }

    public void SetResult(ActiveTraining activeTraining, int gainedXP, TrainingGameResultFeedbackData data) {
        skillNameField.text = activeTraining.Training.Name;
        skillSpriteImage.sprite = activeTraining.Training.Visual;
        xpAmountField.text = gainedXP.ToString();
        feedbackData = data;
    }

    protected override void OnShowing() {

    }

    protected override void OnHiding() {

    }
}
