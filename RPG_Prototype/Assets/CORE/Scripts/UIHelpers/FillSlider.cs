using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FillSlider : MonoBehaviour {

    [Range(0, 1)] public float Startpoint = 0f;
    [Range(0, 1)] public float Fill = 0.1f;

    [SerializeField] private RectTransform containingRect = null;
    [SerializeField] private RectTransform rect = null;

    public void SetValues(float startpoint, float fill) {
        Startpoint = startpoint;
        Fill = fill;
        UpdateView();
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
