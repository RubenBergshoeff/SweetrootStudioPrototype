using System.Collections;
using UnityEngine;
using Doozy.Engine;
using TMPro;

public class BrokerControlTestResult : BrokerBaseResult {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private TextMeshProUGUI textmeshSkillName = null;
    [SerializeField] private ControlTestResult controlTestResult = null;
    [SerializeField] private string uiEventStringDone = "";

    private ActiveSkillLevel activeSkillLevel;
    private TrainingData newTrainingData;

    public override void SetResult(ActiveBaseData activeResult) {
        base.SetResult(activeResult);
        activeSkillLevel = activeResult as ActiveSkillLevel;
        textmeshSkillName.text = activeSkillLevel.SkillLevel.Skill.Name;
    }

    protected override void OnVisible() {
        StartCoroutine(ControlTestAnimation());
    }

    protected override void OnInvisible() {
    }

    private IEnumerator ControlTestAnimation() {

        // test character skill
        float previousCompletionLevel = activeSkillLevel.LastCompletionLevel;
        float completionLevel = playerCharacterController.GetActiveSkillXP(activeSkillLevel.SkillLevel.Skill) / (float)activeSkillLevel.SkillLevel.XPCap;

        yield return new WaitForSeconds(5f);

        if (completionLevel >= 1) {
            activeSkillLevel.IsCompleted = true;
            Debug.Log("yay level completed");
        }
        else {
            Debug.Log("Completion: " + completionLevel);
        }
        activeSkillLevel.LastCompletionLevel = Mathf.Min(completionLevel, 1);

        //int previousStars = Mathf.FloorToInt(previousCompletionLevel * 3);
        //int newStars = Mathf.FloorToInt(activeSkillLevel.LastCompletionLevel * 3);

        //if (previousStars < newStars) {
        //    ShowPopup(previousStars, newStars);
        //    yield return new WaitForSeconds(0.3f);
        //}

        controlTestResult.SetResult(previousCompletionLevel, activeSkillLevel);
        GameEventMessage.SendEvent(uiEventStringDone);
    }

    //private void ShowPopup(int oldStarAmount, int newStarAmount) {
    //    UIPopup popup = UIPopup.GetPopup(Popups.CONTROLSTAR_POPUP);

    //    if (popup == null) { return; }

    //    PopupControlTestStars popupUnlock = popup.GetComponent<PopupControlTestStars>();
    //    popupUnlock.Setup(oldStarAmount, newStarAmount);

    //    popup.Show();
    //}
}
