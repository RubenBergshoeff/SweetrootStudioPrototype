using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrainerController : MonoBehaviour {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private int foodCost = 1;
    [SerializeField] private int skillIncrement = 1;

    [SerializeField] private UnityEvent OnSucces = null;
    [SerializeField] private UnityEvent OnFail = null;

    private void OnEnable() {
        if (playerCharacterController.UseFood(foodCost)) {
            playerCharacterController.IncreasePotentialSkillLevel(skillIncrement);
            OnSucces.Invoke();
        } else {
            OnFail.Invoke();
        }
    }
}
