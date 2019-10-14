using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    public PlayerCharacterData Data;

    public int MaxFood = 3;
    public int MaxLevelDeviation = 2;

    public void FillFood() {
        Data.CurrentMoodLevel = MaxFood;
    }

    public bool LowerMoodBy(int amount) {
        if (Data.CurrentMoodLevel >= amount) {
            Data.CurrentMoodLevel -= amount;
            return true;
        }
        else {
            return false;
        }
    }

    public void SetProvenSkillLevel(int level) {
        Data.ProvenLevel = level;
    }
}
