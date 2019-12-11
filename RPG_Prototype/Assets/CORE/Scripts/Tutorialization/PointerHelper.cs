using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PointerHelper : MonoBehaviour {

    [SerializeField] private RectTransform rectTransform = null;

    private Sequence tweenSequence = null;

    private void OnEnable() {
        float ypos = rectTransform.anchoredPosition.y;
        tweenSequence = DOTween.Sequence();
        tweenSequence.Append(rectTransform.DOAnchorPosY(ypos - 30, 0.3f).SetEase(Ease.InOutSine));
        tweenSequence.Append(rectTransform.DOAnchorPosY(ypos, 0.3f).SetEase(Ease.InOutSine));
        tweenSequence.SetLoops(-1);
        tweenSequence.Play();
    }

    private void OnDisable() {
        tweenSequence.Kill();
    }
}
