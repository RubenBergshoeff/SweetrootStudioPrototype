using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class FillSlider : MonoBehaviour {

    [Range(0, 1)] public float Startpoint = 0f;
    [Range(0, 1)] public float Fill = 0.1f;

    [SerializeField] private RectTransform containingRect = null;
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private Image fillImage = null;

    public void SetValues(float startpoint, float fill) {
        Startpoint = startpoint;
        Fill = fill;
        UpdateView();
    }

    public void SetValues(float startpoint, float fill, Color color) {
        fillImage.color = color;
        Startpoint = startpoint;
        Fill = fill;
        UpdateView();
    }

    public void SetValues(float startpoint, float startFill, float endFill, float time, Color color) {
        fillImage.color = color;
        Startpoint = startpoint;
        Fill = startFill;
        UpdateView(endFill, time);
    }

    private void UpdateView(float endFill, float time) {
        float xPos = Startpoint * containingRect.rect.width;
        rect.anchoredPosition = new Vector2(xPos, 0);

        float desiredWidth = Fill * containingRect.rect.width;
        float maxWidth = containingRect.rect.width - xPos;
        rect.sizeDelta = new Vector2(Mathf.Min(desiredWidth, maxWidth), 0);
        desiredWidth = containingRect.rect.width * endFill;
        Vector2 endDelta = new Vector2(Mathf.Min(desiredWidth, maxWidth), 0);
        rect.DOSizeDelta(endDelta, time).OnComplete(() => Fill = endFill);
    }

    private void OnValidate() {
        UpdateView();
    }

    private void UpdateView() {
        float xPos = Startpoint * containingRect.rect.width;
        rect.anchoredPosition = new Vector2(xPos, 0);

        float desiredWidth = Fill * containingRect.rect.width;
        float maxWidth = containingRect.rect.width - xPos;
        rect.sizeDelta = new Vector2(Mathf.Min(desiredWidth, maxWidth), 0);
    }
}
