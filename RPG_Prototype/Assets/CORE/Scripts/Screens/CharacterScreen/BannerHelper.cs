using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerHelper : MonoBehaviour {
    public int Level = 1;
    public Vector2 ScoreRange = Vector2.zero;

    [SerializeField] private RectTransform fade = null;

    public void UpdateFade(float lastScore) {
        float relativeScore = (lastScore - ScoreRange.x) / (ScoreRange.y - ScoreRange.x);
        fade.anchoredPosition = new Vector2(0, (1 - relativeScore) * -665);
    }
}
