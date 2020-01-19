using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHappyEverAfter : ShowTutorialTextWithUIHighlight {
    protected override bool Requirement() {
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            return false;
        } else {
            return SaveController.Instance.GameData.BoterKroon.SkillResults[SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1].Score > 0;
        }
    }
}
