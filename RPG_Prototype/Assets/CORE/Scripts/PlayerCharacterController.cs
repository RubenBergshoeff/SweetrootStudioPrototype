using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {

    public int LevelMood {
        get {
            return data.CurrentMoodLevel;
        }
    }

    public int TotalPoints {
        get {
            return data.TotalPoints;
        }
    }

    public int LevelPower {
        get {
            return data.StatPower.Level;
        }
    }

    public int XPPower {
        get {
            return data.StatPower.XP;
        }
    }

    public int LevelMagic {
        get {
            return data.StatMagic.Level;
        }
    }

    public int XPMagic {
        get {
            return data.StatMagic.XP;
        }
    }

    public int ProvenLevel {
        get {
            return data.ProvenLevel;
        }
    }

    private PlayerCharacterData data {
        get {
            return SaveController.Instance.GameData.PlayerData;
        }
    }

    public int MaxFood = 3;
    public int MaxLevelDeviation = 2;

    public void FillFood() {
        data.CurrentMoodLevel = MaxFood;
    }

    public bool LowerMoodBy(int amount) {
        if (data.CurrentMoodLevel >= amount) {
            data.CurrentMoodLevel -= amount;
            return true;
        }
        else {
            return false;
        }
    }

    public void SetMoodLevel(int level) {
        data.CurrentMoodLevel = level;
    }

    public void SetProvenLevel(int level) {
        data.ProvenLevel = level;
    }

    public void AddXP(StatType stat, int amount) {
        switch (stat) {
            case StatType.Power:
                data.StatPower.XP += amount;
                break;
            case StatType.Magic:
                data.StatMagic.XP += amount;
                break;
            default:
                throw new System.ArgumentException("Stat not implemented " + stat);
        }
    }

    public void SetProvenSkillLevel(int level) {
        data.ProvenLevel = level;
    }
}
