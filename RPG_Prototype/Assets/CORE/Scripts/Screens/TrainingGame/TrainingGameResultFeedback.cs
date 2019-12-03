using UnityEngine;
using System.Collections;

public abstract class TrainingGameResultFeedback : MonoBehaviour {
    public abstract void Setup(TrainingGameResultFeedbackData feedbackData);

    public abstract void OnVisible();
}
