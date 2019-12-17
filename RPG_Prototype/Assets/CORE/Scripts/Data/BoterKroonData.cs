using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BoterkroonSkills {
    Baking,
    Sword,
    Research
}

[System.Serializable]
public class ActiveBoterkroonData {
    public List<BoterkroonSkillResult> SkillResults = new List<BoterkroonSkillResult>();

    public List<BoterkroonControlResult> ControlResultsBaking = new List<BoterkroonControlResult>();
    public List<BoterkroonControlResult> ControlResultsSword = new List<BoterkroonControlResult>();
    public List<BoterkroonControlResult> ControlResultsRoyal = new List<BoterkroonControlResult>();

    public List<BoterkroonTrainingResult> TrainingResultsBaking = new List<BoterkroonTrainingResult>();
    public List<BoterkroonTrainingResult> TrainingResultsSword = new List<BoterkroonTrainingResult>();
    public List<BoterkroonTrainingResult> TrainingResultsRoyal = new List<BoterkroonTrainingResult>();

    public int TutorialIndex = 0;
    public bool IsBakingLocked = false;
    public bool IsSwordLocked = true;
    public bool IsResearchLocked = true;
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
    private int turnsLeft = 0;

    public ActiveBoterkroonData(int amountOfTurns) {
        turnsLeft = amountOfTurns;
    }

    public bool IsSkillActive(BoterkroonSkills skill) {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return !IsBakingLocked;
            case BoterkroonSkills.Sword:
                return !IsSwordLocked;
            case BoterkroonSkills.Research:
                return !IsResearchLocked;
        }
        throw new System.NotImplementedException();
    }

    public List<BoterkroonTrainingResult> GetTrainingResultsFor(BoterkroonSkills skill) {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return TrainingResultsBaking;
            case BoterkroonSkills.Sword:
                return TrainingResultsSword;
            case BoterkroonSkills.Research:
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
            case BoterkroonSkills.Research:
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

    public BoterkroonSkillResult CreateSkillTestResult(int level) {
        float currentScore = 0;
        float bakingScore = 0;
        float swordScore = 0;
        float researchScore = 0;
        bool succeededTest = true;
        BoterkroonSkillResult currentResult;

        if (IsSkillActive(BoterkroonSkills.Baking)) {
            succeededTest = GetSkillScore(level, BoterkroonSkills.Baking, out bakingScore);
            currentScore += bakingScore;
        }
        if (succeededTest && IsSkillActive(BoterkroonSkills.Sword)) {
            succeededTest = GetSkillScore(level, BoterkroonSkills.Sword, out swordScore);
            currentScore += swordScore;
        }
        if (succeededTest && IsSkillActive(BoterkroonSkills.Research)) {
            succeededTest = GetSkillScore(level, BoterkroonSkills.Research, out researchScore);
            currentScore += researchScore;
        }
        currentResult = new BoterkroonSkillResult(level, currentScore, succeededTest);
        currentResult.UnlockSword = GetUnlockSword(succeededTest, bakingScore, swordScore, researchScore);
        currentResult.UnlockResearch = GetUnlockResearch(succeededTest, bakingScore, swordScore, researchScore);
        if (currentResult.UnlockSword) {
            IsSwordLocked = false;
        }
        if (currentResult.UnlockResearch) {
            IsResearchLocked = false;
        }
        SkillResults.Add(currentResult);
        return currentResult;
    }

    private bool GetSkillScore(int currentLevel, BoterkroonSkills skill, out float skillScore) {
        float maxScore = BoterkroonScoreRequirements.GetMaxScoreFor(currentLevel).Skill(skill);
        float minScore = BoterkroonScoreRequirements.GetMinScoreFor(currentLevel).Skill(skill);
        skillScore = Mathf.Min(maxScore, GetLastScoreFor(skill));
        return minScore <= skillScore;
    }

    private int GetLastScoreFor(BoterkroonSkills skill) {
        if (HasNewTrainingFor(skill)) {
            CreateControlResult(skill);
        }
        var controlResults = GetControlResultsFor(skill);
        if (controlResults.Count == 0) {
            return 0;
        }
        //foreach (var result in controlResults) {
        //    result.IsNew = false;
        //}
        return controlResults[controlResults.Count - 1].TotalXP;
    }

    private bool GetUnlockSword(bool succeededTest, float bakingScore, float swordScore, float researchScore) {
        if (succeededTest == false) {
            return false;
        }
        if (IsSkillActive(BoterkroonSkills.Sword)) {
            return false;
        }
        bool bakingRequirementMet = bakingScore >= BoterkroonValues.Values.UnlockSwordMinBakeControl * BoterkroonValues.Values.MaxSkillXP;
        bool swordRequirementMet = swordScore >= BoterkroonValues.Values.UnlockSwordSwordControl * BoterkroonValues.Values.MaxSkillXP;
        bool researchRequirementMet = researchScore >= BoterkroonValues.Values.UnlockSwordResearchControl * BoterkroonValues.Values.MaxSkillXP;
        return bakingRequirementMet && swordRequirementMet && researchRequirementMet;
    }

    private bool GetUnlockResearch(bool succeededTest, float bakingScore, float swordScore, float researchScore) {
        if (succeededTest == false) {
            return false;
        }
        if (IsSkillActive(BoterkroonSkills.Research)) {
            return false;
        }
        bool bakingRequirementMet = bakingScore >= BoterkroonValues.Values.UnlockResearchMinBakeControl * BoterkroonValues.Values.MaxSkillXP;
        bool swordRequirementMet = swordScore >= BoterkroonValues.Values.UnlockResearchSwordControl * BoterkroonValues.Values.MaxSkillXP;
        bool researchRequirementMet = researchScore >= BoterkroonValues.Values.UnlockResearchResearchControl * BoterkroonValues.Values.MaxSkillXP;
        return bakingRequirementMet && swordRequirementMet && researchRequirementMet;
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
    public bool UnlockSword;
    public bool UnlockResearch;

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
                return BoterkroonValues.Values.Lvl2MinBakeControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return BoterkroonValues.Values.Lvl2MinSwordControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Research:
                return BoterkroonValues.Values.Lvl2MinResearchControl * BoterkroonValues.Values.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (BoterkroonValues.Values.Lvl2MinBakeControl + BoterkroonValues.Values.Lvl2MinSwordControl + BoterkroonValues.Values.Lvl2MinResearchControl) * BoterkroonValues.Values.MaxSkillXP);

    private static ScoreRequirement MinLevelThreeScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return BoterkroonValues.Values.Lvl3MinBakeControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return BoterkroonValues.Values.Lvl3MinSwordControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Research:
                return BoterkroonValues.Values.Lvl3MinResearchControl * BoterkroonValues.Values.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (BoterkroonValues.Values.Lvl3MinBakeControl + BoterkroonValues.Values.Lvl3MinSwordControl + BoterkroonValues.Values.Lvl3MinResearchControl) * BoterkroonValues.Values.MaxSkillXP);

    private static ScoreRequirement MaxLevelOneScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return BoterkroonValues.Values.Lvl1MaxBakeControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return BoterkroonValues.Values.Lvl1MaxSwordControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Research:
                return BoterkroonValues.Values.Lvl1MaxResearchControl * BoterkroonValues.Values.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (BoterkroonValues.Values.Lvl1MaxBakeControl + BoterkroonValues.Values.Lvl1MaxSwordControl + BoterkroonValues.Values.Lvl1MaxResearchControl) * BoterkroonValues.Values.MaxSkillXP);

    private static ScoreRequirement MaxLevelTwoScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return BoterkroonValues.Values.Lvl2MaxBakeControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return BoterkroonValues.Values.Lvl2MaxSwordControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Research:
                return BoterkroonValues.Values.Lvl2MaxResearchControl * BoterkroonValues.Values.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (BoterkroonValues.Values.Lvl2MaxBakeControl + BoterkroonValues.Values.Lvl2MaxSwordControl + BoterkroonValues.Values.Lvl2MaxResearchControl) * BoterkroonValues.Values.MaxSkillXP);

    private static ScoreRequirement MaxLevelThreeScoreRequirement = new ScoreRequirement((BoterkroonSkills skill) => {
        switch (skill) {
            case BoterkroonSkills.Baking:
                return BoterkroonValues.Values.Lvl3MaxBakeControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Sword:
                return BoterkroonValues.Values.Lvl3MaxSwordControl * BoterkroonValues.Values.MaxSkillXP;
            case BoterkroonSkills.Research:
                return BoterkroonValues.Values.Lvl3MaxResearchControl * BoterkroonValues.Values.MaxSkillXP;
        }
        throw new NotImplementedException();
    }, (BoterkroonValues.Values.Lvl3MaxBakeControl + BoterkroonValues.Values.Lvl3MaxSwordControl + BoterkroonValues.Values.Lvl3MaxResearchControl) * BoterkroonValues.Values.MaxSkillXP);
}