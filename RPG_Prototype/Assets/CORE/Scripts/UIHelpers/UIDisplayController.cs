using UnityEngine;
using System.Collections;

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

    private void OnVisibilityChanged(bool visible) {
        if (visible) {
            OnVisible();
        }
        else {
            OnInvisible();
        }
    }

    protected abstract void OnVisible();
    protected abstract void OnInvisible();
}
