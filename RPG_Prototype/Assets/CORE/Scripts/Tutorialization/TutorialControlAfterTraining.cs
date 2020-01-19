using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControlAfterTraining : ShowTutorialTextWithUIHighlight {
    protected override bool Requirement() {
        return SaveController.Instance.GameData.BoterKroon.TrainingResultsBaking.Count > 0;
    }
}
