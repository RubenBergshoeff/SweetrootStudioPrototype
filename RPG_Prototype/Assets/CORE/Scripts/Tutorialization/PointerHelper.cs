using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PointerHelper : MonoBehaviour {

    [SerializeField] private RectTransform rectTransform = null;

    private Sequence tweenSequence = null;

    public void StartAnimation(PointDirection direction) {
        tweenSequence = DOTween.Sequence();
        float moveTowards = direction == PointDirection.Down ? -30 : 30;
        tweenSequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + moveTowards, 0.5f).SetEase(Ease.InOutSine));
        tweenSequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y, 0.5f).SetEase(Ease.InOutSine));
        tweenSequence.SetLoops(-1);
        tweenSequence.Play();
    }

    private void OnDisable() {
        tweenSequence.Kill();
    }
}
