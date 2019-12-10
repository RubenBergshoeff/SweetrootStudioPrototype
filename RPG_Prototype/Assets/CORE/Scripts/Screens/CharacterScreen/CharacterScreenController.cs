﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI.Base;

public class CharacterScreenController : UIDisplayController {

    [SerializeField] private SkillResultController skillResultController = null;
    [SerializeField] private BannerHelper[] bannerHelpers = new BannerHelper[0];

    protected override void OnShowing() {
        BoterkroonSkillResult lastSkillResult = GetLastSucceededResult();
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

    private BoterkroonSkillResult GetLastSucceededResult() {
        for (int i = SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1; i >= 0; i--) {
            if (SaveController.Instance.GameData.BoterKroon.SkillResults[i].Succeeded) {
                return SaveController.Instance.GameData.BoterKroon.SkillResults[i];
            }
        }
        return new BoterkroonSkillResult(1, 0, true);
    }
}
