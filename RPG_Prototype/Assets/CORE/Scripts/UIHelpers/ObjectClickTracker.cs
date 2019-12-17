using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickTracker : MonoBehaviour {
    public Action OnObjectClicked = null;

    protected void OnMouseDown() {
        OnObjectClicked?.Invoke();
    }
}
