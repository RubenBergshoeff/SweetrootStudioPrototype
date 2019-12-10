using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarController : MonoBehaviour {

    [SerializeField] private BoterkroonSkills targetSkill;
    [SerializeField] private MultiLevelSlider multiSlider = null;

    private void OnEnable() {
        Debug.Log(gameObject.name);
        List<float> normalizedResults = new List<float>();
        float previousValue = 0;
        foreach (var result in SaveController.Instance.GameData.BoterKroon.GetControlResultsFor(targetSkill)) {
            float newValue = result.TotalXP / (float)SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            normalizedResults.Add(newValue - previousValue);
            previousValue = newValue;
        }
        Debug.Log(normalizedResults.Count);
        multiSlider.UpdateValues(normalizedResults.ToArray());
    }
}
