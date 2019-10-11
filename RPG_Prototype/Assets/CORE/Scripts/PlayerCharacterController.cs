using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    public PlayerCharacterData Data;

    public int MaxFood = 3;
    public int MaxLevelDeviation = 2;

    public void FillFood() {
        Data.CurrentFoodAmount = MaxFood;
    }

    public bool UseFood(int amount) {
        if (Data.CurrentFoodAmount >= amount) {
            Data.CurrentFoodAmount -= amount;
            return true;
        } else {
            return false;
        }
    }

    public void SetProvenSkillLevel(int level) {
        Data.ProvenSkillLevel = level;
    }

    public void IncreasePotentialSkillLevel(int amount) {
        Data.PotentialSkillLevel += amount;
        if (Data.PotentialSkillLevel - Data.ProvenSkillLevel > MaxLevelDeviation) {
            Data.PotentialSkillLevel = Data.ProvenSkillLevel + MaxLevelDeviation;
        }
    }
}
