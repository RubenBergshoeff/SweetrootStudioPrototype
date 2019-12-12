using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Engine;
using Doozy.Engine.UI;

public abstract class TutorialFragment : MonoBehaviour {
    public bool AreRequirementsMet {
        get {
            return RequiredView.IsVisible && Requirement();
        }
    }
    protected abstract bool Requirement();
    public UIView RequiredView;
    public float DelayShowTime;
}

public abstract class TutorialFragmentPointer : TutorialFragment {
    public PointDirection PointDirection;
    public Button LinkedButton;
    public ObjectClickTracker LinkedClickableObject;
    public RectTransform TargetTransform;
}