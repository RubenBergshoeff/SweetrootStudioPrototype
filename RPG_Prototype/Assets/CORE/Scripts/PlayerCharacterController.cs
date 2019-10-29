using System;
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

    //public int LevelPower {
    //    get {
    //        return data.StatPower.Level;
    //    }
    //}

    //public int XPPower {
    //    get {
    //        return data.StatPower.XP;
    //    }
    //}

    //public int LevelMagic {
    //    get {
    //        return data.StatMagic.Level;
    //    }
    //}

    //public int XPMagic {
    //    get {
    //        return data.StatMagic.XP;
    //    }
    //}

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

    public void AddActiveSkillXP(Skill targetSkill, int amount) {
        ActiveSkill activeSkill = GetActiveSkill(targetSkill);
        activeSkill.XP += amount;
        activeSkill.XP = Mathf.Min(activeSkill.XP, activeSkill.GetXPCap());
    }

    public int GetActiveSkillXPCap(Skill targetSkill) {
        return GetActiveSkill(targetSkill).GetXPCap();
    }

    public int GetActiveSkillXP(Skill targetSkill) {
        return GetActiveSkill(targetSkill).XP;
    }

    public int GetActiveSkillLevel(Skill targetSkill) {
        return GetActiveSkill(targetSkill).GetLevel();
    }

    public void SetProvenSkillLevel(int level) {
        data.ProvenLevel = level;
    }

    private ActiveSkill GetActiveSkill(Skill targetSkill) {
        ActiveSkillCategory activeSkillCategory = data.GetActiveSkillCategory(targetSkill.SkillCategory);
        if (activeSkillCategory == null) {
            throw new System.ArgumentException("No active skillcategory found for category " + targetSkill.SkillCategory);
        }
        ActiveSkill activeSkill = activeSkillCategory.GetActiveSkill(targetSkill);
        if (activeSkill == null) {
            throw new System.ArgumentException("No active skill found for skill " + targetSkill);
        }
        return activeSkill;
    }
}
