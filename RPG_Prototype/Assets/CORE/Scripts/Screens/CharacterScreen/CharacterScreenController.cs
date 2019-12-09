﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI.Base;

public class CharacterScreenController : UIDisplayController {

    [SerializeField] private SkillResultController skillResultController = null;

    protected override void OnShowing() {
        if (SaveController.Instance.GameData.BoterKroon.SkillResults.Count > 0) {
            BoterkroonSkillResult lastSkillResult = SaveController.Instance.GameData.BoterKroon.SkillResults[SaveController.Instance.GameData.BoterKroon.SkillResults.Count - 1];
            skillResultController.UpdateView(lastSkillResult);
        }
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }
}
