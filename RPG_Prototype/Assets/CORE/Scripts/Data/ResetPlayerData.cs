using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerData : MonoBehaviour {
    [SerializeField] private PlayerCharacterController playerCharacterController = null;

    public void Reset() {
        playerCharacterController.Data.CurrentMoodLevel = 2;
        playerCharacterController.Data.StatPower.XP = 0;
        playerCharacterController.Data.StatMagic.XP = 0;
        playerCharacterController.Data.ProvenLevel = 0;
    }
}
