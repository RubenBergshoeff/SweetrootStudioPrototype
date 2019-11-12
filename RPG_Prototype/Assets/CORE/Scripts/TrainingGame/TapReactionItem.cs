using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TapReactionItem : MonoBehaviour {

    public delegate void TapReactionItemDelegate(TapReactionItem item);
    public TapReactionItemDelegate OnTapped;
    public TapReactionItemDelegate OnTimedOut;

    [SerializeField] private Button button = null;
    [SerializeField] private Image baseImage = null;
    [SerializeField] private Image timedOutImage = null;

    private float timeAlive;
    private float allowedTime;
    private bool isTimedOut;
    private bool isTapped;

    public void Setup(float allowedTime) {
        timeAlive = 0;
        this.allowedTime = allowedTime;
        isTimedOut = false;
        baseImage.enabled = true;
        timedOutImage.enabled = false;
    }

    public void UpdateItem(float deltaTime) {
        if (isTimedOut || isTapped) { return; }

        timeAlive += deltaTime;
        if (timeAlive > allowedTime) {
            isTimedOut = true;
            OnTimedOut?.Invoke(this);
            SwitchVisual();
        }
    }

    private void SwitchVisual() {
        baseImage.enabled = !baseImage.enabled;
        timedOutImage.enabled = !timedOutImage.enabled;
    }

    private void OnEnable() {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked() {
        if (isTimedOut) { return; }

        isTapped = true;
        OnTapped?.Invoke(this);
    }
}
