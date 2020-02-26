using UnityEngine;
using System.Collections;
using Doozy.Engine.UI;
using System;

public abstract class UIDisplayController : MonoBehaviour {

    private Doozy.Engine.UI.UIView uiView = null;

    private void Awake() {
        uiView = GetComponent<Doozy.Engine.UI.UIView>();
        if (uiView == null) {
            throw new System.Exception("Could not find Doozy UIView");
        }
        uiView.OnVisible.AddListener(OnVisibilityChanged);
    }

    private void OnDestroy() {
        if (uiView != null) {
            uiView.OnVisible.RemoveListener(OnVisibilityChanged);
        }
    }

    private void OnVisibilityChanged(UIView.VisibleEventState state) {
        switch (state) {
            case UIView.VisibleEventState.StartVisible:
                OnShowing();
                break;
            case UIView.VisibleEventState.EndVisible:
                OnVisible();
                break;
            case UIView.VisibleEventState.StartInvisible:
                OnHiding();
                break;
            case UIView.VisibleEventState.EndInvisible:
                OnInvisible();
                break;
        }
    }

    protected abstract void OnVisible();
    protected abstract void OnInvisible();
    protected abstract void OnShowing();
    protected abstract void OnHiding();
}
