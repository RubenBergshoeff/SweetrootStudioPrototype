using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class SkillCategory : ResultDataBase {
    public Color color = Color.grey;
}

[System.Serializable]
public class ActiveSkillCategory : ActiveResultData {
    public SkillCategory Category {
        get {
            return Data as SkillCategory;
        }
    }
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