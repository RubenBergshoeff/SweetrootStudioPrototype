using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TrainingGameResult : UIDisplayController {

    [SerializeField] private TextMeshProUGUI skillNameField = null;
    [SerializeField] private TextMeshProUGUI xpAmountField = null;
    [SerializeField] private Image skillSpriteImage = null;

    protected override void OnVisible() {

    }

    protected override void OnInvisible() {

    }

    public void SetResult(ActiveTraining activeTraining, int gainedXP) {
        skillNameField.text = activeTraining.Training.Name;
        skillSpriteImage.sprite = activeTraining.Training.Visual;
        xpAmountField.text = gainedXP.ToString();
    }

    protected override void OnShowing() {

    }

    protected override void OnHiding() {

    }
}
