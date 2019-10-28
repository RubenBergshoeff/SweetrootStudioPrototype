using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class BrokerResultFood : BrokerResultBase {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private FillSlider slider = null;
    [SerializeField] private Button backButton = null;

    public override void SetResult(ResultDataBase result) {
        base.SetResult(result);
        slider.SetValues(0, playerCharacterController.LevelMood / 3.0f);
        if (playerCharacterController.LevelMood < 3) {
            playerCharacterController.SetMoodLevel(Mathf.Min(playerCharacterController.LevelMood + (result as MoodData).MoodImproveAmount, 3));
        }
    }

    private void OnEnable() {
        backButton.gameObject.SetActive(false);
        StartCoroutine(ShowFoodAnimation());
    }

    private IEnumerator ShowFoodAnimation() {
        yield return new WaitForSeconds(2.5f);
        slider.SetValues(0, playerCharacterController.LevelMood / 3.0f);

        yield return new WaitForSeconds(1f);
        backButton.gameObject.SetActive(true);
    }
}
