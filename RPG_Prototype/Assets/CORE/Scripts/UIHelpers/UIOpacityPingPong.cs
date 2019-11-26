using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIOpacityPingPong : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup = null;
    private Sequence tweenSequence;

    private void OnEnable() {
        tweenSequence = DOTween.Sequence();
        tweenSequence.Append(canvasGroup.DOFade(0, 1));
        tweenSequence.Append(canvasGroup.DOFade(1, 1));
        tweenSequence.SetLoops(-1);
    }

    private void OnDisable() {
        tweenSequence.Kill();
    }
}
