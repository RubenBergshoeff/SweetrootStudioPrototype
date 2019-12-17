using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarController : MonoBehaviour {

    [SerializeField] private BoterkroonSkills targetSkill;
    [SerializeField] private MultiLevelSlider multiSlider = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private void OnEnable() {
        if (SaveController.Instance.GameData.BoterKroon.IsSkillActive(targetSkill)) {
            canvasGroup.alpha = 1;
        }
        else {
            canvasGroup.alpha = 0;
        }
        List<float> normalizedResults = new List<float>();
        float previousValue = 0;
        foreach (var result in SaveController.Instance.GameData.BoterKroon.GetControlResultsFor(targetSkill)) {
            float newValue = result.TotalXP / (float)BoterkroonValues.Values.MaxSkillXP;
            normalizedResults.Add(newValue - previousValue);
            previousValue = newValue;
        }
        multiSlider.UpdateValues(normalizedResults.ToArray());
    }
}
