using UnityEngine;
using System.Collections;
using System;

public class BrokerResultTraining : BrokerResultBase {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private GameObject gainedDisplay = null;

    private TrainingData training;

    public override void SetResult(ResultDataBase result) {
        base.SetResult(result);
        training = result as TrainingData;
    }

    private void OnEnable() {
        gainedDisplay.SetActive(false);
        StartCoroutine(TrainingAnimation());
    }

    private IEnumerator TrainingAnimation() {
        yield return new WaitForSeconds(2.5f);

        // apply training
        switch (training.StatType) {
            case StatType.Power:
                playerCharacterController.Data.StatPower.XP += training.XP;
                break;
            case StatType.Magic:
                playerCharacterController.Data.StatMagic.XP += training.XP;
                break;
        }


    }
}
