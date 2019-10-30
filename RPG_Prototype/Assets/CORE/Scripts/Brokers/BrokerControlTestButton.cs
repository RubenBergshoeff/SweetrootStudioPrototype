using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrokerControlTestButton : BrokerButton {
    private ActiveSkillLevel activeSkillLevel {
        get {
            return activeResultData as ActiveSkillLevel;
        }
    }

    [SerializeField] private Image starOne = null;
    [SerializeField] private Image starTwo = null;
    [SerializeField] private Image starThree = null;

    protected override void UpdateView() {
        base.UpdateView();
        if (activeResultData == null) { return; }
        if (Application.isPlaying == false) {
            starOne.gameObject.SetActive(true);
            starTwo.gameObject.SetActive(true);
            starThree.gameObject.SetActive(true);
            return;
        }
        starOne.gameObject.SetActive(false);
        starTwo.gameObject.SetActive(false);
        starThree.gameObject.SetActive(false);
        if (activeSkillLevel.LastCompletionLevel >= (1 / 3.0f)) {
            starOne.gameObject.SetActive(true);
        }
        if (activeSkillLevel.LastCompletionLevel >= (2 / 3.0f)) {
            starTwo.gameObject.SetActive(true);
        }
        if (activeSkillLevel.IsCompleted) {
            starThree.gameObject.SetActive(true);
        }
    }
}
