using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPointFlipOver : MonoBehaviour {

    public ResultDataBase Data {
        get {
            return data;
        }
    }

    public Action<ActiveResultData> OnPointClicked;

    [SerializeField] private ResultDataBase data = null;
    private FlipPlane[] flipPlanes = new FlipPlane[0];
    private ActiveResultData activeResultData = null;

    private void Awake() {
        flipPlanes = GetComponentsInChildren<FlipPlane>();
    }

    public void Initialize(ActiveResultData activeResultData) {
        this.activeResultData = activeResultData;
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
        OnPointClicked?.Invoke(activeResultData);
    }
}
