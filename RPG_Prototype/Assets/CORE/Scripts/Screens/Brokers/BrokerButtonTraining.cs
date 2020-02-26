using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TrainingType {
    Slow,
    Fast
}

public class BrokerButtonTraining : MonoBehaviour {

    public BoterkroonSkills TargetSkill;
    public TrainingType TrainingType;
    public Action<BoterkroonSkills, TrainingType> OnButtonClicked;

    private Button button;

    private void OnEnable() {
        if (button == null) {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(OnClickResult);
    }

    private void OnClickResult() {
        OnButtonClicked.Invoke(TargetSkill, TrainingType);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnClickResult);
    }
}
