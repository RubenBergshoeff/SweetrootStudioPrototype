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
            (activeSkillCategory.LastResult.Score - activeSkillCategory.LastResult.Test.MinCategoryScore) /
            (float)(activeSkillCategory.LastResult.Test.MaxCategoryScore - activeSkillCategory.LastResult.Test.MinCategoryScore);
        endFill = Mathf.Max(0, endFill);
        fillSlider.SetValues(0, 0, endFill, time, activeSkillCategory.Category.color);
    }

    private void UpdateStartView() {
        nameField.text = activeSkillCategory.Category.Name;
        testLevelField.text = activeSkillCategory.LastResult.Test.Level.ToString();
        fillSlider.SetValues(0, 0);
    }
}
