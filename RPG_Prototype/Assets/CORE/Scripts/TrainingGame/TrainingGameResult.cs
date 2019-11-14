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

}
