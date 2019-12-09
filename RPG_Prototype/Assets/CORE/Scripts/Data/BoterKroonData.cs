using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BoterkroonSkills {
    Baking,
    Sword,
    Royal
}

[System.Serializable]
public class ActiveBoterkroonData {

    public readonly int MaxSkillXP = 1000;

    public List<BoterkroonSkillResult> SkillResults = new List<BoterkroonSkillResult>();

    public List<BoterkroonControlResult> ControlResultsBaking = new List<BoterkroonControlResult>();
    public List<BoterkroonControlResult> ControlResultsSword = new List<BoterkroonControlResult>();
    public List<BoterkroonControlResult> ControlResultsRoyal = new List<BoterkroonControlResult>();

    public List<BoterkroonTrainingResult> TrainingResultsBaking = new List<BoterkroonTrainingResult>();
    public List<BoterkroonTrainingResult> TrainingResultsSword = new List<BoterkroonTrainingResult>();
    public List<BoterkroonTrainingResult> TrainingResultsRoyal = new List<BoterkroonTrainingResult>();

    public bool IsBakingLocked = true;
    public bool IsSwordLocked = true;
    public bool IsRoyalLocked = true;

    public bool IsSkillActive(BoterkroonSkills skill) {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return !IsBakingLocked;
            case BoterkroonSkills.Sword:
                return !IsSwordLocked;
            case BoterkroonSkills.Royal:
                return !IsRoyalLocked;
        }
        throw new System.NotImplementedException();
    }

    public List<BoterkroonTrainingResult> GetTrainingResultsFor(BoterkroonSkills skill) {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return TrainingResultsBaking;
            case BoterkroonSkills.Sword:
                return TrainingResultsSword;
            case BoterkroonSkills.Royal:
                return TrainingResultsRoyal;
        }
        throw new System.NotImplementedException();
    }

    public List<BoterkroonControlResult> GetControlResultsFor(BoterkroonSkills skill) {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return ControlResultsBaking;
            case BoterkroonSkills.Sword:
                return ControlResultsSword;
            case BoterkroonSkills.Royal:
                return ControlResultsRoyal;
        }
        throw new System.NotImplementedException();
    }

    public void CreateControlResult(BoterkroonSkills skill) {
        int currentXPLevel = 0;
        foreach (var trainingResult in GetTrainingResultsFor(skill)) {
            currentXPLevel += trainingResult.GainedXP;
            trainingResult.IsNew = false;
        }

        BoterkroonControlResult result = new BoterkroonControlResult(currentXPLevel);
        GetControlResultsFor(skill).Add(result);
    }

    public bool HasNewTrainingFor(BoterkroonSkills skill) {
        var results = GetTrainingResultsFor(skill);
        if (results.Count == 0) {
            return false;
        }
        return results[results.Count - 1].IsNew;
    }
}

[System.Serializable]
public class BoterkroonTrainingResult {
    public int GainedXP;
    public bool IsNew = true;

    public BoterkroonTrainingResult(int gainedXP) {
        this.GainedXP = gainedXP;
        this.IsNew = true;
    }
}

[System.Serializable]
public class BoterkroonControlResult {
    public int TotalXP;
    public bool IsNew = true;

    public BoterkroonControlResult(int totalXP) {
        this.TotalXP = totalXP;
        this.IsNew = true;
    }
}

[System.Serializable]
public class BoterkroonSkillResult {
    public int Level;
    public float Score;

    public BoterkroonSkillResult(int level, float score) {
        this.Level = level;
        this.Score = score;
    }
}