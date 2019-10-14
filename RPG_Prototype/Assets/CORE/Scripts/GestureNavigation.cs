using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class GestureNavigation : MonoBehaviour {

    private List<LeanFinger> activeFingers = new List<LeanFinger>();

    private void OnEnable() {
        Lean.Touch.LeanTouch.OnFingerDown += OnFingerDown;
        Lean.Touch.LeanTouch.OnFingerUp += OnFingerUp;
    }

    private void OnFingerDown(LeanFinger finger) {
        if (activeFingers.Contains(finger) == false) {
            activeFingers.Add(finger);
        }
    }

    private void OnFingerUp(LeanFinger finger) {
        if (activeFingers.Contains(finger)) {
            activeFingers.Remove(finger);
        }
    }

    private void Update() {

    }
}
