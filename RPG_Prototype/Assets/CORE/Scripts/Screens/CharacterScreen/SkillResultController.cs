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

        float fadeHeight = CalculateFadeHeight(lastResult.Score);
        bannerLvl01.UpdateFade(fadeHeight);
        bannerLvl02.UpdateFade(fadeHeight);
        bannerLvl03.UpdateFade(fadeHeight);
    }

    public float CalculateFadeHeight(float score) {
        float normalizedScore = 0;
        float heightDifference = 0;
        if (score < BoterkroonScoreRequirements.GetMinScoreFor(2).Total) {
            normalizedScore = score / BoterkroonScoreRequirements.GetMinScoreFor(2).Total;
            heightDifference = bannerLvl02.GetComponent<RectTransform>().anchoredPosition.y - bannerLvl01.GetComponent<RectTransform>().anchoredPosition.y;
            return bannerLvl01.GetComponent<RectTransform>().anchoredPosition.y + heightDifference * normalizedScore;
        }
        else if (score < BoterkroonScoreRequirements.GetMinScoreFor(3).Total) {
            normalizedScore = (score - BoterkroonScoreRequirements.GetMinScoreFor(2).Total) / (BoterkroonScoreRequirements.GetMinScoreFor(3).Total - BoterkroonScoreRequirements.GetMinScoreFor(2).Total);
            heightDifference = bannerLvl03.GetComponent<RectTransform>().anchoredPosition.y - bannerLvl02.GetComponent<RectTransform>().anchoredPosition.y;
            return bannerLvl02.GetComponent<RectTransform>().anchoredPosition.y + heightDifference * normalizedScore;
        }
        else {
            normalizedScore = (score - BoterkroonScoreRequirements.GetMinScoreFor(3).Total) / (BoterkroonScoreRequirements.GetMaxScoreFor(3).Total - BoterkroonScoreRequirements.GetMinScoreFor(3).Total);
            heightDifference = bannerLvl03.GetComponent<RectTransform>().sizeDelta.y;
            return bannerLvl03.GetComponent<RectTransform>().anchoredPosition.y + heightDifference * normalizedScore;
        }
    }
}
