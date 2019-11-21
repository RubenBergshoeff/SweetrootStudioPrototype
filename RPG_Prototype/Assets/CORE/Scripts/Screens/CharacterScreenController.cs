using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI.Base;

public class CharacterScreenController : UIDisplayController {
    //[SerializeField] private PlayerCharacterController playerCharacterController = null;
    //[SerializeField] private FillSlider moodSlider = null;
    //[SerializeField] private FillSlider powerSlider = null;
    //[SerializeField] private TextMeshProUGUI powerLevelField = null;
    //[SerializeField] private FillSlider magicSlider = null;
    //[SerializeField] private TextMeshProUGUI magicLevelField = null;

    protected override void OnVisible() {
    }

    protected override void OnInvisible() {
    }

    private float GetProgressForStat(int level, int xp) {
        int relativeXPValue = xp - PlayerStat.MinLevelXPs[level];
        int xpInLevel = PlayerStat.MinLevelXPs[level + 1] - PlayerStat.MinLevelXPs[level];
        return relativeXPValue / (float)xpInLevel;
    }

    protected override void OnShowing() {

    }

    protected override void OnHiding() {

    }
}
