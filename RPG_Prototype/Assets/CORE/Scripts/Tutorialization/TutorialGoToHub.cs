using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGoToHub : TutorialFragmentPointer {
    protected override bool Requirement() {
        return SaveController.Instance.GameData.BoterKroon.IsNew == false;
    }
}
