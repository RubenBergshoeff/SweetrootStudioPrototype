using UnityEngine;
using System.Collections;
using TMPro;

public class TapReactionGameFeedback : TrainingGameResultFeedback {

    [SerializeField] private TextMeshProUGUI textMeshRawCookie = null;
    [SerializeField] private TextMeshProUGUI textMeshCorrectCookie = null;
    [SerializeField] private TextMeshProUGUI textMeshBurntCookie = null;

    public override void Setup(TrainingGameResultFeedbackData feedbackData) {
        TapReactionFeedbackData feedbackDataTapReaction = feedbackData as TapReactionFeedbackData;
        textMeshRawCookie.text = feedbackDataTapReaction.CookiesRaw.ToString();
        textMeshCorrectCookie.text = feedbackDataTapReaction.CookiesCorrect.ToString();
        textMeshBurntCookie.text = feedbackDataTapReaction.CookiesBurnt.ToString();
    }
}
