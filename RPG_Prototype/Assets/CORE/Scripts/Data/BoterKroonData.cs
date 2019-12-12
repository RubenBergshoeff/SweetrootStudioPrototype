using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

    public int TutorialIndex = 0;
    public bool IsBakingLocked = true;
    public bool IsSwordLocked = true;
    public bool IsRoyalLocked = true;
    public bool IsNew = true;

    public Action<int> OnTurnsChanged;

    public int TurnsLeft {
        get {
            return turnsLeft;
        }
        set {
            turnsLeft = value;
            OnTurnsChanged?.Invoke(turnsLeft);
        }
    }
    private int turnsLeft = 20;

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
    public bool Succeeded;
    public bool IsNew;

    public BoterkroonSkillResult(int level, float score, bool succeeded) {
        this.Level = level;
        this.Score = score;
        this.Succeeded = succeeded;
        this.IsNew = true;
    }
}

public class ScoreRequirement {
    public Func<BoterkroonSkills, float> Skill;
    public float Total;

    public ScoreRequirement(Func<BoterkroonSkills, float> scoreFunc, float total) {
        Skill = scoreFunc;
        Total = total;
    }
}

public static class BoterkroonScoreRequirements {

    public static ScoreRequirement GetMinScoreFor(int level) {
        if (level == 1) {
            return MinLevelOneScoreRequirement;
        }
        else if (level == 2) {
            return MinLevelTwoScoreRequirement;
        }
        else if (level == 3) {
            return MinLevelThreeScoreRequirement;
        }
        throw new NotImplementedException();
    }

    public static ScoreRequirement GetMaxScoreFor(int level) {
        if (level == 1) {
            return MaxLevelOneScoreRequirement;
        }
        else if (level == 2) {
            return MaxLevelTwoScoreRequirement;
        }
        else if (level == 3) {
            return MaxLevelThreeScoreRequirement;
        }
        throw new NotImplementedException();
    }

    private static ScoreRequirement MinLevelOneScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        return 0;
    }, 0);

    private static ScoreRequirement MinLevelTwoScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return 0.4f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return 0f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Royal:
                return 0f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, 0.4f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP);

    private static ScoreRequirement MinLevelThreeScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return 0.5f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return 0.3f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Royal:
                return 0.0f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (0.5f + 0.3f) * SaveController.Instance.GameData.BoterKroon.MaxSkillXP);

    private static ScoreRequirement MaxLevelOneScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return 0.5f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return 0f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Royal:
                return 0f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, 0.5f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP);

    private static ScoreRequirement MaxLevelTwoScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return 0.6f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return 0.3f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Royal:
                return 0f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (0.7f + 0.4f) * SaveController.Instance.GameData.BoterKroon.MaxSkillXP);

    private static ScoreRequirement MaxLevelThreeScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return 0.8f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return 0.7f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
            case BoterkroonSkills.Royal:
                return 0.5f * SaveController.Instance.GameData.BoterKroon.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (0.8f + 0.7f + 0.5f) * SaveController.Instance.GameData.BoterKroon.MaxSkillXP);
}