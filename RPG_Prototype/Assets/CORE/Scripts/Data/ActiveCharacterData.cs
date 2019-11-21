using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveCharacterData : ActiveBaseData {
    public int CurrentMoodLevel;
    public int TotalPoints {
        get {
            return 0;
        }
    }
    public List<ActiveSkillCategory> ActiveSkillCategories = new List<ActiveSkillCategory>();
    public int ProvenLevel;

    public ActiveCharacterData(CharacterData data) : base(data) {
        ActiveSkillCategories = new List<ActiveSkillCategory>();
        foreach (var trainingData in data.AvailableTrainingData) {
            HandleUnlock(trainingData);
        }
        foreach (var skillLevel in data.AvailableSkillLevels) {
            HandleUnlock(skillLevel);
        }
        CurrentMoodLevel = data.StartMoodLevel;
    }

    public UnlockReturn HandleUnlock(BaseData newUnlock) {
        if (newUnlock is Skill) {
            Skill newSkill = newUnlock as Skill;
            ActiveSkillCategory activeSkillCategory = GetActiveSkillCategory(newSkill.SkillCategory);
            if (activeSkillCategory == null) {
                activeSkillCategory = new ActiveSkillCategory(newSkill.SkillCategory);
                ActiveSkillCategories.Add(activeSkillCategory);
            }
            ActiveSkill activeSkill = activeSkillCategory.GetActiveSkill(newSkill);
            if (activeSkill == null) {
                activeSkill = new ActiveSkill(newSkill);
                activeSkillCategory.ActiveSkills.Add(activeSkill);
                return UnlockReturn.NewUnlock;
            }
            return UnlockReturn.WasUnlocked;
        }
        if (newUnlock is SkillLevel) {
            SkillLevel newSkillLevel = newUnlock as SkillLevel;
            ActiveSkillCategory activeSkillCategory = GetActiveSkillCategory(newSkillLevel.Skill.SkillCategory);
            if (activeSkillCategory == null) {
                activeSkillCategory = new ActiveSkillCategory(newSkillLevel.Skill.SkillCategory);
                ActiveSkillCategories.Add(activeSkillCategory);
            }
            ActiveSkill activeSkill = activeSkillCategory.GetActiveSkill(newSkillLevel.Skill);
            if (activeSkill == null) {
                activeSkill = new ActiveSkill(newSkillLevel.Skill);
                activeSkillCategory.ActiveSkills.Add(activeSkill);
            }
            ActiveSkillLevel activeSkillLevel = activeSkill.GetActiveSkillLevel(newSkillLevel);
            if (activeSkillLevel == null) {
                activeSkillLevel = new ActiveSkillLevel(newSkillLevel);
                activeSkill.ActiveSkillLevels.Add(activeSkillLevel);
                return UnlockReturn.NewUnlock;
            }
            return UnlockReturn.WasUnlocked;
        }
        if (newUnlock is TrainingData) {
            TrainingData newTraining = newUnlock as TrainingData;
            ActiveSkillCategory activeSkillCategory = GetActiveSkillCategory(newTraining.TargetSkill.SkillCategory);
            if (activeSkillCategory == null) {
                activeSkillCategory = new ActiveSkillCategory(newTraining.TargetSkill.SkillCategory);
                ActiveSkillCategories.Add(activeSkillCategory);
            }
            ActiveSkill activeSkill = activeSkillCategory.GetActiveSkill(newTraining.TargetSkill);
            if (activeSkill == null) {
                activeSkill = new ActiveSkill(newTraining.TargetSkill);
                activeSkillCategory.ActiveSkills.Add(activeSkill);
            }
            ActiveTraining activeTraining = activeSkill.ActiveTraining;
            if (activeTraining == null) {
                activeTraining = new ActiveTraining(newTraining);
                activeSkill.ActiveTraining = activeTraining;
                return UnlockReturn.NewUnlock;
            }
            if (activeTraining.Data == null) {
                activeTraining.Data = newTraining;
                return UnlockReturn.NewUnlock;
            }
            else {
                if (activeTraining.Training.XPGainTraining < newTraining.XPGainTraining) {
                    activeTraining.Data = newTraining;
                    return UnlockReturn.NewUnlock;
                }
            }
            return UnlockReturn.WasUnlocked;
        }
        throw new System.ArgumentException("Unlock not supported for " + newUnlock);
    }

    public int GetSkillScore(Skill targetSkill) {
        ActiveSkillCategory activeSkillCategory = GetActiveSkillCategory(targetSkill.SkillCategory);
        ActiveSkill activeSkill = activeSkillCategory.GetActiveSkill(targetSkill);
        return activeSkill.GetScore();
    }

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