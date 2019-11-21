using UnityEngine;
using System.Collections;

public class ControlTestResult : UIDisplayController {
    private ActiveSkillLevel activeSkillLevel;
    private float previousCompletionLevel;

    public void SetResult(float previousCompletionLevel, ActiveSkillLevel activeSkillLevel) {
        this.activeSkillLevel = activeSkillLevel;
        this.previousCompletionLevel = previousCompletionLevel;
    }

    protected override void OnShowing() {

    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }
}
