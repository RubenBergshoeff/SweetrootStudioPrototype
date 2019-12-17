using UnityEngine;
using System.Collections;

public class ShowTutorialTextWithUIHighlight : TutorialFragmentText {
    protected override bool Requirement() {
        return true;
    }

    public RectTransform HighlightUIElement = null;
}