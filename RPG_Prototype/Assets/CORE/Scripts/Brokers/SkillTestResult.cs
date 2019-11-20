using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillTestResult : UIDisplayController {
    private ActiveSkillCategory activeSkillCategory;
    private List<UnlockResult> newUnlocks;

    public void SetResult(ActiveSkillCategory activeSkillCategory, List<UnlockResult> newUnlocks) {
        this.activeSkillCategory = activeSkillCategory;
        this.newUnlocks = newUnlocks;
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
