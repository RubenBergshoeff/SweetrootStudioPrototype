using UnityEngine;
using System.Collections;
using System;

public class ControlTestResult : UIDisplayController {

    [SerializeField] private FillSlider fillSlider = null;
    [SerializeField] private UnityEngine.UI.Button backButton = null;
    [SerializeField] private TMPro.TextMeshProUGUI textmeshSkillName = null;

    private ActiveSkillLevel activeSkillLevel;
    private float previousCompletionLevel;

    public void SetResult(float previousCompletionLevel, ActiveSkillLevel activeSkillLevel) {
        this.activeSkillLevel = activeSkillLevel;
        this.previousCompletionLevel = previousCompletionLevel;
        fillSlider.SetValues(0, previousCompletionLevel, Color.magenta);
        textmeshSkillName.text = activeSkillLevel.SkillLevel.Skill.Name;
    }

    protected override void OnShowing() {
        backButton.gameObject.SetActive(false);
    }

    protected override void OnVisible() {
        StartCoroutine(AnimateTestResult());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private IEnumerator AnimateTestResult() {
        yield return new WaitForSeconds(0.5f);

        fillSlider.SetValues(0, previousCompletionLevel, Mathf.Max(activeSkillLevel.LastCompletionLevel, 0.05f), 1, Color.magenta);

        yield return new WaitForSeconds(1.5f);

        backButton.gameObject.SetActive(true);
    }
}
