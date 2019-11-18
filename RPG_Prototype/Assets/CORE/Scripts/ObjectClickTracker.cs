using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickTracker : MonoBehaviour {
    public Action OnObjectClicked = null;

    private void OnMouseDown() {
        OnObjectClicked?.Invoke();
    }
}
