using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BrokerFoodResult : BrokerBaseResult {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private FillSlider slider = null;
    [SerializeField] private Button backButton = null;

    private ActiveMoodData activeMoodData;

    public override void SetResult(ActiveResultData result) {
        base.SetResult(result);
        activeMoodData = result as ActiveMoodData;
        slider.SetValues(0, playerCharacterController.LevelMood / 3.0f);
        if (playerCharacterController.LevelMood < 3) {
            playerCharacterController.SetMoodLevel(Mathf.Min(playerCharacterController.LevelMood + activeMoodData.MoodData.MoodImproveAmount, 3));
        }
    }

    protected override void OnVisible() {
        backButton.gameObject.SetActive(false);
        StartCoroutine(ShowFoodAnimation());
    }

    protected override void OnInvisible() {
    }

    private IEnumerator ShowFoodAnimation() {
        yield return new WaitForSeconds(2.5f);
        slider.SetValues(0, playerCharacterController.LevelMood / 3.0f);

        yield return new WaitForSeconds(1f);
        backButton.gameObject.SetActive(true);
    }
}
