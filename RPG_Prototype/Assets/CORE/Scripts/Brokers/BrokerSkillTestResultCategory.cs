using UnityEngine;
using System.Collections;

public class BrokerSkillTestResultCategory : MonoBehaviour {
    [SerializeField] private TMPro.TextMeshProUGUI nameField = null;
    [SerializeField] private TMPro.TextMeshProUGUI testLevelField = null;
    [SerializeField] private FillSlider fillSlider = null;

    private ActiveSkillCategory activeSkillCategory = null;

    public void Setup(ActiveSkillCategory activeSkillCategory) {
        this.activeSkillCategory = activeSkillCategory;
        UpdateStartView();
    }

    public void Animate(float time) {
        float endFill =
            (activeSkillCategory.GetScore() - activeSkillCategory.SelectedTest.MinCategoryScore) /
            (activeSkillCategory.SelectedTest.MaxCategoryScore - activeSkillCategory.SelectedTest.MinCategoryScore);
        fillSlider.SetValues(0, 0, endFill, time, activeSkillCategory.Category.color);
    }

    private void UpdateStartView() {
        nameField.text = activeSkillCategory.Category.Name;
        testLevelField.text = activeSkillCategory.SelectedTest.Level.ToString();
        fillSlider.SetValues(0, 0);
    }
}
