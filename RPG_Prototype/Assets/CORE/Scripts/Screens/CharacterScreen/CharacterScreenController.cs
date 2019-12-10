using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI.Base;

public class CharacterScreenController : UIDisplayController {

    [SerializeField] private SkillResultController skillResultController = null;
    [SerializeField] private BannerHelper[] bannerHelpers = new BannerHelper[0];

    protected override void OnShowing() {
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count == 0) {
            SaveController.Instance.GameData.BoterKroon.SkillResults.Add(new BoterkroonSkillResult(1, 0, true));
        }
        BoterkroonSkillResult lastSkillResult = SaveController.Instance.GameData.BoterKroon.SkillResults[SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1];
        skillResultController.UpdateView(lastSkillResult);
        foreach (var bannerHelper in bannerHelpers) {
            bannerHelper.UpdateFade(lastSkillResult.Score);
        }
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }
}
