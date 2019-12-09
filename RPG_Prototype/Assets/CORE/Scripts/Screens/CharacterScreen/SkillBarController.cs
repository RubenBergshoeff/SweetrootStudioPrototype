using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBarController : MonoBehaviour {

    [SerializeField] private BoterkroonSkills targetSkill;
    [SerializeField] private MultiLevelSlider multiSlider = null;

    private void OnEnable() {
        List<float> normalizedResults = new List<float>();
        foreach (var result in SaveController.Instance.GameData.BoterKroon.GetControlResultsFor(targetSkill)) {

        }
    }
}
