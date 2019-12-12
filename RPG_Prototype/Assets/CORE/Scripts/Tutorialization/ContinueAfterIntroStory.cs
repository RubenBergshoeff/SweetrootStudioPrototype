using UnityEngine;
using System.Collections;

public class ContinueAfterIntroStory : TutorialFragmentPointer {
    [SerializeField] private NewCharacterController newCharacterController = null;

    protected override bool Requirement() {
        return newCharacterController.IsDone;
    }
}
