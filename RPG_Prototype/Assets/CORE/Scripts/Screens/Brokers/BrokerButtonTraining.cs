using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokerButtonTraining : MonoBehaviour {

    public BoterkroonSkills TargetSkill;
    public Action<BoterkroonSkills> OnButtonClicked;

    private Button button;

    private void OnEnable() {
        if (button == null) {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(OnClickResult);
    }

    private void OnClickResult() {
        OnButtonClicked.Invoke(TargetSkill);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnClickResult);
    }
}
