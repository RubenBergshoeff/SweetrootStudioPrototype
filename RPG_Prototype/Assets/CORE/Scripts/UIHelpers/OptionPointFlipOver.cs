﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPointFlipOver : MonoBehaviour {

    public BaseData Data {
        get {
            return data;
        }
    }

    public Action OnPointClicked;

    [SerializeField] private BaseData data = null;
    private FlipPlane[] flipPlanes = new FlipPlane[0];

    private void Awake() {
        flipPlanes = GetComponentsInChildren<FlipPlane>();
    }

    public void ToStartPosition() {
        foreach (var plane in flipPlanes) {
            plane.GoToStartRotation();
        }
    }

    public void FlipUp() {
        foreach (var plane in flipPlanes) {
            plane.FlipUp();
        }
    }

    private void OnMouseDown() {
        OnPointClicked?.Invoke();
    }
}
