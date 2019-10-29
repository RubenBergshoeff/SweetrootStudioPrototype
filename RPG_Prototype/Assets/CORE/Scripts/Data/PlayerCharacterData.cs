using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharacterData {
    public int CurrentMoodLevel;
    public int TotalPoints {
        get {
            return 0;
        }
    }
    public List<ActiveSkillCategory> ActiveSkillCategories = new List<ActiveSkillCategory>();
    public int ProvenLevel;

    public ActiveSkillCategory GetActiveSkillCategory(SkillCategory skillCategory) {
        foreach (var activeSkillCategory in ActiveSkillCategories) {
            if (activeSkillCategory.Category == skillCategory) {
                return activeSkillCategory;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ActiveSkillCategory {
    public SkillCategory Category = null;
    public List<ActiveSkill> ActiveSkills = new List<ActiveSkill>();

    public int GetScore() {
        int score = 0;
        foreach (var activeSkill in ActiveSkills) {
            score += activeSkill.GetScore();
        }
        return score;
    }

    public ActiveSkill GetActiveSkill(Skill skill) {
        foreach (var activeSkill in ActiveSkills) {
            if (activeSkill.Skill == skill) {
                return activeSkill;
            }
        }
        return null;
    }
}

[System.Serializable]
public class ActiveSkill {
    public Skill Skill = null;
    public List<ActiveSkillLevel> ActiveSkillLevels = new List<ActiveSkillLevel>();
    public int XP = 0;

    public int GetScore() {
        int score = 0;
        foreach (var activeSkillLevel in ActiveSkillLevels) {
            score += activeSkillLevel.SkillLevel.SkillCategoryScore;
        }
        return score;
    }

    public int GetXPCap() {
        int XPCap = 0;
        foreach (var activeSkillLevel in ActiveSkillLevels) {
            XPCap = Mathf.Max(XPCap, activeSkillLevel.SkillLevel.XPCap);
        }
        return XPCap;
    }

    public int GetLevel() {
        int level = 0;
        foreach (var activeSkillLevel in ActiveSkillLevels) {
            if (activeSkillLevel.IsCompleted) {
                level = activeSkillLevel.SkillLevel.Level;
            }
        }
        return level;
    }
}

[System.Serializable]
public class ActiveSkillLevel {
    public SkillLevel SkillLevel;
    public bool IsCompleted = false;
}