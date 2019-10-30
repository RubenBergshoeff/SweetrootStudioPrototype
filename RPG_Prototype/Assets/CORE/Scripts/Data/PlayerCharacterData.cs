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

    public List<ActiveSkillLevel> GetAllActiveSkillLevels() {
        List<ActiveSkillLevel> skillLevels = new List<ActiveSkillLevel>();
        foreach (var activeSkillCategory in ActiveSkillCategories) {
            foreach (var activeSkill in activeSkillCategory.ActiveSkills) {
                foreach (var activeSkillLevel in activeSkill.ActiveSkillLevels) {
                    skillLevels.Add(activeSkillLevel);
                }
            }
        }
        return skillLevels;
    }

    public List<ActiveTraining> GetAllActiveTrainingData() {
        List<ActiveTraining> trainingData = new List<ActiveTraining>();
        foreach (var activeSkillCategory in ActiveSkillCategories) {
            foreach (var activeSkill in activeSkillCategory.ActiveSkills) {
                trainingData.Add(activeSkill.ActiveTraining);
            }
        }
        return trainingData;
    }
}