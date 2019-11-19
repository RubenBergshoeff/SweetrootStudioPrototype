using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class SkillCategory : BaseData {
    public Color color = Color.grey;
    public List<SkillCategoryTest> Tests = new List<SkillCategoryTest>();
    public List<UnlockResult> PotentialUnlocks = new List<UnlockResult>();
}

[System.Serializable]
public class ActiveSkillCategory : ActiveBaseData {
    public SkillCategory Category {
        get {
            return Data as SkillCategory;
        }
    }
    public SkillCategoryTestResult LastResult {
        get {
            if (TestResults.Count == 0) {
                return null;
            }
            else {
                return TestResults[TestResults.Count - 1];
            }
        }
    }
    public List<ActiveSkill> ActiveSkills = new List<ActiveSkill>();
    public List<SkillCategoryTestResult> TestResults = new List<SkillCategoryTestResult>();
    public SkillCategoryTest SelectedTest = null;

    public ActiveSkillCategory(SkillCategory data) : base(data) {

    }

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
public class SkillCategoryTestResult {
    public SkillCategoryTest Test = null;
    public int Score = 0;
}

[System.Serializable]
public class UnlockResult {
    public int RequiredScore = 0;
    public BaseData ResultToUnlock = null;
    public bool ShowPopup = true;
}