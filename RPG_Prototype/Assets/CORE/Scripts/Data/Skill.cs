﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class Skill : BaseData {
    public SkillCategory SkillCategory = null;
    public TrainingData Training = null;
    public Sprite Icon;
}

[System.Serializable]
public class ActiveSkill : ActiveBaseData {
    public Skill Skill {
        get {
            return Data as Skill;
        }
    }
    public SkillTrainingResult LastTrainingResult {
        get {
            if (TrainingResults.Count == 0) {
                return null;
            }
            else {
                return TrainingResults[TrainingResults.Count - 1];
            }
        }
    }
    public ActiveTraining ActiveTraining = new ActiveTraining(null);
    public List<SkillTrainingResult> TrainingResults = new List<SkillTrainingResult>();
    public List<ActiveSkillLevel> ActiveSkillLevels = new List<ActiveSkillLevel>();
    public int XP = 0;

    public ActiveSkill(Skill data) : base(data) {

    }

    public int GetScore() {
        int score = 0;
        foreach (var activeSkillLevel in ActiveSkillLevels) {
            if (activeSkillLevel.IsCompleted) {
                score += activeSkillLevel.SkillLevel.SkillCategoryScore;
            }
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

    public ActiveSkillLevel GetActiveSkillLevel(SkillLevel skillLevel) {
        foreach (var activeSkillLevel in ActiveSkillLevels) {
            if (activeSkillLevel.SkillLevel == skillLevel) {
                return activeSkillLevel;
            }
        }
        return null;
    }
}

[System.Serializable]
public class SkillTrainingResult {
    public TrainingData Training = null;
    public int XPGain = 0;
}