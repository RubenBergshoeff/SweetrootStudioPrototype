using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDependentHider : MonoBehaviour {
    [SerializeField] private BoterkroonSkills targetSkill = BoterkroonSkills.Baking;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private void OnEnable() {
        if (SaveController.Instance.GameData.BoterKroon.IsSkillActive(targetSkill)) {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
