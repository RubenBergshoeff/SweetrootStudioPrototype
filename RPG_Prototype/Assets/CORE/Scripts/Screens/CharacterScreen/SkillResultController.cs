using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillResultController : MonoBehaviour {
    [SerializeField] private RectTransform bannerContainer = null;
    [SerializeField] private Vector2 levelOneOffset = Vector2.zero;
    [SerializeField] private Vector2 levelTwoOffset = Vector2.zero;
    [SerializeField] private Vector2 levelThreeOffset = Vector2.zero;
    [SerializeField] private BannerHelper bannerLvl01 = null;
    [SerializeField] private BannerHelper bannerLvl02 = null;
    [SerializeField] private BannerHelper bannerLvl03 = null;

    public void UpdateView(BoterkroonSkillResult lastResult) {
        if (lastResult.Level == 1) {
            bannerContainer.anchoredPosition = levelOneOffset;
        }
        else if (lastResult.Level == 2) {
            bannerContainer.anchoredPosition = levelTwoOffset;
        }
        else if (lastResult.Level == 3) {
            bannerContainer.anchoredPosition = levelThreeOffset;
        }
    }
}
